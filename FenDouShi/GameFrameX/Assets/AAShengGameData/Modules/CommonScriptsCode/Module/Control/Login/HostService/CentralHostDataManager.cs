public class CentralHostDataManager : Singleton<CentralHostDataManager>
{
    public string playerName {private set; get;} 

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

}