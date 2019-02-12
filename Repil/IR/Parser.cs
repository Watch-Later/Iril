// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Repil/IR/IR.jay"
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

using Repil.Types;

#pragma warning disable 219,414

namespace Repil.IR
{
	public partial class Parser
	{
#line default

  /** error output stream.
      It should be changeable.
    */
  public System.IO.TextWriter ErrorOutput = new StringWriter ();

  /** simplified error message.
      @see <a href="#yyerror(java.lang.String, java.lang.String[])">yyerror</a>
    */
  public void yyerror (string message) {
    yyerror(message, null);
  }

  /* An EOF token */
  public int eof_token;
  
  public int yacc_verbose_flag;

  /** (syntax) error message.
      Can be overwritten to control message format.
      @param message text to be displayed.
      @param expected vector of acceptable tokens, if available.
    */
  public void yyerror (string message, string[] expected) {
    if ((yacc_verbose_flag > 0) && (expected != null) && (expected.Length  > 0)) {
      ErrorOutput.Write (message+", expecting");
      for (int n = 0; n < expected.Length; ++ n)
        ErrorOutput.Write (" "+expected[n]);
        ErrorOutput.WriteLine ();
    } else
      ErrorOutput.WriteLine (message);
  }

  /** debugging support, requires the package jay.yydebug.
      Set to null to suppress debugging messages.
    */
//t  internal yydebug.yyDebug debug;

  protected const int yyFinal = 8;
//t // Put this array into a separate class so it is only initialized if debugging is actually used
//t // Use MarshalByRefObject to disable inlining
//t class YYRules : MarshalByRefObject {
//t  public static readonly string [] yyRule = {
//t    "$accept : module",
//t    "module : module_parts",
//t    "module_parts : module_part",
//t    "module_parts : module_parts module_part",
//t    "module_part : SOURCE_FILENAME '=' STRING",
//t    "module_part : TARGET DATALAYOUT '=' STRING",
//t    "module_part : TARGET TRIPLE '=' STRING",
//t    "module_part : LOCAL_SYMBOL '=' TYPE literal_structure",
//t    "module_part : function_definition",
//t    "module_part : function_declaration",
//t    "module_part : ATTRIBUTES ATTRIBUTE_GROUP_REF '=' '{' attributes '}'",
//t    "module_part : META_SYMBOL '=' '!' '{' metadata '}'",
//t    "attributes : attribute",
//t    "attributes : attributes attribute",
//t    "attribute : NORECURSE",
//t    "attribute : NOUNWIND",
//t    "attribute : SSP",
//t    "attribute : UWTABLE",
//t    "attribute : ARGMEMONLY",
//t    "attribute : STRING '=' STRING",
//t    "attribute : STRING",
//t    "metadata : metadatum",
//t    "metadata : metadata ',' metadatum",
//t    "metadatum : typed_value",
//t    "metadatum : META_SYMBOL",
//t    "literal_structure : '{' type_list '}'",
//t    "type_list : type",
//t    "type_list : type_list ',' type",
//t    "type : literal_structure",
//t    "type : VOID",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I1",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
//t    "type : type '(' type_list ')'",
//t    "type : type '*'",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_declaration : DECLARE type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list ',' parameter",
//t    "parameter : type",
//t    "parameter : type parameter_attributes",
//t    "parameter_attributes : parameter_attribute",
//t    "parameter_attributes : parameter_attributes parameter_attribute",
//t    "parameter_attribute : NONNULL",
//t    "parameter_attribute : NOCAPTURE",
//t    "parameter_attribute : WRITEONLY",
//t    "function_addr : UNNAMED_ADDR",
//t    "function_addr : LOCAL_UNNAMED_ADDR",
//t    "attribute_group_refs : attribute_group_ref",
//t    "attribute_group_refs : attribute_group_refs attribute_group_ref",
//t    "attribute_group_ref : ATTRIBUTE_GROUP_REF",
//t    "icmp_condition : EQ",
//t    "icmp_condition : NE",
//t    "icmp_condition : UGT",
//t    "icmp_condition : UGE",
//t    "icmp_condition : ULT",
//t    "icmp_condition : ULE",
//t    "icmp_condition : SGT",
//t    "icmp_condition : SGE",
//t    "icmp_condition : SLT",
//t    "icmp_condition : SLE",
//t    "value : constant",
//t    "value : LOCAL_SYMBOL",
//t    "value : GLOBAL_SYMBOL",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : '<' typed_constants '>'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_constant : type constant",
//t    "typed_constants : typed_constant",
//t    "typed_constants : typed_constants ',' typed_constant",
//t    "tbaa : META_SYMBOL META_SYMBOL",
//t    "element_index : typed_value",
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "blocks : block",
//t    "blocks : blocks block",
//t    "block : assignments terminator_assignment",
//t    "assignments : assignment",
//t    "assignments : assignments assignment",
//t    "terminator_assignment : terminator_instruction",
//t    "assignment : instruction",
//t    "assignment : LOCAL_SYMBOL '=' instruction",
//t    "function_pointer : value",
//t    "function_args : function_arg",
//t    "function_args : function_args ',' function_arg",
//t    "function_arg : type value",
//t    "function_arg : type parameter_attributes value",
//t    "phi_vals : phi_val",
//t    "phi_vals : phi_vals ',' phi_val",
//t    "phi_val : '[' value ',' value ']'",
//t    "switch_cases : switch_case",
//t    "switch_cases : switch_cases switch_case",
//t    "switch_case : typed_constant ',' label_value",
//t    "wrappings : wrapping",
//t    "wrappings : wrappings wrapping",
//t    "wrapping : NUW",
//t    "wrapping : NSW",
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL type function_pointer '(' function_args ')'",
//t    "instruction : CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FMUL type value ',' value",
//t    "instruction : GETELEMENTPTR type ',' typed_value ',' element_indices",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : LOAD type ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t    "instruction : LSHR EXACT type value ',' value",
//t    "instruction : MUL wrappings type value ',' value",
//t    "instruction : PHI type phi_vals",
//t    "instruction : SEXT typed_value TO type",
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t    "instruction : SUB type value ',' value",
//t    "instruction : SUB wrappings type value ',' value",
//t    "instruction : TRUNC typed_value TO type",
//t    "instruction : ZEXT typed_value TO type",
//t  };
//t public static string getRule (int index) {
//t    return yyRule [index];
//t }
//t}
  protected static readonly string [] yyNames = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"'!'",null,null,null,null,null,
    null,"'('","')'","'*'",null,"','",null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,"'<'","'='","'>'",null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,"'['",
    null,"']'",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,"'{'",null,"'}'",null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,
    "INTEGER","FLOAT_LITERAL","STRING","TRUE","FALSE","UNDEF","VOID",
    "NULL","LABEL","X","SOURCE_FILENAME","TARGET","DATALAYOUT","TRIPLE",
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","TYPE","HALF","FLOAT",
    "DOUBLE","I1","I8","I16","I32","I64","DEFINE","DECLARE",
    "UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NONNULL","NOCAPTURE","WRITEONLY",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","SSP",
    "UWTABLE","ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR","INVOKE",
    "RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG",
    "ADD","NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV",
    "FDIV","UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR",
    "XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT",
    "FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT",
    "INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE",
    "ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ","OGT","OGE","OLT",
    "OLE","ONE","ORD","UEQ","UNE","UNO","PHI","SELECT","CALL","TAIL",
    "VA_ARG","LANDINGPAD","CATCHPAD","CLEANUPPAD",
  };

