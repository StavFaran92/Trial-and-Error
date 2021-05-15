using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Singleton which manages game state and behaviour. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }
}
