using UnityEngine;

public class PlayerSFXManager : MonoBehaviour, IValueAdjustable
{
    //Audio Variables
    [Header("SFX Manager Attributes")]
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
        SetValue(0.5f);
    }


    public void PlaySFX(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void SetValue(float volume)
    {
        audioSource.volume = volume;
    }
}
