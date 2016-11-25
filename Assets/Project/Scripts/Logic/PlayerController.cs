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

    public float baseForce;
    public LayerMask collisionMask;
    public GridMap gridMap;
    private Vector3 velocity;
    private PlayerState playerState;

    private void Start () {
        playerState = PlayerState.Waiting;
        velocity = Vector3.zero;
	}

    private void Update()
    {
        if (playerState == PlayerState.Waiting)
        {
            //waiting for player input
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
            Vector3 deltaMovement = velocity * baseForce * Time.deltaTime;
            Vector3 displacedPosition = transform.position + deltaMovement + velocity * 1.28f;
            int objectValue = gridMap.GetObjectValue(displacedPosition);

            //has the player hit something
            if(objectValue == 1)
            {
                playerState = PlayerState.Waiting;
            }else if(objectValue == 2)
            {
                gridMap.DestroyObjectAt(transform.position);
                transform.Translate(velocity * baseForce * Time.deltaTime);

            }
            else if (objectValue == 0)
            {
                transform.Translate(velocity * baseForce * Time.deltaTime);
            }
            
        }
    }

  
}
