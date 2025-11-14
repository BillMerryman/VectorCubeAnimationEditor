using ST7735Point85;
using System.Buffers.Binary;
using System.Drawing.Drawing2D;
using System.IO;

namespace VectorCubeAnimationEditor
{
    internal class RotatedRect : Primitive
    {
        private Int16 cenX;
        private Int16 cenY;
        private Int16 w;
        private Int16 h;
        private Int16 angleDeg;
        private UInt16 color;

        public Int16 CenX
        {
            get { return cenX; }
            set { cenX = value; }
        }

        public Int16 CenY
        {
            get { return cenY; }
            set { cenY = value; }
        }

        public Int16 W
        {
            get { return w; }
            set { w = value; }
        }

        public Int16 H
        {
            get { return h; }
            set { h = value; }
        }

        public Int16 AngleDeg
        {
            get { return angleDeg; }
            set { angleDeg = (value < 0) ? (Int16)(360 + (value % 360)) : (Int16)(value % 360); }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RotatedRect()
        {
            cenX = AnimationConstants.SCREEN_CENTER_X;
            cenY = AnimationConstants.SCREEN_CENTER_Y;
            w = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            h = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            angleDeg = 0;
            color = 0;
        }

        public RotatedRect(RotatedRect rotatingRectangle)
        {
            cenX = rotatingRectangle.CenX;
            cenY = rotatingRectangle.CenY;
            w = rotatingRectangle.W;
            h = rotatingRectangle.H;
            angleDeg = rotatingRectangle.AngleDeg;
            color = rotatingRectangle.Color;
        }

        public override Primitive Clone()
        {
            return new RotatedRect(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;

            // Split rectangle into two triangles (diagonal from top-left to bottom-right)
            Point[] corners = ScreenGetVertices();
            Point[] triangle1 = new Point[] { corners[0], corners[1], corners[2] };
            Point[] triangle2 = new Point[] { corners[1], corners[2], corners[3] };
            e.FillPolygon(brush, triangle1);
            e.FillPolygon(brush, triangle2);
            if (isHighlighted)
            {
                e.DrawEllipse(pen,
                                (float)(ScreenCenX - ScreenHalfDiagonal),
                                (float)(ScreenCenY - ScreenHalfDiagonal),
                                (float)ScreenHalfDiagonal * 2,
                                (float)ScreenHalfDiagonal * 2);
            }
        }

        public override void Move(Point offset)
        {
            cenX += (Int16)offset.X;
            cenY += (Int16)offset.Y;
        }

        public Point[] GetVertices()
        {
            Point center = new Point(w / 2, h / 2);
            Point topLeft = new Point(center.X - w, center.Y - h);
            Point topRight = new Point(w - center.X, center.Y - h);
            Point bottomRight = new Point(w - center.X, h - center.Y);
            Point bottomLeft = new Point(center.X - w, h - center.Y);

            Point[] vertices = new Point[] { topRight, topLeft, bottomRight, bottomLeft };

            Matrix matrix = new Matrix();
            matrix.Rotate(angleDeg);
            matrix.TransformPoints(vertices);
            return vertices;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._RotatedRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), cenX);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), cenY);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), w);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), h);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), angleDeg);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 4;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            cenX = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            cenY = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            w = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            h = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            angleDeg = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
        }

        #region Screen mapped methods

        public Point ScreenCen
        {
            get { return new Point(ScreenCenX, ScreenCenY); }
        }

        public Int16 ScreenCenX
        {
            get { return (Int16)(cenX * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenCenY
        {
            get { return (Int16)(cenY * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenW
        {
            get { return (Int16)(w * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenH
        {
            get { return (Int16)(h * AnimationConstants._ScaleFactor); }
        }

        public Point ScreenRectangleCenter
        {
            get { return new Point((w / 2) * AnimationConstants._ScaleFactor, (h / 2) * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenHalfDiagonal
        {
            get { return (Int16)(Math.Sqrt(Math.Pow(ScreenRectangleCenter.X + 1, 2) + Math.Pow(ScreenRectangleCenter.Y + 1, 2))); }
        }

        public Point[] ScreenGetVertices()
        {
            Point[] vertices = GetVertices();

            Matrix matrix = new Matrix();
            matrix.Scale(AnimationConstants._ScaleFactor, AnimationConstants._ScaleFactor);
            matrix.TransformPoints(vertices);
            matrix.Reset();
            matrix.Translate(ScreenCenX, ScreenCenY);
            matrix.TransformPoints(vertices);

            return vertices;
        }

        public bool IsPointNearCenter(Point point, Double margin)
        {
            return Utility.ArePointsWithinMargin(point, ScreenCen, AnimationConstants._ScaleFactor * margin);
        }

        public bool IsPointOnRadius(Point point, int margin)
        {
            return Utility.IsPointOnRadius(ScreenCen, point, ScreenHalfDiagonal, margin);
        }

        public int IsPointNearVertex(Point point, Double margin)
        {
            Point[] vertices = ScreenGetVertices();
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, vertices[index], AnimationConstants._ScaleFactor * margin))
                {
                    selectedVertex = index;
                }

            }
            return selectedVertex;
        }

        public Int16 GetAngle(Point point)
        {
            return Utility.GetAngleFromReferencePoint(ScreenCen, point);
        }

        #endregion

    }
}
