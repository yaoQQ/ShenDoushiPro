using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum farward
{
    left,
    right,
    down,
    up,

    none
}

public class PlayerBlood : MonoBehaviour
{
    private Image blood;
    private Image bloodFade;
    private Text bloodText;
    private Text nameText;
    private float _bloodValue = 100;
    private float _totalVaue = 100;
    private float booldFadeBlooad = 100;
    private bool isUpdateBloodBg = false;
    private float countDelayTime = 1;//时间内没攻击 不更新血条
    private BloodDamage bloodDamaget;
    private float recoverLife = 0;
    private float recoverLifeTime = 1;
    void Awake()
    {
        blood = this.transform.Find("blood").GetComponent<Image>();
        bloodText = this.transform.Find("bloodText").GetComponent<Text>();
        bloodFade = this.transform.Find("bloodFade").GetComponent<Image>();
        nameText = this.transform.Find("nameText").GetComponent<Text>();
        booldFadeBlooad = _totalVaue;

        bloodDamaget = this.transform.Find("BloodLog").GetComponent<BloodDamage>();

    }
    void Start()
    {
        NoticeManager.Instance.AddNoticeLister(OutSpaceNotice.PlayerAttriDataUpdate, PlayerAttriChange);
    }

    /// <summary>
    /// 玩家属性改变
    /// </summary>
    private void PlayerAttriChange(string noticeType, BaseNotice notice)
    {

        ObjectNotice obj = (ObjectNotice)notice;
        AttriInfo attriInfo = obj.GetObj() as AttriInfo;
        if (attriInfo.attriType == AttriEnum.totalLife)
        {
            _totalVaue = _totalVaue + _totalVaue * OutSpacePlayerInfoManager.Instance.CharacterAddInfo.totalLife;
            _bloodValue = _totalVaue;
            booldFadeBlooad = _totalVaue;
            SetBloodValue(_totalVaue);
            Logger.PrintColor("blue", "更新AttriEnum.totalLife=" + attriInfo.currValue + " _totalVaue=" + _totalVaue);
        }
        else if (attriInfo.attriType == AttriEnum.recoverLife)
        {
            recoverLife = OutSpacePlayerInfoManager.Instance.CharacterAddInfo.recoverLife;
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
        if (recoverLife > 0)//恢复血量
        {
            recoverLifeTime -= Time.deltaTime;
            if (recoverLifeTime <= 0)
            {
                _bloodValue += recoverLife;
                SetBloodValue(_bloodValue);
                recoverLifeTime = 1;
            }
        }
        if (isUpdateBloodBg)
        {

            if (booldFadeBlooad <= _bloodValue)
            {
                isUpdateBloodBg = false;
                countDelayTime = 1;
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
    }
    //收到打击
    public float damage(float damageValue, GameObject hitFromTarget)
    {
        // Debug.Log("damageValue="+ damageValue);
        _bloodValue = _bloodValue - damageValue + OutSpacePlayerInfoManager.Instance.CharacterAddInfo.defend;
        if (_bloodValue < 0)
        {
            _bloodValue = 0;
        }
        SetBloodValue(_bloodValue);


        StartCoroutine(showDamageEffect(blood));
        StartCoroutine(damageFard(hitFromTarget));

        return _bloodValue;
    }



    IEnumerator showDamageEffect(Image img, bool isHide = false)
    {
        float time = 0.2f;
        img.CrossFadeAlpha(0, time, true);
        yield return new WaitForSeconds(time);
        img.CrossFadeAlpha(1, time, true);
        yield return new WaitForSeconds(time);
        img.CrossFadeAlpha(0, time, true);
        yield return new WaitForSeconds(time);
        img.CrossFadeAlpha(1, time, true);
        yield return new WaitForSeconds(time);
        if (isHide)
        {
            img.gameObject.SetActive(false);
        }
    }
    IEnumerator damageFard(GameObject hitTarget)
    {

        farward fa = worldPosToMap2(hitTarget);
        if (fa == farward.none)
        {
            yield return null;
        }
        Image img = bloodDamaget.damageFard(fa);
        // StartCoroutine(showDamageEffect(img,true));
        img.color = new Color(img.color.b, img.color.g, img.color.r, 1);
        img.gameObject.SetActive(true);
        img.CrossFadeAlpha(0, 2, true);
        yield return new WaitForSeconds(2);
        img.gameObject.SetActive(false);
    }

    private farward worldPosToMap2(GameObject enemyStruct)
    {
        GameObject obj = enemyStruct;
        if (obj == null)
            return farward.none;
        Vector3 pos = obj.transform.position;
        Vector3 targetPos = OutSpaceCameraManager.Instance.Player.position;
        float angel = MyUtils.Angle(targetPos, OutSpaceCameraManager.Instance.MainCamera.transform.forward, obj.transform.position);
        if (angel >= 0 && angel <= 35)
        {
            return farward.up;
        }
        if (angel >= 145 && angel <= 180)
        {
            return farward.down;
        }


        Vector3 left = OutSpaceCameraManager.Instance.MainCamera.transform.TransformDirection(Vector3.right);
        Vector3 toOtherL = pos - targetPos;
        if (Vector3.Dot(left, toOtherL) < 0)
        {
            return farward.left;
        }
        else
        {
            return farward.right;
        }

    }

    private void ColorUpdate(float value)
    {
        blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, value);
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
    public void OnDestroy()
    {
        NoticeManager.Instance.RemoveNoticeLister(OutSpaceNotice.PlayerAttriDataUpdate, PlayerAttriChange);
    }
}

