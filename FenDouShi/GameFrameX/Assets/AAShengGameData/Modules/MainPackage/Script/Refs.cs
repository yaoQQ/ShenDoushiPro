



public class Refs
{
    public static GmData gmData;

    //public static BagModel bagData;

    //public static SystemOpenData systemOpenData;

    public Refs()
    {
        InitModel();
    }

    public void InitModel()
    {
        gmData = new GmData();
        //bagData = new BagModel();
        //systemOpenData = new SystemOpenData();
    }

    public static void OnReset()
    {
        gmData = null;
        //bagData = null;
        //systemOpenData = null;
    }
}

//基本玩家数据 
public class BaseMoudleData 
{
}


