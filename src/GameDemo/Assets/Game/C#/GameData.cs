using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    protected static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<GameData>();
            if (instance != null)
                return instance;

            Create();
            return instance;
        }
    }

    public static GameData Create()
    {
        GameObject dataManagerGameObject = new GameObject("Data");
        DontDestroyOnLoad(dataManagerGameObject);
        instance = dataManagerGameObject.AddComponent<GameData>();
        return instance;
    }
    public bool First = false;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
