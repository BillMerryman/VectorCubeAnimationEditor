using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class Animation
    {

        private UInt16 frameCount = 0;
        private AnimationFrame []frames;

        public UInt16 FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        public Animation()
        {
            frames = new AnimationFrame[AnimationConstants._MaxFrameCount];
            for(int i = 0; i < frames.Length; i++)
            {
                frames[i] = new AnimationFrame();
            }
        }

        public AnimationFrame? GetFrameNumber(int frameNumber)
        {
            if(frameNumber < 0 ||  frameNumber >= frames.Length) return null;
            return frames[frameNumber - 1];
        }

        public int GetNumberOfFrame(AnimationFrame? frame)
        {
            if(frame == null) return -1;
            for(int index = 0; index < frames.Length; index++)
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
            if(frameCount >= AnimationConstants._MaxFrameCount) return null;
            AnimationFrame frame = new AnimationFrame();
            frames[frameCount] = frame;
            frame.FillColor = fillColor;
            frame.Duration = duration;
            frameCount++;
            return frame;
        }

        public AnimationFrame? DuplicateFrame(AnimationFrame frame)
        {
            if(frame == null) return null;
            if (frameCount >= AnimationConstants._MaxFrameCount) return null;
            int frameNumber = GetNumberOfFrame(frame);
            int newFrameNumber = frameNumber + 1;
            int newFrameIndex = newFrameNumber - 1;
            for (int index = AnimationConstants._MaxFrameCount - 1; index > newFrameIndex; index--)
            {
                frames[index - 1] = frames[index];
            }
            AnimationFrame newFrame = new AnimationFrame(frame);
            frames[newFrameIndex] = newFrame;
            frameCount++;
            return newFrame;
        }

        public int RemoveFrame(AnimationFrame? frame)
        {
            if(frame == null) return -1;
            for(int index = 0; index < frames.Length; index++)
            {
                if (object.ReferenceEquals(frame, frames[index]))
                {
                    for(int innerIndex = index;  innerIndex < frames.Length - 1; innerIndex++)
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

        public byte[] serialize()
        {
            byte[] animationBytes = new byte[2402];
            int bytePosition = 0;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), frameCount);
            bytePosition += 2;
            for (int index = 0; index < frames.Length; index++)
            {
                frames[index].serialize(ref bytePosition, animationBytes);
            }
            return animationBytes;
        }

        public void deserialize(byte[] animationBytes)
        {
            if (animationBytes.Length != 2402) return;
            int bytePosition = 0;
            frameCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            for (int index = 0; index < frames.Length; index++)
            {
                frames[index].deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}
