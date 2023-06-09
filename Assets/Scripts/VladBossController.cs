using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VladBossController : MonoBehaviour
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

    public GameObject vladFightMusic;
    public GameObject vladDeathMusic;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        vladDeathMusic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange && PlayerController.instance.hasDied == false && health > 0)
        {
            AttackPlayer();
        } 
        else if (health <= 0)
        {
            Stand();
        }
        else 
        {
            Stand();
        }

    }

    public void AttackPlayer()
    {
        anim.SetBool("isMoving", true);
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
            anim.SetBool("isDead", true);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioController.instance.PlayEnemyDeath();
            vladFightMusic.SetActive(false);
            vladDeathMusic.SetActive(true);


            Invoke("StartCutScene", 11);
        }
        else
        {
            AudioController.instance.PlayEnemyShot();
        }
    }

    void StartCutScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
