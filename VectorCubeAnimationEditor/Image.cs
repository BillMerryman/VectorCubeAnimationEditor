using System.Buffers.Binary;

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
            BinaryPrimitives.WriteUInt16LittleEndian(gifBytes.AsSpan()[bytePosition..], command);
            bytePosition += 2;
            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    BinaryPrimitives.WriteUInt16LittleEndian(gifBytes.AsSpan()[bytePosition..], displayBuffer[y,x]);
                    bytePosition += 2;
                }
            }
            return gifBytes;
        }

    }
}
