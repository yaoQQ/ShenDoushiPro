using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
	public abstract void Play(AudioSource source);
	public abstract void PlayOneShot(AudioSource source);
	public abstract void PlayOneShotAtPos(AudioSource source,Vector3 pos);
	
}