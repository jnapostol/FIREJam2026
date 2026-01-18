using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource _sfxSource;
    [SerializeField]private AudioSource _musicSource;

    public List<AudioResource> SFXClips;
    public List<AudioClip> MusicClips;

    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayResource(int index)
    {
        _sfxSource.resource = SFXClips[index];
        _sfxSource.Play();
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
