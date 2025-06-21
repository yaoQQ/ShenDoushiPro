using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//public enum AttriEnum
//{
//    totalLife=1,//percent@ 增加百分比生命值
//    recoverLife,//float@ 恢复
//    defend,//int@ 伤害抵挡
//    speed,//percent 船速度

//    shipDamage,//percent @伤害增加百分比 @Bullet-->ButtleDamage--->gunInfo.getAttriButtleDamage 
//    bulletSpeed,//percent @子弹速度增加百分比 @Bullet-->ButtleSpeed-->gunInfo.getAttriButtleSpeed  
//    lifeTime,//percent @Bullet-->timeLife-->gunInfo.getAttriTimeLife 持续时间增加百分比
//    attackDis,//percent @距离相关增加百分比 @GunInfo---->getAttriShootDistance（无人机巡航范围、AOEBullet爆炸范围、BulletLight攻击距离）
//    coolTime,//percent @GunBase--->GunInfo.getAttriCoolTime
//    bulletNum,//int

//    reSurvive,//percent
//    getItemDistance,//percent
//    lucky,//percent

//    expAttri,//percent @获得经验增加百分比 @OutSpacePlayerInfoManager-->AddExp()-->CharacterInfo-->expAttri
//    moneyGet,//percent @获得金钱增加百分比

//    curse,
//    nextNum,

//    None
//}
public enum AttriInfVvalueType
{
    Percent,
    IntValue,
    FloatValue,

    None
}
public enum AttriAddOrReduce
{
    Add,
    Reduce,

    None
}
[CreateAssetMenu(menuName = "OutSpace/AttriInfo")]
public class AttriInfo : ScriptableObject
{
    [Header("变量-记录游戏当前属性等级")]
    public int currLevel = 0;
    [Header("属性的类型")]
    public AttriEnum attriType;
    [Header("图片的路径 等同 AttriEnum_Img")]
    public string imagePath;
    [Header("属性值类型")]
    public AttriInfVvalueType attriValueType;
    [Header("是加还是减")]
    public AttriAddOrReduce addOrReduce= AttriAddOrReduce.Add;
    [Header("属性值")]
    public float attriValue;
    [Header("介绍")]
    public string descrip;

    [Header("属性值等级变化")]
    public List<float> attriList = new List<float>();

    public string getAttriText(int level)
    {
        // Logger.PrintColor
        StringBuilder sb = new StringBuilder();
        sb.Append("AssertName:<color='red'>" + this.name.ToString() + "</color>");
        sb.Append("\n属性名:<color='red'>" + attriType.ToString() + "</color>");
        sb.Append("\n等级:<color='yellow'>" + level.ToString() + "</color>");
        sb.Append("\n属性值:<color='yellow'>" + attriList[level - 1] + "</color>");
        //sb.Append("\n描述:<color='yellow'>" + descrip + "</color>");
        for (int i = 0; i < attriList.Count; i++)
        {
           string levelStr= getDescribeByLevel(i);
            sb.Append("\n等级[" + (i + 1) + "]:" +"<color='white'>" + levelStr + "</color>");
        }
        return sb.ToString();
    }
    public string getDescribeByLevel(int level)
    {
        string addOrReduceStr = "";
        if (addOrReduce == AttriAddOrReduce.Add)
        {
            addOrReduceStr = "+";
        }
        else if (addOrReduce == AttriAddOrReduce.Reduce)
        {
            addOrReduceStr = "-";
        }
        else
        {
            addOrReduceStr = "";
        }
        string valueStr = "";
        switch (attriValueType)
        {
            case AttriInfVvalueType.Percent:
                valueStr = attriList[level] * 100 + "%";
                break;
            case AttriInfVvalueType.FloatValue:
                valueStr = attriList[level].ToString();
                break;
            case AttriInfVvalueType.IntValue:
                valueStr = attriList[level].ToString();
                break;
            case AttriInfVvalueType.None:
                break;

        }
        string levelStr = descrip.Replace("@", "<color='red'>" + addOrReduceStr + valueStr + "</color>");
        return levelStr;
    }
    public float currValue
    {
        get
        {
            if (currLevel <= 0)
            {
                Logger.PrintError("错误 currLevel <= 0");
                return 0;
            }
            if (currLevel > attriList.Count)
            {
                Logger.PrintError("错误 currLevel="+ currLevel+" 大于最大等级值限制"+ attriList.Count);
                return 0;
            }
            return attriList[currLevel - 1];
        }
    }
}
