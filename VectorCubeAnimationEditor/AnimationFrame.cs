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
        private IdentifiedPrimitive[] identifiedPrimitives = new IdentifiedPrimitive[AnimationConstants._MaxPrimitiveCount];

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
            for(int i = 0; i < identifiedPrimitives.Length; i++)
            {
                identifiedPrimitives[i] = new IdentifiedPrimitive();
            }
        }

        public int GetNumberOfIdentifiedPrimitive(IdentifiedPrimitive? identifiedPrimitive)
        {
            if (identifiedPrimitive == null) return -1;
            for (int index = 0; index < identifiedPrimitives.Length; index++)
            {
                if (object.ReferenceEquals(identifiedPrimitive, identifiedPrimitives[index]))
                {
                    return index + 1;
                }
            }
            return -1;
        }

        public IdentifiedPrimitive? GetIdentifiedPrimitiveNumber(int identifiedPrimitiveNumber)
        {
            if(identifiedPrimitiveNumber < 1 || identifiedPrimitiveNumber > primitiveCount) return null;
            return identifiedPrimitives[identifiedPrimitiveNumber - 1];
        }

        public IdentifiedPrimitive? AddIdentifiedPrimitive()
        {
            if (primitiveCount > AnimationConstants._MaxPrimitiveCount) return null;
            primitiveCount++;
            return identifiedPrimitives[primitiveCount - 1];
        }

        public int RemoveIdentifiedPrimitive(IdentifiedPrimitive? identifiedPrimitive)
        {
            if(identifiedPrimitive == null) return -1;
            for (int index = 0; index < identifiedPrimitives.Length; index++)
            {
                if (object.ReferenceEquals(identifiedPrimitive, identifiedPrimitives[index]))
                {
                    for (int innerIndex = index; innerIndex < identifiedPrimitives.Length - 1; innerIndex++)
                    {
                        identifiedPrimitives[innerIndex] = identifiedPrimitives[innerIndex + 1];
                    }
                    identifiedPrimitives[^1] = new IdentifiedPrimitive();
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
            for (int index = 0; index < identifiedPrimitives.Length; index++)
            {
                identifiedPrimitives[index].serialize(ref bytePosition, animationBytes);
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
            for (int index = 0; index < identifiedPrimitives.Length; index++)
            {
                identifiedPrimitives[index].deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}
