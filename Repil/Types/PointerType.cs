﻿using System;

namespace Repil.Types
{
    public class PointerType : AggregateType
    {
        public readonly LType ElementType;
        public readonly int AddressSpace;

        public static readonly PointerType I8Pointer = new PointerType (IntegerType.I8, 0);
        public static readonly PointerType VoidPointer = new PointerType (VoidType.Void, 0);

        public PointerType (LType elementType, int addressSpace)
        {
            ElementType = elementType;
            AddressSpace = addressSpace;
        }

        public override string ToString () => $"{ElementType}*";

        public override long GetByteSize (Module module) => module.PointerByteSize;
    }
}
