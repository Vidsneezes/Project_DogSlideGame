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
    public LayerMask CollisionMask;
    public GridMap GridMap;
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
            Vector3 deltaMovement = velocity * BaseForce * Time.deltaTime;
            Vector3 displacedPosition = transform.position + deltaMovement + velocity * 1.28f;
            int objectValue = GridMap.GetObjectValue(displacedPosition);

            //has the player hit something
            if(objectValue == 1)
            {
                playerState = PlayerState.Waiting;
            }else if(objectValue == 2)
            {
                GridMap.DestroyObjectAt(transform.position);
                transform.Translate(velocity * BaseForce * Time.deltaTime);

            }
            else if (objectValue == 0)
            {
                transform.Translate(velocity * BaseForce * Time.deltaTime);
            }
            
        }
    }

  
}
