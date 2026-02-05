using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Buffers.Binary;
using System.Text.Json.Serialization;

namespace VectorCubeAnimationEditor
{

    internal class AnimationFrame
    {
        private UInt32 duration;
        private UInt16 fillColor;
        public List<Primitive> primitives { get; set; }

        public Animation AnimationRoot
        {
            get; 
        }

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

        [JsonIgnore]
        public int PrimitiveCount
        {
            get { return primitives.Count; }
        }

        public AnimationFrame()
        {
            duration = 0;
            fillColor = 0x0000;
            primitives = [];
        }

        public AnimationFrame(AnimationFrame frame)
        {
            this.duration = frame.duration;
            this.fillColor = frame.fillColor;
            primitives = [.. frame.primitives.Select(item => item.Clone())];
            AnimationRoot = frame.AnimationRoot;
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

        public void SerializeBinary(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(animationBytes.AsSpan()[bytePosition..], duration);
            bytePosition += 4;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], fillColor);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], (ushort)primitives.Count);
            bytePosition += 2;
            for (int index = 0; index < primitives.Count; index++)
            {
                primitives[index].SerializeBinary(ref bytePosition, animationBytes);
            }
            for (int index = primitives.Count; index < AnimationConstants._MaxPrimitiveCount; index++)
            {
                bytePosition += AnimationConstants._PrimitiveTypeWidth;
                bytePosition += AnimationConstants._LargestPrimitiveByteCount;
            }
        }

        public void DeserializeBinary(ref int bytePosition, byte[] animationBytes)
        {
            duration = BinaryPrimitives.ReadUInt32LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 4;
            fillColor = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            UInt16 primitiveCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            for (int index = 0; index < primitiveCount; index++)
            {
                UInt16 type = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
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
                    case AnimationConstants._RotatedRect:
                        newPrimitive = new RotatedRect();
                        break;
                    case AnimationConstants._QuarterCircle:
                        newPrimitive = new Circle();
                        break;
                    default:
                        bytePosition += AnimationConstants._LargestPrimitiveByteCount;
                        break;
                }
                if (newPrimitive != null)
                {
                    newPrimitive.DeserializeBinary(ref bytePosition, animationBytes);
                    primitives.Add(newPrimitive);
                }

            }
            for (int index = primitiveCount; index < AnimationConstants._MaxPrimitiveCount; index++)
            {
                bytePosition += AnimationConstants._CommandWidth;
                bytePosition += AnimationConstants._LargestPrimitiveByteCount;
            }
        }

        public Offset<AnimationFrameFB> SerializeFB(FlatBufferBuilder builder)
        {
            PrimitiveFB[] primitiveFBs = new PrimitiveFB[PrimitiveCount];
            int[] primitiveOffsetsAsInts = new int[PrimitiveCount];
            for (int index = 0; index < PrimitiveCount; index++)
            {
                (primitiveFBs[index], primitiveOffsetsAsInts[index]) = primitives[index].SerializeFB(builder);
            }
            VectorOffset primitives_TypeOffset = AnimationFrameFB.CreatePrimitivesTypeVector(builder, primitiveFBs);
            VectorOffset primitivesOffset = AnimationFrameFB.CreatePrimitivesVector(builder, primitiveOffsetsAsInts);
            return AnimationFrameFB.CreateAnimationFrameFB(builder, duration, fillColor, primitives_TypeOffset, primitivesOffset);
        }

        public void DeserializeFB(AnimationFrameFB animationFrameFB)
        {
            for (int index = 0; index < animationFrameFB.PrimitivesLength; index++)
            {
                PrimitiveFB primitiveFB = animationFrameFB.PrimitivesType(index);
                switch (primitiveFB)
                {
                    case PrimitiveFB.LineFB:
                        LineFB lineFB = animationFrameFB.Primitives<LineFB>(index).Value;
                        Line line = (Line)AddPrimitive(typeof(Line), lineFB.Color);
                        line.DeserializeFB(lineFB);
                        break;
                    case PrimitiveFB.TriangleFB:
                        TriangleFB triangleFB = animationFrameFB.Primitives<TriangleFB>(index).Value;
                        Triangle triangle = (Triangle)AddPrimitive(typeof(Triangle), triangleFB.Color);
                        triangle.DeserializeFB(triangleFB);
                        break;
                    case PrimitiveFB.RoundRectFB:
                        RoundRectFB roundRectFB = animationFrameFB.Primitives<RoundRectFB>(index).Value;
                        RoundRect roundRect = (RoundRect)AddPrimitive(typeof(RoundRect), roundRectFB.Color);
                        roundRect.DeserializeFB(roundRectFB);
                        break;
                    case PrimitiveFB.RotatedRectFB:
                        RotatedRectFB rotatedRectFB = animationFrameFB.Primitives<RotatedRectFB>(index).Value;
                        RotatedRect rotatedRect = (RotatedRect)AddPrimitive(typeof(RotatedRect), rotatedRectFB.Color);

                        break;
                    case PrimitiveFB.CircleFB:
                        CircleFB circleFB = animationFrameFB.Primitives<CircleFB>(index).Value;
                        Circle circle = (Circle)AddPrimitive(typeof(Circle), circleFB.Color);
                        circle.DeserializeFB(circleFB);
                        break;
                }   
            }
        }

    }
}
