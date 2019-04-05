﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iril.IR
{
    // https://itanium-cxx-abi.github.io/cxx-abi/abi.html#mangle.unscoped-name
    // https://demangler.com

    public class MangledName
    {
        public readonly Symbol Symbol;

        readonly Encoding encoding;

        readonly List<object> subs = new List<object> ();

        public MangledName (Symbol symbol)
        {
            Symbol = symbol;
            var t = Symbol.Text;
            if (IsMangled (symbol)) {
                encoding = ParseEncoding (3);
                Console.WriteLine ($"DE: {encoding}");
            }
        }

        static bool IsMangled (Symbol symbol)
        {
            Console.WriteLine (symbol.Text);
            return symbol.Text.StartsWith ("@_Z", StringComparison.Ordinal);
        }

        void SyntaxError (string message, int p)
        {
            throw new Exception (message + ": " + Symbol.Text.Substring (p));
        }

        Encoding ParseEncoding (int p)
        {
            var t = Symbol.Text;
            var ch = t[p];
            if (ch == 'T') {
                switch (t[p + 1]) {
                    case 'V':
                        p += 2;
                        return new VirtualTable (ParseType (ref p));
                    case 'T':
                        p += 2;
                        return new VttStructure (ParseType (ref p));
                    case 'I':
                        p += 2;
                        return new TypeInfoStructure (ParseType (ref p));
                    case 'S':
                        p += 2;
                        return new TypeInfoName (ParseType (ref p));
                }
            }
            return new NamedEncoding { Name = ParseName (ref p) };
        }

        TypeName ParseType (ref int p)
        {
            var t = Symbol.Text;
            var ch = t[p];

            switch (ch) {
                case 'v':
                    p++;
                    return TypeName.Void;
                case 'w':
                    p++;
                    return TypeName.Wchar;
                case 'b':
                    p++;
                    return TypeName.Bool;
                case 'c':
                    p++;
                    return TypeName.Char;
                case 'a':
                    p++;
                    return TypeName.SChar;
                case 'h':
                    p++;
                    return TypeName.UChar;
                case 's':
                    p++;
                    return TypeName.Short;
                case 'i':
                    p++;
                    return TypeName.Int;
                case 'P': {
                        p++;
                        var et = ParseType (ref p);
                        return new PointerType { ElementType = et };
                    }
                case 'S' when p + 1 < t.Length && t[p + 1] != 't': {
                        p++;
                        return (TypeName)ParseSubstitution (ref p);
                    }
                default:                    
                    return new NamedType { Name = ParseName (ref p) };
            }

            throw new Exception ();
        }

        Name ParseName (ref int p)
        {
            var t = Symbol.Text;
            var ch = t[p];

            switch (ch) {
                case 'N':
                    p++;
                    return ParseNestedName (ref p);
                case 'S' when p + 1 < t.Length && t[p + 1] == 't':
                    p += 2;
                    return new NestedName { Names = { Name.Std, ParseUnqualifiedName (ref p) } };
                default:
                    return ParseUnqualifiedName (ref p);
            }
        }

        Name ParseNestedName (ref int p)
        {
            ParseCVQualifiers (ref p);
            var t = Symbol.Text;
            var nn = new NestedName ();
            while (t[p] != 'E') {
                if (t[p] == 'I') {
                    p++;
                    nn.Names.Add (ParseTemplateArgs (ref p));
                }
                else if (t[p] == 'S' && p + 1 < t.Length && t[p + 1] != 't') {
                    p++;
                    nn.Names.Add ((Name)ParseSubstitution (ref p));
                }
                else if (t[p] == 'L') {
                    // IDK
                    p++;
                }
                else {
                    nn.Names.Add (ParseUnqualifiedName (ref p));
                    subs.Add (nn.Names.Last ());
                }
            }
            p++;
            return nn;
        }

        void ParseCVQualifiers (ref int p)
        {
            var t = Symbol.Text;
            if (t[p] == 'r')
                p++;
            if (t[p] == 'V')
                p++;
            if (t[p] == 'K')
                p++;
        }

        Name ParseUnqualifiedName (ref int p)
        {
            var t = Symbol.Text;
            var ch = t[p];

            if (ch == 'C' || ch == 'D') {
                var n = new CtorName { Kind = t.Substring (p, 2) };
                p += 2;
                return n;
            }

            Name prefix = null;
            if (ch == 'S' && p + 1 < t.Length && t[p + 1] == 't') {
                p += 2;
                prefix = Name.Std;
            }

            var e = p;
            while (char.IsDigit (t[e]) && e + 1 < t.Length)
                e++;
            if (e == p) {
                SyntaxError ("Bad Source Name", p);
                return null;
            }
            var length = int.Parse (t.Substring (p, e - p));
            var text = t.Substring (e, length);
            p = e + length;

            var un = new UnqualifiedName { Text = text };
            if (prefix == null)
                return un;

            return new NestedName { Names = { prefix, un } };
        }

        TemplateArgsName ParseTemplateArgs (ref int p)
        {
            var t = Symbol.Text;
            var r = new TemplateArgsName ();
            while (t[p] != 'E') {
                var type = ParseTemplateArg (ref p);
                r.Args.Add (type);
                subs.Add (type);
            }
            p++;
            return r;
        }

        TypeName ParseTemplateArg (ref int p)
        {
            var t = Symbol.Text;

            if (t[p] == 'J') {
                p++;
                var tr = new TemplateArgsType ();
                while (t[p] != 'E') {
                    var a = ParseTemplateArg (ref p);
                    tr.Args.Add (a);
                }
                p++;
                return tr;
            }

            var r = TryParseExpression (ref p);
            if (r != null)
                return r;

            return ParseType (ref p);
        }

        TypeName TryParseExpression (ref int p)
        {
            var t = Symbol.Text;
            switch (t[p]) {
                case 'L':
                    p++;
                    return ParsePrimaryExpression (ref p);
                default:
                    return null;
            }
        }

        TypeName ParsePrimaryExpression (ref int p)
        {
            var t = Symbol.Text;

            var typ = ParseType (ref p);

            if (char.IsDigit (t[p])) {
                var e = p;
                while (e < t.Length && char.IsDigit (t[e])) {
                    e++;
                }
                var value = int.Parse (t.Substring (p, e - p));
                typ = new ValueExprType { Type = typ, Value = value };
                p = e;
            }

            while (p < t.Length && t[p] != 'E') p++;
            p++;

            return typ;
        }

        object ParseSubstitution (ref int p)
        {
            var t = Symbol.Text;
            var e = p;
            while (t[e] != '_') {
                e++;
            }
            var index = -1;
            if (e > p) {
                var v = t.Substring (p, e - p);
                index = int.Parse (v);
            }
            index++;
            p = e + 1;
            if (index < subs.Count) {
                return subs[index];
            }
            return new UnqualifiedName { Text = "S" + index };
        }

        abstract class Name
        {
            public static readonly Name Std = new UnqualifiedName { Text = "std" };
        }

        class CtorName : Name
        {
            public string Kind;
            public override string ToString () => Kind;
        }

        class UnqualifiedName : Name
        {
            public string Text;
            public override string ToString () => Text;
        }

        class NestedName : Name
        {
            public List<Name> Names = new List<Name> ();
            public override string ToString ()
            {
                var s = new StringBuilder ();
                foreach (var n in Names) {
                    if (s.Length == 0) {
                    }
                    else {
                        if (n is TemplateArgsName) {
                        }
                        else {
                            s.Append ("::");
                        }
                    }
                    s.Append (n);
                }
                return s.ToString ();
            }
        }

        class TemplateArgsName : Name
        {
            public List<TypeName> Args = new List<TypeName> ();
            public override string ToString () => "<" + string.Join (", ", Args) + ">";
        }

        abstract class TypeName
        {
            public static readonly TypeName Void = new BuiltinType { Name = "void" };
            public static readonly TypeName Wchar = new BuiltinType { Name = "wchar_t" };
            public static readonly TypeName Bool = new BuiltinType { Name = "bool" };
            public static readonly TypeName Char = new BuiltinType { Name = "char" };
            public static readonly TypeName SChar = new BuiltinType { Name = "signed char" };
            public static readonly TypeName UChar = new BuiltinType { Name = "unsigned char" };
            public static readonly TypeName Short = new BuiltinType { Name = "short" };
            public static readonly TypeName Int = new BuiltinType { Name = "int" };
        }

        class ValueExprType : TypeName
        {
            public TypeName Type;
            public int Value;
            public override string ToString () => Value.ToString ();
        }

        class BuiltinType : TypeName
        {
            public string Name;
            public override string ToString () => Name;
        }

        class NamedType : TypeName
        {
            public Name Name;
            public override string ToString () => Name.ToString ();
        }

        class PointerType : TypeName
        {
            public TypeName ElementType;
            public override string ToString () => $"{ElementType}*";
        }

        class TemplateArgsType : TypeName
        {
            public List<TypeName> Args = new List<TypeName> ();
            public override string ToString () => "!<" + string.Join (", ", Args) + ">!";
        }

        abstract class Encoding
        {
        }

        class NamedEncoding : Encoding
        {
            public Name Name;
            public override string ToString () => Name.ToString ();
        }

        class SpecialName : Encoding
        {
            public readonly TypeName Type;

            public SpecialName (TypeName type)
            {
                Type = type ?? throw new ArgumentNullException (nameof (type));
            }
        }
        class VirtualTable : SpecialName
        {
            public VirtualTable (TypeName type) : base (type)
            {
            }
            public override string ToString () => $"@vtable for {Type}";
        }
        class VttStructure : SpecialName
        {
            public VttStructure (TypeName type) : base (type)
            {
            }
        }
        class TypeInfoStructure : SpecialName
        {
            public TypeInfoStructure (TypeName type) : base (type)
            {
            }
            public override string ToString () => $"@typeinfo for {Type}";

        }
        class TypeInfoName : SpecialName
        {
            public TypeInfoName (TypeName type) : base (type)
            {
            }
        }
    }
}
