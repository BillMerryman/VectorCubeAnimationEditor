﻿using System;
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

        public AnimationFrame? AddFrame(String strFillColor, String strDuration)
        {
            if(frameCount >= AnimationConstants._MaxFrameCount) return null;
            UInt16 fillColor;
            UInt32 duration;
            if (!Utility.GetUInt32FromString(strDuration, out duration)) return null;
            if (!Utility.GetUInt16FromRGBString(strFillColor, out fillColor)) return null;
            AnimationFrame frame = new AnimationFrame();
            frames[frameCount] = frame;
            frame.FillColor = fillColor;
            frame.Duration = duration;
            frameCount++;
            return frame;
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
            byte[] animationBytes = new byte[2404];
            int bytePosition = 0;
            UInt16 command = 1;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), command);
            bytePosition += 2;
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
            if (animationBytes.Length != 2404) return;
            int bytePosition = 0;
            UInt16 command = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            frameCount = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            for (int index = 0; index < frames.Length; index++)
            {
                frames[index].deserialize(ref bytePosition, animationBytes);
            }
        }

    }
}