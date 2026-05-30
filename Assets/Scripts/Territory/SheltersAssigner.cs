using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltersAssigner : MonoBehaviour
{
    [SerializeField] private ShelterPoint[] _points;
    [SerializeField] private GameObject[] _shelters;
    [SerializeField] private GameObject[] _Backgrounds;

    private int _randomIndex;

    private void Start()
    {
        _randomIndex = Random.Range(0, _points.Length);
    }

    public void Mixing()
    {
        for (int i = 0;i<_points.Length;i++)
        {
            _points[i].SetFree(true);
        }
        for (int i = 0;i< _shelters.Length;i++)
        {

            while (_points[_randomIndex].GetFree() == false)
            {
                _randomIndex = Random.Range(0, _points.Length);
            }
            _shelters[i].transform.position = _points[_randomIndex].transform.position;
            _points[_randomIndex].SetFree(false);
        }
        for (int i = 0; i < _Backgrounds.Length; i++)
        {
            _Backgrounds[i].SetActive(false);
        }
        int randomBgIndex = Random.Range(0, _Backgrounds.Length);
        _Backgrounds[randomBgIndex].SetActive(true);

    }


}
