using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D rb;
    public CapsuleCollider2D touchingcol;

    public bool isFlipped = false;

    TouchingDirection touchingDirection;
    Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingcol = GetComponent<CapsuleCollider2D>();
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void OnHit(int damage, Vector2 knockback)//받은 데미지, 넉백
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }


    public float AttackCooldown
    {
        get
        { // 공격 쿨타임
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

}
