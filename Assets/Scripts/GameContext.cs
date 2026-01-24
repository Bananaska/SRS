using Unity.VisualScripting;
using UnityEngine;
public class GameContext : MonoBehaviour
{
    private static GameContext _instance;
    public static GameContext Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameContext").AddComponent<GameContext>();
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

}
