using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move1 : MonoBehaviour
{
    //加速度
    public int Acc;
    //最大速度
    public float MaxSpeed;
    private bool keyStar;
    [HideInInspector]
    public bool ground = true;
    //长跳
    public float LongJumpSpeed = 500.0f;
    //短跳
    public float ShortJumpSpeed = 250.0f;
    [HideInInspector]
    public bool shortJump = false;
    // Use this for initialization
    private Rigidbody2D m_rigid;
    private Vector2 m_vx;
    void Start()
    {
        keyStar = false;
        m_rigid = GetComponent<Rigidbody2D>();
        this.GetComponent<Rigidbody2D>().freezeRotation = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //水平移动
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(m_rigid.velocity.x) < MaxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * Acc);
        }
        //判断按键状态
        if (h == 0)
        {
            keyStar = true;
        }
        else
        {
            keyStar = false;
        }
        //减速
        if (keyStar && m_rigid.velocity.x> 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * Acc);
        }
        if (keyStar && m_rigid.velocity.x< 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * Acc);
        }
        // if (Input.GetKeyUp(KeyCode.A))
        // {
        //     GetComponent<Rigidbody2D>().AddForce(Vector2.right * Acc);
        // }
        // if (Input.GetKeyUp(KeyCode.D))
        // {
        //     GetComponent<Rigidbody2D>().AddForce(Vector2.left * Acc);
        // }


    }
    void OnCollisionEnter2D(Collision2D hit)
    {
        ground = true;

    }


    void jump1()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        {
            if (ground == true)
            {

                if (shortJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * ShortJumpSpeed);
                    shortJump = false;
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * LongJumpSpeed);
                }
                ground = false;

            }

        }
    }
}


