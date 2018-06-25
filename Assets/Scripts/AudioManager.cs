using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Referencees

    private static AudioManager instance = null;

    #endregion

    #region Fields

    private AudioSource audioBGM;
    private AudioSource audioMovement;
    private AudioSource audioEnvironment;

    [SerializeField] private float defaultSoundLevel = 0.7f;

    #endregion

    #region Properties

    [SerializeField] private AudioClip menuMusic;

    [SerializeField] private AudioMixerGroup groupMusic;

    [SerializeField] private AudioMixerGroup groupSFX;



    #endregion

    #region Unity methods

    void Awake()
    {
        audioBGM = AddAudio(menuMusic, true, true, defaultSoundLevel, groupMusic);
       // audioMovement = AddAudio(name, false, false, 1f, groupSFX);
       // audioEnvironment = AddAudio(name, false, false, 0.3f, groupSFX);


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioBGM.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (audioBGM.isPlaying)
            {
                if (audioBGM.volume == 0)
                {
                    audioBGM.volume = defaultSoundLevel;
                }
                else
                {
                    audioBGM.volume = 0;
                }
            }
        }
    }

    #endregion

    #region Play SFX and music metohds

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol, AudioMixerGroup group)
    {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.outputAudioMixerGroup = group;

        return newAudio;

    }

    public void PlayEnvironment()
    {
        audioEnvironment.Play();
    }

    public void ChangeMusic(AudioSource source, AudioClip music, bool letItRepeat)
    {
        if (source.clip.name == music.name) { return; }

        source.Stop();
        source.clip = music;
        source.Play();
    }

    #endregion
}
