using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;

    internal class Utility
    {

        public static String GetDrawColorStringFromPrimitive(PrimitiveType primitiveType, Primitive primitive)
        {
            switch (primitiveType)
            {
                case AnimationConstants._Circle:
                    return Utility.GetRGBStringFromUIint16(primitive.Circle.Color);
                case AnimationConstants._QuarterCircle:
                    return Utility.GetRGBStringFromUIint16(primitive.QuarterCircle.Color);
                case AnimationConstants._Triangle:
                    return Utility.GetRGBStringFromUIint16(primitive.Triangle.Color);
                case AnimationConstants._RoundRect:
                    return Utility.GetRGBStringFromUIint16(primitive.RoundRect.Color);
                case AnimationConstants._Line:
                    return Utility.GetRGBStringFromUIint16(primitive.Line.Color);
            }
            return String.Empty;
        }

        public static String GetRGBStringFromUIint16(UInt16 _fillColor)
        {
            // Extract components from RGB565
            int red = (_fillColor >> 11) & 0x1F;
            int green = (_fillColor >> 5) & 0x3F;
            int blue = _fillColor & 0x1F;

            // Scale components to 8 bits
            red = (red * 255) / 31;
            green = (green * 255) / 63;
            blue = (blue * 255) / 31;

            return red.ToString("X2") +
                    green.ToString("X2") +
                    blue.ToString("X2");
        }

        public static bool GetUInt16FromRGBString(String strRGB, out UInt16 uint16)
        {
            try
            {
            int red = int.Parse(strRGB.Substring(0, 2), NumberStyles.HexNumber);
            int green = int.Parse(strRGB.Substring(2, 2), NumberStyles.HexNumber);
            int blue = int.Parse(strRGB.Substring(4, 2), NumberStyles.HexNumber);

            // Scale RGB components from 8 bits to the respective RGB565 ranges
            red = (red * 31) / 255;
            green = (green * 63) / 255;
            blue = (blue * 31) / 255;

            // Pack components into a 16-bit RGB565 value
            uint16 = (UInt16)((red << 11) | (green << 5) | blue);
            return true;
            } 
            catch
            {
                uint16 = 0;
                return false;
            }
        }

        public static Color GetColorFromUIint16(UInt16 _fillColor)
        {
            // Extract components from RGB565
            int red = (_fillColor >> 11) & 0x1F;
            int green = (_fillColor >> 5) & 0x3F;
            int blue = _fillColor & 0x1F;

            // Scale components to 8 bits
            red = (red * 255) / 31;
            green = (green * 255) / 63;
            blue = (blue * 255) / 31;

            return Color.FromArgb(255, red, green, blue);
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

        public static void DrawPrimitive(Graphics e, Primitive primitive)
        {
            switch (primitive.Type)
            {
                case AnimationConstants._Circle:
                    Utility.DrawCircle(e, primitive.Circle);
                    break;
                case AnimationConstants._QuarterCircle:
                    Utility.DrawQuarterCircle(e, primitive.QuarterCircle);
                    break;
                case AnimationConstants._Triangle:
                    Utility.DrawTriangle(e, primitive.Triangle);
                    break;
                case AnimationConstants._RoundRect:
                    Utility.DrawRoundRect(e, primitive.RoundRect);
                    break;
                case AnimationConstants._Line:
                    Utility.DrawLine(e, primitive.Line);
                    break;
            }
        }

        public static void DrawCircle(Graphics e, Circle circle)
        {
            Brush brush = new SolidBrush(GetColorFromUIint16(circle.Color));
            int ScreenX = circle.X0 * AnimationConstants._ScaleFactor;
            int ScreenY = circle.Y0 * AnimationConstants._ScaleFactor;
            int ScreenR = circle.R * AnimationConstants._ScaleFactor;
            int boundingX = ScreenX - ScreenR;
            int boundingY = ScreenY - ScreenR;
            e.FillEllipse(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR);
        }

        public static void DrawQuarterCircle(Graphics e,  QuarterCircle quarterCircle)
        {
            Brush brush = new SolidBrush(GetColorFromUIint16(quarterCircle.Color));
            int ScreenX = quarterCircle.X0 * AnimationConstants._ScaleFactor;
            int ScreenY = quarterCircle.Y0 * AnimationConstants._ScaleFactor;
            int ScreenR = quarterCircle.R * AnimationConstants._ScaleFactor;
            int boundingX = ScreenX - ScreenR;
            int boundingY = ScreenY - ScreenR;
            if ((quarterCircle.Quadrants & 1) == 1)
            {
                e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 270, 90);
            }
            if ((quarterCircle.Quadrants & 2) == 2)
            {
                e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 0, 90);
            }
            if ((quarterCircle.Quadrants & 4) == 4)
            {
                e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 90, 90);
            }
            if ((quarterCircle.Quadrants & 8) == 8)
            {
                e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 180, 90);
            }

        }

        public static void DrawTriangle(Graphics e, Triangle triangle)
        {

            Point[] trianglePoints = {
                new Point(triangle.X0 * AnimationConstants._ScaleFactor, triangle.Y0 * AnimationConstants._ScaleFactor),
                new Point(triangle.X1 * AnimationConstants._ScaleFactor, triangle.Y1 * AnimationConstants._ScaleFactor),
                new Point(triangle.X2 * AnimationConstants._ScaleFactor, triangle.Y2 * AnimationConstants._ScaleFactor)
            };

            Brush brush = new SolidBrush(GetColorFromUIint16(triangle.Color));
            e.FillPolygon(brush, trianglePoints);
        }

        public static void DrawRoundRect(Graphics e, RoundRect roundRect)
        {
            GraphicsPath path = GetRoundRectPath(roundRect);
            Brush brush = new SolidBrush(GetColorFromUIint16(roundRect.Color));
            e.FillPath(brush, path);
        }

        public static void DrawLine(Graphics e, Line line)
        {
            Pen pen = new Pen(GetColorFromUIint16(line.Color), 3);
            e.DrawLine(pen, line.X0 * AnimationConstants._ScaleFactor, line.Y0 * AnimationConstants._ScaleFactor, line.X1 * AnimationConstants._ScaleFactor, line.Y1 * AnimationConstants._ScaleFactor);
            pen.Dispose();
        }

        public static GraphicsPath GetRoundRectPath(RoundRect roundRect)
        {
            Rectangle bounds = new Rectangle(roundRect.X0 * AnimationConstants._ScaleFactor,
                                            roundRect.Y0 * AnimationConstants._ScaleFactor,
                                            roundRect.W * AnimationConstants._ScaleFactor,
                                            roundRect.H * AnimationConstants._ScaleFactor);
            int radius = roundRect.Radius * AnimationConstants._ScaleFactor;
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
            }
            else
            {
                path.AddArc(arc, 180, 90);
                arc.X = bounds.Right - diameter;
                path.AddArc(arc, 270, 90);
                arc.Y = bounds.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                arc.X = bounds.Left;
                path.AddArc(arc, 90, 90);
                path.CloseFigure();
            }
            return path;
        }

        public static bool IsPointInsideRoundRect(Point point, RoundRect roundRect)
        {
            GraphicsPath path = GetRoundRectPath(roundRect);
            return path.IsVisible(point);
        }

        public static bool IsScreenPointOnRadius(Point screenPoint, Point primitivePoint, Int16 radius) 
        {
            int c = radius * AnimationConstants._ScaleFactor;
            int a = ((primitivePoint.X * AnimationConstants._ScaleFactor) - screenPoint.X);
            int b = ((primitivePoint.Y * AnimationConstants._ScaleFactor) - screenPoint.Y);
            int cSquare = c * c;
            int aSquare = a * a;
            int bSquare = b * b;
            if ((aSquare + bSquare > cSquare * .9) && (aSquare + bSquare < cSquare * 1.1))return true;
            return false;
        }

        public static bool IsScreenPointNearPoint(Point mouseLocation, Point vertex)
        {
            return Math.Abs(mouseLocation.X - (vertex.X * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2
                && Math.Abs(mouseLocation.Y - (vertex.Y * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2;
        }

        public static Point GetCentroid(Triangle triangle)
        {
            int centerX = (triangle.X0 + triangle.X1 + triangle.X2) / 3;
            int centerY = (triangle.Y0 + triangle.Y1 + triangle.Y2) / 3;
            return new Point(centerX, centerY);
        }

    }
}
