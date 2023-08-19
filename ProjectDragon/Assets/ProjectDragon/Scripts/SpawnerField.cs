using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerField : MonoBehaviour
{
    public float spawnFrequency;
    public Transform spawnPoint;
    public GameObject prefab;


    private float curSpawnTime = 0;

    public void Spawn()
    {
        if (Time.time < curSpawnTime + spawnFrequency)
            return;

        curSpawnTime = Time.time;

        var newInstance = Instantiate(prefab);
        newInstance.transform.position = spawnPoint.position;
        newInstance.SetActive(true);

    }
}
