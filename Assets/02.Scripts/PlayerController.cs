using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection),typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float distance = 3;
    public float attackSpeed = 3f;
    public float walkSpeed = 5f; //캐릭터 걷는 속도
    public float runSpeed = 8f; //캐릭터 뛰는 속도
    public float airWalkSpeed = 6f; //공중에 떠있는 상태에서 이동 속도
    public float jumpImpulse = 8f; //점프하는 힘
    //UI
    public int staminaValueNow = 60; //staminaMaxValue = 100;//스태미나 값
    public int staminaUse = 20;//스태미나 사용값
    public int staminaRecover = 50;//스태미나 회복량
    //스킬
    private int selectedSkillIndex = 0; // 선택된 스킬 인덱스
    public Skill_Data[] skillList;// 사용 가능한 스킬 목록
    public GameObject mpMange;
    int mpPoint;
    bool skillOn;
    //회피기
    public float dodgeForce = 200f; // 회피 힘
    public float dodgeDuration = 0.2f; // 회피 지속 시
    //대쉬

    //카메라
    public GameObject cameraObj;
    //음향
    //public AudioClip walkingSound;
    private AudioSource audioSourcePlay;
    public GameObject soundObj;
    public AudioClip audioClipPlay;
    //이벤트
    public UnityEvent<int,int> staminaChanged; // 스태미나 사용 시 발생하는 유니티 이벤트
    //public UnityEvent<int, int> staminaCharge; // 스태미나 회복 시 발생하는 유니티 이벤트
    
    Vector2 moveInput; //입력 방향
    TouchingDirection touchingDirection; //땅이나 벽에 닿아있는 방향을 판단
    Damageable damagable; //데미지를 받을 수 있는지 여부를 판단

    //현재 캐릭터의 이동 속도
    public float CurrentMoveSpeed {  get
        {
            if (CanMove)
             {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    //땅에 있는 경우
                    if (touchingDirection.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {                     
                        return airWalkSpeed;
                    }
                }
                else
                {               
                    return 0;
                }
            }
            else
            {            
                return 0;
            }
        } }
    [SerializeField]
    private bool _isMoving = false;

    //캐릭터가 이동하고 있는지 여부 확인
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
     }

    Rigidbody2D rb;
    Animator animator;
    private InputAction attackAction;
    //컴포넌트 캐싱
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damagable = GetComponent<Damageable>();
        //audioSource = GetComponent<AudioSource>();
        mpPoint = mpMange.GetComponent<MP_Manage>().mpPoint;
        skillOn = mpMange.GetComponent<MP_Manage>().skillOn;
    }

    private void FixedUpdate()
    {
        if (!damagable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        if (mpPoint == 100)
        {
            skillOn = true;
        }
        if (staminaValueNow <= staminaMaxValue)
        {
            staminaValueNow += staminaRecover;//* (int)Time.deltaTime;
        }
    }
    [SerializeField]
    private bool _isRunning = false;

    //캐릭터가 달리고 있는지에 대한 여부
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    //캐릭터가 이동 가능한지 여부
    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    //캐릭터가 올바르게 이동하고 있는지 여부
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //캐릭터 스프라이트 좌우 반전
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    //이동 입력 액션 감지
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            audioClipPlay = soundObj.GetComponent<AudioMange>().audioClip[0];
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
      
    }
    //입력값에 따른 방향
    private void SetFacingDirection(Vector2 moveInput)
        {           
           if(moveInput.x > 0 && !IsFacingRight)
           {
                IsFacingRight = true;
           } 
           else if(moveInput.x < 0 && IsFacingRight)
           {
                IsFacingRight = false;
           }
        }
    //달리기 입력 액션 감지
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
            //audioSource.Play();
        } else if (context.canceled)
        {
            IsRunning = false;
            //audioSource.Stop();
        }
    }
    //점프 입력 액션 감지
    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO Check if alive as well
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            audioClipPlay = soundObj.GetComponent<AudioMange>().audioClip[1];
            animator.SetTrigger(AnimationStrings.jumpTrgger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    [SerializeField]
    private int staminaMaxValue = 100; //    최대 스태미나
    public int MaxStamina
    {
        get
        {
            return staminaMaxValue;
        }
        set
        {
            staminaMaxValue = value;
        }
    }
    public int StaminaValue
    {
        get
        {
            return staminaValueNow;
        }
        set
        {
            staminaValueNow = value;
            staminaChanged?.Invoke(staminaValueNow, MaxStamina);
        }
    }
    public void AirAttack(InputAction.CallbackContext context)
    {
        if (StaminaValue < 20)
        {
            Debug.Log("스태미나가 부족합니다");
            return;
        }
        if (context.started && touchingDirection.IsGrounded == false)
        {
            Debug.Log("공중공격");
            animator.SetTrigger(AnimationStrings.airAttackTrigger);

        }
    }

    //공격
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (StaminaValue < 20)
        {
            Debug.Log("스태미나가 부족합니다");
            return;
        }

        if (context.started && touchingDirection.IsGrounded == true)
        {
            StaminaValue -= staminaUse;
            soundObj.GetComponent<AudioMange>().AttackSound(0);
            Debug.Log("기본공격");
            animator.SetTrigger(AnimationStrings.attackTrigger);
            rb.velocity = new Vector2(moveInput.x * attackSpeed, rb.velocity.y);
        }
    }
    public void UseSkill(InputAction.CallbackContext context)
    {
        skillOn = mpMange.GetComponent<MP_Manage>().skillOn;
        if (skillOn == false)
        {
            Debug.Log("도력이 부족합니다");
            return;
        }
        if (context.started && touchingDirection.IsGrounded == true && skillOn == true)
        {
            Debug.Log("스킬사용");
            animator.SetTrigger(AnimationStrings.useSkillTrigger);
            mpMange.GetComponent<MP_Manage>().MpValue -= mpMange.GetComponent<MP_Manage>().skillCost;
            mpMange.GetComponent<MP_Manage>().skillOn = false;
            if (skillList[selectedSkillIndex].cooldown <= 0)
            {
                
                // 스킬 사용 로직
                Vector2 spawnPosition = new Vector2(transform.position.x , transform.position.y);
                cameraObj.GetComponent<CameraMange>().DoShake(1.5f);
                AudioSource skillAudioSource = gameObject.AddComponent<AudioSource>();
                
                if (transform.localScale.x >= 0 )//(IsFacingRight == true)
                {
                    
                    skillList[selectedSkillIndex].projectilePrefab.GetComponent<SpriteRenderer>().flipX = false;
                    spawnPosition = new Vector2(transform.position.x + 2, transform.position.y);
                    Instantiate(skillList[selectedSkillIndex].projectilePrefab, spawnPosition, Quaternion.identity);
                    skillAudioSource.Play();
                }
                else
                {
                    
                    skillList[selectedSkillIndex].projectilePrefab.GetComponent<SpriteRenderer>().flipX = true;
                    spawnPosition = new Vector2(transform.position.x - 2, transform.position.y);
                    Instantiate(skillList[selectedSkillIndex].projectilePrefab, spawnPosition, Quaternion.identity);
                    skillAudioSource.Play();

                }
            }
        }
    }

    //캐릭터가 적에게 맞아 데미지를 입고 날라가는 넉백 효과
    public void OnHit(int damage, Vector2 knockback) 
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public bool _isTeleport = false;
    public bool IsTeleport
    {
        get
        {
            return _isTeleport;
        }
        set
        {
            _isTeleport = value;
            //animator.SetBool(AnimationStrings.isTeleport, value);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.isDash);
            while (true){
                rb.velocity = Vector2.right * 5;
            }
            
        }
    }
    public void Doge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool(AnimationStrings.isTeleport, true);
            //animator.SetTrigger(AnimationStrings.isTeleport);
            //IsTeleport = true;
            Dodge(-1);
        }
    }

    IEnumerator DogeAnime()
    {
            yield return new WaitForSecondsRealtime(1.0f);// + 조건

        //animator.SetBool(AnimationStrings.isTeleport,false);
        IsTeleport = false;
    }
    void Dodge(int direction)
    {
        rb.velocity = Vector2.zero; // 현재 속도 초기화
        rb.AddForce(new Vector2(dodgeForce * direction, 0f)); // 회피 방향으로 힘 가하기

        Invoke("StopDodge", dodgeDuration); // dodgeDuration 후 StopDodge 함수 호출
        

    }

    void StopDodge()
    {
        rb.velocity = Vector2.zero; // 회피 후 속도 다시 0으로 설정

        animator.SetBool(AnimationStrings.isTeleport, false);
        //IsTeleport = false;
        //StartCoroutine(DogeAnime());
    }

    public void SkillChangeA(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // animator.SetBool(AnimationStrings.isTeleport, true);
            Debug.Log("스킬 사용 A");
        }
    }
    public void SkillChangeS(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //animator.SetBool(AnimationStrings.isTeleport, true);
            Debug.Log("스킬 사용 S");
        }
    }
}
