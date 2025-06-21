using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShopDiamondConfig:IConfig
{

    public int id;
    public int cash;
    public int type;
    public int item;
}
public class ShopGoldConfig : IConfig
{

    public int id;
    public int src_type;
    public int src_item;//cost_Text 1:1 2:88 3:1000 4:8880 5:3800 6:30000
    public int dest_type;
    public int dest_item;//return_num_Text 1:100 2:8800 3:100000  4:1000000 5:420000 6:5000000
    public int dest_level;
}
public class TableBaseRechargeShop : Singleton<TableBaseRechargeShop>
    {
        List<ShopDiamondConfig> diamondList;
         List<ShopGoldConfig> goldList;
    private bool m_isInit = false;
    public List<ShopDiamondConfig> dataDiamond
    {
        get
        {
            return diamondList;
        }
    }
   public List<object> dataObjects(List<IConfig> dataList)
    {
        if (dataList != null&& dataList.Count>0)
        {
            List<object> objectLists = new List<object>();
            objectLists.AddRange(dataList);
            return objectLists;
        }
        return null;
    }
    public List<ShopGoldConfig> dataGold
    {
        get
        {
            return goldList;
        }
    }
    public void InitDiamondConfig()
        {
            if (m_isInit)
            {
                return;
            }
             diamondList = new List<ShopDiamondConfig>();
            for (int i = 1; i <= 6; i++)
            {
                ShopDiamondConfig data = new ShopDiamondConfig();
                data.id = i;
                data.cash = 6 * i;
                data.type = 2;
                data.item = 600 * i;
                diamondList.Add(data);
            }
            m_isInit = true;
        }
    public void InitGoldConfig()
    {
        if (m_isInit)
        {
            return;
        }
        goldList = new List<ShopGoldConfig>();
     
        //
        for (int i = 1; i <= 6; i++)
        {
            ShopGoldConfig data = new ShopGoldConfig();
            data.id = i;
            
            data.src_type = ((i%2)==0)?2:1;
            data.src_item = (i *100);


            setGoldInfo(data);
            data.dest_level =  i;
            goldList.Add(data);
        }
        m_isInit = true;
    }
    private void setGoldInfo(ShopGoldConfig data)
    {
        // public int src_item;//cost_Text 1:1 2:88 3:1000 4:8880 5:3800 6:30000
        // public int dest_item;//return_num_Text 1:100 2:8800 3:100000  4:1000000 5:420000 6:5000000
        switch (data.id)
        {
            case 1:
                data.src_item = 1;
                data.dest_item = 100;
                    
                break;
            case 2:
                data.src_item = 88;
                data.dest_item = 8800;
                break;
            case 3:
                data.src_item = 1000;
                data.dest_item = 100000;
                break;
            case 4:
                data.src_item = 8880;
                data.dest_item = 1000000;
                break;
            case 5:
                data.src_item = 3800;
                data.dest_item = 420000;
                break;
            case 6:
                data.src_item = 30000;
                data.dest_item = 5000000;
                break;
        }
    }
    public void clear()
        {
            m_isInit = false;
            diamondList = null;
        }
    }



