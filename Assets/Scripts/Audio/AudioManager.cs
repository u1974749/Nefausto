using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    public AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if(s == null)
            return;
        s.source.Play();
    }

    public void Stop()
    {
       for(int i= 0; i<sounds.Count; i++)
        {
            sounds[i].source.Stop();
        }
    }
}
