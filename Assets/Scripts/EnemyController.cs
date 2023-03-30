using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public GameObject explosion;

    public float playerRange = 10f;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public bool shouldShoot;
    public float fireRate = .5f;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;

    public Animator anim;

    public RoomEnter room;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playerIsInRange = Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange;
        bool playerIsAlive = PlayerController.instance.hasDied == false;
        bool shouldAttack = room.isEntered == true;


        if ((playerIsInRange && playerIsAlive) || (shouldAttack && playerIsAlive))

        {
            AttackPlayer();
        } 
        else 
        {
            Stand();
        }
    }

    public void AttackPlayer()
    {
        anim.SetBool("isMoving", true);

        Ray rayFront = new Ray();
        rayFront.origin = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(rayFront, out hit))
        {
            //Debug.Log("Distance is " + Vector3.Distance(gameObject.transform.position, hit.transform.position));
        }



        Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
        theRB.velocity = playerDirection.normalized * moveSpeed;
        if (shouldShoot)
        {
            ShootPlayer();
        }
    }

    public void ShootPlayer()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = fireRate;
        }
    }

    public void Stand()
    {
        anim.SetBool("isMoving", false);
        theRB.velocity = Vector2.zero;
    }


    public void TakeDamage()
    {
        health--;
        if (health <= 0) 
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

            AudioController.instance.PlayEnemyDeath();
        }
        else
        {
            AudioController.instance.PlayEnemyShot();
        }
    }


    public static void PlayAlert()
    {
        AudioController.instance.PlayVladAlert();
    }
}
