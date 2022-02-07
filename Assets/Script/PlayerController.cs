using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private FixedJoystick fixedJoystick;
    const float speedXMultiplier = 50f;

    private float _horizontal;
    private bool _isFacingRight = true;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFinish = false;
    private bool _isLeverArm = false;

    Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _leverArm = FindObjectOfType<LeverArm>();
    }

    private void Update()
    {
        //_horizontal = Input.GetAxis("Horizontal"); для пк
        _horizontal = fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }               
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if(_isJump)
        {
            _rb.AddForce(new Vector2(0f, 400f));
            _isGround = false;
            _isJump = false;
        }

        if(_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }

        else if(_horizontal < 0f && _isFacingRight)
        {
            Flip();
        }
        
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (_isGround)
        {
            _isJump = true;
            jumpSound.Play();
        }
        
    }

    public void Interact()
    {
        if (_isFinish)
        {
            _finish.FinishLevel();
        }
        if (_isLeverArm)
        {
            _leverArm.ActivateLeverArm();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
            Vector3 playerScale = transform.localScale;
        }        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Work");
            _isFinish = true;
        }
        if (leverArmTemp != null)
        {
            _isLeverArm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Not work");
            _isFinish = false;
        }
        if(leverArmTemp != null)
        {
            _isLeverArm = false;
        }
    }
}
