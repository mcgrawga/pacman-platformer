using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    Animator animator;
    public GameObject bullet;
    public GameObject explosion;
    public GameObject coinExplosion;
    public GameObject coinGobble;
    public GameObject playerPrefab;

    public string directionFacing = "right";

    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isMoving", true);
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = false;
            }
            directionFacing = "left";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isMoving", true);
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = true;
            }
            directionFacing = "right";
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        Jump();
        Shoot();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameController.bullets > 0)
        {
            // Instantiate(bullet, transform.position, Quaternion.identity);
            gameController.shootBullet(transform.position);
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            { 
                // only jump if not already jumping
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            gameController.destroyPlayer(gameObject);
        }
        if (collision.gameObject.tag == "Floor") {
            gameController.destroyPlayer(gameObject);
        }            
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin") {
            gameController.destroyCoin(collision.gameObject);
        }
    }

    void Die() {
        animator.enabled = false;
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        GameObject ce = Instantiate(coinExplosion, transform.position, Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f)));
        Destroy(e, 3.0f);
        Destroy(ce, 5.0f);
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        GameObject sp = GameObject.Find("PlayerSpawnPoint");
        gameObject.transform.position = sp.gameObject.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.flipX = true;
        }
        directionFacing = "right";
        animator.enabled = true;
    }
}
