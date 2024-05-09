using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]

public class Scarecrow : MonoBehaviour // 허수아비 (적)
{
    public float walkAcceleration = 3f; //허수아비 가속도
    public float maxSpeed = 3f; // 최대 속력
    public float walkStopRate = 0.1f; // 감속 속도
    public DetectionZone attackZone; // 공격 범위
    public DetectionZone cliffDetectionZone; // 절벽 감지
    
    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left } //이동 방향


    private WalkableDirection _walkDirection;//현재 이동 방향
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection  //스케일 반전 및 이동 방향 벡터 설정
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left; 
                }
            }
                _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget { 
        get { return _hasTarget; } 
        private set  // 타겟 공격 범위 감지 여부
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }

    public bool CanMove //이동 가능 여부
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown { get { // 공격 쿨타임
            return animator.GetFloat(AnimationStrings.attackCooldown);
        } private set {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    // Update is called once per frame
    void Update()  //공격 범위 감지 이용해 HasTarget 설정하고, 공격 쿨타임 시간 감소
    {
        HasTarget = attackZone.detectedColliders.Count > 0; ; 

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate() //이동 및 방향 전환
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall) //바닥과 벽에 동시에 닿아 있을 때
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity) //데미지 받은 상태가 아닌 경우 이동 가능 상태 확인 후 이동 처리
        {
            if (CanMove)
                rb.velocity = new Vector2( 
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                    rb.velocity.y);//현재 속력에 가속도와 이동 방향 벡터, 고정 프레임 시간을 이용해 x축 속력 계산
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            //계산된 속력을 최대 속도 사이로 제한
        }

    }

    private void FlipDirection()//이동 방향 반전
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Curren walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)//받은 데미지, 넉백
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    public void OnCliffDetected() // 절벽 감지
    {
        if (touchingDirection.IsGrounded) //바닥에 닿아있는지
        {
            FlipDirection(); //이동 방향 반전
        }
    }
}
