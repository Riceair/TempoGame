using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjGenerator : MonoBehaviour
{
    public GameObject hitObj_R, hitObj_B, GeneratePoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(hitObj_R, GeneratePoint.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
