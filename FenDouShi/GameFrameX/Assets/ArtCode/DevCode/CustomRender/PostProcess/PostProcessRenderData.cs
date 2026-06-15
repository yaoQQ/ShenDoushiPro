using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YLib.PostProcess
{
    public static class RadialBlurData 
    {
        public static bool active = false;

        public static float RadialBlurHorizontalCenter = 0.5f;
        public static float RadialBlurVerticalCenter = 0.5f;
        public static float RadialBlurWidth = 0.2f;
        public static float RadialBlurStrength = 0f;
        public static int RadialBlurIterTimes = 10;
    }

    public static class BlackWhiteData
    {
        public static bool active = false;

        public static float BlackWhiteThreshold = 0.5f;
        public static float BlackWhiteWidth = 0.001f;
        public static Color BlackWhiteWhiteColor = Color.white;
        public static Color BlackWhiteBlackColor = Color.black;
        public static bool BlackWhiteFlip = false;
    }

    public static class ColorDispersionData
    {
        public static bool active = false;

        public static float ColorDispersionU = 0f;
        public static float ColorDispersionV = 0f;
        public static float ColorDispersionStrength = 0f;
    }
}
