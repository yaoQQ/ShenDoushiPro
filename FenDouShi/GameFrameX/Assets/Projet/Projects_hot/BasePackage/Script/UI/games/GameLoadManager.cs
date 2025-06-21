using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
public enum EnumGameID
{
    Bowling =0,
    Eliminate=1,
    littleprince=2,
}
public class GameLoadInfo
{
    public EnumGameID gameId;
    public int gameType;
    public int roomId;
    public int size;
}
public class GameLoadManager : Singleton<GameLoadManager>
{
    public void startGame(EnumGameID gameId, int gameType=0)
    {
        switch (gameId)
        {
          
            case EnumGameID.littleprince:
                PreloadManager.Instance.PreLoadPackage(ProjectControler.littlePrincePackage);
                break;
        }
    }
    public void checkDownloadOrStartGame(EnumGameID gameId,int gameType = 0)
    {
        Logger.PrintDebug("GameLoadManager.checkDownloadOrStartGame gameId=" + gameId);
        MainThread.Instance.StartCoroutine(getPackageSize(gameId));
    }
    IEnumerator getPackageSize(EnumGameID gameId)
    {
        var downLoadTotalSzeHandle  =Addressables.GetDownloadSizeAsync(gameId.ToString());
        yield return downLoadTotalSzeHandle;
        long downLoadTotalSze = (downLoadTotalSzeHandle.Result / 1024 / 1024);
        string str = "下载总量=" + downLoadTotalSze + "M";
        Logger.PrintDebug("GameLoadManager.checkDownloadOrStartGame " + str);
        GameLoadInfo gameInfo = new GameLoadInfo();
        gameInfo.gameId = gameId;
        gameInfo.size = (int)downLoadTotalSze;

        UIViewManager.Instance.Open(UIViewEnum.Platform_Game_Update_View, gameInfo);
    }
    public void BackPlatform(EnumGameID gameId)
    {

    }
    private void returnToMain()
    {
        PreloadManager.Instance.BackToScene(ProjectControler.basePackage);
    }
}