using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelfDestruct : MonoBehaviour
{
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car = GameObject.Find("Car(Clone)");
        if (car != null && car.GetComponent<Transform>().position.y < 0) {
            // Debug.Log("Destroy Car");
            Destroy(car);
        }
    }
}
