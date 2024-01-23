﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace VectorCubeAnimationEditor
{
    internal class Image
    {
        UInt16[,] displayBuffer;

        public UInt16[,] DisplayBuffer
        {
            get { return displayBuffer; }
            set { displayBuffer = value; }
        }

        public Image()
        {
            displayBuffer = new UInt16[128, 128];
        }

        public byte[] serialize()
        {
            byte[] gifBytes = new byte[2 + (128 * 128 * 2)];
            int bytePosition = 0;
            UInt16 command = 2;
            BinaryPrimitives.WriteUInt16LittleEndian(gifBytes.AsSpan().Slice(bytePosition), command);
            bytePosition += 2;
            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    BinaryPrimitives.WriteUInt16LittleEndian(gifBytes.AsSpan().Slice(bytePosition), displayBuffer[y,x]);
                    bytePosition += 2;
                }
            }
            return gifBytes;
        }

    }
}
