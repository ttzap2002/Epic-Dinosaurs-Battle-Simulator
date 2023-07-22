using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    void Start()
    {
        // Przy starcie, wywo³ujemy funkcjê do losowego odtwarzania muzyki
        PlayRandomMusic();
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
            int randomIndex = Random.Range(0, musicClips.Length);
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
}