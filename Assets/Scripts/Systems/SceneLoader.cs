using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;

    private bool _loopToFirstScene;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SceneLoader").AddComponent<SceneLoader>();
            }
            return _instance;
        }
    }
    private void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
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
}
