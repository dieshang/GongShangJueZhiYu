using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Gamekit2D;

    public  class Interaction : MonoBehaviour
    {
        [Serializable]
        public class NoteEvent : UnityEvent<string>
        { }

    public GameObject LongAttackEffect;
    //拥有的音符
    public  List<string> Hasnote = new List<string>();
        Dictionary<string, string> YinFuMusicLoad = new Dictionary<string, string>();
        public NoteEvent OnGinNote;
        // Start is called before the first frame update
        void Start()
        {
            YinFuMusicLoad.Add("GMusic", @"PlayerMusic/Gong");
            YinFuMusicLoad.Add("SMusic", @"PlayerMusic/Shang");
            YinFuMusicLoad.Add("JMusic", @"PlayerMusic/Jue");
            YinFuMusicLoad.Add("ZMusic", @"PlayerMusic/Zi");
            YinFuMusicLoad.Add("YMusic", @"PlayerMusic/Yu");
       

        OnGinNote.AddListener(GameObject.Find("GameCanvas").transform.Find("yinfu").GetComponent<YinFuUI>().YinFu);
    }

    string GetGainMusicPrefabsAdress(string musicName) {
        switch (musicName)
        {
            case "GMusic":
                return @"GainMusic/G_GainMusic";
            case "SMusic":
                return @"GainMusic/S_GainMusic";
            case "JMusic":
                return @"GainMusic/J_GainMusic";
            case "ZMusic":
                return @"GainMusic/Z_GainMusic";
            case "YMusic":
                return @"GainMusic/Y_GainMusic";

        }

        return null;
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            //得到音符
            if (collision.tag == "Note")
            {
           GameObject gainMusic= Instantiate(Resources.Load<GameObject>(GetGainMusicPrefabsAdress(collision.name)));
            gainMusic.transform.position = collision.transform.position;
                Hasnote.Add(collision.name);
                //学会相应的笛声
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(YinFuMusicLoad[collision.name]);
                OnGinNote.Invoke(collision.name);
            collision.gameObject.transform.parent.GetComponent<AudioSource>().Play();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject.transform.parent.gameObject, collision.gameObject.transform.parent.GetComponent<AudioSource>().clip.length);
            LongAttackEffect.SetActive(true);
            var ts = LongAttackEffect.GetComponent<ParticleSystem>(). textureSheetAnimation;
            ts.startFrame = Hasnote.Count*0.2f;
            

        }
        }
        // Update is called once per frame
        void Update()
        {

        }


        
    }
