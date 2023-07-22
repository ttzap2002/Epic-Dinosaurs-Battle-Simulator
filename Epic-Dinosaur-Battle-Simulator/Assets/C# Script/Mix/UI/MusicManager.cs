using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    void Start()
    {
        // Przy starcie, wywo�ujemy funkcj� do losowego odtwarzania muzyki
        PlayRandomMusic();
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
            int randomIndex = Random.Range(0, musicClips.Length);
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
}