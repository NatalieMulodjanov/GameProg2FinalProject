using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelfDestruct2 : MonoBehaviour
{
    public GameObject car2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car2 = GameObject.Find("Car(Clone)");
        if (car2 != null && car2.GetComponent<Transform>().position.y < 0) {
            // Debug.Log("Destroy 2");
            Destroy(car2);
        }
    }
}
