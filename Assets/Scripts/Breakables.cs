using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{

    public GameObject brokenPiece;

    void Start()
    {
        
    }
     void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //if(PlayerController.instance.dashCounter > 0 )
            //{
                Destroy(gameObject);

                Instantiate(brokenPiece,transform.position,transform.rotation);
            //}
        }

          if(other.tag == "Bullet")
        {
            //if(PlayerController.instance.dashCounter > 0 )
            //{
                Destroy(gameObject);

                Instantiate(brokenPiece,transform.position,transform.rotation);
            //}
        }


    }

}
