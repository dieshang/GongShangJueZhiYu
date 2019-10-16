using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string DoorKeyName;
    bool CanOpenDoor = false;
    public int GoScene = 2;
    private QTE qte;
    private void Start()
    {
        GameObject.Find("player").GetComponent<playeranimation>().OpenDoorEvent += OpenDoor;
        qte=GameObject.Find("GameCanvas").transform.Find("QTE").GetComponent<QTE>();
        qte.OnFailEvent += StopOpenDoor;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      
        {
            if (collision.name == "player")
            {

                if (HasKey(collision))
                {
                    if (CanOpenDoor)
                    {
                       
                        if (!GetComponent<AudioSource>().isPlaying) {
                            //降低背景音乐
                            AudioMgr.OnLowerBackGroundMusicEvent(0.1f);
                           // GameObject.Find("BgMusic").GetComponent<AudioSource>().volume = 0.1f;
                            //GetComponent<AudioSource>().volume =1f;
                            //GetComponent<AudioSource>().Play();
                            GetComponent<AudioSource>().volume =0f;
                            AudioMgr.OnPlayMusicEvent(GetComponent<AudioSource>());

                           // StartCoroutine("StartDoorMusic");
                            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                            StartCoroutine("ChuangShong");
                            qte.OpenQte(GetComponent<AudioSource>().clip.length, collision.gameObject.GetComponent<Interaction>().Hasnote.Count+1);
                        }
                    }

                }


            }
            }
        
    }

    private void Update()
    {
       if (GetComponent<AudioSource>().isPlaying)
        {
          GameObject.Find("BgMusic").GetComponent<AudioSource>().volume = 0.1f;
       }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        qte.CloseQte();
        StopOpenDoor();
    }

  public  void StopOpenDoor()
    {

        CanOpenDoor = false;
        //  GameObject.Find("BgMusic").GetComponent<AudioSource>().volume =1f;
        AudioMgr.OnIncreaseBackGroundMusicEvent(1);
        //  StartCoroutine("StopDoorMusic");
        AudioMgr.OnStopMusicEvent(GetComponent<AudioSource>());
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        StopCoroutine("ChuangShong");
    }

    bool HasKey(Collider2D collision)
    {
        foreach (string KeyName in collision.gameObject.GetComponent<Interaction>().Hasnote)
        {
            if (KeyName == DoorKeyName)
            {
                return true;
            }

        }

        return false;
    }

    //IEnumerator StartDoorMusic()
    //{
    //    //声音渐变变小
    //    while (GetComponent<AudioSource>().volume < 1)
    //    {
    //        GetComponent<AudioSource>().volume += 0.1f;
    //        yield return new WaitForSeconds(0.07f);
    //    }
    //    GetComponent<AudioSource>().Play();
    //}

    //IEnumerator StopDoorMusic()
    //{
    //    //声音渐变变小
    //   while (GetComponent<AudioSource>().volume > 0) {
    //        GetComponent<AudioSource>().volume -= 0.1f;
    //    yield return new WaitForSeconds(0.07f);
    //    }
    //    GetComponent<AudioSource>().Stop();
    //}

    IEnumerator ChuangShong()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        //   qte.CloseQte();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>().OnHealthSet.RemoveAllListeners();
        qte.OnFailEvent -= StopOpenDoor;
       GameData.Instance. First = true;
        SceneManager.LoadScene(GoScene);
    }
    public void OpenDoor(bool Di)
    {
        
            CanOpenDoor = Di;
       
    }
}
