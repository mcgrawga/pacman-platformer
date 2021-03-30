using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 movement;
    Animator animator;
    public int pointVal;

    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = gameObject.GetComponent<Animator>();
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            movement= new Vector3(1f, 0f, 0f);
        } else 
        {
            // Reverse movement
            this.movement = new Vector3(-1f, 0f, 0f);
            // Flip sprites
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites) {
                sprite.flipX = true;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * Time.deltaTime * moveSpeed;  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RightBound")
        {
            // Reverse movement
            this.movement = new Vector3(-1f, 0f, 0f);
            // Flip sprites
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites) {
                sprite.flipX = true;
            }
        }
        if (collision.gameObject.tag == "LeftBound")
        {
            // Reverse movement
            this.movement = new Vector3(1f, 0f, 0f);
            // Flip sprites
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites) {
                sprite.flipX = false;
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            gameController.destroyEnemy(gameObject);
        }
    }
}
