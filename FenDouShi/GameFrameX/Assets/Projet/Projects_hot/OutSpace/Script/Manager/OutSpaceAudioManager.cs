using UnityEngine;
using System.Collections;

/// <summary>
/// 播放场景 子弹 特效等声音
/// </summary>
public class OutSpaceAudioManager : SingleMonobehaviour<OutSpaceAudioManager>
{

    private AudioSource _audioSource;
    private void Start()
    {

    }
    public override void Init()
    {
        if (_audioSource == null)
        {
            _audioSource = this.gameObject.GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = this.gameObject.AddComponent<AudioSource>();
            }
        }
    }
    public AudioSource audioSource
    {
        get
        {
            return _audioSource;
        }
    }
    public void Play(SimpleAudioEvent audioObj)
    {
        if (audioObj)
            audioObj.Play(_audioSource);
    }
    public void PlayOnShot(SimpleAudioEvent audioObj)
    {
        if (audioObj)
            audioObj.PlayOneShot(_audioSource);
        // audioObj.PlayOneShotAtPos(_audioSource, pos);
    }
    public void PlayOnShotAtPos(SimpleAudioEvent audioObj, Vector3 pos)
    {
        if (audioObj)
            audioObj.PlayOneShotAtPos(_audioSource, pos);
    }

}
