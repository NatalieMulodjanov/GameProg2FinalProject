using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour
{
    float speed = 2f;
    public GameObject[] myObjects;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, speed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() {
        int randomIndex = Random.Range(0, myObjects.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(10,-140), 0f, Random.Range(-60,90));
        Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
    }
}
