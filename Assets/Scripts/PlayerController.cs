using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //movimiento
    public float moveSpeed;
    public Vector2 moveInput;

    public Rigidbody2D rb;

    //movimiento del arma
    public Transform gunArm;

    private Camera theCam;

    public Animator anim;

    //disparo de arma
    public  GameObject bulletToFire;
    public Transform firePoint;

    //tiempo entre cada bala
    public float timeBetweenShorts;
    private float shotCounter;

    private float  activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f,dashCooldown = 1f,dashInvinvivility = .5f;

    [HideInInspector]
    public float  dashCounter;
    private float dashCoolCounter;


    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        theCam = Camera.main;

        activeMoveSpeed = moveSpeed;
    }

    
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed,moveInput.y * Time.deltaTime * moveSpeed, 0f);

        rb.velocity = moveInput * activeMoveSpeed;

        //movimiento arma

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
            gunArm.localScale = new Vector3(-1f,-1f,1f);
        } else{
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        //rotador de arma
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x,mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; 
        gunArm.rotation = Quaternion.Euler(0, 0, angle); 


        //disparo de arma
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShorts;
        }

        //regular disparos
        if(Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                shotCounter = timeBetweenShorts; 
            }
        }

        //DASH

        if(Input.GetKeyDown(KeyCode.Space))
        {   if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                anim.SetTrigger("Dash");

                PlayerHealthController.instance.MakeInvincible(dashInvinvivility);
             
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {      
                 activeMoveSpeed = moveSpeed;
                 dashCoolCounter = dashCooldown;
                
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }






        if(moveInput != Vector2.zero)
        {
            anim.SetBool("Walk" , true);
        }else{
            anim.SetBool("Walk" , false);
        }



    }
}
