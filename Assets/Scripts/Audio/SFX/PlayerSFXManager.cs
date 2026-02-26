using UnityEngine;

public class PlayerSFXManager : MonoBehaviour, IVolumeAdjustable
{
    //Audio Variables
    [Header("Music Manager Attributes")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip coin;
    private AudioSource audioSource;
        
    // Properties
    public AudioClip PlayerJumpAudioClip => jumpClip;
    public AudioClip PlayerDeathAudioClip => death;
    public AudioClip PlayerCoinAudioClip => coin;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetVolume(0.0f);
    }


    public void PlaySFX(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
