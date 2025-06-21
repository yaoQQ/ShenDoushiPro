using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestOutSpaceFun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject testMenu;
    public Slider MusicSlider;
    public Slider MasterVolumeSlider;

    private void Start()
    {
        this.MasterVolumeSlider.onValueChanged.AddListener(SetGameSound);
        this.MusicSlider.onValueChanged.AddListener(SetMusicSound);

    }

    private void SetGameSound(float value)
    {
        if (OutSpaceAudioManager.Instance.audioSource)
            OutSpaceAudioManager.Instance.audioSource.volume = value;
    }
    private void SetMusicSound(float value)
    {
        #if SHOW_MUSIC_DATA
        if (GetAudioDataManager.Instance.getMusicAudioSource)
            GetAudioDataManager.Instance.getMusicAudioSource.volume = value;
#endif
    }
}
