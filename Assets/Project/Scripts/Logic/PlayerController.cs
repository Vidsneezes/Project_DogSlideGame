using UnityEngine;
using System.Collections;

public enum PlayerState
{
    Waiting,
    Moving
}
/// <summary>
/// Control class for player.
/// Movement and collision detection for play
/// </summary>
public class PlayerController : MonoBehaviour {

    public float BaseForce;
    public LayerMask collisionMask;
    private Rigidbody2D rigidBody2d;
    private Vector2 velocity;
    private PlayerState playerState;

    private void Start () {
        playerState = PlayerState.Waiting;
        rigidBody2d = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
	}

    private void Update()
    {
        if (playerState == PlayerState.Waiting)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 || horizontal < 0)
            {
                velocity.x = Mathf.Sign(horizontal);
                velocity.y = 0;
                playerState = PlayerState.Moving;
            }
            else if (vertical > 0 || vertical < 0)
            {
                velocity.x = 0;
                velocity.y = Mathf.Sign(vertical);
                playerState = PlayerState.Moving;
            }
        }else if(playerState == PlayerState.Moving)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidBody2d.position, velocity, 1.28f, collisionMask);
            if(hit.collider != null)
            {
                playerState = PlayerState.Waiting;
                rigidBody2d.MovePosition(rigidBody2d.position - velocity * BaseForce * Time.deltaTime);
                velocity = Vector2.zero;
            }
        }
        
    }

    private void FixedUpdate()
    {
        rigidBody2d.MovePosition(rigidBody2d.position + velocity * BaseForce * Time.deltaTime);
    }
}
