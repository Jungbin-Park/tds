using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCtrl : MonoBehaviour
{
    public enum ZombieState
    {
        TRACE,
        ATTACK,
        JUMP,
        DIE,
    }

    ZombieState state = ZombieState.TRACE;

    private Transform zombieTr;
    private Transform playerTr;
    private Vector3 initPosition;
    private Animator anim;

    private float m_Speed = 1.0f;

    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask monsterLayer;

    [SerializeField] private float jumpForce = 6.0f;
    private Rigidbody2D rb;

    void Start()
    {
        zombieTr = GetComponent<Transform>();
        initPosition = zombieTr.position;
        rb = GetComponent<Rigidbody2D>();

        playerTr = GameObject.FindWithTag("TRUCK").GetComponent<Transform>();

        anim = GetComponent<Animator>();
    }

    void UpdateTrace()
    {
        float dirX = playerTr.transform.position.x - zombieTr.transform.position.x;
        Vector3 vecDir = new Vector3(dirX, 0.0f, 0.0f);
        vecDir.Normalize();

        transform.position += vecDir * Time.deltaTime * m_Speed;
    }

    void JumpOver()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void UpdateAttack()
    {

    }

    void UpdateJump()
    {
        //rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //if(transform.position.y > initPosition.y + 1.5f)
        //{
        //    state = ZombieState.TRACE;
        //}
    }

    void UpdateDie()
    {

    }

    void Update()
    {
        switch(state)
        {
            case ZombieState.TRACE:
                anim.SetBool("IsAttacking", false);
                UpdateTrace();
                break;
            case ZombieState.ATTACK:
                anim.SetBool("IsAttacking", true);
                UpdateAttack();
                break;
            case ZombieState.JUMP:
                UpdateJump();
                break;
            case ZombieState.DIE:
                UpdateDie();
                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter");
        if(collision.collider.CompareTag("BOX"))
        {
            state = ZombieState.ATTACK;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ZOMBIE"))
        {
            if (collision.transform.position.x < transform.position.x)
            //&& collision.transform.position.y >= transform.position.y)
            {
                JumpOver();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BOX"))
        {
            state = ZombieState.ATTACK;
        }
        
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("OnTriggerEnter");
    //    if (collision.CompareTag("BOX"))
    //    {
    //        state = ZombieState.ATTACK;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("BOX"))
    //    {
    //        state = ZombieState.TRACE;
    //    }
    //}
}
