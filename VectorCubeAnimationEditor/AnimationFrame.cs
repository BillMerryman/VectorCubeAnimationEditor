using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class AnimationFrame
    {
        private UInt32 duration = 0;
        private UInt16 fillColor = 0x0000;
        private UInt16 primitiveCount = 0;
        private Primitive[] primitives = new Primitive[AnimationConstants._MaxPrimitiveCount];

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
            for(int i = 0; i < primitives.Length; i++)
            {
                primitives[i] = new Primitive();
            }
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

        public Primitive? GetPrimitiveNumber(int primitiveNumber)
        {
            if(primitiveNumber < 1 || primitiveNumber > primitiveCount) return null;
            return primitives[primitiveNumber - 1];
        }

        public Primitive? AddPrimitive()
        {
            if (primitiveCount > AnimationConstants._MaxPrimitiveCount) return null;
            primitiveCount++;
            return primitives[primitiveCount - 1];
        }

        public int RemovePrimitive(Primitive? primitive)
        {
            if(primitive == null) return -1;
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

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(animationBytes.AsSpan().Slice(bytePosition), duration);
            bytePosition += 4;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), fillColor);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), primitiveCount);
            bytePosition += 2;
            for (int index = 0; index < primitives.Length; index++)
            {
                primitives[index].serialize(ref bytePosition, animationBytes);
            }
        }

        public void deserialize(ref int bytePosition, byte[] animationBytes)
        {
            duration = BinaryPrimitives.ReadUInt32LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
            fillColor = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            primitiveCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            for (int index = 0; index < primitives.Length; index++)
            {
                primitives[index].deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}
