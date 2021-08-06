using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update

    public GameObject impactEffect;

    //recibir daño
    public int damageToGive = 1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damageToGive);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
