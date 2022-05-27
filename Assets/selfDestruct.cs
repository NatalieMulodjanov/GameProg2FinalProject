using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public GameObject helicopter;
    public GameObject helicopterMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        helicopter = GameObject.Find("HelicopterV2");
        helicopterMaterial = GameObject.Find("Helicopter(Clone)");
        if (helicopter != null && helicopterMaterial != null && helicopter.GetComponent<Transform>().position.z > 50) {
            // Debug.Log("Destroy me");
            Destroy(helicopter);
            Destroy(helicopterMaterial);
        }
    }
}
