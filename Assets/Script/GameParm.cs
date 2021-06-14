using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParm : MonoBehaviour
{
    private string song_path;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void setSongPath(string path)
    {
        song_path=path;
    }

    public string getSongPath()
    {
        return song_path;
    }
}
