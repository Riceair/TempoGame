using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObj : MonoBehaviour
{
    private int myCode=0;
    private const int RED_CODE=1;
    private const int BLUE_CODE=2;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 generate_position = GameObject.Find("GeneratePoint").transform.position;
        Vector3 hit_position = GameObject.Find("HitPosition").transform.position;
        float distance = Vector3.Distance(generate_position,hit_position);
        speed = distance/2f; //距離除以時間(秒)

        if(name=="HitObj_R(Clone)")
            myCode=RED_CODE;
        else if(name=="HitObj_B(Clone)")
            myCode=BLUE_CODE;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,-Time.deltaTime*speed);
    }

    private void CheckTrigger(Collider other)
    {
        if(other.name=="HitRed")
        {
            if(myCode==RED_CODE)
                Debug.Log("Red");
            else
                Debug.Log("Hit Fail");
            Destroy(gameObject);
        }
        else if(other.name=="HitBlue")
        {
            if(myCode==BLUE_CODE)
                Debug.Log("Blue");
            else
                Debug.Log("Hit Fail");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name=="DestroyPoint")
        {
            Destroy(gameObject);
            Debug.Log("Miss Fail");
        }
        CheckTrigger(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckTrigger(other);
    }

    void OnTriggerExit(Collider other)
    {
        CheckTrigger(other);
    }
}
