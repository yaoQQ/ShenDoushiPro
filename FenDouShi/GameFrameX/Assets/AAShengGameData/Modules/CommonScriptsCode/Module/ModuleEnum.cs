using UnityEngine;
using System.Collections;

//需移植到热更 
public enum ModuleEnum
{
    Common,
    Login,
    MainGame,
    SystemCommonModule,

    Mail,
    GM,
    Bag,
    SystemOpen,
    Chat,
    Role,
    Task
}

public enum ProjectPackage
{
    None =0,
    Base=1,
    GameMain=2,
}
