using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadBannerData
{
    public int id;
    public string package;
    public int sort_id;
    public string name;
    public string info;
    public int count;
    public string rule1;
    public string intro1;
    public string rule2;
    public string intro2;
    public string GameSketch;
    public string GameRule;
    public List<string> GamePicture;
    public string name_e;
    public string banner_name;
    public int players;
    public int isStableOpen;
    public string iconURL;

}
public class TableBaseGameList :Singleton<TableBaseGameList>
{
    private List<HeadBannerData> bannerList;
    public void Init()
    {
        bannerList = new List<HeadBannerData>();
        HeadBannerData banner = new HeadBannerData();
        banner.id = 0;
        banner.package = "Bowling";
        banner.sort_id = 6;
        banner.name = "3D保龄球";
        banner.info = "1局10回合，按最终得分进行排名";
        banner.count = 1;
        banner.rule1 = "游戏限制";
        banner.intro1 = "1局10回合";
        banner.rule2 = "排名规则";
        banner.intro2 = "以最后得分进行排名";
        banner.GameSketch = "描述暂无";
        banner.GameRule = "点击移动保龄球，然后向前滑动出手保龄球，尽量碰倒所有瓶子获得高分数";
        banner.name_e = "bowling";
        banner.banner_name = "animal_b1";
        banner.GamePicture = new List<string>() { "screen1.jpg", "screen2.jpg", "screen3.jpg"};
        banner.players = 2;
        banner.isStableOpen = 0;
        banner.iconURL = "baolingqiu.jpg";
        bannerList.Add(banner);

        HeadBannerData banner2 = new HeadBannerData();
        banner2.id = 1;
        banner2.package = "Eliminate";
        banner2.sort_id = 3;
        banner2.name = "炫舞消除";
        banner2.info = "经典消除竞技，每局1分钟，按最终得分进行排名";
        banner2.count = 1;
        banner2.rule1 = "游戏时间";
        banner2.intro1 = "3分钟";
        banner2.rule2 = "排名规则";
        banner2.intro2 = "以最后得分进行排名";
        banner2.GameSketch = "炫酷热舞,经典消除";
        banner2.GameRule = "移动小魔怪，同色小魔怪在行或列的方向上组成3个及以上即可消除。";
        banner2.name_e = "eliminate";
        banner2.banner_name = "animal_b1";
        banner2.GamePicture = new List<string>() { "screen1.jpg", "screen2.jpg", "screen3.jpg" };
        banner2.players = 1;
        banner2.isStableOpen = 1;
        banner2.iconURL = "animal_b1.jpg";
        bannerList.Add(banner2);

        bannerList.Sort((x, y) => { return x.id.CompareTo(y.id); });
    }
    public List<HeadBannerData> GetBannerList()
    {
        return bannerList;
    }

}
