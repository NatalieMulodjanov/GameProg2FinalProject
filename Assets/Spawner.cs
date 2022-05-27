using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToBeSpawned;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    Vector3 vector3 = new Vector3(7.5f,0.5f,-45f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        Instantiate(objectToBeSpawned, vector3, Quaternion.identity);
        if(stopSpawning == true) {
            CancelInvoke("SpawnObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
