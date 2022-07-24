using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    Movement controller;

    void Start()
    {
        controller = GetComponent<Movement>();
    }
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartWinSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartWinSequence()
    {
        // TODO: add SFX
        // TODO: add Particle effects
        controller.enabled = false;
        Invoke(nameof(LoadNextLevelOrReset), levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // TODO: add SFX
        // TODO: add Particle effects
        controller.enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void LoadNextLevelOrReset()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int levelToLoad = nextLevelIndex < SceneManager.sceneCountInBuildSettings ? nextLevelIndex : 0;

        SceneManager.LoadScene(levelToLoad);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
