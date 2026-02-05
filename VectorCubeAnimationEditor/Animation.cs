using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Buffers.Binary;
using System.Text.Json.Serialization;

namespace VectorCubeAnimationEditor
{
    internal class Animation
    {
        private bool interruptCurrent = false;
        private Point animationCenter = new Point(0, 0);
        public List<AnimationFrame> frames { get; set; }

        public bool InterruptCurrent
        {
            set
            {
                interruptCurrent = value;
            }
            get
            {
                return interruptCurrent;
            }

        }

        public Point AnimationCenter
        {
            get { return animationCenter; }
            set { animationCenter = value; }
        }

        [JsonIgnore]
        public int FrameCount
        {
            get { return frames.Count; }
        }

        public Animation()
        {
            frames = [];
        }

        public AnimationFrame? GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex > frames.Count - 1) return null;
            return frames[frameIndex];
        }

        public int IndexOf(AnimationFrame? frame)
        {
            if (frame == null) return -1;
            return frames.IndexOf(frame);
        }

        public AnimationFrame? AddFrame(UInt16 fillColor, UInt32 duration)
        {
            if (FrameCount >= AnimationConstants._MaxFrameCount) return null;
            AnimationFrame frame = new()
            {
                FillColor = fillColor,
                Duration = duration
            };
            frames.Add(frame);
            return frame;
        }

        public AnimationFrame? DuplicateFrame(AnimationFrame frame)
        {
            if (frame == null) return null;
            if (FrameCount >= AnimationConstants._MaxFrameCount) return null;
            int frameIndex = IndexOf(frame);
            if (frameIndex < 0) return null;
            AnimationFrame newFrame = new(frame);
            frames.Insert(frameIndex + 1, newFrame);
            return newFrame;
        }

        public int RemoveFrame(AnimationFrame? frame)
        {
            if (frame == null) return -1;
            int frameIndex = IndexOf(frame);
            frames.Remove(frame);
            if (frameIndex > FrameCount - 1) return frameIndex - 1;
            return frameIndex;
        }

        public bool MoveFrameUp(AnimationFrame frame)
        {
            int frameIndex = IndexOf(frame);
            if (frameIndex < 0) return false;
            if (frameIndex == FrameCount - 1) return false;
            (frames[frameIndex + 1], frames[frameIndex]) = (frames[frameIndex], frames[frameIndex + 1]);
            return true;
        }

        public bool MoveFrameDown(AnimationFrame frame)
        {
            int frameIndex = IndexOf(frame);
            if (frameIndex < 1) return false;
            (frames[frameIndex - 1], frames[frameIndex]) = (frames[frameIndex], frames[frameIndex - 1]);
            return true;
        }

        public byte[] SerializeBinary()
        {
            byte[] animationBytes = new byte[2402];
            int bytePosition = 0;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], (ushort)frames.Count);
            bytePosition += 2;
            for (int index = 0; index < frames.Count; index++)
            {
                frames[index].SerializeBinary(ref bytePosition, animationBytes);
            }
            for (int index = frames.Count; index < AnimationConstants._MaxFrameCount; index++)
            {
                for (int primitiveIndex = 0; primitiveIndex < AnimationConstants._MaxPrimitiveCount; primitiveIndex++)
                {
                    bytePosition += AnimationConstants._PrimitiveTypeWidth;
                    bytePosition += AnimationConstants._LargestPrimitiveByteCount;
                }
            }
            return animationBytes;
        }

        public void DeserializeBinary(byte[] animationBytes)
        {
            if (animationBytes.Length != 2402) return;
            int bytePosition = 0;
            UInt16 frameCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            for (int index = 0; index < frameCount; index++)
            {
                AddFrame(0, 0);
                frames[index].DeserializeBinary(ref bytePosition, animationBytes);
            }
        }

        public byte[] SerializeFB()
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1024);
            Offset<AnimationFrameFB>[] animationFrameOffsets = new Offset<AnimationFrameFB>[FrameCount];
            for (int index = 0; index < FrameCount; index++)
            {
                animationFrameOffsets[index] = frames[index].SerializeFB(builder);
            }
            VectorOffset animationFramesOffset = AnimationFB.CreateAnimationFramesVector(builder, animationFrameOffsets);
            Offset<AnimationFB> animationFB = AnimationFB.CreateAnimationFB(builder, InterruptCurrent, (Int16)AnimationCenter.X, (Int16)AnimationCenter.Y, animationFramesOffset);
            AnimationFB.FinishAnimationFBBuffer(builder, animationFB);
            return builder.DataBuffer.ToSizedArray();
        }

        public void DeserializeFB(byte[] buffer)
        {
            ByteBuffer bb = new ByteBuffer(buffer);
            AnimationFB animationFB = AnimationFB.GetRootAsAnimationFB(bb);
            interruptCurrent = animationFB.InterruptCurrent;
            animationCenter = new Point(animationFB.AnimationCenterX, animationFB.AnimationCenterY);
            for (int frame = 0; frame < animationFB.AnimationFramesLength; frame++)
            {
                AnimationFrameFB animationFrameFB = animationFB.AnimationFrames(frame).Value;
                AnimationFrame af = AddFrame(animationFrameFB.FillColor, animationFrameFB.Duration);
                af.DeserializeFB(animationFrameFB);
            }

        }

    }
}
