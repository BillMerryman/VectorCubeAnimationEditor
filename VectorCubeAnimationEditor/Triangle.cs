﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class Triangle
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 x1;
        private Int16 y1;
        private Int16 x2;
        private Int16 y2;
        private UInt16 color;

        public Int16 X0 
        { 
            get { return x0; } 
            set {  x0 = value; } 
        }

        public Int16 Y0
        {
            get { return y0; }
            set { y0 = value; }
        }

        public Int16 X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        public Int16 Y1
        {
            get { return y1; }
            set { y1 = value; }
        }

        public Int16 X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        public Int16 Y2
        {
            get { return y2; }
            set { y2 = value; }
        }

        public UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Triangle()
        {
            x0 = 63;
            y0 = 53;
            x1 = 43;
            y1 = 73;
            x2 = 73;
            y2 = 73;
            color = 0;
        }

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x2);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y2);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 2;
        }

    }
}
