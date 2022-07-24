using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int maxLevel = 1;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("We bumped into a friendly object");
                break;
            case "Finish":
                LoadNextLevelOrReset();
                break;
            default:
                ReloadLevel();
                break;
        }

    }

    private void LoadNextLevelOrReset()
    {
        int levelToLoad;
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        levelToLoad = nextLevelIndex > maxLevel ? 0 : nextLevelIndex;
        SceneManager.LoadScene(levelToLoad);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
