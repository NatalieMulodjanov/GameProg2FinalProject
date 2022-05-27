using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelfDestruct1 : MonoBehaviour
{
    public GameObject car1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car1 = GameObject.Find("Car(Clone)");
        if (car1 != null && car1.GetComponent<Transform>().position.y < 0) {
            // Debug.Log("Destroy Car");
            Destroy(car1);
        }
    }
}
