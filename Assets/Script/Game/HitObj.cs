using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HitObj : MonoBehaviour
{
    private int myCode=0;
    private const int RED_CODE=1;
    private const int BLUE_CODE=2;
    private float speed;

    public GameObject Perfect, Miss;

    private GameObject RedHitSound, BlueHitSound, HitNum, MissNum, HitRecall, RecallObj, GameSystem;

    // Start is called before the first frame update
    void Start()
    { 
        Vector3 generate_position = GameObject.Find("GeneratePoint").transform.position;
        Vector3 hit_position = GameObject.Find("HitPosition").transform.position;
        float distance = Vector3.Distance(generate_position,hit_position);
        speed = distance/1.16f; //距離除以時間(秒)

        RedHitSound = GameObject.Find("RedHitSound");
        BlueHitSound = GameObject.Find("BlueHitSound");
        HitNum = GameObject.Find("HitNum");
        MissNum = GameObject.Find("MissNum");
        HitRecall = GameObject.Find("HitRecall");

        GameSystem = GameObject.Find("GameSystem");

        if(name=="HitObj_R(Clone)")
            myCode=RED_CODE;
        else if(name=="HitObj_B(Clone)")
            myCode=BLUE_CODE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0,0,-Time.fixedDeltaTime*speed);
    }

    private void CheckTrigger(Collider other)
    {

        if(other.name=="HitRed")
        {
            if(myCode==RED_CODE)
            {
                RedHitSound.GetComponent<AudioSource>().Play();
                recordScore(true);
            }
            else
                recordScore(false);
            StartCoroutine(WaitToDel());
            Destroy(gameObject);
        }
        else if(other.name=="HitBlue")
        {
            if(myCode==BLUE_CODE)
            {
                BlueHitSound.GetComponent<AudioSource>().Play();
                recordScore(true);
            }
            else
                recordScore(false);
            StartCoroutine(WaitToDel());
            Destroy(gameObject);
        }
    }

    private void recordScore(bool isTP)
    {
        int num=0;
        if(!isTP)
        {
            num = Convert.ToInt32(MissNum.GetComponent<Text>().text) + 1;
            MissNum.GetComponent<Text>().text = Convert.ToString(num);
            RecallObj = Instantiate(Miss, HitRecall.transform.position, transform.rotation);
        }
        else
        {
            num = Convert.ToInt32(HitNum.GetComponent<Text>().text) + 1;
            HitNum.GetComponent<Text>().text = Convert.ToString(num);
            RecallObj = Instantiate(Perfect, HitRecall.transform.position, transform.rotation);
        }
    }

    private IEnumerator WaitToDel()
    {
        yield return new WaitForSeconds(3);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name=="DestroyPoint")
        {
            recordScore(false);
            StartCoroutine(WaitToDel());
            Destroy(gameObject);
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
