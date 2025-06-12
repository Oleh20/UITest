using UnityEngine;

public class UIAudioManager : Singleton<UIAudioManager>
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioSource _audioSource;

    public void PlayClick()
    {
        PlaySound(_clickSound);
    }

    public void PlayHover()
    {
        PlaySound(_hoverSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || _audioSource == null) return;
        _audioSource.PlayOneShot(clip);
    }
}
