using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillMaker : MonoBehaviour
{
    [SerializeField] GameObject pillPrefab;
    [SerializeField] List<GameObject> spawnPlaces;
    [SerializeField] float delayTime = 15f;
    [SerializeField] float spawnTIme = 15f;

    

    void Start()
    {
        InvokeRepeating("spawnPill", delayTime, spawnTIme);
    }

    public void spawnPill()
    {
        Instantiate(pillPrefab, spawnPlaces[Random.Range(0, spawnPlaces.Capacity)].transform.position, Quaternion.identity);
    }
}
