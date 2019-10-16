using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBullet : MonoBehaviour
{
    /*  private void OnCollisionEnter2D(Collision2D collision)
      {
          if (collision.gameObject.tag == "Player")
          {

              Destroy(gameObject);
          }
          if (collision.gameObject.tag == "Enemy")
          {
              Destroy(gameObject);
          }

      }*/
    private void Start()
    {
        Destroy(gameObject,4);
    }
}
