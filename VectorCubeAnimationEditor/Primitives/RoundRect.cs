using ST7735Point85;
using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class RoundRect : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 w;
        private Int16 h;
        private Int16 radius;
        private UInt16 color;

        public Int16 X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        public Int16 Y0
        {
            get { return y0; }
            set { y0 = value; }
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

        public Int16 Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RoundRect()
        {
            x0 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            w = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            h = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            radius = 0;
            color = 0;
        }

        public RoundRect(RoundRect rectangle)
        {
            x0 = rectangle.X0;
            y0 = rectangle.Y0;
            w = rectangle.W;
            h = rectangle.H;
            radius = rectangle.Radius;
            color = rectangle.Color;
        }

        public override Primitive Clone()
        {
            return new RoundRect(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            GraphicsPath path = GetRectanglePath();
            Color drawColor = Utility.GetColorFromUIint16(color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            e.FillPath(brush, path);
            if (isHighlighted) e.DrawPath(pen, path);
        }

        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMoving = false;
        private bool isResizing = false;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            if (IsPointNearBottomRight(MouseLocation, 2))
            {
                isResizing = true;
            }
            else if (IsPointInside(MouseLocation))
            {
                isMoving = true;
            }
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));
            
            bool result = false;
            if (isResizing)
            {
                if (W + unscaledMouseDelta.X > 0) W += (Int16)unscaledMouseDelta.X;
                if (H + unscaledMouseDelta.Y > 0) H += (Int16)unscaledMouseDelta.Y;
                result = true;
            }
            if (isMoving)
            {
                Move(unscaledMouseDelta);
                result = true;
            }
            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;
            return result;
        }

        public override void MouseUp()
        {
            isMoving = false;
            isResizing = false;
        }

        #endregion

        public override void Move(Point offset)
        {
            x0 += (Int16)offset.X;
            y0 += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._RoundRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), w);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), h);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), radius);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 4;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            x0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            w = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            h = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            radius = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
        }

        #region Screen mapped methods

        public Point ScreenCen
        {
            get { return new Point(ScreenX, ScreenY); }
        }

        public Int16 ScreenX
        {
            get { return (Int16)(x0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY
        {
            get { return (Int16)(y0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenW
        {
            get { return (Int16)(w * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenH
        {
            get { return (Int16)(h * AnimationConstants._ScaleFactor); }
        }

        public bool IsPointNearPerimeter(Point point, Double margin)
        {
            UInt16 halfWidth = (UInt16)(w / 2);
            UInt16 halfHeight = (UInt16)(h / 2);
            UInt16 screenHalfWidth = (UInt16)(halfWidth * AnimationConstants._ScaleFactor);
            UInt16 screenHalfHeight = (UInt16)(halfHeight * AnimationConstants._ScaleFactor);
            UInt16 screenMidX = (UInt16)(x0 * AnimationConstants._ScaleFactor + screenHalfWidth);
            UInt16 screenMidY = (UInt16)(y0 * AnimationConstants._ScaleFactor + screenHalfHeight);
            int xInRange = Math.Abs(point.X - screenMidX);
            int yInRange = Math.Abs(point.Y - screenMidY);
            if ((Math.Abs(point.Y - (y0 * AnimationConstants._ScaleFactor))) < margin && xInRange < halfWidth) return true;
            if ((Math.Abs(point.Y - ((y0 + h) * AnimationConstants._ScaleFactor))) < margin && xInRange < halfWidth) return true;
            if ((Math.Abs(point.X - (x0 * AnimationConstants._ScaleFactor))) < margin && yInRange < halfHeight) return true;
            if ((Math.Abs(point.X - ((x0 + w) * AnimationConstants._ScaleFactor))) < margin && yInRange < halfHeight) return true;
            return false;
        }

        public bool IsPointNearBottomRight(Point point, Double margin)
        {
            Point bottomRight = new Point(x0 + w, y0 + h);

            return Math.Abs(point.X - (bottomRight.X * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                    && Math.Abs(point.Y - (bottomRight.Y * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin;
        }

        public bool IsPointInside(Point point)
        {
            GraphicsPath path = GetRectanglePath();
            return path.IsVisible(point);
        }

        public GraphicsPath GetRectanglePath()
        {
            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(ScreenX, ScreenY, ScreenW, ScreenH);
            int _radius = radius * AnimationConstants._ScaleFactor;
            int diameter = 2 * _radius;
            Size size = new Size(diameter, diameter);
            System.Drawing.Rectangle arc = new System.Drawing.Rectangle(bounds.Location, size);
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

        #endregion

    }
}
