using System.Buffers.Binary;

namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;

    internal class AnimationFrame
    {
        private UInt32 duration;
        private UInt16 fillColor;
        private List<Primitive> primitives;

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

        public int PrimitiveCount
        {
            get { return primitives.Count; }
        }

        public AnimationFrame()
        {
            duration = 0;
            fillColor = 0x0000;
            primitives = new List<Primitive>();
        }

        public AnimationFrame(AnimationFrame frame)
        {
            this.duration = frame.duration;
            this.fillColor = frame.fillColor;
            primitives = frame.primitives.Select(item => item.Clone()).ToList();
        }

        public Primitive? GetPrimitive(int primitiveIndex)
        {
            if (primitiveIndex < 0 || primitiveIndex > primitives.Count - 1) return null;
            return primitives[primitiveIndex];
        }

        public int IndexOf(Primitive? primitive)
        {
            if (primitive == null) return -1;
            return primitives.IndexOf(primitive);
        }

        public Primitive? AddPrimitive(Type primitiveType, UInt16 color)
        {
            if (PrimitiveCount >= AnimationConstants._MaxPrimitiveCount) return null;
            Primitive? primitive = (Primitive?)Activator.CreateInstance(primitiveType);
            if (primitive != null)
            {
                primitive.Color = color;
                primitives.Add(primitive);
            }
            return primitive;
        }

        public int RemovePrimitive(Primitive? primitive)
        {
            if (primitive == null) return -1;
            int index = primitives.IndexOf(primitive);
            primitives.Remove(primitive);
            return index;
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
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), (ushort)primitives.Count);
            bytePosition += 2;
            for (int index = 0; index < primitives.Count; index++)
            {
                primitives[index].Serialize(ref bytePosition, animationBytes);
            }
            for (int index = primitives.Count; index < AnimationConstants._MaxPrimitiveCount; index++)
            {
                bytePosition += AnimationConstants._PrimitiveTypeWidth;
                bytePosition += AnimationConstants._LargestPrimitiveByteCount;
            }
        }

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            duration = BinaryPrimitives.ReadUInt32LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
            fillColor = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            UInt16 primitiveCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            for (int index = 0; index < primitiveCount; index++)
            {
                PrimitiveType type = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
                bytePosition += AnimationConstants._PrimitiveTypeWidth;
                Primitive newPrimitive = null;
                switch (type)
                {
                    case AnimationConstants._Line:
                        newPrimitive = new Line();
                        break;
                    case AnimationConstants._Triangle:
                        newPrimitive = new Triangle();
                        break;
                    case AnimationConstants._RoundRect:
                        newPrimitive = new RoundRect();
                        break;
                    case AnimationConstants._Circle:
                        newPrimitive = new Circle();
                        break;
                }
                if (newPrimitive != null)
                {
                    newPrimitive.Deserialize(ref bytePosition, animationBytes);
                    primitives.Add(newPrimitive);
                }

            }
            for (int index = primitiveCount; index < AnimationConstants._MaxPrimitiveCount; index++)
            {
                bytePosition += AnimationConstants._CommandWidth;
                bytePosition += AnimationConstants._LargestPrimitiveByteCount;
            }
        }

    }
}
