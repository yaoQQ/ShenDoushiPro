using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;

public enum GameHardLevelEnum
{
    Easy,
    Normal,
    Hard,

    none
}
public class OutSpaceGameManager : Singleton<OutSpaceGameManager>
{
    private GameObject _gameCanvas;

    public void Init()
    {
        _gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
    }
    public GameObject gameCanvas
    {
        get
        {
            if (_gameCanvas == null)
            {
                _gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
            }
            return _gameCanvas;
        }
    }

    private GameHardLevelEnum _currGameHard;
    public GameHardLevelEnum GameHardLevel
    {
        get
        {
            return _currGameHard;
        }
        set
        {
            _currGameHard = value;
        }
    }
    public void ShowLevel()
    {
       var op=Addressables.InstantiateAsync("LevelInfoPanel");
        op.Completed += opFun => {
            if (opFun.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Logger.PrintColor("yellow", "LevelInfoPanel load success!!!");
            }
        };
    }
    public void PauseGame()
    {
        Logger.PrintColor("yellow","############################################PauseGame()");
        Time.timeScale = 0;
        if (_gameCanvas != null)
            _gameCanvas.SetActive(false);
    }
    public void ResumeGame()
    {
        Logger.PrintColor("yellow", "############################################ResumeGame()");
        Time.timeScale = 1;
        if (_gameCanvas)
        {
            Debug.Log("_gameCanvas.SetActive(true);");
            _gameCanvas.SetActive(true);
        }
    }
}
