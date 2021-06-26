using UnityEngine;
using DG.Tweening;
using AC;

/// <summary>
/// Singleton which manages game state and behaviour. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float originalSFXVolume;
    private float originalMusicVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Setup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Setup()
    {
        DontDestroyOnLoad(Instance);
        DOTween.Init();

        originalSFXVolume = Options.GetSFXVolume();
        originalMusicVolume = Options.GetMusicVolume();
    }

    void OnEnable()
    {
        EventManager.OnMenuElementClick += ElementClick;
    }

    void OnDisable()
    {
        EventManager.OnMenuElementClick += ElementClick;
    }

    private void ElementClick(Menu _menu, MenuElement _element, int _slot, int _buttonPressed)
    {
        if (_element.title == "Sound Volume")
        {
            var soundVolumeToggle = _element as MenuToggle;

            if (soundVolumeToggle.isOn)
            {
                Options.SetSFXVolume(originalSFXVolume);
                Options.SetMusicVolume(originalMusicVolume);
            }
            else
            {
                Options.SetSFXVolume(0);
                Options.SetMusicVolume(0);
            }
        }
    }
}
