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

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="audio">Sound effect, given by the PlayerSFXManager</param>
    public void PlaySFX(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    /// <summary>
    /// Sets the value of the volume of the Audio Source of the SFX Manager
    /// </summary>
    /// <param name="volume"></param>
    public void SetValue(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }
}
