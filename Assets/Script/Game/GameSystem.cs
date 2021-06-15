using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class GameSystem : MonoBehaviour
{
    public GameObject hitObj_R, hitObj_B, GeneratePoint, MusicObj;
    private Queue<int> hit_timing = new Queue<int>(); //打擊時間
    private Queue<int> hit_obj = new Queue<int>(); //打擊物件
    private int next_time = 0; //紀錄下一個打擊時間
    private int next_obj = 0; //紀錄下一個打擊物件
    private float total_time = 0; //紀錄遊戲的時間
    private int wait_time = 0; //紀錄音樂播放延遲的時間
    private bool isMusicStart = false;
    private string audio_path="\\audio.wav";
    private string map_path="\\4";
    private bool isMapDone = false;

    // Start is called before the first frame update
    void Start()
    {
        string root_path = GameObject.Find("GameParm").GetComponent<GameParm>().getSongPath();
        audio_path = root_path+audio_path;
        map_path = root_path+map_path;
        StartCoroutine(GetAudioClip(audio_path)); //載入音樂
        loadMap(map_path);
        setNextObj(); //初始化第一個打擊點
        
        if(wait_time!=0) //播放音樂時間需延遲 --> 第一個打擊點需提前產生
        {
            genHitObj();
            setNextObj();
        }
        else
        {
            isMusicStart=true;
            //play Music
            MusicObj.GetComponent<AudioSource>().Play();
            StartCoroutine(AudioPlayFinished(MusicObj.GetComponent<AudioSource>().clip.length));
        }
    }

    private void loadMap(string path)
    {
        StreamReader sr;
        string str;
        bool isFirstSet = false;
        int first=0; //儲存第一個時間

        using (sr=new StreamReader(path))
        {
            while ((str = sr.ReadLine()) != null)
            {
                //由於物件飛向打擊點，所以兩秒前出現的打擊點需要校正，並且要記錄歌曲延後播放時間
                int timing = Convert.ToInt32(str.Split(' ')[0])-2000; //物件飛向打擊點需兩秒
                if(!isFirstSet) //紀錄第一個時間點(方便兩秒前的節奏校正)
                {
                    isFirstSet = true;
                    first = timing;
                    if(first<0) //紀錄歌曲延後播放時間
                        wait_time=first*(-1);
                }
                if(timing<0)
                    timing = timing-first;

                hit_timing.Enqueue(timing);
                hit_obj.Enqueue(Convert.ToInt32(str.Split(' ')[1]));
            }
        }
    }

    private void setNextObj()
    {
        if (hit_timing.Count > 0)
        {
            next_time = hit_timing.Dequeue();
            next_obj = hit_obj.Dequeue();
        }
        else
        {
            isMapDone=true;
        }
    }

    private void genHitObj() //生成打擊物件
    {
        if(next_obj==0 || next_obj==4)
            Instantiate(hitObj_R, GeneratePoint.transform.position, transform.rotation);
        else if(next_obj==8 || next_obj==12)
            Instantiate(hitObj_B, GeneratePoint.transform.position, transform.rotation);
    }

    IEnumerator GetAudioClip(string directory)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///"+directory, AudioType.WAV))
        {
           
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
 
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                MusicObj.GetComponent<AudioSource>().clip = myClip;
                //MusicObj.GetComponent<AudioSource>().Play();
            }
        }
    }

    private IEnumerator AudioPlayFinished(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("結束");
    }

    // Update is called once per frame
    void Update()
    {
        total_time+=Time.deltaTime;
        if(!isMusicStart)
        {
            if(total_time*1000>=wait_time)
            {
                isMusicStart=true;
                //play music
                MusicObj.GetComponent<AudioSource>().Play();
                StartCoroutine(AudioPlayFinished(MusicObj.GetComponent<AudioSource>().clip.length));
            }
        }
        if(isMapDone)
            return;

        if(total_time*1000>=next_time)
        {
            genHitObj();
            setNextObj();
        }
    }
}
