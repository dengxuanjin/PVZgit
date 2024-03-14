using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> dictAudio;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    // ������Ƶ
    public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);

    }
    // ��ȡ��Ƶ

    public AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path))
        {
            dictAudio[path] = LoadAudio(path);
        }
        return dictAudio[path];
    }

    public void PlayBGM(string name, float volume = 1.0f)
    {
        audioSource.Stop();
        audioSource.clip = GetAudio(name);
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }
    public void PlaySound(string path, float volume = 1f)
    {
        // PlayOneShot����ͬʱ������Ч
        this.audioSource.PlayOneShot(LoadAudio(path),volume);
    }
    public void PlaySound(AudioSource audiosource, string path, float volume = 1f)
    {
        // PlayOneShot����ͬʱ������Ч
        audioSource.PlayOneShot(LoadAudio(path), volume);
       
    }


}
