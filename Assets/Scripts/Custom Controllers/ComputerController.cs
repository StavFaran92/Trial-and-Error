using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private Image computerBackground;
    [SerializeField] private Image playButton;
    [SerializeField] private Image pauseButton;
    [SerializeField] private VideoPlayer playableVideo;

    private void Start()
    {
        playButton.enabled = true;
        pauseButton.enabled = false;
    }

    public void SetPlayableVideo(VideoPlayer playableVideo)
    {
        if (this.playableVideo != null)
        {
            var playableVideoRenderer = this.playableVideo.GetComponent<MeshRenderer>();

            if (playableVideoRenderer != null) { playableVideoRenderer.enabled = false; }
        }

        this.playableVideo = playableVideo;

        var renderer = playableVideo.GetComponent<MeshRenderer>();

        if (renderer != null) { renderer.enabled = false; }
    }

    public void ResetComputer()
    {
        computerBackground.enabled = true;

        var renderer = playableVideo.GetComponent<MeshRenderer>();

        if (renderer != null) { renderer.enabled = false; }
    }

    public void TogglePlayPause()
    {
        if (playButton.enabled)
        {
            PlayVideo();
        }
        else
        {
            PauseVideo();
        }
    }

    public void PlayVideo()
    {
        computerBackground.enabled = false;
        playButton.enabled = false;
        pauseButton.enabled = true;

        var renderer = playableVideo.GetComponent<MeshRenderer>();

        if (renderer != null && !renderer.enabled)
        {
            renderer.enabled = true;
        }

        playableVideo.Play();
    }

    public void PauseVideo()
    {
        computerBackground.enabled = false;
        playButton.enabled = true;
        pauseButton.enabled = false;

        var renderer = playableVideo.GetComponent<MeshRenderer>();

        if (renderer != null && !renderer.enabled)
        {
            renderer.enabled = true;
        }

        playableVideo.Pause();
    }
}
