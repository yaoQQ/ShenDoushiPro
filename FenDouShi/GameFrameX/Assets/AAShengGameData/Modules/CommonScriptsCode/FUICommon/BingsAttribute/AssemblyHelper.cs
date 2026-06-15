using System;
using System.Collections.Generic;
using System.Reflection;

public static class AssemblyHelper
{
    public static Dictionary<string, Type> GetAssemblyTypes(params Assembly[] args)
    {
        Dictionary<string, Type> types = new Dictionary<string, Type>();

        foreach (Assembly ass in args)
        {
            foreach (Type type in ass.GetTypes())
            {
                types[type.FullName] = type;
            }
        }

        return types;
    }

    public static PackageEnum GetPackageEnum(Type type,string pakcageName)
    {
        if (pakcageName.Equals(PackageEnum.LoginPackage.ToString()))
        {
            return PackageEnum.LoginPackage;
        }
        else if (pakcageName.Equals(PackageEnum.GameMainPackage.ToString()))
        {
            return PackageEnum.GameMainPackage;
        }
        else if (pakcageName.Equals(PackageEnum.CommonScriptCode.ToString()))
        {
            return PackageEnum.CommonScriptCode;
        }
        else if (pakcageName.Equals(PackageEnum.FightPackage.ToString()))
        {
            return PackageEnum.FightPackage;
        }
        Logger.PrintError($"{type}获取程序集包失败:{pakcageName}");
        return PackageEnum.None;
    }
}