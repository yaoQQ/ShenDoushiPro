using System.Collections.Generic;

public enum ItemQuality
{
    White = 1,
    Green = 2,
    Blue = 3,
    Purple = 4,
    Orange = 5,
    Red = 6
}

public static class ItemDefine 
{
    public const string commonPackage = "common";

    public const string IconPackage = "icon";


    private static readonly Dictionary<int, string> ItemQualityIcons = new Dictionary<int, string>
    {
        { (int)ItemQuality.White,  "common_frame_touxiang_green" },
        { (int)ItemQuality.Green,   "common_frame_touxiang_green" },
        { (int)ItemQuality.Blue,  "common_frame_touxiang_blue" },
        { (int)ItemQuality.Purple,   "common_frame_touxiang_purple" },
        { (int)ItemQuality.Orange,   "common_frame_touxiang_orange" },
        { (int)ItemQuality.Red,  "common_frame_touxiang_red" }
    };


    public static string GetItemQualityByQuality(int quality)
    {
        if (ItemQualityIcons.TryGetValue(quality, out string iconName))
        {
            return iconName;
        }
        Logger.PrintLog($"Invalid item quality: {quality}, using default icon.");
        return ItemQualityIcons[(int)ItemQuality.White];
    }
}

