using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip winAudio;
    [SerializeField] AudioClip crashAudio;

    Movement controller;
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        controller = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

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
        // TODO: add Particle effects
        playAudio(winAudio);
        EnterTransitionState();
        Invoke(nameof(LoadNextLevelOrReset), levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // TODO: add Particle effects
        playAudio(crashAudio);
        EnterTransitionState();
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void playAudio(AudioClip clip)
    {
        audioSource.Stop(); // to stop the thrusting sound
        audioSource.PlayOneShot(clip);
    }

    private void EnterTransitionState()
    {
        isTransitioning = true;
        controller.enabled = false;
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
