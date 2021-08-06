using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;


    public float  rangeToChasePlayer;
    private Vector3 moveDirection;


    public Animator anim;

    //daño de enemigos
    public int health;

    //muerte del enemigo
    public GameObject[] deathZombi;

     //efecto de golpe
     public GameObject hitEffect;   

     //daño enemigo
     public  bool shouldShoot;
     public GameObject bullet;
     public Transform firePoint;
     public float fireRate;
     private float fireCounter;

     //no atacar

     public float shootRange;
     public SpriteRenderer theBody;

    
     

    void Start()
    {
    

    }

    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {

          //Vector3 direccion = p.position - transform.position;
          //float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
             // transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

            if(Vector3.Distance(transform.position,PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;

               // Debug.Log("Hello: " + moveDirection);
                
                if(moveDirection.x > 0 )
                {
                    transform.localScale = new Vector3 (-1f, 1f, 1f);
                } else {
                     transform.localScale = new Vector3 (1f, 1f, 1f);
                }
                               
            } else {
                moveDirection = Vector3.zero;
            }

         

            moveDirection.Normalize();

            rb.velocity = moveDirection * moveSpeed;


            if(shouldShoot && Vector3.Distance(transform.position,PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;

                if(fireCounter <=0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet,firePoint.position,firePoint.rotation);
                }           
            }

        }else {
            rb.velocity = Vector2.zero;
        }

        if(moveDirection != Vector3.zero)
        {
            anim.SetBool("Walk",true);

        }else{
            anim.SetBool("Walk",false);
        }

    }


  

    //recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;

        Instantiate(hitEffect,transform.position,transform.rotation);

        if(health <= 0)
        {
            Destroy(gameObject);

           // Instantiate(deathZombi,transform.position,transform.rotation);
            int selecteddeathZombi = Random.Range(0,deathZombi.Length);

            int rotation = Random.Range(0,2);

            Instantiate(deathZombi[selecteddeathZombi],transform.position,transform.rotation);

        }
    }


}




