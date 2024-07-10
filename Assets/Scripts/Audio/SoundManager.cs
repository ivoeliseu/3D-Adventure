using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = musicSetups.Find(i => i.musicType == musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }
    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    public void PlaySFXByType(SFXType sfxType)
    {
        var sfx = sfxSetups.Find(i => i.musicType == sfxType);
        musicSource.clip = sfx.audioClip;
        musicSource.Play();
    }
    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.musicType == sfxType);
    }

    public void MuteVolume()
    {
        AudioListener.volume = 0;
    }
    public void ResumeVolume()
    {
        AudioListener.volume = 1;
    }
}
public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}
public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03
}
[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}
[System.Serializable]
public class SFXSetup
{
    public SFXType musicType;
    public AudioClip audioClip;
}
