using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_MUSIC_VOLUME, .3f);
        audioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1)
        {
            volume = 0f;
        }

        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