  /** index-checked interface to yyNames[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
  public static string yyname (int token) {
    if ((token < 0) || (token > yyNames.Length)) return "[illegal]";
    string name;
    if ((name = yyNames[token]) != null) return name;
    return "[unknown]";
  }

  //int yyExpectingState;
  /** computes list of expected tokens on error by tracing the tables.
      @param state for which to compute the list.
      @return list of token names.
    */
  protected int [] yyExpectingTokens (int state){
    int token, n, len = 0;
    bool[] ok = new bool[yyNames.Length];
    if ((n = yySindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    if ((n = yyRindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    int [] result = new int [len];
    for (n = token = 0; n < len;  ++ token)
      if (ok[token]) result[n++] = token;
    return result;
  }
  protected string[] yyExpecting (int state) {
    int [] tokens = yyExpectingTokens (state);
    string [] result = new string[tokens.Length];
    for (int n = 0; n < tokens.Length;  n++)
      result[n] = yyNames[tokens [n]];
    return result;
  }

  /** the generated parser, with debugging messages.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @param yydebug debug message writer implementing yyDebug, or null.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex, Object yyd)
				 {
//t    this.debug = (yydebug.yyDebug)yyd;
    return yyparse(yyLex);
  }

  /** initial size and increment of the state/value stack [default 256].
      This is not final so that it can be overwritten outside of invocations
      of yyparse().
    */
  protected int yyMax;

  /** executed at the beginning of a reduce action.
      Used as $$ = yyDefault($1), prior to the user-specified action, if any.
      Can be overwritten to provide deep copy, etc.
      @param first value for $1, or null.
      @return first.
    */
  protected Object yyDefault (Object first) {
    return first;
  }

	static int[] global_yyStates;
	static object[] global_yyVals;
	protected bool use_global_stacks;
	object[] yyVals;					// value stack
	object yyVal;						// value stack ptr
	int yyToken;						// current input
	int yyTop;

  /** the generated parser.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex)
  {
    if (yyMax <= 0) yyMax = 256;		// initial size
    int yyState = 0;                   // state stack ptr
    int [] yyStates;               	// state stack 
    yyVal = null;
    yyToken = -1;
    int yyErrorFlag = 0;				// #tks to shift
	if (use_global_stacks && global_yyStates != null) {
		yyVals = global_yyVals;
		yyStates = global_yyStates;
   } else {
		yyVals = new object [yyMax];
		yyStates = new int [yyMax];
		if (use_global_stacks) {
			global_yyVals = yyVals;
			global_yyStates = yyStates;
		}
	}

    /*yyLoop:*/ for (yyTop = 0;; ++ yyTop) {
      if (yyTop >= yyStates.Length) {			// dynamically increase
        global::System.Array.Resize (ref yyStates, yyStates.Length+yyMax);
        global::System.Array.Resize (ref yyVals, yyVals.Length+yyMax);
      }
      yyStates[yyTop] = yyState;
      yyVals[yyTop] = yyVal;
//t      if (debug != null) debug.push(yyState, yyVal);

      /*yyDiscarded:*/ while (true) {	// discarding a token does not change stack
        int yyN;
        if ((yyN = yyDefRed[yyState]) == 0) {	// else [default] reduce (yyN)
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t              debug.lex(yyState, yyToken, yyname(yyToken), yyLex.value());
          }
          if ((yyN = yySindex[yyState]) != 0 && ((yyN += yyToken) >= 0)
              && (yyN < yyTable.Length) && (yyCheck[yyN] == yyToken)) {
//t            if (debug != null)
//t              debug.shift(yyState, yyTable[yyN], yyErrorFlag-1);
            yyState = yyTable[yyN];		// shift to yyN
            yyVal = yyLex.value();
            yyToken = -1;
            if (yyErrorFlag > 0) -- yyErrorFlag;
            goto continue_yyLoop;
          }
          if ((yyN = yyRindex[yyState]) != 0 && (yyN += yyToken) >= 0
              && yyN < yyTable.Length && yyCheck[yyN] == yyToken)
            yyN = yyTable[yyN];			// reduce (yyN)
          else
            switch (yyErrorFlag) {
  
            case 0:
              //yyExpectingState = yyState;
              Console.WriteLine(String.Format ("syntax error, got token `{0}' expecting: {1}",
                                yyname (yyToken),
                                String.Join(", ", yyExpecting(yyState))));
//t              if (debug != null) debug.error("syntax error");
              if (yyToken == 0 /*eof*/ || yyToken == eof_token) throw new yyParser.yyUnexpectedEof ();
              goto case 1;
            case 1: case 2:
              yyErrorFlag = 3;
              do {
                if ((yyN = yySindex[yyStates[yyTop]]) != 0
                    && (yyN += Token.yyErrorCode) >= 0 && yyN < yyTable.Length
                    && yyCheck[yyN] == Token.yyErrorCode) {
//t                  if (debug != null)
//t                    debug.shift(yyStates[yyTop], yyTable[yyN], 3);
                  yyState = yyTable[yyN];
                  yyVal = yyLex.value();
                  goto continue_yyLoop;
                }
//t                if (debug != null) debug.pop(yyStates[yyTop]);
              } while (-- yyTop >= 0);
//t              if (debug != null) debug.reject();
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
              }
//t              if (debug != null)
//t                debug.discard(yyState, yyToken, yyname(yyToken),
//t  							yyLex.value());
              yyToken = -1;
              goto continue_yyDiscarded;		// leave stack alone
            }
        }
        int yyV = yyTop + 1-yyLen[yyN];
//t        if (debug != null)
//t          debug.reduce(yyState, yyStates[yyV-1], yyN, YYRules.getRule (yyN), yyLen[yyN]);
        yyVal = yyV > yyTop ? null : yyVals[yyV]; // yyVal = yyDefault(yyV > yyTop ? null : yyVals[yyV]);
        switch (yyN) {
case 4:
#line 56 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 60 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 64 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 68 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
  case_8();
  break;
case 9:
  case_9();
  break;
case 25:
  case_25();
  break;
case 26:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 27:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 29:
#line 129 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 30:
#line 130 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 31:
#line 131 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 32:
#line 132 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 33:
#line 133 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 34:
#line 134 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 35:
#line 135 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 36:
#line 136 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 37:
#line 137 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 38:
#line 141 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 39:
#line 145 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 40:
#line 149 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 153 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 42:
#line 157 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 43:
#line 164 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 44:
#line 171 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 45:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 46:
#line 182 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 47:
#line 189 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 48:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 50:
#line 201 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 51:
#line 205 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 52:
#line 206 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 53:
#line 207 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 59:
#line 225 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 60:
#line 226 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 61:
#line 227 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 62:
#line 228 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 63:
#line 229 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 64:
#line 230 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 65:
#line 231 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 66:
#line 232 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 67:
#line 233 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 68:
#line 234 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 70:
#line 239 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 71:
#line 240 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 72:
#line 244 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 73:
#line 245 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 74:
#line 246 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 75:
#line 247 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 76:
#line 248 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 77:
#line 252 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 78:
#line 259 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 79:
#line 266 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 80:
#line 273 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 280 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 82:
#line 284 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 299 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 303 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 89:
  case_89();
  break;
case 90:
#line 330 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 91:
#line 334 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 92:
#line 341 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 93:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 97:
#line 367 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 374 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 99:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 100:
#line 385 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 389 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 395 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 103:
#line 402 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 406 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 413 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 430 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 434 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 438 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 442 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 114:
#line 449 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 115:
#line 453 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 116:
#line 457 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 117:
#line 461 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 465 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 119:
#line 469 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 120:
#line 473 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 121:
#line 477 "Repil/IR/IR.jay"
  {
        yyVal = new FloatAddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 122:
#line 481 "Repil/IR/IR.jay"
  {
        yyVal = new FloatMultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 485 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 124:
#line 489 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 125:
#line 493 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 126:
#line 497 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 127:
#line 501 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 128:
#line 505 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 129:
#line 509 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 130:
#line 513 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 131:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 132:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 133:
#line 525 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 134:
#line 529 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 135:
#line 533 "Repil/IR/IR.jay"
  {
        yyVal = new ZextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
#line default
        }
        yyTop -= yyLen[yyN];
        yyState = yyStates[yyTop];
        int yyM = yyLhs[yyN];
        if (yyState == 0 && yyM == 0) {
//t          if (debug != null) debug.shift(0, yyFinal);
          yyState = yyFinal;
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t               debug.lex(yyState, yyToken,yyname(yyToken), yyLex.value());
          }
          if (yyToken == 0) {
//t            if (debug != null) debug.accept(yyVal);
            return yyVal;
          }
          goto continue_yyLoop;
        }
        if (((yyN = yyGindex[yyM]) != 0) && ((yyN += yyState) >= 0)
            && (yyN < yyTable.Length) && (yyCheck[yyN] == yyState))
          yyState = yyTable[yyN];
        else
          yyState = yyDgoto[yyM];
//t        if (debug != null) debug.shift(yyStates[yyTop], yyState);
	 goto continue_yyLoop;
      continue_yyDiscarded: ;	// implements the named-loop continue: 'continue yyDiscarded'
      }
    continue_yyLoop: ;		// implements the named-loop continue: 'continue yyLoop'
    }
  }

/*
 All more than 3 lines long rules are wrapped into a method
*/
void case_8()
#line 70 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 75 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_25()
#line 110 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

void case_89()
#line 319 "Repil/IR/IR.jay"
{
        var l = (List<Assignment>)yyVals[-1+yyTop];
        l.Add ((Assignment)yyVals[0+yyTop]);
        yyVal = new Block (LocalSymbol.None, l);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    6,    6,    8,    8,    8,    8,    8,    8,    8,
    7,    7,    9,    9,    3,   11,   11,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,    4,    5,   13,   13,   17,   17,   18,   18,
   19,   19,   19,   14,   14,   15,   15,   20,   21,   21,
   21,   21,   21,   21,   21,   21,   21,   21,   22,   22,
   22,   23,   23,   23,   23,   23,   23,   25,   10,   26,
   24,   24,   27,   28,   29,   29,   16,   16,   30,   31,
   31,   32,   33,   33,   36,   37,   37,   38,   38,   39,
   39,   40,   41,   41,   42,   43,   43,   44,   44,   34,
   34,   34,   34,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    6,
    6,    1,    2,    1,    1,    1,    1,    1,    3,    1,
    1,    3,    1,    1,    3,    1,    3,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    4,    2,    1,
    5,    5,   11,    7,    1,    3,    1,    2,    1,    2,
    1,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    2,    2,    2,
    1,    3,    2,    1,    1,    3,    1,    2,    2,    1,
    2,    1,    1,    3,    1,    1,    3,    2,    3,    1,
    3,    5,    1,    2,    3,    1,    2,    1,    1,    2,
    7,    2,    7,    6,    5,    5,    4,    6,    7,    8,
    5,    5,    6,    7,    6,    9,    6,    6,    3,    4,
    9,    5,    6,    4,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    2,
    8,    9,    0,    0,    0,    0,    0,   29,   40,   30,
   31,   32,   33,   34,   35,   36,   37,    0,    0,    0,
   28,    0,    0,    0,    3,    4,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   39,    0,    0,    5,
    6,    7,    0,   25,    0,    0,    0,    0,    0,    0,
    0,   24,    0,   21,   23,    0,    0,    0,    0,    0,
    0,   45,   38,    0,    0,   14,   15,   16,   17,   18,
    0,   12,   11,    0,   74,   73,   75,   76,   72,   71,
   70,    0,   79,   69,   41,   42,   51,   52,   53,    0,
   49,    0,    0,    0,    0,   10,   13,   22,    0,    0,
   81,   50,   46,   54,   55,    0,   58,    0,   56,   19,
   80,    0,   77,    0,   57,   82,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   87,    0,
   90,   93,    0,  108,  109,    0,  106,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   59,   60,   61,   62,   63,   64,   65,
   66,   67,   68,    0,    0,    0,    0,   43,   88,    0,
    0,    0,   89,   91,   92,   94,    0,  107,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  100,   95,    0,
    0,  112,    0,    0,  110,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   78,    0,
    0,    0,  121,  132,    0,    0,  122,    0,  116,  115,
    0,    0,    0,    0,    0,    0,  101,    0,    0,   96,
    0,    0,    0,  114,  133,  128,  127,    0,    0,    0,
   84,   85,    0,  125,    0,    0,   98,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  102,   99,   97,    0,
    0,    0,    0,    0,  103,    0,    0,   86,    0,  111,
    0,  113,  104,    0,  126,  131,  105,   83,
  };
  protected static readonly short [] yyDgoto  = {             8,
    9,   10,   31,   11,   12,   81,   63,   82,   64,  281,
   41,   66,   71,  116,  118,  148,   72,  100,  101,  119,
  184,  219,   94,  110,  225,  303,  315,  282,  283,  149,
  150,  193,  151,  195,  152,  220,  269,  270,  217,  218,
  304,  305,  156,  157,
  };
  protected static readonly short [] yySindex = {         -124,
  -19, -197,    7,   18,  199,  199, -205,    0, -124,    0,
    0,    0, -170,   53,   55, -151,   91,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  199, -130, -128,
    0,  -30,  -26,   76,    0,    0, -121, -114,   23,   24,
  -29,   70, -116, -112,  112,  199,    0,  113,   34,    0,
    0,    0,  109,    0,  199,  199,  199,  199,   -9,  199,
 -230,    0,  -23,    0,    0,  -14,   70,   14,  -15,  -34,
   -8,    0,    0,   33,  100,    0,    0,    0,    0,    0,
 -120,    0,    0,  109,    0,    0,    0,    0,    0,    0,
    0,  199,    0,    0,    0,    0,    0,    0,    0, -169,
    0,  199, -187, -126,  -97,    0,    0,    0,   73,   -4,
    0,    0,    0,    0,    0, -126,    0, -126,    0,    0,
    0,  199,    0, -110,    0,    0,  806,  104, -188,  199,
  -42, -188,  199, -158,  199,  199,  199,  199,  -53,  199,
  199,  199,  199,   49,  199,  199, -212,  -20,    0,  234,
    0,    0,  857,    0,    0,  -42,    0,  -14,  -14,  -42,
  -42,  -14,  199,  -14,   17,   27,  127,  199,   50, -172,
 -171, -168, -166,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  199,   -3,  -14,  199,    0,    0,  199,
 -234,  199,    0,    0,    0,    0,  -14,    0,  142,  143,
  -14,  -14,  144,  -14,  145, -150,  199,  199,   51,  199,
  199,  199,  199,  199,  -14,  135,  148,    0,    0,  153,
  -14,    0,  -75,  135,    0,  154,  155,  135,  135,  167,
  168,  135,  169,  135,  -43,  171,  172,  199,  173,   70,
   70,   70,   70,  176,  187,  157,  199,  216,    0,  217,
   -2,  135,    0,    0,  135,  135,    0,  135,    0,    0,
  -81,  -79,  220,  199,  135,  135,    0,   40,   42,    0,
  199,   -2,  174,    0,    0,    0,    0,   15,   16,  199,
    0,    0,  227,    0,  183,   82,    0,  199, -126,   63,
  233,  199,  235,  236,  227,  199,    0,    0,    0, -126,
 -126,   -2,  238,  158,    0,    5,    5,    0, -126,    0,
   -2,    0,    0,   11,    0,    0,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,  296,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -24,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  -22,    0,    0,   65,
    0,    0,    0,    0,  -91,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   67,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    2,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  286,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  338,
  390,  442,  494,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  546,    0,    0,    0,    0,    0,  598,    0,
    0,    0,    0,    0,  650,    0,    0,    0,    0,  702,
    0,    0,    0,    0,    0,    0,    0,    0,  754,    0,
    0,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  293,  266,    0,    0,    0,    0,  226,  224,  260,
  263,   -5,  250,    0, -105,    0,  215,   64,  -96, -115,
    0,  285,  209,    0, -227,  -62,   12,   25,   43,  193,
    0,    0,  185,    0,  192,  126,   77,   61,    0,  106,
    0,   46,    4, -113,
  };
  protected static readonly short [] yyTable = {            32,
   33,   44,  125,  112,  106,   46,   29,   47,  125,   46,
  124,   47,  127,   46,   55,   47,   26,   29,   27,   26,
   84,   27,   42,  273,   46,   46,   47,   47,   75,  111,
  223,   73,  103,   20,   55,  102,   46,   30,   47,  122,
   42,   13,  198,  224,  291,   92,  198,  198,   30,   67,
   68,   69,   70,   46,   70,   47,   46,  123,   47,  126,
  206,   76,   77,   78,   79,   80,   46,   16,   47,   28,
  207,   14,   15,  104,  310,   95,  102,   96,   17,   46,
   28,   47,  289,  317,   34,  288,  109,  216,   36,   46,
   46,   47,   47,  210,  238,   54,   70,  114,  115,   92,
   26,   83,   27,  301,  188,   47,  288,   48,   47,   46,
   48,   47,   46,   37,   47,   38,  109,   97,   98,   99,
  154,  155,   39,   40,  158,  159,   43,  162,   44,  164,
  165,  166,   92,  169,  160,  161,   49,   50,   75,  185,
  186,   92,    1,    2,   51,   28,   53,    3,    4,   56,
  197,   58,   60,   57,  201,  202,   61,  204,    5,    6,
  105,  120,  209,  117,  153,  163,    7,   20,   29,  187,
  208,   76,   77,   78,   79,   80,  211,  212,  215,  117,
  213,  221,  214,  300,  125,  228,  229,  232,  234,  112,
  235,  246,  247,  125,   92,  309,  249,  251,  252,   30,
   20,   20,   20,   20,   20,  240,  241,  242,  243,   18,
  255,  256,  258,  260,  261,  262,  264,   29,   19,  265,
   18,   20,   21,   22,   23,   24,   25,   26,   27,   19,
  266,   28,   20,   21,   22,   23,   24,   25,   26,   27,
   45,  268,   85,   86,   48,   87,   88,  216,   30,   89,
  312,  128,   97,   98,   99,  271,   90,   91,   29,  278,
  272,  279,  223,  280,  292,  268,  154,  155,   44,   44,
  296,  293,  294,   44,   44,  297,  302,  314,  306,  307,
   28,  311,  268,  318,   44,   44,  109,  129,  168,   30,
  130,  131,   44,  132,  133,    1,   85,   86,  109,   87,
   88,   35,  134,   89,   52,  135,  107,  108,   59,   74,
   90,   91,   65,  136,  137,  138,  113,  121,  316,  139,
  308,   28,  295,  140,  141,  142,   97,   98,   99,   85,
   86,  286,   87,   88,  194,  143,   89,  144,   85,   86,
  189,   87,   88,   65,  196,   89,  248,  290,  299,  313,
   93,  267,   90,   91,    0,    0,    0,    0,    0,  145,
    0,  146,  147,    0,    0,    0,    0,    0,   97,   98,
   99,   18,    0,    0,    0,    0,    0,    0,    0,    0,
   19,   62,    0,   20,   21,   22,   23,   24,   25,   26,
   27,   85,   86,    0,   87,   88,    0,  167,   89,  170,
  171,  172,  173,    0,    0,   90,   91,  174,  175,  176,
  177,  178,  179,  180,  181,  182,  183,    0,    0,    0,
   18,    0,    0,    0,    0,    0,    0,    0,    0,   19,
    0,    0,   20,   21,   22,   23,   24,   25,   26,   27,
    0,    0,  199,  200,    0,    0,  203,    0,  205,  222,
    0,  226,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   18,    0,    0,    0,    0,  236,  237,    0,  239,
   19,    0,    0,   20,   21,   22,   23,   24,   25,   26,
   27,  227,    0,    0,    0,  230,  231,    0,  233,    0,
    0,    0,    0,    0,    0,    0,    0,  263,    0,  244,
  245,    0,    0,    0,    0,  128,    0,    0,  250,    0,
    0,    0,  253,  254,    0,    0,  257,    0,  259,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  190,  191,  192,    0,    0,    0,  274,    0,    0,  275,
  276,  129,  277,    0,  130,  131,    0,  132,  133,  284,
  285,    0,  287,    0,    0,    0,  134,  129,    0,  135,
    0,    0,    0,    0,    0,    0,    0,  136,  137,  138,
  298,    0,    0,  139,    0,    0,    0,  140,  141,  142,
    0,    0,  129,  129,  129,    0,    0,    0,    0,  143,
    0,  144,    0,  129,    0,    0,  129,  129,    0,  129,
  129,    0,    0,    0,    0,    0,    0,    0,  129,  134,
    0,  129,    0,  145,    0,  146,  147,    0,    0,  129,
  129,  129,    0,    0,    0,  129,    0,    0,    0,  129,
  129,  129,    0,    0,  134,  134,  134,    0,    0,    0,
    0,  129,    0,  129,    0,  134,    0,    0,  134,  134,
    0,  134,  134,    0,    0,    0,    0,    0,    0,    0,
  134,  135,    0,  134,    0,  129,    0,  129,  129,    0,
    0,  134,  134,  134,    0,    0,    0,  134,    0,    0,
    0,  134,  134,  134,    0,    0,  135,  135,  135,    0,
    0,    0,    0,  134,    0,  134,    0,  135,    0,    0,
  135,  135,    0,  135,  135,    0,    0,    0,    0,    0,
    0,    0,  135,  130,    0,  135,    0,  134,    0,  134,
  134,    0,    0,  135,  135,  135,    0,    0,    0,  135,
    0,    0,    0,  135,  135,  135,    0,    0,  130,  130,
  130,    0,    0,    0,    0,  135,    0,  135,    0,  130,
    0,    0,  130,  130,    0,  130,  130,    0,    0,    0,
    0,    0,    0,    0,  130,  117,    0,  130,    0,  135,
    0,  135,  135,    0,    0,  130,  130,  130,    0,    0,
    0,  130,    0,    0,    0,  130,  130,  130,    0,    0,
  117,  117,  117,    0,    0,    0,    0,  130,    0,  130,
    0,  117,    0,    0,  117,  117,    0,  117,  117,    0,
    0,    0,    0,    0,    0,    0,  117,  123,    0,  117,
    0,  130,    0,  130,  130,    0,    0,  117,  117,  117,
    0,    0,    0,  117,    0,    0,    0,  117,  117,  117,
    0,    0,  123,  123,  123,    0,    0,    0,    0,  117,
    0,  117,    0,  123,    0,    0,  123,  123,    0,  123,
  123,    0,    0,    0,    0,    0,    0,    0,  123,  118,
    0,  123,    0,  117,    0,  117,  117,    0,    0,  123,
  123,  123,    0,    0,    0,  123,    0,    0,    0,  123,
  123,  123,    0,    0,  118,  118,  118,    0,    0,    0,
    0,  123,    0,  123,    0,  118,    0,    0,  118,  118,
    0,  118,  118,    0,    0,    0,    0,    0,    0,    0,
  118,  124,    0,  118,    0,  123,    0,  123,  123,    0,
    0,  118,  118,  118,    0,    0,    0,  118,    0,    0,
    0,  118,  118,  118,    0,    0,  124,  124,  124,    0,
    0,    0,    0,  118,    0,  118,    0,  124,    0,    0,
  124,  124,    0,  124,  124,    0,    0,    0,    0,    0,
    0,    0,  124,  119,    0,  124,    0,  118,    0,  118,
  118,    0,    0,  124,  124,  124,    0,    0,    0,  124,
    0,    0,    0,  124,  124,  124,    0,    0,  119,  119,
  119,    0,    0,    0,    0,  124,    0,  124,    0,  119,
    0,    0,  119,  119,    0,  119,  119,    0,    0,    0,
    0,    0,    0,    0,  119,  120,    0,  119,    0,  124,
    0,  124,  124,    0,    0,  119,  119,  119,    0,    0,
    0,  119,    0,    0,    0,  119,  119,  119,    0,    0,
  120,  120,  120,    0,    0,    0,    0,  119,    0,  119,
    0,  120,    0,    0,  120,  120,    0,  120,  120,    0,
    0,    0,    0,    0,    0,    0,  120,  128,    0,  120,
    0,  119,    0,  119,  119,    0,    0,  120,  120,  120,
    0,    0,    0,  120,    0,    0,    0,  120,  120,  120,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  120,
    0,  120,    0,  129,    0,    0,  130,  131,    0,  132,
  133,    0,    0,    0,    0,    0,    0,    0,  134,    0,
    0,  135,    0,  120,    0,  120,  120,    0,    0,  136,
  137,  138,    0,    0,    0,  139,    0,    0,    0,  140,
  141,  142,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  143,    0,  144,  129,    0,    0,  130,  131,    0,
  132,  133,    0,    0,    0,    0,    0,    0,    0,  134,
    0,    0,  135,    0,    0,  145,    0,  146,  147,    0,
  136,  137,  138,    0,    0,    0,  139,    0,    0,    0,
  140,  141,  142,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  143,    0,  144,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  145,    0,  146,  147,
  };
  protected static readonly short [] yyCheck = {             5,
    6,    0,  118,  100,  125,   40,   60,   42,  124,   40,
  116,   42,  123,   40,   44,   42,   41,   60,   41,   44,
   44,   44,   28,  251,   40,   40,   42,   42,  259,   92,
  265,   41,   41,  125,   44,   44,   40,   91,   42,   44,
   46,   61,  156,  278,  272,   60,  160,  161,   91,   55,
   56,   57,   58,   40,   60,   42,   40,   62,   42,  122,
   44,  292,  293,  294,  295,  296,   40,   61,   42,  123,
   44,  269,  270,   41,  302,   62,   44,   93,   61,   40,
  123,   42,   41,  311,  290,   44,   92,   91,  259,   40,
   40,   42,   42,   44,   44,  125,  102,  285,  286,   60,
  125,  125,  125,   41,  125,   41,   44,   41,   44,   40,
   44,   42,   40,   61,   42,   61,  122,  287,  288,  289,
  309,  310,  274,   33,  130,  131,  257,  133,  257,  135,
  136,  137,   60,  139,  131,  132,   61,  259,  259,  145,
  146,   60,  267,  268,  259,  123,  123,  272,  273,  266,
  156,   40,   40,  266,  160,  161,  123,  163,  283,  284,
   61,  259,  168,  290,   61,  324,  291,  259,   60,  382,
   44,  292,  293,  294,  295,  296,  349,  349,  184,  290,
  349,  187,  349,  289,  300,   44,   44,   44,   44,  286,
  341,   44,   40,  309,   60,  301,  272,   44,   44,   91,
  292,  293,  294,  295,  296,  211,  212,  213,  214,  263,
   44,   44,   44,  257,   44,   44,   44,   60,  272,   44,
  263,  275,  276,  277,  278,  279,  280,  281,  282,  272,
   44,  123,  275,  276,  277,  278,  279,  280,  281,  282,
  271,  247,  257,  258,  271,  260,  261,   91,   91,  264,
   93,  272,  287,  288,  289,   40,  271,  272,   60,  341,
   44,  341,  265,   44,   91,  271,  309,  310,  267,  268,
   44,  257,  257,  272,  273,   93,   44,  273,   44,   44,
  123,   44,  288,  273,  283,  284,  292,  308,  342,   91,
  311,  312,  291,  314,  315,    0,  257,  258,  304,  260,
  261,    9,  323,  264,   39,  326,   81,   84,   46,   60,
  271,  272,   53,  334,  335,  336,  102,  109,  307,  340,
  296,  123,  280,  344,  345,  346,  287,  288,  289,  257,
  258,  268,  260,  261,  150,  356,  264,  358,  257,  258,
  148,  260,  261,   84,  153,  264,  221,  271,  288,  304,
   66,  246,  271,  272,   -1,   -1,   -1,   -1,   -1,  380,
   -1,  382,  383,   -1,   -1,   -1,   -1,   -1,  287,  288,
  289,  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,  275,  276,  277,  278,  279,  280,  281,
  282,  257,  258,   -1,  260,  261,   -1,  138,  264,  140,
  141,  142,  143,   -1,   -1,  271,  272,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,   -1,   -1,   -1,
  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   -1,   -1,  275,  276,  277,  278,  279,  280,  281,  282,
   -1,   -1,  158,  159,   -1,   -1,  162,   -1,  164,  190,
   -1,  192,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  263,   -1,   -1,   -1,   -1,  207,  208,   -1,  210,
  272,   -1,   -1,  275,  276,  277,  278,  279,  280,  281,
  282,  197,   -1,   -1,   -1,  201,  202,   -1,  204,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  238,   -1,  215,
  216,   -1,   -1,   -1,   -1,  272,   -1,   -1,  224,   -1,
   -1,   -1,  228,  229,   -1,   -1,  232,   -1,  234,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  297,  298,  299,   -1,   -1,   -1,  252,   -1,   -1,  255,
  256,  308,  258,   -1,  311,  312,   -1,  314,  315,  265,
  266,   -1,  268,   -1,   -1,   -1,  323,  272,   -1,  326,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
  286,   -1,   -1,  340,   -1,   -1,   -1,  344,  345,  346,
   -1,   -1,  297,  298,  299,   -1,   -1,   -1,   -1,  356,
   -1,  358,   -1,  308,   -1,   -1,  311,  312,   -1,  314,
  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  323,  272,
   -1,  326,   -1,  380,   -1,  382,  383,   -1,   -1,  334,
  335,  336,   -1,   -1,   -1,  340,   -1,   -1,   -1,  344,
  345,  346,   -1,   -1,  297,  298,  299,   -1,   -1,   -1,
   -1,  356,   -1,  358,   -1,  308,   -1,   -1,  311,  312,
   -1,  314,  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  323,  272,   -1,  326,   -1,  380,   -1,  382,  383,   -1,
   -1,  334,  335,  336,   -1,   -1,   -1,  340,   -1,   -1,
   -1,  344,  345,  346,   -1,   -1,  297,  298,  299,   -1,
   -1,   -1,   -1,  356,   -1,  358,   -1,  308,   -1,   -1,
  311,  312,   -1,  314,  315,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  323,  272,   -1,  326,   -1,  380,   -1,  382,
  383,   -1,   -1,  334,  335,  336,   -1,   -1,   -1,  340,
   -1,   -1,   -1,  344,  345,  346,   -1,   -1,  297,  298,
  299,   -1,   -1,   -1,   -1,  356,   -1,  358,   -1,  308,
   -1,   -1,  311,  312,   -1,  314,  315,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  323,  272,   -1,  326,   -1,  380,
   -1,  382,  383,   -1,   -1,  334,  335,  336,   -1,   -1,
   -1,  340,   -1,   -1,   -1,  344,  345,  346,   -1,   -1,
  297,  298,  299,   -1,   -1,   -1,   -1,  356,   -1,  358,
   -1,  308,   -1,   -1,  311,  312,   -1,  314,  315,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  323,  272,   -1,  326,
   -1,  380,   -1,  382,  383,   -1,   -1,  334,  335,  336,
   -1,   -1,   -1,  340,   -1,   -1,   -1,  344,  345,  346,
   -1,   -1,  297,  298,  299,   -1,   -1,   -1,   -1,  356,
   -1,  358,   -1,  308,   -1,   -1,  311,  312,   -1,  314,
  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  323,  272,
   -1,  326,   -1,  380,   -1,  382,  383,   -1,   -1,  334,
  335,  336,   -1,   -1,   -1,  340,   -1,   -1,   -1,  344,
  345,  346,   -1,   -1,  297,  298,  299,   -1,   -1,   -1,
   -1,  356,   -1,  358,   -1,  308,   -1,   -1,  311,  312,
   -1,  314,  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  323,  272,   -1,  326,   -1,  380,   -1,  382,  383,   -1,
   -1,  334,  335,  336,   -1,   -1,   -1,  340,   -1,   -1,
   -1,  344,  345,  346,   -1,   -1,  297,  298,  299,   -1,
   -1,   -1,   -1,  356,   -1,  358,   -1,  308,   -1,   -1,
  311,  312,   -1,  314,  315,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  323,  272,   -1,  326,   -1,  380,   -1,  382,
  383,   -1,   -1,  334,  335,  336,   -1,   -1,   -1,  340,
   -1,   -1,   -1,  344,  345,  346,   -1,   -1,  297,  298,
  299,   -1,   -1,   -1,   -1,  356,   -1,  358,   -1,  308,
   -1,   -1,  311,  312,   -1,  314,  315,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  323,  272,   -1,  326,   -1,  380,
   -1,  382,  383,   -1,   -1,  334,  335,  336,   -1,   -1,
   -1,  340,   -1,   -1,   -1,  344,  345,  346,   -1,   -1,
  297,  298,  299,   -1,   -1,   -1,   -1,  356,   -1,  358,
   -1,  308,   -1,   -1,  311,  312,   -1,  314,  315,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  323,  272,   -1,  326,
   -1,  380,   -1,  382,  383,   -1,   -1,  334,  335,  336,
   -1,   -1,   -1,  340,   -1,   -1,   -1,  344,  345,  346,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  356,
   -1,  358,   -1,  308,   -1,   -1,  311,  312,   -1,  314,
  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  323,   -1,
   -1,  326,   -1,  380,   -1,  382,  383,   -1,   -1,  334,
  335,  336,   -1,   -1,   -1,  340,   -1,   -1,   -1,  344,
  345,  346,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  356,   -1,  358,  308,   -1,   -1,  311,  312,   -1,
  314,  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  323,
   -1,   -1,  326,   -1,   -1,  380,   -1,  382,  383,   -1,
  334,  335,  336,   -1,   -1,   -1,  340,   -1,   -1,   -1,
  344,  345,  346,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  356,   -1,  358,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  380,   -1,  382,  383,
  };

#line 537 "Repil/IR/IR.jay"

}

#line default
namespace yydebug {
        using System;
	 internal interface yyDebug {
		 void push (int state, Object value);
		 void lex (int state, int token, string name, Object value);
		 void shift (int from, int to, int errorFlag);
		 void pop (int state);
		 void discard (int state, int token, string name, Object value);
		 void reduce (int from, int to, int rule, string text, int len);
		 void shift (int from, int to);
		 void accept (Object value);
		 void error (string message);
		 void reject ();
	 }
	 
	 class yyDebugSimple : yyDebug {
		 void println (string s){
			 System.Diagnostics.Debug.WriteLine (s);
		 }
		 
		 public void push (int state, Object value) {
			 println ("push\tstate "+state+"\tvalue "+value);
		 }
		 
		 public void lex (int state, int token, string name, Object value) {
			 println("lex\tstate "+state+"\treading "+name+"\tvalue "+value);
		 }
		 
		 public void shift (int from, int to, int errorFlag) {
			 switch (errorFlag) {
			 default:				// normally
				 println("shift\tfrom state "+from+" to "+to);
				 break;
			 case 0: case 1: case 2:		// in error recovery
				 println("shift\tfrom state "+from+" to "+to
					     +"\t"+errorFlag+" left to recover");
				 break;
			 case 3:				// normally
				 println("shift\tfrom state "+from+" to "+to+"\ton error");
				 break;
			 }
		 }
		 
		 public void pop (int state) {
			 println("pop\tstate "+state+"\ton error");
		 }
		 
		 public void discard (int state, int token, string name, Object value) {
			 println("discard\tstate "+state+"\ttoken "+name+"\tvalue "+value);
		 }
		 
		 public void reduce (int from, int to, int rule, string text, int len) {
			 println("reduce\tstate "+from+"\tuncover "+to
				     +"\trule ("+rule+") "+text);
		 }
		 
		 public void shift (int from, int to) {
			 println("goto\tfrom state "+from+" to "+to);
		 }
		 
		 public void accept (Object value) {
			 println("accept\tvalue "+value);
		 }
		 
		 public void error (string message) {
			 println("error\t"+message);
		 }
		 
		 public void reject () {
			 println("reject");
		 }
		 
	 }
}
// %token constants
 class Token {
  public const int INTEGER = 257;
  public const int FLOAT_LITERAL = 258;
  public const int STRING = 259;
  public const int TRUE = 260;
  public const int FALSE = 261;
  public const int UNDEF = 262;
  public const int VOID = 263;
  public const int NULL = 264;
  public const int LABEL = 265;
  public const int X = 266;
  public const int SOURCE_FILENAME = 267;
  public const int TARGET = 268;
  public const int DATALAYOUT = 269;
  public const int TRIPLE = 270;
  public const int GLOBAL_SYMBOL = 271;
  public const int LOCAL_SYMBOL = 272;
  public const int META_SYMBOL = 273;
  public const int TYPE = 274;
  public const int HALF = 275;
  public const int FLOAT = 276;
  public const int DOUBLE = 277;
  public const int I1 = 278;
  public const int I8 = 279;
  public const int I16 = 280;
  public const int I32 = 281;
  public const int I64 = 282;
  public const int DEFINE = 283;
  public const int DECLARE = 284;
  public const int UNNAMED_ADDR = 285;
  public const int LOCAL_UNNAMED_ADDR = 286;
  public const int NONNULL = 287;
  public const int NOCAPTURE = 288;
  public const int WRITEONLY = 289;
  public const int ATTRIBUTE_GROUP_REF = 290;
  public const int ATTRIBUTES = 291;
  public const int NORECURSE = 292;
  public const int NOUNWIND = 293;
  public const int SSP = 294;
  public const int UWTABLE = 295;
  public const int ARGMEMONLY = 296;
  public const int RET = 297;
  public const int BR = 298;
  public const int SWITCH = 299;
  public const int INDIRECTBR = 300;
  public const int INVOKE = 301;
  public const int RESUME = 302;
  public const int CATCHSWITCH = 303;
  public const int CATCHRET = 304;
  public const int CLEANUPRET = 305;
  public const int UNREACHABLE = 306;
  public const int FNEG = 307;
  public const int ADD = 308;
  public const int NUW = 309;
  public const int NSW = 310;
  public const int FADD = 311;
  public const int SUB = 312;
  public const int FSUB = 313;
  public const int MUL = 314;
  public const int FMUL = 315;
  public const int UDIV = 316;
  public const int SDIV = 317;
  public const int FDIV = 318;
  public const int UREM = 319;
  public const int SREM = 320;
  public const int FREM = 321;
  public const int SHL = 322;
  public const int LSHR = 323;
  public const int EXACT = 324;
  public const int ASHR = 325;
  public const int AND = 326;
  public const int OR = 327;
  public const int XOR = 328;
  public const int EXTRACTELEMENT = 329;
  public const int INSERTELEMENT = 330;
  public const int SHUFFLEVECTOR = 331;
  public const int EXTRACTVALUE = 332;
  public const int INSERTVALUE = 333;
  public const int ALLOCA = 334;
  public const int LOAD = 335;
  public const int STORE = 336;
  public const int FENCE = 337;
  public const int CMPXCHG = 338;
  public const int ATOMICRMW = 339;
  public const int GETELEMENTPTR = 340;
  public const int ALIGN = 341;
  public const int INBOUNDS = 342;
  public const int INRANGE = 343;
  public const int TRUNC = 344;
  public const int ZEXT = 345;
  public const int SEXT = 346;
  public const int FPTRUNC = 347;
  public const int FPEXT = 348;
  public const int TO = 349;
  public const int FPTOUI = 350;
  public const int FPTOSI = 351;
  public const int UITOFP = 352;
  public const int SITOFP = 353;
  public const int PTRTOINT = 354;
  public const int INTTOPTR = 355;
  public const int BITCAST = 356;
  public const int ADDRSPACECAST = 357;
  public const int ICMP = 358;
  public const int EQ = 359;
  public const int NE = 360;
  public const int UGT = 361;
  public const int UGE = 362;
  public const int ULT = 363;
  public const int ULE = 364;
  public const int SGT = 365;
  public const int SGE = 366;
  public const int SLT = 367;
  public const int SLE = 368;
  public const int FCMP = 369;
  public const int OEQ = 370;
  public const int OGT = 371;
  public const int OGE = 372;
  public const int OLT = 373;
  public const int OLE = 374;
  public const int ONE = 375;
  public const int ORD = 376;
  public const int UEQ = 377;
  public const int UNE = 378;
  public const int UNO = 379;
  public const int PHI = 380;
  public const int SELECT = 381;
  public const int CALL = 382;
  public const int TAIL = 383;
  public const int VA_ARG = 384;
  public const int LANDINGPAD = 385;
  public const int CATCHPAD = 386;
  public const int CLEANUPPAD = 387;
  public const int yyErrorCode = 256;
 }
 namespace yyParser {
  using System;
  /** thrown for irrecoverable syntax errors and stack overflow.
    */
  internal class yyException : System.Exception {
    public yyException (string message) : base (message) {
    }
  }
  internal class yyUnexpectedEof : yyException {
    public yyUnexpectedEof (string message) : base (message) {
    }
    public yyUnexpectedEof () : base ("") {
    }
  }

  /** must be implemented by a scanner object to supply input to the parser.
    */
  internal interface yyInput {
    /** move on to next token.
        @return false if positioned beyond tokens.
        @throws IOException on input error.
      */
    bool advance (); // throws java.io.IOException;
    /** classifies current token.
        Should not be called if advance() returned false.
        @return current %token or single character.
      */
    int token ();
    /** associated with current token.
        Should not be called if advance() returned false.
        @return value for token().
      */
    Object value ();
  }
 }
} // close outermost namespace, that MUST HAVE BEEN opened in the prolog
