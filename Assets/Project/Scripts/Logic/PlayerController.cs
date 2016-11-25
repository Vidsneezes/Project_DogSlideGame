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
    private Rigidbody2D _rigidbody2d;
    private Vector2 _velocity;
    private PlayerState _playerState;
    private Vector2 _offsetPosition;

    private void Start () {
        _offsetPosition = new Vector2(1.28f, 1.28f);
        _playerState = PlayerState.Waiting;
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _velocity = Vector2.zero;
	}

    private void Update()
    {
        if (_playerState == PlayerState.Waiting)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 || horizontal < 0)
            {
                _velocity.x = Mathf.Sign(horizontal);
                _velocity.y = 0;
                _playerState = PlayerState.Moving;
            }
            else if (vertical > 0 || vertical < 0)
            {
                _velocity.x = 0;
                _velocity.y = Mathf.Sign(vertical);
                _playerState = PlayerState.Moving;
            }
        }else if(_playerState == PlayerState.Moving)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody2d.position, _velocity, 1.28f, collisionMask);
            if(hit.collider != null)
            {
                _playerState = PlayerState.Waiting;
                _rigidbody2d.MovePosition(_rigidbody2d.position - _velocity * baseForce * Time.deltaTime);
                _velocity = Vector2.zero;
            }
        }
        
    }

    private void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _velocity * baseForce * Time.deltaTime);
    }
}
