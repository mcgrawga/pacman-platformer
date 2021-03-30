using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 movement = new Vector3(1f, 0f, 0f);
    private string direction;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().directionFacing;
        if (direction == "right")
        {
            movement = new Vector3(1f, 0f, 0f);
        }
        if (direction == "left")
        {
            movement = new Vector3(-1f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boundary")
        {
            gameController.destroyBullet(gameObject);
        }
    }

}
