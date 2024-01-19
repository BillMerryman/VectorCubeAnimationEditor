using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class QuarterCircle
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 r;
        private byte quadrants;
        private Int16 delta;
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

        public byte Quadrants
        {
            get { return quadrants; }
            set { quadrants = value; }
        }

        public Int16 Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        public UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public QuarterCircle()
        {
            x0 = 63;
            y0 = 63;
            r = 10;
            quadrants = 15;
            delta = 0;
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
            animationBytes[bytePosition] = quadrants;
            bytePosition += 1;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), delta);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 5;
        }

        public void deserialize(ref int bytePosition, byte[] animationBytes)
        {
            x0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            r = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            quadrants = animationBytes[bytePosition];
            bytePosition += 1;
            delta = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 5;
        }

    }
}
