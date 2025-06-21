using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public Image targetImage;
    public float alphaSpeed = 1;

    private Color color;
    private float alphaNum = 1;
    private float countDelay = 1;
    // Update is called once per frame
    void Awake()
    {
        if (targetImage==null)
        {
            targetImage = this.transform.GetComponent<Image>();
        }
        color = Color.white;
    }
    private void Update()
    {
        if (countDelay>0)
        {
            countDelay = countDelay - Time.deltaTime;
            return;
        }
        alphaNum = alphaNum - Time.deltaTime * alphaSpeed;
        targetImage.color = new Color(1, 1, 1, alphaNum);
        if (alphaNum < 0.1f|| alphaNum>1)
        {
           
            alphaSpeed *= -1;
        }
        countDelay = 1;
    }
}
