using UnityEngine.Audio;
using System;
using UnityEngine;

using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Slider slider;
    

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //slider.value = (0.5f);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = slider.value;
            s.source.pitch = 1;
        }
        
    }

    private void Start()
    {
        //Add main menu theme
    }


    void Update()
    {

        foreach (Sound s in sounds)
        {
            //s.source.volume = slider.value;
        }
        
    }




    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }
    
    public bool HasEnded(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {Debug.LogWarning("Sound is Null"); return false; }
        if (s.source.isPlaying == false)
        { return true; }
        else { return false; }
    }

}