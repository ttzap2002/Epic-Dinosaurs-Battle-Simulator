using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicClips;
    public static float musicIntense;
    int randomIndex;
    private bool wasMusicDisgusted;

    void Start()
    {
        // Przy starcie, wywo³ujemy funkcjê do losowego odtwarzania muzyki
        wasMusicDisgusted = GameManager.Instance.dynamicData.WantMusic;
        MusicManager.musicIntense = GameManager.Instance.dynamicData.musicIntense;
        ImplementIntense();
        PlayRandomMusic();
    }

    void Update()
    {
        if (wasMusicDisgusted != GameManager.Instance.dynamicData.WantMusic)
        {
            wasMusicDisgusted = GameManager.Instance.dynamicData.WantMusic;
        }
        OnAudioClipRead();
    }

    public void PlayRandomMusic()
    {
        // Jeœli nie ma ¿adnych utworów w kolekcji, koñczymy funkcjê
        if (musicClips.Length == 0)
        {
            Debug.LogWarning("Brak dostêpnych utworów muzycznych.");
            return;
        }
        if(GameManager.Instance.dynamicData.WantMusic)
        {
            // Losowo wybieramy utwór z tablicy musicClips
            randomIndex = Random.Range(0, musicClips.Length);
            AudioClip randomClip = musicClips[randomIndex];

            // Przypisujemy wybrany utwór do AudioSource i odtwarzamy
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
            StopMusic();
    }

    public void PlayMusic()
    {
        // W³¹czamy muzykê, odtwarzaj¹c przypisany utwór
        audioSource.Play();
    }

    public void StopMusic()
    {
        // Zatrzymujemy odtwarzanie muzyki
        audioSource.Stop();
    }

    // Ta funkcja jest wywo³ywana po zakoñczeniu odtwarzania dŸwiêku
    void OnAudioClipRead()
    {
        if(!GameManager.Instance.dynamicData.WantMusic)
        {
            StopMusic();
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                // Jeœli dŸwiêk siê zakoñczy³, inkrementujemy indeks utworu i odtwarzamy kolejny
                randomIndex = (randomIndex + 1) % musicClips.Length;
                AudioClip randomClip = musicClips[randomIndex];

                // Przypisujemy wybrany utwór do AudioSource i odtwarzamy
                audioSource.clip = randomClip;
                PlayMusic();
            }
            if(SceneManager.GetActiveScene().name == "MainMenu")
                ImplementIntense();
        }
    }

    public void ImplementIntense()
    {
        audioSource.volume = MusicManager.musicIntense;
    }
}