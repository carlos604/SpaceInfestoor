using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    //
    private float moveDirection;

    

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position -transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        moveDirection = transform.position.x;

          if(moveDirection > 0 )
                {
                    transform.localScale = new Vector3 (-1f, 1f, 1f);
                } else {
                     transform.localScale = new Vector3 (1f, 1f, 1f);
                }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
