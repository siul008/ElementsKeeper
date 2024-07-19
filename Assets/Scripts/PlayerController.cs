using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir;
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
            //Debug.Log("IsMoving");
            isMoving = true;
        }
        else
        {
            //Debug.Log("IsNotMoving");
            isMoving = false;
        }
    }
}
