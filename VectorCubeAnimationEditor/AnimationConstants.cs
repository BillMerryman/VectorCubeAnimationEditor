using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    using TransmissionType = UInt16;
    using PrimitiveType = UInt16;
    internal class AnimationConstants
    {
        public const TransmissionType _Animation = 1;
        public const TransmissionType _Image = 2;

        public const Int16 SCREEN_WIDTH = 128;
        public const Int16 SCREEN_HEIGHT = 128;
        public const Int16 SCREEN_CENTER_X = SCREEN_WIDTH / 2;
        public const Int16 SCREEN_CENTER_Y = SCREEN_HEIGHT / 2;

        public const Int16 DEFAULT_PRIMITIVE_SIZE = 20;
        public const Int16 DEFAULT_PRIMITIVE_RADIUS = DEFAULT_PRIMITIVE_SIZE / 2;
        public const Int16 DEFAULT_PRIMITIVE_LEFT = SCREEN_CENTER_X - DEFAULT_PRIMITIVE_RADIUS;
        public const Int16 DEFAULT_PRIMITIVE_TOP = SCREEN_CENTER_Y - DEFAULT_PRIMITIVE_RADIUS;
        public const Int16 DEFAULT_PRIMITIVE_RIGHT = SCREEN_CENTER_X + DEFAULT_PRIMITIVE_RADIUS;
        public const Int16 DEFAULT_PRIMITIVE_BOTTOM = SCREEN_CENTER_Y + DEFAULT_PRIMITIVE_RADIUS;

        public const PrimitiveType _Error = 0;
        public const PrimitiveType _Line = 5;
        public const PrimitiveType _Rect = 6;
        public const PrimitiveType _RoundRect = 4;
        public const PrimitiveType _Triangle = 3;
        public const PrimitiveType _Circle = 1;
        public const PrimitiveType _QuarterCircle = 2;


        public static UInt16 _MaxFrameCount = 12;
        public static UInt16 _MaxPrimitiveCount = 12;
        public static int _ScaleFactor = 3;
    }
}
