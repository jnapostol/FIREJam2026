using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _sfxSource;
    private AudioSource _musicSource;

    public List<AudioClip> SFXClips;
    public List<AudioClip> MusicClips;

    public static AudioManager Instance;
    private void Awake()
    {
        _sfxSource = GetComponent<AudioSource>();
        _musicSource = GetComponent<AudioSource>();

        Instance = this;
    }

    public void PlayOneShotSFX(int index)
    {
        _sfxSource.PlayOneShot(SFXClips[index]);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
