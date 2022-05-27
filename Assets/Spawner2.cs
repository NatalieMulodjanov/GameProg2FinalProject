using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
     public GameObject objectToBeSpawned;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    Vector3 vector3 = new Vector3(95f,0.5f,7.5f);
            
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        Instantiate(objectToBeSpawned, vector3, Quaternion.Euler(0,270,0));
        if(stopSpawning == true) {
            CancelInvoke("SpawnObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
