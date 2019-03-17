using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private float _moveSpeed;
    private float _directionX;
    private bool _facingRight;
    private Vector3 _localScale;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _localScale = this.transform.localScale;
        _moveSpeed = 5f;
    }

    private void Update()
    {
        _directionX = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        if(Input.GetButtonDown("Jump") && _rigidBody.velocity.y == 0)
        {
            _rigidBody.AddForce(Vector2.up * 200f);
        }

        if(Mathf.Abs(_directionX) > 0 && _rigidBody.velocity.y == 0)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        if(_rigidBody.velocity.y == 0)
        {
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsFalling", false);
        }

        if(_rigidBody.velocity.y > 0)
        {
            _animator.SetBool("IsJumping", true);
        }

        if(_rigidBody.velocity.y < 0)
        {
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsFalling", true);
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_directionX, _rigidBody.velocity.y);
    }

    private void LateUpdate()
    {
        if(_directionX > 0)
        {
            _facingRight = true;
        }
        else if(_directionX < 0)
        {
            _facingRight = false;
        }

        if((_facingRight && _localScale.x < 0) || (!_facingRight && _localScale.x > 0))
        {
            _localScale.x *= -1;
        }

        this.transform.localScale = _localScale;
    }
}
