public enum ResPackage
{
    Base,
    Eliminate,
    Mahjong,
    mvp
}

public class ResPackageUtil
{
    public static string GetPackageName(ResPackage resPackage)
    {
        switch (resPackage)
        {
            case ResPackage.Base:
                return "BasePackage";
            case ResPackage.Eliminate:
                return "eliminate";
            case ResPackage.Mahjong:
                return "mahjonghul";
            case ResPackage.mvp:
                return "mvp";
            default:
                return "";
        }
    }
}
