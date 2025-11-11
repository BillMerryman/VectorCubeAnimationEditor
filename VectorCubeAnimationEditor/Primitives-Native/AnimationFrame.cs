using System.Buffers.Binary;

namespace ST7735Point85
{
    using PrimitiveType = UInt16;

    internal class AnimationFrame
    {
        private UInt32 duration;
        private UInt16 fillColor;
        private UInt16 primitiveCount;
        private Primitive[] primitives;

        public UInt32 Duration
        {
            set { duration = value; }
            get { return duration; }
        }

        public UInt16 FillColor
        {
            set { fillColor = value; }
            get { return fillColor; }
        }

        public UInt16 PrimitiveCount
        {
            get { return primitiveCount; }
        }

        public AnimationFrame()
        {
            duration = 0;
            fillColor = 0x0000;
            primitiveCount = 0;
            primitives = new Primitive[AnimationConstants._MaxPrimitiveCount];
            for(int i = 0; i < primitives.Length; i++)
            {
                primitives[i] = new Primitive();
            }
        }

        public AnimationFrame(AnimationFrame frame)
        {
            this.duration = frame.duration;
            this.fillColor = frame.fillColor;
            this.primitiveCount = frame.primitiveCount;
            primitives = new Primitive[AnimationConstants._MaxPrimitiveCount];
            for (int i = 0; i < primitives.Length; i++)
            {
                primitives[i] = new Primitive(frame.primitives[i]);
            }
        }

        public Primitive? GetPrimitiveNumber(int primitiveNumber)
        {
            if (primitiveNumber < 1 || primitiveNumber > primitiveCount) return null;
            return primitives[primitiveNumber - 1];
        }

        public int GetNumberOfPrimitive(Primitive? primitive)
        {
            if (primitive == null) return -1;
            for (int index = 0; index < primitives.Length; index++)
            {
                if (object.ReferenceEquals(primitive, primitives[index]))
                {
                    return index + 1;
                }
            }
            return -1;
        }

        public Primitive? AddPrimitive(PrimitiveType primitiveType, UInt16 color)
        {
            if (primitiveCount > AnimationConstants._MaxPrimitiveCount) return null;
            Primitive primitive = new Primitive();
            primitives[primitiveCount] = primitive;
            primitive.Type = primitiveType;
            switch (primitiveType)
            {
                case AnimationConstants._Circle:
                    primitive.Circle.Color = color;
                    break;
                case AnimationConstants._QuarterCircle:
                    primitive.QuarterCircle.Color = color;
                    break;
                case AnimationConstants._Triangle:
                    primitive.Triangle.Color = color;
                    break;
                case AnimationConstants._RoundRect:
                    primitive.RoundRect.Color = color;
                    break;
                case AnimationConstants._Line:
                    primitive.Line.Color = color;
                    break;
            }
            primitiveCount++;
            return primitive;
        }

        public int RemovePrimitive(Primitive? primitive)
        {
            if (primitive == null) return -1;
            for (int index = 0; index < primitives.Length; index++)
            {
                if (object.ReferenceEquals(primitive, primitives[index]))
                {
                    for (int innerIndex = index; innerIndex < primitives.Length - 1; innerIndex++)
                    {
                        primitives[innerIndex] = primitives[innerIndex + 1];
                    }
                    primitives[^1] = new Primitive();
                    primitiveCount--;
                    return index + 1;
                }
            }
            return -1;
        }

        public bool MovePrimitiveUp(Primitive? primitive)
        {
            return false;
        }

        public bool MovePrimitiveDown(Primitive? primitive)
        {
            return false;
        }

        public void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(animationBytes.AsSpan().Slice(bytePosition), duration);
            bytePosition += 4;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), fillColor);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), primitiveCount);
            bytePosition += 2;
            for (int index = 0; index < primitives.Length; index++)
            {
                primitives[index].Serialize(ref bytePosition, animationBytes);
            }
        }

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            duration = BinaryPrimitives.ReadUInt32LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
            fillColor = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            primitiveCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            for (int index = 0; index < primitives.Length; index++)
            {
                primitives[index].Deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}
