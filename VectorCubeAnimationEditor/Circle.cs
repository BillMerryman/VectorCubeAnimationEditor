using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class Circle
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 r;
        private UInt16 color;

        public Int16 X0 
        { 
            get { return x0; } 
            set { x0 = value; } 
        }

        public Int16 Y0 
        { 
            get { return y0; } 
            set { y0 = value; } 
        }

        public Int16 R 
        { 
            get { return r; } 
            set { r = value; } 
        }

        public UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Circle()
        {
            x0 = 63; 
            y0 = 63; 
            r = 10; 
            color = 0;
        }

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), r);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 8;
        }

    }
}
