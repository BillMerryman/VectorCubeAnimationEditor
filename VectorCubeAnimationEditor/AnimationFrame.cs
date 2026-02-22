using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Buffers.Binary;
using System.Text.Json.Serialization;

namespace VectorCubeAnimationEditor
{
    internal class AnimationFrame
    {
        Animation parent = null!;

        public List<Primitive> primitives;
        private Point center = new(0, 0);

        private UInt32 duration;
        private UInt16 fillColor;

        [JsonIgnore]
        public Animation Parent
        {
            get { return parent; }
            internal set
            {
                parent = value;
                foreach (Primitive p in primitives)
                {
                    p.Parent = this;
                }
            }
        }

        public List<Primitive> Primitives
        { 
            get { return primitives;  } 
            set {  primitives = value; } 
        }

        [JsonIgnore]
        public int PrimitiveCount
        {
            get { return primitives.Count; }
        }

        public Point Center
        {
            get { return center; }
        }

        [JsonIgnore]
        public Point RelativeCenter
        {
            get
            {
                return new Point(Parent.Center.X + Center.X, Parent.Center.Y + Center.Y);
            }
        }

        public UInt32 Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public UInt16 FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public AnimationFrame()
        {
            primitives = [];
        }

        public AnimationFrame(UInt32 duration, UInt16 fillColor, Animation parent) : this()
        {
            this.duration = duration;
            this.fillColor = fillColor;
            this.parent = parent;
        }

        public AnimationFrame(AnimationFrame frame)
        {
            this.duration = frame.duration;
            this.fillColor = frame.fillColor;
            this.primitives = [.. frame.primitives.Select(item => item.Clone())];
            this.parent = frame.Parent;
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
            Primitive? primitive = (Primitive?)Activator.CreateInstance(primitiveType, new object[]{this});
            if (primitive is not null)
            {
                primitive.Color = color;
                primitives.Add(primitive);
            }
            return primitive;
        }

        public int RemovePrimitive(Primitive primitive)
        {
            int index = primitives.IndexOf(primitive);
            primitives.Remove(primitive);
            return index;
        }

        public bool MovePrimitiveUp(Primitive primitive)
        {
            return false;
        }

        public bool MovePrimitiveDown(Primitive primitive)
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
                Primitive? newPrimitive = null;
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
                if (newPrimitive is not null)
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
            return AnimationFrameFB.CreateAnimationFrameFB(builder, (Int16)Center.X, (Int16)Center.Y, duration, fillColor, primitives_TypeOffset, primitivesOffset);
        }

        public void DeserializeFB(AnimationFrameFB animationFrameFB)
        {
            for (int index = 0; index < animationFrameFB.PrimitivesLength; index++)
            {
                PrimitiveFB primitiveFB = animationFrameFB.PrimitivesType(index);
                switch (primitiveFB)
                {
                    case PrimitiveFB.LineFB:
                        LineFB? nullableLineFB = animationFrameFB.Primitives<LineFB>(index);
                        if (nullableLineFB is not null)
                        {
                            LineFB lineFB = nullableLineFB.Value;
                            Line? line = (Line?)AddPrimitive(typeof(Line), lineFB.Color);
                            line?.DeserializeFB(lineFB);
                        }
                        break;
                    case PrimitiveFB.TriangleFB:
                        TriangleFB? nullableTriangleFB = animationFrameFB.Primitives<TriangleFB>(index);
                        if (nullableTriangleFB is not null)
                        {
                            TriangleFB triangleFB = nullableTriangleFB.Value;
                            Triangle? triangle = (Triangle?)AddPrimitive(typeof(Triangle), triangleFB.Color);
                            triangle?.DeserializeFB(triangleFB);
                        }
                        break;
                    case PrimitiveFB.RoundRectFB:
                        RoundRectFB? nullableRoundRectFB = animationFrameFB.Primitives<RoundRectFB>(index);
                        if (nullableRoundRectFB is not null)
                        {
                            RoundRectFB roundRectFB = nullableRoundRectFB.Value;
                            RoundRect? roundRect = (RoundRect?)AddPrimitive(typeof(RoundRect), roundRectFB.Color);
                            roundRect?.DeserializeFB(roundRectFB);
                        }
                        break;
                    case PrimitiveFB.RotatedRectFB:
                        RotatedRectFB? nullableRotatedRectFB = animationFrameFB.Primitives<RotatedRectFB>(index);
                        if (nullableRotatedRectFB is not null)
                        {
                            RotatedRectFB rotatedRectFB = nullableRotatedRectFB.Value;
                            RotatedRect? rotatedRect = (RotatedRect?)AddPrimitive(typeof(RotatedRect), rotatedRectFB.Color);
                            rotatedRect?.DeserializeFB(rotatedRect);
                        }
                        break;
                    case PrimitiveFB.CircleFB:
                        CircleFB? nullableCircleFB = animationFrameFB.Primitives<CircleFB>(index);
                        if (nullableCircleFB is not null)
                        {
                            CircleFB circleFB = nullableCircleFB.Value;
                            Circle? circle = (Circle?)AddPrimitive(typeof(Circle), circleFB.Color);
                            circle?.DeserializeFB(circleFB);
                        }
                        break;
                }   
            }
        }

    }
}
