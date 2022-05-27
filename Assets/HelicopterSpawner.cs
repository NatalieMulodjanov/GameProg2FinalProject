using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public GameObject objectToBeSpawned;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    Vector3 vector3 = new Vector3(0f,0f,-45f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        Instantiate(objectToBeSpawned, vector3, Quaternion.Euler(0,45,0));
        if(stopSpawning == true) {
            CancelInvoke("SpawnObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
