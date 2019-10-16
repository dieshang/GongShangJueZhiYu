using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }
    private void OnDisable() {

        Cursor.visible = false;
    }

public void exit() { 
        //foreach(GameObject go in GameObject.Find("GameCanvas").GetComponent<DontDestroy>().DontDestroyObjects)
        //{

        //    Destroy(go);
        //}
        GameData.Instance.First = false ;
        AudioMgr.Instance.DeleteAudio();
        SceneManager.LoadScene(0);

    }

}
