using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private Color original_color;
    // Start is called before the first frame update
    void Start()
    {
        original_color = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        name="HitPoint";
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor",original_color);

        if (Input.GetKey (KeyCode.X) || Input.GetKey (KeyCode.C))
        {
            name="HitRed";
            StartCoroutine(UpDateDelay());
        }
        else if (Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.V))
        {
            name="HitBlue";
            StartCoroutine(UpDateDelay());
        }
    }

    IEnumerator UpDateDelay(){
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor",original_color*2.5f);
        yield return new WaitForSeconds(0.1f);
    }
}
