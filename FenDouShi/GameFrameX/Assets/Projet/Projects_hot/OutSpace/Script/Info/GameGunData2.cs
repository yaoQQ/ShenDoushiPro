using UnityEngine;
using UnityEditor;

public enum GunType2
{

    AutoFastGun = 1,//自动攻击
    normalGun = 2,//普通 無人機搶
    LightGun = 3,//激光枪
    FastGun = 4,//子弹枪
    DefendGun = 5,//防御
    DisturbGun = 6,//清屏
    AOEGun = 7,//导弹


    UVAGun = 8,//无人机
    None = 0
}

public enum AttriEnum2
{
    totalLife = 1,//percent@ 增加百分比生命值
    recoverLife,//float@ 恢复
    defend,//int@ 伤害抵挡
    speed,//percent 船速度

    shipDamage,//percent @伤害增加百分比 @Bullet-->ButtleDamage--->gunInfo.getAttriButtleDamage 
    bulletSpeed,//percent @子弹速度增加百分比 @Bullet-->ButtleSpeed-->gunInfo.getAttriButtleSpeed  
    lifeTime,//percent @Bullet-->timeLife-->gunInfo.getAttriTimeLife 持续时间增加百分比
    attackDis,//percent @距离相关增加百分比 @GunInfo---->getAttriShootDistance（无人机巡航范围、AOEBullet爆炸范围、BulletLight攻击距离）
    coolTime,//percent @GunBase--->GunInfo.getAttriCoolTime
    bulletNum,//int

    reSurvive,//percent
    getItemDistance,//percent
    lucky,//percent

    expAttri,//percent @获得经验增加百分比 @OutSpacePlayerInfoManager-->AddExp()-->CharacterInfo-->expAttri
    moneyGet,//percent @获得金钱增加百分比

    curse,
    nextNum,

    None
}


/// <summary>
/// 升级类--选择玩家的属性 和枪械升级对象类(两种类型：1。武器对象2.属性对象)
/// </summary>
public class GameGunData2
{
    public GunType2 gunType;//GunType.None是属性对象，其它为武器对象
    public AttriEnum2 attriType;
    //public string gunType;//GunType.None是属性对象，其它为武器对象
    //public string attriType;
    public int level;
    public string name;
    public GameGunData2()
    {

    }
    public void clone(GameGunData2 other)
    {
        this.gunType = other.gunType;
        this.attriType = other.attriType;
        level = other.level;
        name = other.name;
    }
    public string attriInfoAssertPath
    {
        get
        {
            return attriType.ToString() + "Info";
        }
    }
    public string attriImgStrPath
    {
        get
        {
            return attriType.ToString() + "_Img";
        }
    }
    public string gunInfoAssertPath
    {
        get
        {
            return gunType.ToString() + "Info_" + this.level;
        }
    }
    public string gunImgStrPath
    {
        get
        {
            return gunType.ToString() + "_Img";
        }
    }
    //public void setLevel(int currLevel)
    //{
    //    this.level = currLevel;
    //    if (this.level > OutSpacePlayerInfoManager.levelMax)
    //    {
    //        this.level = OutSpacePlayerInfoManager.levelMax;
    //    }

    //}
}