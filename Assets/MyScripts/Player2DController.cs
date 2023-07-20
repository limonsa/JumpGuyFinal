using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    private float _speed;
    private float _moveForce;
    private float _jumpHeight;
    private float _impulse;
    private bool _isMovingToRight;
    private bool _isJumping;
    private bool _isGrounded;
    private bool _isWalking;
    private float _damagePower;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private Animator _anim;
    private ExplosionController _explosion;
    private GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        //_explosion = gameObject.Find("Explosion").GetComponent<ExplosionController>();
        _explosion = GameObject.Find("Explosion").GetComponent<ExplosionController>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _speed = 1f;
        _jumpHeight = 8f;
        _impulse = 5f;
        _isMovingToRight = true;
        _damagePower = 0.15f;
        SetIsGrounded(true);
        SetIsWalking(false);
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move2D();
    }

    public void Move2D()
    {
        //Player 2D Movement over x-axes
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _moveForce = _speed;

        if (_direction.x != 0)
        {
            if (_direction.x > 0)
            {
                //Set player facing right
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _isMovingToRight = true;
            }
            else if (_direction.x < 0)
            {
                //Set player facing left
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _isMovingToRight = false;
            }
            SetIsWalking(true);
            SetIsGrounded(true);
        }
        else {
            SetIsWalking(false);
        }
        
        //Player 3D jump over y-axes
        if (_direction.y > 0)
        {
            SetIsGrounded(false);
            SetIsWalking(false);
            _moveForce = _jumpHeight;
            _anim.SetTrigger("Jump");
            _isJumping = true;
        }
        
        transform.Translate(_direction * Time.deltaTime * _moveForce, Space.World);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _anim.SetBool("isRunning", false);
        }

    }

    private void FixedUpdate()
    {
        GiveImpulse2D();

    }

    private void GiveImpulse2D()
    {
        //Player Impulse
        if (Input.GetKey(KeyCode.Space))
        {
            _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _rb.AddForce(_direction * _impulse, ForceMode2D.Force);
            if (_isGrounded) {
                _anim.SetBool("isRunning", true);
            }
        }
    }

    public void SetIsWalking(bool isWalking)
    {
        _isWalking = isWalking;
        _anim.SetBool("isWalking", _isWalking);
    }

    public void SetIsGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
        _anim.SetBool("isGrounded", _isGrounded);
    }

    public void SetSpeed(float speed) {
        _speed = speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetImpulse(float impulse)
    {
        _impulse = impulse;
    }

    public float GetImpulse()
    {
        return _impulse;
    }

    public void SetJumpHeight(float jumpHeight)
    {
        _jumpHeight = jumpHeight;
    }

    public float GetJumpHeight()
    {
        return _jumpHeight;
    }

    public bool GetIsMovingToRight()
    {
        return _isMovingToRight;
    }

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    public bool GetIsJumping()
    {
        return _isJumping;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground")
        {
            _isJumping = false;
            SetIsGrounded(true);
        }
        else if (collision.collider.name == "Enemy")
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            
            //If TakeDamage returns false means tha damage killed the player
            if( !_gm.TakeDamage(_damagePower) ) {
                _explosion.ShowExplosion();
                collision.otherCollider.enabled = false;
            }
        }
    }
}
