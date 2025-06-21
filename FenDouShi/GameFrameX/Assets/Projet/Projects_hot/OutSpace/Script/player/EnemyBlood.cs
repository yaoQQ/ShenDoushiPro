
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBlood : MonoBehaviour
{
    private Image blood;
    private Image bloodFade;
    private Text bloodText;
    private Text nameText;
    private Image bg;
    private float _bloodValue = 100;
    private float _totalVaue = 100;
    private float booldFadeBlooad = 100;
    private bool isUpdateBloodBg = false;
    private float countDelayTime = 0.5f;//时间内没攻击 不更新血条
    void Awake()
    {
        bg = this.transform.Find("bg").GetComponent<Image>();
        bloodFade = this.transform.Find("bloodFade").GetComponent<Image>();
        blood = this.transform.Find("blood").GetComponent<Image>();
        bloodText = this.transform.Find("bloodText").GetComponent<Text>();
       
        nameText = bg.transform.Find("nameText").GetComponent<Text>();
        booldFadeBlooad = _totalVaue;
    }

    private void updateBlooadType()
    {
        if (_totalVaue < 100)
        {
            bg.rectTransform.sizeDelta = new Vector2(200, 100);
            bloodFade.rectTransform.sizeDelta = new Vector2(200, 100);
            blood.rectTransform.sizeDelta = new Vector2(200, 100);
        }
        else
        {
            bg.rectTransform.sizeDelta = new Vector2(400, 100);
            bloodFade.rectTransform.sizeDelta = new Vector2(400,100);
            blood.rectTransform.sizeDelta = new Vector2(400, 100);
        }
    }

    public float SetBloodValue(float value)
    {
        _bloodValue = value;
        float percent = _bloodValue / _totalVaue;
        updateView(percent);
        isUpdateBloodBg = true;
        return _bloodValue;

    }
    private void Update()
    {
        if (isUpdateBloodBg)
        {

            if (booldFadeBlooad <= _bloodValue)
            {
                isUpdateBloodBg = false;
                countDelayTime = 0.5f;
            }

            countDelayTime -= Time.deltaTime;
            if (countDelayTime <= 0)
            {
                float fadeSpeed = booldFadeBlooad - _bloodValue;
                booldFadeBlooad = booldFadeBlooad - fadeSpeed * Time.deltaTime;
                float percentBg = booldFadeBlooad / _totalVaue;
                updateBackBloodBg(percentBg);

            }
        }
    }
    public void SetBloodName(string str)
    {
        nameText.text = str;
        //初始化背景血量
        float percentBg = _bloodValue / _totalVaue;
        booldFadeBlooad = _bloodValue;
        updateBackBloodBg(percentBg);
    }
    //收到打击
    public float damage(float damageValue, GameObject hitFromTarget)
    {
        // Debug.Log("damageValue="+ damageValue);
        _bloodValue -= damageValue;
        if (_bloodValue < 0)
        {
            _bloodValue = 0;
        }
        SetBloodValue(_bloodValue);
      
        return _bloodValue;
    }


    private void updateView(float percentValue)
    {
        //blood.rectTransform.sizeDelta = new Vector2(percentValue * 250, 30);
        blood.fillAmount = percentValue;
        bloodText.text = (int)(_bloodValue) + "/" + _totalVaue;

    }
    private void updateBackBloodBg(float percentValue)
    {
        bloodFade.fillAmount = percentValue;
    }
    public float totalVaue
    {
        set
        {
            _totalVaue = value;
        }
        get
        {
            return _totalVaue;
        }
    }
}

