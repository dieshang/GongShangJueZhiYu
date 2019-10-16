using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNote : MonoBehaviour
{
    public static CollectNote instance;
    private List<string> notes = new List<string>();
    public List<GameObject> noteUI;

    public void Awake()
    {
        instance = this;
    }

    public void AddNote(string _name)
    {
        notes.Add(_name);
        switch (_name)
        {
            case "palace"://宫
                noteUI[0].SetActive(true);
                break;
            case "business"://商
                noteUI[1].SetActive(true);
                break;
            case "angle"://角
                noteUI[2].SetActive(true);
                break;
            case "sign"://徵
                noteUI[3].SetActive(true);
                break;
            case "feather"://羽
                noteUI[4].SetActive(true);
                break;
        }

    }




}
