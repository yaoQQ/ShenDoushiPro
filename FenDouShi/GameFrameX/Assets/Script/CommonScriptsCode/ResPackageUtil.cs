public enum ResPackage
{
    Base,
    Game
}

public class ResPackageUtil
{
    public static string GetPackageName(ResPackage resPackage)
    {
        switch (resPackage)
        {
            case ResPackage.Base:
                return "BasePackage";
            case ResPackage.Game:
                return "Game";
            default:
                return "";
        }
    }
}
