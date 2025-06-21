
using UnityEngine;
using UnityEngine.UI;

public class SelectTextLog : MonoBehaviour
{
    // Start is called before the first frame update
    private Text LogText;
    void Start()
    {
        LogText = this.transform.Find("Group/Window/Text").GetComponent<Text>();
        //this.gameObject.SetActive(false);
    }

    public void setText(string str)
    {
        LogText.text = str;
    }
}
