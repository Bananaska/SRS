using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _loopToFirstScene;

    public static SceneLoader Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("SceneLoader уже существует");
            return;
        }

        Instance = this;
    
        DontDestroyOnLoad(gameObject);
    }


    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex += 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            if (_loopToFirstScene == false)
            {
                Debug.Log("Опасненько");
                return;
            }
            nextIndex = 0;
        }
        SceneManager.LoadScene(nextIndex);
    }

    public void LoadSceneByIndex(int index)
    {
        if (index >= SceneManager.sceneCountInBuildSettings)
        {
            if (_loopToFirstScene == false)
            {
                Debug.Log("Опасненько");
                return;
            }
            index = 0;
        }
        SceneManager.LoadScene(index);
    }
}
