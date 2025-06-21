using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//设置玩家射击按钮，标志位
public enum PlayerGunBtnsEnum
{
    rightUp,
    rightDown,
    leftUp,
    leftDown,

    AutoMiddle,
    none
}

////设置枪的位置 左 右 中
//public enum PlayerGunPosEnum
//{
//    Left,
//    Right,
//    Middle,
//    None
//}

//public enum GunType
//{
   
//    AutoFastGun = 1,//自动攻击
//    normalGun = 2,//普通 無人機搶
//    LightGun = 3,//激光枪
//    FastGun = 4,//子弹枪
//    DefendGun = 5,//防御
//    DisturbGun = 6,//清屏
//    AOEGun = 7,//导弹


//    UVAGun = 8,//无人机
//    None = 0
//}
public class GunManager : Singleton<GunManager>
{
    public static GameObject Bullets;//搜集所有子弹父类，方便编辑器查看
    public static GameObject Ships;//搜集所有飞船父类，方便编辑器查看
    public static GameObject Effects;//搜集所有特效，方便编辑器查看

    private Dictionary<GunType, GunBtn> gunBtnDic;//位置的按钮
    private Dictionary<GunType, GameObject> gunObjDic=new Dictionary<GunType, GameObject>();//生成的枪对象

    private GunType _gunType = GunType.None;
    private List<PlayerGun> autoListGun;

    public void Init()
    {
        //子弹容器

        Bullets = new GameObject("Bullets");
        Ships = new GameObject("Ships");
        Effects = new GameObject("Effects");

        //槽位按钮
        gunBtnDic = new Dictionary<GunType, GunBtn>();
        GunBtn[] objs = GameObject.FindObjectsOfType(typeof(GunBtn)) as GunBtn[];
        for (int i = 0; i < objs.Length; i++)
        {
            gunBtnDic[objs[i]._gunType] = objs[i];
        }
        InitAutoGun();

        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.LevelUpSelectData, LevelUpSelectData);
    }
    /// <summary>
    /// 选择了升级武器
    /// </summary>
    private void LevelUpSelectData(string noticeType, BaseNotice notice)
    {
      
        ObjectNotice objNotice = (ObjectNotice)notice;
        GameGunData selectData = objNotice.GetObj() as GameGunData;
        GunType gunType = selectData.gunType;
        if (gunType== GunType.None)//时属性升级
        {
            addAttriByGameGunData(selectData);
            return;
        }
      
        if (gunObjDic.ContainsKey(gunType))
        {
            Logger.PrintColor("blue", "更新武器表现 gunType=" + gunType+ "selectData.level="+ selectData.level);
            PlayerGun[] playerGunList = gunObjDic[gunType].GetComponentsInChildren<PlayerGun>();
            for(int i=0;i< playerGunList.Length; i++)
            {
                playerGunList[i].UpdateGunInfoByData(selectData);//更新对应枪属性
            }
            if (gunBtnDic.ContainsKey(gunType)) {//更新枪的按钮
                gunBtnDic[gunType].UpdateByGameHunData(selectData);
            }
        }
        else
        {
            Logger.PrintColor("red", "不存在武器表现---》添加新武器 gunType=" + gunType);
        }

    }
    private void addAttriByGameGunData(GameGunData selectData)
    {
        CharacterInfo addAttri = OutSpacePlayerInfoManager.Instance.CharacterAddInfo;

    }

  

    /// <summary>
    /// 添加玩家枪对象
    /// </summary>
    /// <param name="gunType"></param>
    /// <param name="obj"></param>
    public void addPlayerGun(GunType gunType,GameObject obj)
    {
        if (gunObjDic.ContainsKey(gunType))
        {
            gunObjDic[gunType] = obj;
        }
        else {
            gunObjDic.Add(gunType, obj);
        }
        Logger.PrintColor("blue", "addPlayerGun() 添加枪对象实例gunType=" + gunType);
        Logger.PrintColor("blue", "addPlayerGun() obj=" + obj);
    }
    public GameObject getPlayerGunObj(GunType gunType)
    {
        if (gunObjDic.ContainsKey(gunType))
        {
            return gunObjDic[gunType];
        }
          return null;
    }


    //玩家的自动枪械
    public void InitAutoGun()
    {
        UpdateAutoGun(GunType.AutoFastGun);
    }
    private void UpdateAutoGun(GunType gunType)
    {
        if (_gunType == gunType)
        {
            return;
        }
        if (autoListGun != null && autoListGun.Count > 0)
        {
            for (int i = 0; i < autoListGun.Count; i++)
            {
                GameObject.Destroy(autoListGun[i].gameObject);
            }
        }
        _gunType = gunType;
        string gunName = gunType.ToString();
        GameObject playerGunParent = MyUtils.LoadGunPrefab(gunName);
        PlayerGun[] playerGunList = playerGunParent.GetComponentsInChildren<PlayerGun>();
        autoListGun = playerGunList.ToList();
        if (autoListGun.Count > 0)
        {
            autoListGun[0].transform.parent.gameObject.SetActive(false);
        }
        
       
    }


    ////更新枪的位置
    //public void updateGunPos(PlayerGun gun, PlayerGunPosEnum pos)
    //{
    //    PlayerGunPos gunPosObj = GunManager.Instance.getGunPos(pos);
    //    if (gunPosObj != null)
    //    {
    //        gun.transform.parent = gunPosObj.transform;
    //        gun.transform.localPosition = Vector3.zero;
    //    }
    //}
    public void Clear()
    {
        Bullets = null;
        Ships = null;
        Effects = null;
        if (gunBtnDic != null) {
            gunBtnDic.Clear();
        }
    }
}

