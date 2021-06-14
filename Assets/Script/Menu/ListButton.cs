using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ListButton : MonoBehaviour
{
    private string path;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        GameObject.Find("GameParm").GetComponent<GameParm>().setSongPath(path);
        SceneManager.LoadScene("Game");
    }

    public void setPath(string path)
    {
        this.path = path;
    }
}
