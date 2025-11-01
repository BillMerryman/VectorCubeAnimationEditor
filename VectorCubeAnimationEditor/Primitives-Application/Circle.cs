using System.Buffers.Binary;

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
            set { r = (value < 1) ? (Int16)1 : value; } 
        }

        public UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Circle()
        {
            r = AnimationConstants.DEFAULT_PRIMITIVE_RADIUS;
            x0 = AnimationConstants.SCREEN_CENTER_X; 
            y0 = AnimationConstants.SCREEN_CENTER_Y;
            color = 0;
        }

        public Circle(Circle circle)
        {
            x0 = circle.X0;
            y0 = circle.Y0;
            r = circle.R;
            color = circle.Color;
        }

        public void Serialize(ref int bytePosition, byte[] animationBytes)
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

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            x0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            r = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition)); ;
            bytePosition += 8;
        }

    }
}
