﻿using System;
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
        public const TransmissionType _Gif = 2;

        public const PrimitiveType _Error = 0;
        public const PrimitiveType _Circle = 1;
        public const PrimitiveType _QuarterCircle = 2;
        public const PrimitiveType _Triangle = 3;
        public const PrimitiveType _RoundRect = 4;

        public static UInt16 _MaxFrameCount = 12;
        public static UInt16 _MaxPrimitiveCount = 12;
        public static int _ScaleFactor = 3;
    }
}
