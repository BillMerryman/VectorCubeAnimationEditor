using System.Buffers.Binary;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;

    internal static class Utility
    {

        public static byte[] getCommandBytes(UInt16 command)
        {
            byte[] commandBytes = new byte[2];
            BinaryPrimitives.WriteUInt16LittleEndian(commandBytes.AsSpan().Slice(0), command);
            return commandBytes;
        }

        public static Color GetColorFromUIint16(UInt16 _fillColor)
        {
            int blue = (_fillColor >> 11) & 0x1F;
            int green = (_fillColor >> 5) & 0x3F;
            int red = _fillColor & 0x1F;

            red = (red * 255) / 31;
            green = (green * 255) / 63;
            blue = (blue * 255) / 31;

            return Color.FromArgb(255, red, green, blue);
        }

        public static String GetRGBStringFromUIint16(UInt16 _fillColor)
        {
            Color color = GetColorFromUIint16(_fillColor);

            return color.R.ToString("X2") +
                    color.G.ToString("X2") +
                    color.B.ToString("X2");
        }

        public static bool GetUInt16FromRGBString(String strRGB, out UInt16 uint16)
        {
            try
            {
                int red = int.Parse(strRGB.Substring(0, 2), NumberStyles.HexNumber);
                int green = int.Parse(strRGB.Substring(2, 2), NumberStyles.HexNumber);
                int blue = int.Parse(strRGB.Substring(4, 2), NumberStyles.HexNumber);

                red = (red * 31) / 255;
                green = (green * 63) / 255;
                blue = (blue * 31) / 255;

                uint16 = (UInt16)((blue << 11) | (green << 5) | red);
                return true;
            } 
            catch
            {
                uint16 = 0;
                return false;
            }
        }

        public static bool GetByteFromString(String str, out Byte _byte)
        {
            try
            {
                _byte = Byte.Parse(str);
                return true;
            }
            catch (FormatException)
            {
                _byte = 0;
                return false;
            }
        }

        public static bool GetInt16FromString(String str, out Int16 int16)
        {
            try
            {
                int16 = Int16.Parse(str);
                return true;
            }
            catch (FormatException)
            {
                int16 = 0;
                return false;
            }
        }

        public static bool GetUInt32FromString(String str, out UInt32 uint32)
        {
            try
            {
                uint32 = UInt32.Parse(str);
                return true;
            }
            catch (FormatException)
            {
                uint32 = 0;
                return false;
            }
        }

        public static void ConvertToRGB565(Bitmap image, UInt16[,] buffer)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    ushort rgb565Value = (ushort)(((pixelColor.B & 0xF8) << 8) |
                                                    ((pixelColor.G & 0xFC) << 3) |
                                                    (pixelColor.R >> 3));
                    buffer[y, x] = rgb565Value;
                }
            }
        }

        public static Color ColorToInverse(this Color color)
        {
            int invertedR = 255 - color.R;
            int invertedG = 255 - color.G;
            int invertedB = 255 - color.B;

            return Color.FromArgb(color.A, invertedR, invertedG, invertedB);
        }

        public static bool IsPointNearLine(Point endpoint1, Point endpoint2, Point point, double threshold)
        {
            return DistanceFromLine(endpoint1, endpoint2, point) <= threshold;
        }

        public static bool IsPointOnRadius(Point point1, Point point2, Int16 radius, int margin)
        {
            int a = (point2.X - point1.X);
            int b = (point2.Y - point1.Y);
            int aSquare = a * a;
            int bSquare = b * b;
            int h = (int)Math.Sqrt(aSquare + bSquare);
            if (h > radius - margin && h < radius + margin) return true;
            return false;
        }

        public static bool ArePointsWithinMargin(Point point1, Point point2, Double margin)
        {
            return Math.Abs(point1.X - point2.X) < margin
                && Math.Abs(point1.Y - point2.Y) < margin;
        }

        public static double DistanceFromLine(Point endpoint1, Point endpoint2, Point point)
        {
            double dx = endpoint2.X - endpoint1.X;
            double dy = endpoint2.Y - endpoint1.Y;

            if (dx == 0 && dy == 0)
            {
                double pdx = point.X - endpoint1.X;
                double pdy = point.Y - endpoint1.Y;
                return Math.Sqrt(pdx * pdx + pdy * pdy);
            }

            double t = ((point.X - endpoint1.X) * dx + (point.Y - endpoint1.Y) * dy) / (dx * dx + dy * dy);

            t = Math.Max(0, Math.Min(1, t));

            double closestX = endpoint1.X + t * dx;
            double closestY = endpoint1.Y + t * dy;

            double distX = point.X - closestX;
            double distY = point.Y - closestY;

            return Math.Sqrt(distX * distX + distY * distY);
        }

        public static Int16 GetAngleFromReferencePoint(Point referencePoint, Point point)
        {
            double pointDeltaX = point.X - referencePoint.X;
            double pointDeltaY = point.Y - referencePoint.Y;

            double angle = 360 - (Math.Atan2(-pointDeltaY, pointDeltaX) + Math.PI) * (180 / Math.PI);

            return (Int16)angle;
        }

        public static Point RotateFromReferencePoint(Point referencePoint, Point point, Int16 angleDeg)
        {
            point.X -= referencePoint.X;
            point.Y -= referencePoint.Y;

            Point[] points = [point];

            Matrix matrix = new Matrix();
            matrix.Rotate(angleDeg);
            matrix.TransformPoints(points);
            return points[0];
        }

    }
}
