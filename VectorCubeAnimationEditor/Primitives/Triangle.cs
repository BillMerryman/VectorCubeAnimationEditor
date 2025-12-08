using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Triangle : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 x1;
        private Int16 y1;
        private Int16 x2;
        private Int16 y2;
        private UInt16 color;

        public Int16 X0 
        { 
            get { return x0; } 
            set {  x0 = value; } 
        }

        public Int16 Y0
        {
            get { return y0; }
            set { y0 = value; }
        }

        public Int16 X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        public Int16 Y1
        {
            get { return y1; }
            set { y1 = value; }
        }

        public Int16 X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        public Int16 Y2
        {
            get { return y2; }
            set { y2 = value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Triangle()
        {
            X0 = AnimationConstants.SCREEN_CENTER_X;
            Y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            X1 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            Y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            X2 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            Y2 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            Color = 0;
        }

        public Triangle(Triangle triangle)
        {
            X0 = triangle.X0;
            Y0 = triangle.Y0;
            X1 = triangle.X1;
            Y1 = triangle.Y1;
            X2 = triangle.X2;
            Y2 = triangle.Y2;
            Color = triangle.Color;
        }

        public override Primitive Clone()
        {
            return new Triangle(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {

            Point[] trianglePoints = GetScreenVertices();

            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new(drawColor.ColorToInverse())
            {
                DashStyle = DashStyle.Dash
            };
            e.FillPolygon(brush, trianglePoints);
            if (isHighlighted) e.DrawPolygon(pen, trianglePoints);
        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
            X1 += (Int16)offset.X;
            Y1 += (Int16)offset.Y;
            X2 += (Int16)offset.X;
            Y2 += (Int16)offset.Y;
        }

        public void MoveVertex(int vertexNum, Point offset)
        {
            if (vertexNum < 0 || vertexNum > 2) return;
            if (vertexNum == 0)
            {
                X0 += (Int16)offset.X;
                Y0 += (Int16)offset.Y;
            }
            if (vertexNum == 1)
            {
                X1 += (Int16)offset.X;
                Y1 += (Int16)offset.Y;
            }
            if (vertexNum == 2)
            {
                X2 += (Int16)offset.X;
                Y2 += (Int16)offset.Y;
            }
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AnimationConstants._Triangle);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X2);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y2);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Color);
            bytePosition += 2;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            X1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Y1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            X2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Y2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
        }

        #region Screen mapped methods

        public Point ScreenCen
        {
            get 
            { 
                Point centroid = GetCentroid();
                centroid.X *= AnimationConstants._ScaleFactor;
                centroid.Y *= AnimationConstants._ScaleFactor;
                return centroid;
            }
        }

        public Int16 ScreenX0
        {
            get { return (Int16)(X0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY0
        {
            get { return (Int16)(Y0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenX1
        {
            get { return (Int16)(X1 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY1
        {
            get { return (Int16)(Y1 * AnimationConstants._ScaleFactor); }
        }
        public Int16 ScreenX2
        {
            get { return (Int16)(X2 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY2
        {
            get { return (Int16)(Y2 * AnimationConstants._ScaleFactor); }
        }

        #region Mouse handling

        private Point mouseLocation = new(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private int selectedVertex = -1;

        public override void MouseDown(Point point)
        {
            mouseLocation = point;
            isMouseUp = false;

            if (IsPointNearCenter(point)) isMoving = true;
            else selectedVertex = GetSelectedVertex(mouseLocation);
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new(point.X - mouseLocation.X, point.Y - mouseLocation.Y);
            Point unscaledMouseDelta = new((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            if (isMouseUp)
            {
                if (IsPointNearCenter(point)) pctbxCanvas.Cursor = Cursors.SizeAll;
                else
                {
                    if (GetSelectedVertex(point) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                    else pctbxCanvas.Cursor = Cursors.Arrow;
                }
                return false;
            }
            else
            {
                if (isMoving) Move(unscaledMouseDelta);
                else
                {
                    if (selectedVertex > -1) MoveVertex(selectedVertex, unscaledMouseDelta);
                }
            }

            mouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            mouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;

            return true;
        }

        public override void MouseUp()
        {
            isMouseUp = true;
            isMoving = false;
            selectedVertex = -1;
        }

        #endregion

        public Point[] GetVertices()
        {
            Point vertex1 = new(X0, Y0);
            Point vertex2 = new(X1, Y1);
            Point vertex3 = new(X2, Y2);

            Point[] vertices = [vertex1, vertex2, vertex3];

            return vertices;
        }

        public Point[] GetScreenVertices()
        {
            Point vertex0 = new(ScreenX0, ScreenY0);
            Point vertex1 = new(ScreenX1, ScreenY1);
            Point vertex2 = new(ScreenX2, ScreenY2);

            Point[] vertices = [vertex0, vertex1, vertex2];

            return vertices;
        }

        public Point GetCentroid()
        {
            int centerX = (X0 + X1 + X2) / 3;
            int centerY = (Y0 + Y1 + Y2) / 3;
            return new Point(centerX, centerY);
        }

        public int GetSelectedVertex(Point point)
        {
            Point[] vertices = GetScreenVertices();
            int margin = 4;
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, vertices[index], margin))
                {
                    selectedVertex = index;
                }

            }
            return selectedVertex;
        }

        public bool IsPointNearCenter(Point point)
        {
            int margin = 4;
            return Utility.ArePointsWithinMargin(ScreenCen, point, margin);
        }

        #endregion

    }
}
