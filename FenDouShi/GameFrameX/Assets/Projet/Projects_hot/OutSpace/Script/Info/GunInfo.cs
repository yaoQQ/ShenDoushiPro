using UnityEngine;
using System.Collections;
using System.Text;

[CreateAssetMenu(menuName = "OutSpace/BulletInfo")]
public class GunInfo :ScriptableObject
{
    [Header("子弹名")]
    public string BulletName;//子弹名
    [Header("[枪]等级")]
    public int level;//等级
    [Header("[枪]子弹间隔时间")]
    public float coolTime;//子弹间隔时间@
    [Header("[枪]总子弹数")]
    public int BulletTotolCount;//总子弹数@ 飞机
    [Header("[枪]数量")]
    public int gunNum;//[枪]数量@
    [Header("[枪]总子弹冷却时间")]
    public float BulletCollTimeTotal;//总子弹冷却时间

    [Header("[子弹]速度")]
    public float ButtleSpeed;//子弹速度@
    [Header("[子弹]伤害")]
    public float ButtleDamage;//子弹伤害@
    [Header("[子弹]持续时间 激光打击间隔时间")]
    public float timeLife = 3;//子弹持续时间@

    [Header("[枪--爆炸--和激光距离]距离相关 爆炸距离、攻击距离")]
    public float Distance;//距离@
    [Header("[枪skill]防御伤害")]
    public int DefendLife;//防御的伤害@

    [Header("等级描述")]
    public string Descrip;

    public float getAttriCoolTime
    {
        get
        {
            return coolTime - coolTime*OutSpacePlayerInfoManager.Instance.CharacterAddInfo.coolTime;
        }
    }
    public int getAttriBulletTotolCount
    {
        get
        {
            return BulletTotolCount +  (int)OutSpacePlayerInfoManager.Instance.CharacterAddInfo.bulletNum;
        }
    }
    public float getAttriGunNum
    {
        get
        {
            return gunNum + OutSpacePlayerInfoManager.Instance.CharacterAddInfo.bulletNum;
        }
    }
    public float getAttriButtleSpeed
    {
        get
        {
            return ButtleSpeed + ButtleSpeed*OutSpacePlayerInfoManager.Instance.CharacterAddInfo.bulletSpeed;
        }
    }
    public float getAttriButtleDamage
    {
        get
        {
            return ButtleDamage + ButtleDamage * OutSpacePlayerInfoManager.Instance.CharacterAddInfo.shipDamage;
        }
    }
    public float getAttriTimeLife
    {
        get
        {
            return timeLife + timeLife * OutSpacePlayerInfoManager.Instance.CharacterAddInfo.lifeTime;
        }
    }
    public float getAttriShootDistance
    {
        get
        {
            return Distance + Distance * OutSpacePlayerInfoManager.Instance.CharacterAddInfo.attackDis;
        }
    }
    public float getAttriDefendLife
    {
        get
        {
            return DefendLife + OutSpacePlayerInfoManager.Instance.CharacterAddInfo.defend;
        }
    }

    public string getAttriText()
    {
        // Logger.PrintColor
        StringBuilder sb = new StringBuilder();
        sb.Append("assertName:<color='red'>" + this.name + "</color>");
        sb.Append("子弹名:<color='blue'>" + BulletName + "</color>");
        sb.Append("\n子弹间隔时间coolTime:<color='yellow'>" + coolTime + "</color>" + addChangeStr(coolTime.ToString(),getAttriCoolTime.ToString()));
        if (BulletName == "UVAShip")
        {
            sb.Append("\n无人机数量BulletTotolCount:<color='yellow'>" + BulletTotolCount + "</color>" + addChangeStr(BulletTotolCount.ToString(), getAttriBulletTotolCount.ToString()));
        }
        else
        {
            sb.Append("\n总子弹数BulletTotolCount:<color='yellow'>" + BulletTotolCount + "</color>" + addChangeStr(BulletTotolCount.ToString(), getAttriBulletTotolCount.ToString()));
        }
        sb.Append("\n总子弹冷却时间BulletCollTimeTotal:<color='cyan'>" + BulletCollTimeTotal + "</color>");
        sb.Append("\n子弹速度ButtleSpeed:<color='magenta'>" + ButtleSpeed + "</color>" + addChangeStr(ButtleSpeed.ToString(),getAttriButtleSpeed.ToString()));
        sb.Append("\n子弹伤害ButtleDamage:<color='green'>" + ButtleDamage + "</color>" + addChangeStr(ButtleDamage.ToString(),getAttriButtleDamage.ToString()));
        sb.Append("\n子弹持续时间timeLife:<color='yellow'>" + timeLife + "</color>" + addChangeStr(timeLife.ToString(),getAttriTimeLife.ToString()));
        sb.Append("\n距离相关 爆炸距离、攻击距离Distance:<color='grey'>" + Distance + "</color>" + addChangeStr(Distance.ToString(),getAttriShootDistance.ToString()));
        sb.Append("\n防御伤害DefendLife:<color='blue'>" + DefendLife + "</color>" + addChangeStr(DefendLife.ToString(),getAttriDefendLife.ToString()));
        return sb.ToString();
    }
    private string addChangeStr(string oristr,string valueStr)
    {
        if (oristr.Equals(valueStr))
        {
            return "";
        }
        string colorStr = "<color='cyan'>---->" + valueStr + "</color>";
        return colorStr;
    }
 
}
