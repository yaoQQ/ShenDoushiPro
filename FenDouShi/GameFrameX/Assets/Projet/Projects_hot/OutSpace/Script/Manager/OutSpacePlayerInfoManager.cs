using System;
using System.Collections.Generic;
using UnityEngine;


public class OutSpacePlayerInfoManager:Singleton<OutSpacePlayerInfoManager>
{
    private CharacterInfo addCharacterInfo;//获得的玩家属性（选择的获得的属性记录）
    private CharacterInfo configChacterInfo;//当前玩家属性
    private int level = 0;
    private List<Action<int>> levelFunList;
    private List<GameGunData> gameAttriList = new List<GameGunData>();
    private int totalAttriCount = 12;//玩家能拥有的最大升级属性对象
    public static int levelMax = 5;//升级对象的最大等级
    private int attriViewCountMax = 4;//可以选择的最大item数量
    public void Init()
    {
        addCharacterInfo =ScriptableObject.CreateInstance<CharacterInfo>() ;
        levelFunList = new List<Action<int>>();
        configChacterInfo = OutSpaceCameraManager.Instance.Player.GetComponent<PlayerShip>().characterInfo;
        Logger.PrintColor("blue", "addCharacterInfo.exp=" + addCharacterInfo.expList);
        Logger.PrintColor("blue", "addCharacterInfo.exp.count=" + addCharacterInfo.expList.Count);
        AddExp(0);
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.LevelUpSelectData, LevelUpSelectData);
    }
    /// <summary>
    /// 选择了升级武器
    /// </summary>
    private void LevelUpSelectData(string noticeType, BaseNotice notice)
    {
        ObjectNotice objNotice = (ObjectNotice)notice;
        GameGunData selectData = objNotice.GetObj() as GameGunData;
        GameGunData lastData =getHasGameGunData(selectData);//升级的上一个对象数据
        if (lastData != null)
        {
            gameAttriList.Remove(lastData);
        }
        gameAttriList.Add(selectData);
        AddAttriFun(selectData);
        Logger.PrintColor("blue", "升级！！LevelUpSelectData  selectData=" + selectData + " gameAttriList.count=" + gameAttriList.Count);
    }
    /// <summary>
    /// 更新添加属性
    /// </summary>
    private void AddAttriFun(GameGunData selectData)
    {
        if (selectData.gunType != GunType.None)//是选择了武器
        {
            return;
        }
        //更新玩家游戏属性
        OutSpaceResourceManager.Instance.GetAttriInfo(selectData.attriInfoAssertPath, (getAttriInfo) =>
        {
           AttriInfo attriInfo = getAttriInfo;
            attriInfo.currLevel = selectData.level;//根据选择更新当前属性等级
            Loger.PrintColor("blue", "=====更新玩家属性========== attriInfo.currLevel=" + attriInfo.currLevel);
            AddAttri(attriInfo);
        });
    }
    public CharacterInfo CharacterAddInfo
    {

        get {
            return addCharacterInfo;
         }
        
    }
    /// <summary>
    /// 更新玩家属性CharacterInfo 包含子弹属性和玩家属性
    /// </summary>
    /// <param name="attriInfo">更新的玩家属性</param>
    public void AddAttri(AttriInfo attriInfo)
    {
        addCharacterInfo.AddAttri(attriInfo);
        NoticeManager.Instance.Dispatch(OutSpaceNotice.PlayerAttriDataUpdate, attriInfo);
        Logger.PrintColor("yellow", "AddAttri() addCharacterInfo=" + addCharacterInfo);
        Loger.PrintColor("blue", "=====更新玩家属性 end==========");
    }
    public void AddExp(int num)
    {
        if (configChacterInfo==null) {
            return;
        }
        List<float> expConfigList = configChacterInfo.expList;
        addCharacterInfo.exp =  num;//addCharacterInfo里面自动添加经验计算
       // Logger.PrintDebug("addCharacterInfo.exp=" + addCharacterInfo.exp);
        float expToNextLevel = 0;
        if (this.level > 0)
        {
             expToNextLevel = addCharacterInfo.exp - expConfigList[this.level - 1];
        }
        else
        {
            expToNextLevel = addCharacterInfo.exp;
        }
        //Logger.PrintColor("blue","等级="+this.level+ " expToNextLevel=" + expToNextLevel + " nextLevelTotal=" + configChacterInfo.getCurrLevelTotalExp(this.level));
        List<float> expList = new List<float> { expToNextLevel, configChacterInfo.getCurrLevelTotalExp(this.level) };
        NoticeManager.Instance.Dispatch(OutSpaceNotice.UpdatePlayerEXP, expList);
        UpdatePlayerLevel();

    }
    public float getCurrTotalExp
    {
        get
        {
            return configChacterInfo.expList[this.level];
        }
    }
    public float getCurrExp
    {
        get
        {
            return addCharacterInfo.exp;
        }
    }
    private void UpdatePlayerLevel()
    {
       
        List<float> expList = configChacterInfo.expList;

        if (isLevelChange(this.level))
        {

            this.level++;
            levelFunList.Add((int te) => {
                Logger.PrintColor("red", "levelFun() LevelUp=" + te);
                CommonView.showTopTips("Level Up! level=" + te);
                RandomGetGunData();//展示玩家升级武器列表
             
            });
        }
        // Logger.PrintDebug("expList.count=" + expList.Count);
  
        if (isLevelChange(this.level))
        {
            Logger.PrintDebug("连续弹窗@@@@@@@@after addCharacterInfo.exp=" + addCharacterInfo.exp + " expList=" + expList[this.level]);
            UpdatePlayerLevel();
        }
        showLevelUp(this.level);
    }
    private bool isLevelChange(int currLevel)
    {
        List<float> expList = configChacterInfo.expList;
        if (this.level >= expList.Count)
        {
            CommonView.showTopTips("玩家等级已经最高 level=" + this.level);
            return false;
        }
        if (addCharacterInfo.exp >= expList[currLevel])
        {
            return true;
        }
        return false;
    }

    //test  private--->public 
    public List<GameGunData> TestRandomGetGunData()
    {
        int dataCount = this.attriViewCountMax - 1;
        //幸运值大于10 解锁4个选择列表对象
        if (addCharacterInfo.lucky >= CharacterInfo.luckyToOpen)
        {
            dataCount = attriViewCountMax;
        }
        gameAttriList.Sort(new LevelComparer());//等级由小到大
        List<GameGunData> randomDataList = new List<GameGunData>();//保存随机的数据类型对象
        List<GunType> randomGunTypeList = getGunTypeArrList();
        List<AttriEnum> randomAttriList = getAttriEnumArrList();
        for (int i = 0; i < dataCount; i++)
        {
            GameGunData newGunData = new GameGunData();
            //列表已满，从玩家当前升及对象获得升级对象
            if (gameAttriList.Count >= totalAttriCount)
            {
                int limitIndex = getMaxLimit();//获取最大等级的index,最大等级的对象不升级
                if (limitIndex <= 0)
                {
                    CommonView.showTopTips("等级列表已满");
                    return null;
                }
                int index = UnityEngine.Random.Range(0, limitIndex);//随机获得一个等级未满的升级对象
                GameGunData aheadGunData = gameAttriList[index];
                newGunData.clone(aheadGunData);
                // newGunData.setLevel(aheadGunData.level + 1);

            }
            else
            { //随机生成一个升级列表对象属性

                //随机一个枪对象item或属性值对象
                  int randonType = UnityEngine.Random.Range(0, 2);
              
                if (randonType < 1)
                {
                    int index = UnityEngine.Random.Range(0, randomGunTypeList.Count);
                    GunType gunType = randomGunTypeList[index];
                    newGunData.gunType = gunType;
                    randomGunTypeList.RemoveAt(index);//选取过的类型排除
                }
                else
                {
                    int index = UnityEngine.Random.Range(0, randomAttriList.Count);
                    AttriEnum attri = randomAttriList[index];
                    newGunData.attriType = attri;
                    randomAttriList.RemoveAt(index);//选取过的类型排除
                }

                GameGunData HasGameGunData = getHasGameGunData(newGunData);
                if (HasGameGunData != null)//已有升级对象
                {
                    newGunData.clone(HasGameGunData);
                    //  newGunData.setLevel(HasGameGunData.level + 1);
                }
                else//新的升级对象
                {
                    newGunData.level = 1;
                }
            }
            randomDataList.Add(newGunData);
        }
        List<GameGunData> asGameList = randomDataList as List<GameGunData>;
        bool isGameList = randomDataList is List<GameGunData>;
        Logger.PrintDebug("####asGameList=" + asGameList);
        Logger.PrintDebug("####asGameList.GetType()=" + asGameList.GetType());

        Logger.PrintDebug("####randomDataList=" + randomDataList);
        return randomDataList;

    }
    private void RandomGetGunData()
    {
        int dataCount = this.attriViewCountMax-1;
        //幸运值大于10 解锁4个选择列表对象
        if (addCharacterInfo.lucky >= CharacterInfo.luckyToOpen)
        {
            dataCount = attriViewCountMax;
        }
        gameAttriList.Sort(new LevelComparer());//等级由小到大
        List<GameGunData> randomDataList = new List<GameGunData>();//保存随机的数据类型对象
        List<GunType> randomGunTypeList = getGunTypeArrList();
        List<AttriEnum> randomAttriList = getAttriEnumArrList();
        for (int i = 0; i < dataCount; i++)
        {
            GameGunData newGunData = new GameGunData();
            //列表已满，从玩家当前升及对象获得升级对象
            if (gameAttriList.Count >= totalAttriCount)
            {
                int limitIndex = getMaxLimit();//获取最大等级的index,最大等级的对象不升级
                if (limitIndex <= 0)
                {
                    CommonView.showTopTips("等级列表已满");
                    return;
                }
                int index = UnityEngine.Random.Range(0, limitIndex);//随机获得一个等级未满的升级对象
                GameGunData aheadGunData= gameAttriList[index];
                newGunData.clone(aheadGunData);
                newGunData.setLevel(aheadGunData.level + 1);
               
            }
            else
            { //随机生成一个升级列表对象属性
               
                //随机一个枪对象item或属性值对象
               int randonType= UnityEngine.Random.Range(0, 2);
                if (randonType < 1)
                {
                    int index = UnityEngine.Random.Range(0, randomGunTypeList.Count);
                    GunType gunType = randomGunTypeList[index];
                    newGunData.gunType = gunType;
                    randomGunTypeList.RemoveAt(index);//选取过的类型排除
                }
                else
                {
                    int index = UnityEngine.Random.Range(0, randomAttriList.Count);
                    AttriEnum attri = randomAttriList[index];
                    newGunData.attriType = attri;
                    randomAttriList.RemoveAt(index);//选取过的类型排除
                }

                GameGunData HasGameGunData= getHasGameGunData(newGunData);
                if (HasGameGunData!=null)//已有升级对象
                {
                    newGunData.clone(HasGameGunData);
                    newGunData.setLevel(HasGameGunData.level + 1);
                }
                else//新的升级对象
                {
                    newGunData.level = 1;
                }
            }
            randomDataList.Add(newGunData);
        }
     

      //test  UIViewManager.Instance.Open(UIViewEnum.OutSpaceGunListPanel, randomDataList);


        // UnityEngine.Random((int)GunType.normalGun, (int)GunType.UVAGun);

    }
    private List<GunType> getGunTypeArrList()
    {
        List<GunType> list = new List<GunType>();
        for(GunType gun= GunType.LightGun; gun<= GunType.UVAGun; gun++)
        {
            list.Add(gun);
        }
        return list;
    }
    private List<AttriEnum> getAttriEnumArrList()
    {
        List<AttriEnum> list = new List<AttriEnum>();
        for (AttriEnum attri = AttriEnum.totalLife; attri <= AttriEnum.moneyGet; attri++)
        {
            list.Add(attri);
        }
        return list;
    }


    /// <summary>
    /// 获取玩家已有的升级对象
    /// </summary>
    /// <param name="currGame">已有的升级type对象</param>
    /// <returns></returns>
    private GameGunData getHasGameGunData(GameGunData currGame)
    {
        for (int i = 0; i < gameAttriList.Count; i++)
        {
            if(currGame.gunType!= GunType.None)
            {
                if (currGame.gunType == gameAttriList[i].gunType)
                {
                    return gameAttriList[i];
                }
            }
            else
            {
                if (currGame.attriType == gameAttriList[i].attriType)
                {
                    return gameAttriList[i];
                }
            }

        }
        
        return null;
    }

    /// <summary>
    /// 获取最大等级的index 后面的也全是最大等
    /// </summary>
    /// <returns></returns>
    private int getMaxLimit()
    {
        for (int i = 0; i < gameAttriList.Count; i++)
        {
            if(gameAttriList[i].level>= levelMax)
            {
                return i;
            }
           
        }

        return gameAttriList.Count;
    }

    /// <summary>
    /// 获取玩家的升级的装备列表
    /// </summary>
    /// <returns></returns>
    public List<GameGunData> GetGameAttriList()
    {
        return gameAttriList;
    }
    private void showLevelUp(int currLevel)
    {

        if (levelFunList.Count > 0)
        {
            levelFunList[0].Invoke(currLevel);
            levelFunList.RemoveAt(0);
            Logger.PrintDebug("this.level=" + this.level + " configChacterInfo.expList[this.level]=" + configChacterInfo.expList[this.level]);
            Logger.PrintColor("yellow", "addCharacterInfo.exp=" + addCharacterInfo.exp);
            addCharacterInfo.exp = 0;
            Logger.PrintColor("yellow", "set 0 addCharacterInfo.exp=" + addCharacterInfo.exp);
            AddExp(0);
        }
        
    }


    public void Clear()
    {
        addCharacterInfo = null;
        configChacterInfo = null;
        levelFunList = null;
        gameAttriList = new List<GameGunData>();
        NoticeManager.Instance.RemoveNoticeLister(OutSpaceNotice.LevelUpSelectData, LevelUpSelectData);
    }
   
}