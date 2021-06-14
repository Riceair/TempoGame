using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    //Menu Scroll設定
    public Button mItemPrefab;//要新增到列表的預設體按鈕元件
    public Transform mContentTransform;//容器Content的transform
    public Scrollbar mScrollbar;//滑動條
    List<Button> lists = new List<Button>();//存放按鈕元件
    float itemHeight;//單個按鈕元件的height
    RectTransform rect;//容器content的rect
    public VerticalLayoutGroup group;//用於計算內容的高度

    private List<string> music_folders;

    // Start is called before the first frame update
    void Start()
    {
        rect = mContentTransform.GetComponent<RectTransform>();
        itemHeight = mItemPrefab.GetComponent<RectTransform>().rect.height;
        music_folders=new List<string>();
        readFolder(); //讀取所有音檔資料夾
        ShowItems();
        //mScrollbar.value = 1.0f;
    }

    private void readFolder()
    {
        DirectoryInfo diTop;
        string strPath = "song";

        if (Directory.Exists(strPath)) //確認是否有歌曲
        {
            diTop = new DirectoryInfo(strPath);
            foreach (var folder in diTop.EnumerateDirectories("*"))
            {
                music_folders.Add(folder.FullName);
            }
        }
        else
        {
            //沒歌曲退出(有緣一定做)
            Application.Quit();
        }
    }

    private void ShowItems()
    {
        foreach(string music_folder in music_folders)
        {
            AddItem(music_folder);
        }
    }

    private void AddItem(string path)
    {
        string name = path.Substring(path.LastIndexOf("\\")+1);

        Button item = Instantiate(mItemPrefab, transform.position, transform.rotation);
        item.GetComponentInChildren<Text>().text = name;
        item.transform.parent = mContentTransform;
        lists.Add(item);

        item.GetComponent<ListButton>().setPath(path); //傳遞路徑

        //rect.sizeDelta的x是width 
        //rect.sizeDelta的y是height
        //rect.sizeDelta=new Vector2(rect.sizeDelta.x, lists.Count * itemHeight);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x,
            group.padding.top + group.padding.bottom + lists.Count * itemHeight + (lists.Count - 1) * group.spacing);
        //rect.sizeDelta = new Vector2(rect.sizeDelta.x, lists.Count * itemHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
