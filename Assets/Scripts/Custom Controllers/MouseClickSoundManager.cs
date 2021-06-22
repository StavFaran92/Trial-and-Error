using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MouseClickSoundManager: MonoBehaviour
{
    private AudioSource mouseClickSound;

    void Start()
    {
        mouseClickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !mouseClickSound.isPlaying)
        {
            mouseClickSound.Play();
        }
    }
}
