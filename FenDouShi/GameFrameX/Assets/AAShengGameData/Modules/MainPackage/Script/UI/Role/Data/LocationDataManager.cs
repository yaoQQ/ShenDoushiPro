using System.Collections.Generic;

public class LocationDataManager : Singleton<LocationDataManager>
{

    Dictionary<int, LocationVo> locationDic;

    public Dictionary<int, LocationVo> InitLocationList()
    {
        if (locationDic != null)
        {
            return locationDic;
        }
        locationDic = ConfigMgr.Instance.GetConfig<LocationVo>();
        return locationDic;
    }
    public LocationVo GetLocation(int provinceCode, int cityCode)
    {
        if (locationDic == null)
        {
            InitLocationList();
        }
        foreach (var pair in locationDic)
        {
            if (pair.Value.ProvinceCode == provinceCode && pair.Value.CityCode == cityCode)
            {
                return pair.Value;
            }
        }
        return null;
    }

}