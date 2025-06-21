using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutSpaceLevel : SingleMonobehaviour<OutSpaceLevel>
{
    public Text waveText;
    public Text waveIndexText;
    public Text enemyText;
    public EnemyBoidsMotionBehaviour boidPrefabe;
    // Start is called before the first frame update
    public List<CreateEnemyWave> waveList = new List<CreateEnemyWave>();
    
    [Header("当前波数")]
    [SerializeField]
    private int currWave = 0;
    [SerializeField]
    private CreateEnemyWave currEnemyWave;

    private float delayTime = 2;
    private void Awake()
    {
       GameObject levelUI= GameObject.Find("LevelUI");
        if (levelUI)
        {
            waveText = levelUI.transform.Find("WaveInfoText").GetComponent<Text>();
            waveIndexText = levelUI.transform.Find("WaveIndexText").GetComponent<Text>();
            enemyText = levelUI.transform.Find("WaveEnemyCountText").GetComponent<Text>();
        }
         if (waveList.Count > 0)
        {
            currEnemyWave = waveList[currWave];
          
        }
        else
        {
            Logger.PrintError("没有设置关卡数据");
        }
    }
    private void Start()
    {

        if (currEnemyWave)
        {
            Logger.PrintColor("red", this.gameObject.name + ":=================OutSpaceLevel Start()==============");
            currEnemyWave.gameObject.SetActive(true);
            UpdateWaveInfo();
            UpdateWaveEnemyCount();
            NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.UpdateWaveMonsterCount, UpdateWaveMonsterCount);
        }
    }

    private void UpdateWaveMonsterCount(string noticeType, BaseNotice notice)
    {
        UpdateWaveEnemyCount();
    }

        // Update is called once per frame
    public void ToUpdate()
    {
        if (currEnemyWave)
        {
            delayTime -= Time.deltaTime;
            if (delayTime > 0)
            {
                return;
            }
            currEnemyWave.ToUpdate(delayTime);
          //  Debug.LogFormat("currEnemyWave.isEnd ={0},MonsterManager.Instance.currEnemyCount={1}", currEnemyWave.isEnd, MonsterManager.Instance.currEnemyCount);
            //currEnemyWave.ToUpdate();
            //跳转下一波
            if ((currEnemyWave.isEnd && MonsterManager.Instance.currEnemyCount <=0))
            {
                //过了当前管卡
                ShowNextWave();
            }
            delayTime = 2;
        }
    }
    private void ShowNextWave()
    {
        currWave++;
       Logger.PrintColor("red", "=========ShowNextWave()====currWave="+ currWave);
        if (currWave>= waveList.Count)
        {
            //下一关
            CommonView.showTopTips("=========通关下一关开启==庆祝动画======");
            currEnemyWave = null;
            return;
        }
        currEnemyWave.gameObject.SetActive(false);
        currEnemyWave = waveList[currWave];
        currEnemyWave.gameObject.SetActive(true);
      //  StartCoroutine(currEnemyWave.showWaveEnemy());
        UpdateWaveInfo();
    }
    private void UpdateWaveInfo()
    {
        if(waveText)
        waveText.text = "Wave:<color='yellow'>" + (currWave + 1)+"</color>";
        
    }
    public void UpdateWaveEnemyCount()
    {
        if (currEnemyWave == null)
        {
            return;
        }
        // Logger.PrintDebug("enemyText="+ enemyText);
        //  Logger.PrintDebug("currEnemyWave=" + currEnemyWave);
        //  Logger.PrintDebug("MonsterManager.Instance.currEnemyCount=" + MonsterManager.Instance.currEnemyCount);
        if (waveIndexText)
        {
            enemyText.text = "EnemyLeft:<color='yellow'>" + MonsterManager.Instance.currEnemyCount + "</color> Time = " + currEnemyWave.waveTotalTime;
            waveIndexText.text = "SmallWave:<color='yellow'>" + currEnemyWave.curentWaveIndex + "/" + currEnemyWave.enemyWaveList.Count + "</color> ";
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Logger.PrintColor("red", "OutSpaceLevel.OnDestroy()");
        NoticeManager.Instance.RemoveNoticeLister(OutSpaceNotice.UpdateWaveMonsterCount, UpdateWaveMonsterCount);
    }
}
