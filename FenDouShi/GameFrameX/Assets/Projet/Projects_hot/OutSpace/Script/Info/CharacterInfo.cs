using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(menuName = "OutSpace/CharacterInfo")]
public class CharacterInfo  :ScriptableObject
{
    public float totalLife;// 生命上限
    public float recoverLife;//生命恢复
    public float defend;//防御
    public float speed;// 移动速度上限

    public float shipDamage;//力量伤害
    public float bulletSpeed;//飞射速度
    public float lifeTime;//持续时间
    public float attackDis;//攻击范围

    public float coolTime;//冷却时间
    public float bulletNum;//发射数量



    public float reSurvive;//复活机会
    public float getItemDistance;//念力
    public float lucky;//运气

    private  float expTotal;//经验计算值
    public float expAttri=0;//经验属性添加值
    public float moneyGet;//贪欲

    public int curse;//诅咒
    public int nextNum;//跳过

    public List<float> expList = new List<float>();

    public static int luckyToOpen = 5;
    public float exp
    {
        get
        {
            return expTotal;
        }
        set
        {
            expTotal = expTotal + value + expAttri*value;//增加经验值
        }
    }
 
    //获得达到下一等级的经验
    public float getCurrLevelTotalExp(int level)
    {
        if (level > 0 && level < expList.Count)
        {
            float expNum = expList[level] - expList[level - 1];
            return expNum;
        }
        return expList[0];
    }
    public void AddAttri(AttriInfo attri)
    {
       
        string propertyName = attri.attriType.ToString();
        Logger.PrintColor("yellow", "propertyName=" + propertyName);
       
        if ((this[propertyName] is float))
        {
            Logger.PrintColor("yellow", "（原）Set value=" + attri.attriValue);
            this[propertyName] = attri.attriValue;
        }
        else
        {
            Logger.PrintColor("yellow", "（原）Set value=" + attri.attriValue);
            this[propertyName] = (int)attri.attriValue;
        }
        Logger.PrintColor("yellow", "更新玩家属性后的值 " + attri.attriType.ToString() + "=" + this[propertyName]);

    }
    public object this[string propertyName]
    {
        get
        {
            FieldInfo pi = this.GetType().GetField(propertyName);
            return pi.GetValue(this);
        }
        set
        {
         //   Logger.PrintColor("yellow", "Set this.GetType()=" + this.GetType());
            FieldInfo pi = this.GetType().GetField(propertyName);
            Logger.PrintColor("yellow", "Set pi=" + pi);
            Logger.PrintColor("yellow", "Set value=" + value);
            pi.SetValue(this, value);

        }

    }
    public string getAttri()
    {
       // Logger.PrintColor
       StringBuilder sb = new StringBuilder();
        sb.Append("生命上限totalLife:<color='red'>" + totalLife+"</color>");
        sb.Append("\n生命恢复recoverLife:<color='yellow'>" + recoverLife + "</color>");
        sb.Append("\n防御defend:" + defend);
        sb.Append("\n移动速度上限speed:" + speed);

        sb.Append("\n力量伤害shipDamage:" + shipDamage);
        sb.Append("\n飞射速度bulletSpeed:" + bulletSpeed);
        sb.Append("\n持续时间lifeTime:" + lifeTime);
        sb.Append("\n攻击范围attackDis:" + attackDis);

        sb.Append("\n攻击范围coolTime:" + coolTime);
        sb.Append("\n发射数量bulletNum:" + bulletNum);

        sb.Append("\n复活机会reSurvive:" + reSurvive);
        sb.Append("\n念力getItemDistance:" + getItemDistance);
        sb.Append("\n运气lucky:" + lucky);

        sb.Append("\n当前经验 成长expTotal:" + expTotal);
        sb.Append("\n经验成长百分比expAttri:" + expAttri);
        sb.Append("\n诅咒curse:" + curse);
        sb.Append("\n跳过nextNum:" + nextNum);

        return sb.ToString();
    }
}
