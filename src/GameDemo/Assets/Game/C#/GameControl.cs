using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit2D;
public class GameControl : MonoBehaviour
{
   // public GameObject[] DontDestroyObjects;
    // Start is called before the first frame update


    void Awake()
    {
        GameObject.Find("player").GetComponent<Damageable>().OnHealthSet.AddListener(GameObject.Find("GameCanvas").transform.Find("boold").GetComponent<BooldUI>().ChangeHitPointUI);
       
        // if (SceneManager.GetActiveScene().name == "G_scene"&& GameData.Instance.First/*&& 5 == GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>().Hasnote.Count*/) {
        //   GameObject.Find("player").transform.position = GameObject.Find("CompletedPos").transform.position;
        //     GameObject.Find("BgMusic").GetComponent<AudioSource>().clip=Resources.Load<AudioClip>(@"End");
        //     GameObject.Find("BgMusic").GetComponent<AudioSource>().Play();
        //     GameObject.Find("AllGainMusic").gameObject.SetActive(true);

        //     //foreach (GameObject obj in DontDestroyObjects)
        //     //{
        //     //   obj.gameObject.SetActive(false);
        //     //   Destroy(obj);
        //     //}

        // }
        //else

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            GameObject.Find("BgMusic").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(@"End");
              GameObject.Find("BgMusic").GetComponent<AudioSource>().Play();
        }
        if (GameObject.Find("BronPos"))
            GameObject.Find("player").transform.position = GameObject.Find("BronPos").transform.position;
        
    }

    private void Start()
    {
        Cursor.visible = false;
    }









    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject.Find("GameCanvas").transform.GetChild(3).gameObject.SetActive(true);

        }
    }
}
