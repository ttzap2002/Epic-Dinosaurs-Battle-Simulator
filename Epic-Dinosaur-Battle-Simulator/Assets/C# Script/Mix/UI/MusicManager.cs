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
        // Przy starcie, wywo�ujemy funkcj� do losowego odtwarzania muzyki
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
        // Je�li nie ma �adnych utwor�w w kolekcji, ko�czymy funkcj�
        if (musicClips.Length == 0)
        {
            Debug.LogWarning("Brak dost�pnych utwor�w muzycznych.");
            return;
        }
        if(GameManager.Instance.dynamicData.WantMusic)
        {
            // Losowo wybieramy utw�r z tablicy musicClips
            randomIndex = Random.Range(0, musicClips.Length);
            AudioClip randomClip = musicClips[randomIndex];

            // Przypisujemy wybrany utw�r do AudioSource i odtwarzamy
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
            StopMusic();
    }

    public void PlayMusic()
    {
        // W��czamy muzyk�, odtwarzaj�c przypisany utw�r
        audioSource.Play();
    }

    public void StopMusic()
    {
        // Zatrzymujemy odtwarzanie muzyki
        audioSource.Stop();
    }

    // Ta funkcja jest wywo�ywana po zako�czeniu odtwarzania d�wi�ku
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
                // Je�li d�wi�k si� zako�czy�, inkrementujemy indeks utworu i odtwarzamy kolejny
                randomIndex = (randomIndex + 1) % musicClips.Length;
                AudioClip randomClip = musicClips[randomIndex];

                // Przypisujemy wybrany utw�r do AudioSource i odtwarzamy
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