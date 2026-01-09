
namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;
    using TransmissionType = UInt16;
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
        public const PrimitiveType _Line = 10;
        public const PrimitiveType _Triangle = 25;
        public const PrimitiveType _RoundRect = 35;
        public const PrimitiveType _RotatedRect = 45;
        public const PrimitiveType _Circle = 55;
        public const PrimitiveType _QuarterCircle = 65;

        public static UInt16 _CommandWidth = 2;
        public static UInt16 _PrimitiveTypeWidth = 2;
        public static UInt16 _MaxFrameCount = 12;
        public static UInt16 _MaxPrimitiveCount = 12;
        public static UInt16 _LargestPrimitiveByteCount = 14;
        public static int _ScaleFactor = 3;
    }
}
