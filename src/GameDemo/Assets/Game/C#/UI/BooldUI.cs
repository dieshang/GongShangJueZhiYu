using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Gamekit2D
{
    public class BooldUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //if (GameObject.FindGameObjectWithTag("Player")) {
            //    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>()==null)
            //        GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>().OnHealthSet = new Damageable.HealthEvent();

            //    GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>().OnHealthSet.AddListener(ChangeHitPointUI);}
        }



        public void ChangeHitPointUI(Damageable damageable)
        {
            
            for (int i=0;i<5;i++) {
                if(i< damageable.CurrentHealth)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(@"blood/Blood_01");
                }
                else
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(@"blood/Blood_02");
                }

            }
        }
    }
}