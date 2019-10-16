using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Gamekit2D;

    public class playeranimation : MonoBehaviour
    {
      
        Animator an;
        public float PushSpeed = 0.1f;
    
        public delegate void OpenDoorDelegate(bool o);
       public  OpenDoorDelegate OpenDoorEvent;
        public ParticleSystem LongAttackEffect;
        void OnCollisionEnter2D(Collision2D hit)
        {
            if (hit.gameObject.layer == 31)
            {


                an.SetInteger("OnGround", 1);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 31&& collision.gameObject.tag!= "SceneObject")
            {

                an.SetInteger("OnGround", 0);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            Pushable pushable = collision.GetComponent<Pushable>();
            if (pushable != null)
            {
                m_CurrentPushables.Add(pushable);
               an.SetBool("ReadyPush",true);
               
            }

           
        }
        void OnTriggerExit2D(Collider2D other)
        {
            Pushable pushable = other.GetComponent<Pushable>();
            if (pushable != null)
            {
                if (m_CurrentPushables.Contains(pushable))
                    m_CurrentPushables.Remove(pushable);
                an.SetBool("ReadyPush", false);
            }
        }

        protected Pushable m_CurrentPushable;
        public List<Pushable> m_CurrentPushables = new List<Pushable>(4);
 
        public void MovePushable()
        {

            if (m_CurrentPushables.Count > 0)
            {
                m_CurrentPushable = m_CurrentPushables[0];
                Debug.Log(m_CurrentPushable.Grounded);
                //we don't push ungrounded pushable, avoid pushing floating pushable or falling pushable.
                if (m_CurrentPushable && m_CurrentPushable.Grounded)
                {
                    m_CurrentPushable.Move(Vector2.right * Input.GetAxisRaw("Horizontal") * PushSpeed);
                   
                }
            }
        }
        void Start()
        {
            an = this.GetComponent<Animator>();
           
        }
       


        void Update()
        {


          

            if (Input.GetKeyDown(KeyCode.J)) {

                an.SetTrigger("ShortAttack");
            }
            //吹笛
            if (Input.GetKeyDown(KeyCode.K))
            {
            if (GetComponent<AudioSource>().clip != null) { 
              GetComponent<AudioSource>().Play();
            //降低背景音乐
            AudioMgr.OnIncreaseBackGroundMusicEvent(0.5f);
            //吹笛子特效
                LongAttackEffect.Play();
                StartCoroutine("ChuiDi");
            }
            //播放人物动画
            an.SetBool("LongAttack", true);
                
            }
           
            if (Input.GetKeyUp(KeyCode.K))
            {
            if (GetComponent<AudioSource>().clip != null)
            {
                GetComponent<AudioSource>().Stop();
                AudioMgr.OnIncreaseBackGroundMusicEvent(1);
                LongAttackEffect.Stop();
                LongAttackEffect.Clear();
            }
                an.SetBool("LongAttack", false);
            if (OpenDoorEvent != null)
            {
                OpenDoorEvent(false);
            }
           

            StopCoroutine("ChuiDi");

            }
          
            if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                an.SetBool("Run", true);
                //特效镜像
                LongAttackEffect.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);


            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);

                an.SetBool("Run", true);
                LongAttackEffect.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

            }




            if (Input.GetKeyDown(KeyCode.Space))
            {
                an.SetTrigger("Jump");
              
                an.SetInteger("OnGround", 2);
                GetComponent<move1>().shortJump = false;

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GetComponent<move1>().shortJump = true;

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                an.SetBool("Squat", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                an.SetBool("Squat", false);

            }

            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                an.SetBool("Run", false);
            }


        }

    IEnumerator FallThroughtInvincibility(Damageable damageable)
    {
        damageable.EnableInvulnerability(true);
        yield return new WaitForSeconds(1.5f);
        damageable.DisableInvulnerability();
    }


    public void Hurt(Damager damager, Damageable damageable)
        {


       

        //如果是刺的话
        if (damager.transform.tag == "Spikes") {
            //受伤后撤
            transform.GetComponent<Rigidbody2D>().AddForce(damageable.GetDamageDirection() * 250);
            damageable.EnableInvulnerability();
        }
        //如果是其他攻击物
        else
        {
            StartCoroutine(FallThroughtInvincibility(damageable));
        }
        

        an.SetTrigger("Hurt");



        }


        void NormalColor()
        {
            foreach (Anima2D.SpriteMeshInstance obj in gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Anima2D.SpriteMeshInstance>())
            { obj.color = Color.white; }
        }
        void HurtColor1()
        {
            foreach (Anima2D.SpriteMeshInstance obj in gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Anima2D.SpriteMeshInstance>())
            { obj.color = Color.red; }
        
        }
        void HurtColor()
        {
            foreach (Anima2D.SpriteMeshInstance obj in gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Anima2D.SpriteMeshInstance>())
            { obj.color=new Color(1,1,1,0); }
        }
      
        public void Die(Damager damager, Damageable damageable)
        {
                an.SetTrigger("Die");
          Invoke("Reset", 5.0f);
           StartCoroutine(Reset(damageable));
        }

        //void Reset(Damager damager)
        //{
        //    if (GameObject.Find("BronPos"))
        //        transform.position = GameObject.Find("BronPos").transform.position;
        //    an.SetTrigger("ReSet");
        // //   damager.

        //}

        IEnumerator Reset(Damageable damageable)
        {
            //等待三秒再复活
            yield return new WaitForSeconds(3);
            if (GameObject.Find("BronPos"))
                transform.position = GameObject.Find("BronPos").transform.position;
            an.SetTrigger("ReSet");
            damageable.GainHealth(5);
           // Debug.Log("复活");
        }

        IEnumerator ChuiDi()
        { yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<Damageable>().GainHealth(1);
        if (OpenDoorEvent != null)
            {
            OpenDoorEvent(true);
            }
        }
    }
