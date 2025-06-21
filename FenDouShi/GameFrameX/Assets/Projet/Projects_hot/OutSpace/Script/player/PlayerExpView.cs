using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpView : MonoBehaviour
{
    private Image expImg;
    private Text expLabel;
    private void Awake()
    {
        expImg = this.transform.Find("exp").GetComponent<Image>();
        expLabel = this.transform.Find("expLabel").GetComponent<Text>();
    }
    private void Start()
    {
    
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.UpdatePlayerEXP, UpdatePlayerExp);
    }
    private void UpdatePlayerExp(string noticeType, BaseNotice notice)
    {
        ObjectNotice obj = (ObjectNotice)notice;
        List<float> expList = (List<float>)obj.GetObj();
        if (expImg)
        {
            expImg.fillAmount = expList[0] / expList[1];
        }
     
        if(expLabel)
        expLabel.text = expList[0] + "/" + expList[1];
    }
}
