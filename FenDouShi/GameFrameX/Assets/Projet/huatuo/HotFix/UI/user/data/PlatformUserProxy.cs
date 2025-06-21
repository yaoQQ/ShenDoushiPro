using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

//playFab里配置的游戏参数

public class PlatformUserProxy
{

    //更新玩家Playfab里游戏参数的数据
     public static void UpdateUserGameData(Dictionary<string, UserDataRecord> gameDataDic)
    {
        foreach (var keyPair in gameDataDic)
        {
            UserDataRecord data = keyPair.Value;
            string key = keyPair.Key;
            int value = int.Parse(data.Value);
            if (key.Equals(CharacterDataEnum.CharacterSex.ToString()))
            {
                GameDataManager.Instance.SetSex(value);
            }else if(key.Equals(CharacterDataEnum.CharacterCoin.ToString()))
            {
                GameDataManager.Instance.updateGold(value);
            }
            else if (key.Equals(CharacterDataEnum.CharacterDiamond.ToString()))
            {
                GameDataManager.Instance.updateDiamond(value);
            }
           
        }
        
    }
}
