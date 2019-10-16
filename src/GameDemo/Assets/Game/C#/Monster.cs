using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject bullet;
    GameObject player;
    public List<Vector2> movePoints;
    public float attackDis = 20;
    private Animator anim;
    public bool isRemoteAttack = true;
    public float speed = 1.0f;
    public enum Status
    {
        Idle,
        Patrol,//巡逻
        Pursue,
        Attack,
        Die,
        Hurt
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        targetPos = movePoints[index];
        ToPatrol();
    }
    private Status status = Status.Patrol;//初始化当前状态；
    void PursueUpdate()
    {
        if (movePoints.Count != 0)
        {
            //anim.SetTrigger("walk");

            transform.position += new Vector3(player.transform.position.x * speed * cur * Time.deltaTime * 0.01f, player.transform.position.y * speed * cur * Time.deltaTime * 0.01f, 0);
            //   Debug.Log(player.transform.position.x);
            //   transform.position += new Vector3(1, 0, 0);
            Debug.Log(Vector2.Distance(transform.position, player.transform.position));

        }
        if (attackDis > Vector2.Distance(transform.position, player.transform.position))
        {
            ToAttack();
            Debug.Log("找到了");
        }


        if ((transform.position.x - player.transform.position.x) >= 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            cur = -1;
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            cur = 1;
        }


    }
    Vector2 targetPos = Vector2.zero;
    int index = 0;
    int cur = 1;
    void PatrolUpdate()
    {//???

        /* Vector2 target =movePoints[index] - (Vector2)transform.position;
         this.transform.position += new Vector3(target.x * Time.deltaTime * 1f, target.y * Time.deltaTime * 1f, 0);
         Debug.Log(transform.position+"  "+targetPos+"  "+ Vector2.Distance(transform.position, targetPos));*/
        gameObject.GetComponent<Transform>().position += new Vector3(targetPos.x * speed * cur * Time.deltaTime * 0.01f, targetPos.y * speed * cur * Time.deltaTime * 0.01f, 0);
        //  Debug.Log(targetPos);
        if (Vector2.Distance(transform.position, targetPos) <= 0.8f)
        {
            Debug.Log("到达了一个点");
            index = (index + 1) % movePoints.Count;
            targetPos = movePoints[index];
        }
        if ((transform.position.x - targetPos.x) >= 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            cur = -1;
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            cur = 1;
        }
        if (attackDis > Vector2.Distance(transform.position, player.transform.position))
        {
            ToAttack();
            Debug.Log("找到了");
        }


    }
    float _time = 2.0f;
    void AttackUpdate()
    {
        if (status == Status.Die || status == Status.Hurt)
            return;
        if ((transform.position.x - player.transform.position.x) >= 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }

        //如果是远程会射击
        if (isRemoteAttack)
        {
            _time += Time.deltaTime;
            if (_time >= 2.0f)
            {
                _time = 0.0f;
                GameObject obj = Instantiate(bullet, gameObject.GetComponent<Transform>().position, Quaternion.identity);
                if ((gameObject.GetComponent<Transform>().position.x - player.transform.position.x) > 0)//怪物在右，主角在左
                {
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 0));

                }
                else
                {
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0));
                    obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
                }
            }
        }


        if (attackDis < Vector3.Distance(player.transform.position, transform.position))
        {
            ToPatrol();
        }
    }

    void Update()
    {
        Debug.Log(status);
        switch (status)
        {
            case Status.Idle:
                break;
            case Status.Patrol:
                PatrolUpdate();
                break;
            case Status.Attack:
                AttackUpdate();
                break;
            case Status.Pursue:
                PursueUpdate();
                break;
            case Status.Hurt:
                HurtUpdate();
                break;
            case Status.Die:
                DieUpdate();
                break;
        }
    }
    void HurtUpdate()
    {

    }
    void DieUpdate()
    {

    }
    public void ToIdle()
    {
        status = Status.Idle;
    }
    //巡逻
    public void ToPatrol()
    {
        anim.SetBool("attack", false);
        if (status == Status.Die || status == Status.Hurt)
            return;

        status = Status.Patrol;

        if (movePoints.Count == 0)
        {
            GetComponent<Animator>().SetTrigger("standby");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("walk");
        }
    }
    public void ToDie()
    {
        status = Status.Die;
        anim.SetTrigger("die");
    }
    public void ToHurt()
    {

        if ((transform.position.x - player.transform.position.x) >= 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        status = Status.Hurt;
        anim.SetBool("attack", false);
        anim.SetTrigger("hurt");
        Destroy(this.gameObject, 1.9f);
    }

    /*Vector3 playerPos;
    Vector3 movePos;*/
    public void ToPursue()
    {
        anim.SetBool("attack", false);
        if (status == Status.Die || status == Status.Hurt)
            return;
        /*  movePos = vec - transform.position;
          playerPos = vec;*/
        status = Status.Pursue;

        if (movePoints.Count == 0)
        {
            GetComponent<Animator>().SetTrigger("standby");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("walk");
        }
    }
    public void ToAttack()
    {
        if (status == Status.Die || status == Status.Hurt)
            return;
        status = Status.Attack;
        Debug.Log("播放攻击动画");
       anim.SetBool("attack", true);
    }
}
