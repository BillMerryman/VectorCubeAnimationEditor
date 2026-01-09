using System.Buffers.Binary;

namespace ST7735Point85
{
    internal class Animation
    {

        private UInt16 frameCount;
        private AnimationFrame[] frames;

        public UInt16 FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        public Animation()
        {
            frameCount = 0;
            frames = new AnimationFrame[AnimationConstants._MaxFrameCount];
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = new AnimationFrame();
            }
        }

        public AnimationFrame? GetFrameNumber(int frameNumber)
        {
            if (frameNumber < 1 || frameNumber > frameCount) return null;
            return frames[frameNumber - 1];
        }

        public int GetNumberOfFrame(AnimationFrame? frame)
        {
            if (frame == null) return -1;
            for (int index = 0; index < frames.Length; index++)
            {
                if (object.ReferenceEquals(frame, frames[index]))
                {
                    return index + 1;
                }
            }
            return -1;
        }

        public AnimationFrame? AddFrame(UInt16 fillColor, UInt32 duration)
        {
            if (frameCount >= AnimationConstants._MaxFrameCount) return null;
            AnimationFrame frame = new();
            frames[frameCount] = frame;
            frame.FillColor = fillColor;
            frame.Duration = duration;
            frameCount++;
            return frame;
        }

        public AnimationFrame? DuplicateFrame(AnimationFrame frame)
        {
            if (frame == null) return null;
            if (frameCount >= AnimationConstants._MaxFrameCount) return null;
            int frameNumber = GetNumberOfFrame(frame);
            int newFrameNumber = frameNumber + 1;
            int newFrameIndex = newFrameNumber - 1;
            for (int index = AnimationConstants._MaxFrameCount - 1; index > newFrameIndex; index--)
            {
                frames[index] = frames[index - 1];
            }
            AnimationFrame newFrame = new(frame);
            frames[newFrameIndex] = newFrame;
            frameCount++;
            return newFrame;
        }

        public int RemoveFrame(AnimationFrame? frame)
        {
            if (frame == null) return -1;
            for (int index = 0; index < frames.Length; index++)
            {
                if (object.ReferenceEquals(frame, frames[index]))
                {
                    for (int innerIndex = index; innerIndex < frames.Length - 1; innerIndex++)
                    {
                        frames[innerIndex] = frames[innerIndex + 1];
                    }
                    frames[^1] = new AnimationFrame();
                    frameCount--;
                    return index + 1;
                }
            }
            return -1;
        }

        public bool MoveFrameUp(AnimationFrame frame)
        {
            int frameNumber = GetNumberOfFrame(frame);
            if (frameNumber < 1) return false;
            if (frameNumber == FrameCount) return false;
            int frameIndex = frameNumber - 1;
            (frames[frameIndex + 1], frames[frameIndex]) = (frames[frameIndex], frames[frameIndex + 1]);
            return true;
        }

        public bool MoveFrameDown(AnimationFrame frame)
        {
            int frameNumber = GetNumberOfFrame(frame);
            if (frameNumber < 1) return false;
            if (frameNumber == 1) return false;
            int frameIndex = frameNumber - 1;
            (frames[frameIndex - 1], frames[frameIndex]) = (frames[frameIndex], frames[frameIndex - 1]);
            return true;
        }

        public byte[] Serialize()
        {
            byte[] animationBytes = new byte[2402];
            int bytePosition = 0;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], frameCount);
            bytePosition += 2;
            for (int index = 0; index < frames.Length; index++)
            {
                frames[index].Serialize(ref bytePosition, animationBytes);
            }
            return animationBytes;
        }

        public void Deserialize(byte[] animationBytes)
        {
            if (animationBytes.Length != 2402) return;
            int bytePosition = 0;
            frameCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            for (int index = 0; index < frames.Length; index++)
            {
                frames[index].Deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}
