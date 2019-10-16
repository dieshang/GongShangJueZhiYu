using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   // public float speed = 1;
   // public float jumpDis = 1;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "note")
        {
            CollectNote.instance.AddNote(collision.gameObject.name);
        }
    }
   
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 31)
    //    {
           
    //        anim.SetBool("Squat", true);
            
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 31)
    //    {
            
    //        anim.SetBool("Squat", false);
    //    }
    //}


    // Update is called once per frame
    void Update()
    {

        ////控制移动
        float h = Input.GetAxis("Horizontal");
       //  float v = Input.GetAxis("Vertical");
        // this.gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(h * 5, 0, 0) * speed * Time.deltaTime);

        // if (h == 0 && v == 0)
        //   if (h == 0)
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
        if (anim)
        {
            if (h > 0)
            {
                anim.SetBool("Right", true);
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (h < 0)
            {
                anim.SetBool("Left", true);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (h == 0)
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetBool("AttackShort", true);
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("AttackShort", false);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("Jump", true);


            }
           /* if (Input.GetKeyDown(KeyCode.Space))
            {
      
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 800 * 10));

            }*/

            
            if (Input.GetKeyUp(KeyCode.Space))
            {
            
                anim.SetBool("Jump", false);
                //GetComponent<Rigidbody2D>().velocity += new Vector2(0, 5);
                //this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*10.0f*Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("Squat", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("Squat", false);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetTrigger("Hurt");
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                // anim.SetTrigger("Hurt");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                anim.SetBool("AttackLong", true);
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                anim.SetBool("AttackLong", false);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("Push", true);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                anim.SetBool("Push", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                anim.SetBool("Die", true);
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                anim.SetBool("Die", false);
            }
        }
    }

   
    public void Hurt()
    {
        anim.SetTrigger("Hurt");
    }
    public void Die()
    {
        Debug.Log("死亡");
        anim.SetTrigger("Die");
        
    }

   
}
