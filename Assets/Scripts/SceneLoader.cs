using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
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

    }
}
