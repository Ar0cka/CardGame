using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float _horizontalMove = Input.GetAxisRaw("Horizontal");
        float _verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(_horizontalMove, _verticalMove, 0f);
        moveDirection.Normalize();
        
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
