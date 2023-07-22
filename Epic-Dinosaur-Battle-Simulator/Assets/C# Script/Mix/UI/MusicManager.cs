using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;
    int randomIndex;
    private bool wasMusicDisgusted;

    void Start()
    {
        // Przy starcie, wywo³ujemy funkcjê do losowego odtwarzania muzyki
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
        else if (!audioSource.isPlaying)
        {
            // Jeœli dŸwiêk siê zakoñczy³, inkrementujemy indeks utworu i odtwarzamy kolejny
            randomIndex = (randomIndex + 1) % musicClips.Length;
            AudioClip randomClip = musicClips[randomIndex];

            // Przypisujemy wybrany utwór do AudioSource i odtwarzamy
            audioSource.clip = randomClip;
            PlayMusic();
        }
    }
}