using UnityEngine;

//Sound Manager for playing a variety of sounds
public class SoundManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    public GameObject source;

    public static SoundManager instance;
    public AudioSource atmosphere;

    [Range(0, 1)] public float masterVolume;

    private void Start()
    {
        atmosphere.Play();
        atmosphere.volume = 0.025f * masterVolume;
        instance = this;
    }

    public void PlaySound(string soundName)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == soundName) //Search for matching sound name
            {
                //Create and setup GO
                GameObject source_ = Instantiate(source, transform.position, Quaternion.identity);
                source_.name = "Sound: " + sound.name;
                AudioSource audioSource = source_.GetComponent<AudioSource>();

                //Set clip, volume, random pitch, and play
                audioSource.clip = sound.clip;
                audioSource.volume = sound.volume;// * soundSlider.value;

                //Makes sounds slightly less annoying
                float pitch = Random.Range(sound.pitch.x, sound.pitch.y);
                audioSource.pitch = pitch;

                Debug.Log($"Playing sound: {soundName}");
                audioSource.Play();

                //Cleanup
                Destroy(source_, sound.clip.length);
                return;
            }
        }

        Debug.Log($"Couldn't play sound: {soundName} as it doesn't exist");
    }
}

//Sound Class
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
    public Vector2 pitch;
}