using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;
    int randomIndex;
    private bool wasMusicDisgusted;

    void Start()
    {
        // Przy starcie, wywo�ujemy funkcj� do losowego odtwarzania muzyki
        PlayRandomMusic();
        wasMusicDisgusted = GameManager.Instance.dynamicData.WantMusic;
    }

    void Update()
    {
        OnAudioClipRead();
        if (wasMusicDisgusted == GameManager.Instance.dynamicData.WantMusic)
        {
            PlayRandomMusic();
            wasMusicDisgusted = GameManager.Instance.dynamicData.WantMusic;
        }
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
        else if (!audioSource.isPlaying)
        {
            // Je�li d�wi�k si� zako�czy�, inkrementujemy indeks utworu i odtwarzamy kolejny
            randomIndex = (randomIndex + 1) % musicClips.Length;
            AudioClip randomClip = musicClips[randomIndex];

            // Przypisujemy wybrany utw�r do AudioSource i odtwarzamy
            audioSource.clip = randomClip;
            PlayMusic();
        }
    }
}