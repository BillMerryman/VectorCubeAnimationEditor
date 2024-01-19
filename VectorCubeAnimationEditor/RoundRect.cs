using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class RoundRect
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 w;
        private Int16 h;
        private Int16 radius;
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

        public Int16 W
        {
            get { return w; }
            set { w = value; }
        }

        public Int16 H
        {
            get { return h; }
            set { h = value; }
        }

        public Int16 Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RoundRect()
        {
            x0 = 20;
            y0 = 30;
            w = 40;
            h = 40;
            radius = 8;
            color = 0;
        }

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), w);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), h);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), radius);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 4;
        }

    }
}
