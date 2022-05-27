using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelfDestruct3 : MonoBehaviour
{
    public GameObject car3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car3 = GameObject.Find("Car(Clone)");
        if (car3 != null && car3.GetComponent<Transform>().position.y < 0) {
            // Debug.Log("Destroy 3");
            Destroy(car3);
        }
    }
}
