using UnityEngine;

using System.Collections.Generic;

public class GameAudioManager : SingleMonobehaviour<GameAudioManager>
{
    public AudioClip audioClip = null;
    public AudioSource AudioSource = null;
    private Dictionary<string,AudioClip> audioDic = new Dictionary<string, AudioClip>();

    public void InitializeAudio()
    {

       if (AudioSource == null)
       {
          AudioSource = gameObject.AddComponent<AudioSource>();
          AudioSource.loop = false;
        }
        GameObject.DontDestroyOnLoad(this);
    }
    private void PlayAudioClip(AudioClip clip)
    {
       
        audioClip = AudioSource.clip = clip;
        PlayCurrentAudioClip();
    }
    public void PlayAudio(string audioName)
    {
        if (!audioDic.ContainsKey(audioName))
        {
            ResLoadManager.LoadAsync(AssetType.Audio, ProjectControler.littlePrincePackage, audioName, (relativePath, res) =>
            {
                if (res == null)
                {
                    return;
                }
                AudioClip clip= res as AudioClip;
               audioDic[audioName]= clip;
                PlayAudioClip(audioDic[audioName]);

            });
            //AsyncOperationHandle handle= Addressables.LoadAssetAsync<AudioClip>(audioName);
            //handle.Completed += (op) =>
            //{
            //    if (op.Status == AsyncOperationStatus.Succeeded)
            //    {
            //        AudioClip clip = (AudioClip)op.Result;
            //        audioDic[audioName] = clip;
            //        PlayAudioClip(clip);
            //    }
            //};
        }
        else
        {
            PlayAudioClip(audioDic[audioName]);
        }

       
    }
    public void PlayCurrentAudioClip()
    {
        if(AudioSource.clip!=null)
        AudioSource.Play();
    }
    public void StopAudioClipSource()
    {
        AudioSource.clip = audioClip;
        AudioSource.loop = true;
        AudioSource.mute = false;
        AudioSource.Stop();
    }
    public void Detroy()
    {
        audioClip = null;
        AudioSource = null;
        audioDic.Clear();
        GameObject.Destroy(this.gameObject);
    }
}