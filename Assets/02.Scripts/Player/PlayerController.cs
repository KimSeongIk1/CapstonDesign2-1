using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection),typeof(Damageable))]

//1차 작업자 : 김재성
//2차 작업 및 수정자 : 김장후
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float distance = 3;
    [SerializeField] private float attackSpeed = 3f;
    [SerializeField] private float walkSpeed = 5f; //캐릭터 걷는 속도
    [SerializeField] private float walkMaxAcceleration = 10f; //걷기 최대 가속값
    [SerializeField] private float runSpeed = 8f; //캐릭터 뛰는 속도
    [SerializeField] private float runMaxAcceleration = 10f; //뛰기 최대 가속값
    [SerializeField] private float airWalkSpeed = 6f; //공중에 떠있는 상태에서 이동 속도
    [SerializeField] private float jumpImpulse = 8f; //점프하는 힘
    //UI
    [SerializeField] private int staminaValueNow = 60; //staminaMaxValue = 100;//스태미나 값
    [SerializeField] private int staminaUse = 20;//스태미나 사용값
    [SerializeField] private int staminaRecover = 50;//스태미나 회복량
    public int manaValueNow = 60;  //manaMaxValue = 100;//마나 값
    [SerializeField] private bool skillOn = true;    //스킬 활성화 여부
    [SerializeField] private bool DashOn = true;     //대쉬 활성화 여부
    [SerializeField] private bool skillManaCheck = false; //스킬 사용 마나 확인 여부
    private int selectedSkillIndex = 0; // 선택된 스킬 인덱스
    public SkillData[] skillList;// 사용 가능한 스킬 목록
    //회피기
    [SerializeField] private float idleDashSpeed = 7f;
    [SerializeField] private float walkDashSpeed = 7f; // 걷는 도중 회피 속도
    [SerializeField] private float runDashSpeed = 7f; // 뛰는 도중 회피 속도
    [SerializeField] private float airDashSpeed = 7f; // 공중 회피 속도
    [SerializeField] private float walkDashDuration = 0.1f; // 회피 지속 시간
    [SerializeField] private float runDashDuration = 0.1f; // 회피 지속 시간
    [SerializeField] private float airDashDuration = 0.1f; // 회피 지속 시간
    [SerializeField] private float dashCooltime = 1f; // 회피 쿨타임
    //카메라
    public GameObject cameraObj;
    //음향
    public AudioClip walkingSound;
    private AudioSource audioSource;
    public GameObject soundObj;
    public AudioSource skillAudio;
    //이벤트
    public UnityEvent<int,int> staminaChanged; // 스태미나 사용 시 발생하는 유니티 이벤트
    //public UnityEvent<int, int> staminaCharge; // 스태미나 회복 시 발생하는 유니티 이벤트
    
    Vector2 moveInput; //입력 방향
    TouchingDirection touchingDirection; //땅이나 벽에 닿아있는 방향을 판단
    Damageable damagable; //데미지를 받을 수 있는지 여부를 판단

    //현재 캐릭터의 이동 속도
    public float CurrentMoveSpeed
    {
        get
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
        }
    }
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
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!damagable.LockVelocity)
            //rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            
             rb.AddForce(new Vector2(moveInput.x * CurrentMoveSpeed, 0), ForceMode2D.Impulse); //Addforce로 인한 가속 형태의 이동
        
        if (rb.velocity.x > walkMaxAcceleration && moveInput.x == 1 && !IsDash)
            rb.velocity = new Vector2(walkMaxAcceleration, rb.velocity.y); // 오른쪽 걷기 시 연속된 가속으로 인한 최대 이동속도 제한
        if (rb.velocity.x < walkMaxAcceleration*(-1) && moveInput.x==(-1) && !IsDash)
            rb.velocity = new Vector2(walkMaxAcceleration * (-1), rb.velocity.y); // 오른쪽 걷기 시 연속된 가속으로 인한 최대 이동속도 제한
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y); 
        /*if(manaValueNow == 100)
        {
            skillOn = true;
        }*/
        if(staminaValueNow <= staminaMaxValue)
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
        } 
    }
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
            audioSource.Play();
        } else if (context.canceled)
        {
            IsRunning = false;
            audioSource.Stop();
        }
    }
    //점프 입력 액션 감지
    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO Check if alive as well
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrgger);
            if (context.started )
            {
                rb.AddForce(new Vector2(0, jumpImpulse),ForceMode2D.Impulse);
            }
            /*else if(context.canceled) { 
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y * 0.5f);
            }*/

            //if (context.started && touchingDirection.IsGrounded == false)
            //{
            //    //Debug.Log("공중공격");
            //    //animator.SetTrigger(AnimationStrings.airAttackTrigger);
            //}
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
    //[SerializeField]
    //private int staminaValueNowTest = 100; //현재 스태미나
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
            //soundObj.GetComponent<AudioMange>().AttackSound(1);
            Debug.Log("기본공격");
            animator.SetTrigger(AnimationStrings.attackTrigger);
            rb.velocity = new Vector2(moveInput.x * attackSpeed, rb.velocity.y);
        }
    }


    [SerializeField]
    private int manaMaxValue = 100; //    최대 마나
    public int MaxMana
    {
        get
        {
            return manaMaxValue;
        }
        set
        {
            manaMaxValue = value;
        }
    }
    public void OnSkill1(InputAction.CallbackContext context)   // A키, 기둥 소환 공격
    {
        if (context.started && touchingDirection.IsGrounded == true) // 스킬 활성화(쿨다운이 끝나있는지) 여부 및 땅에 있는 지 확인
        {
            Debug.Log("스킬사용 A");
            selectedSkillIndex = 0;
            if (skillOn == false && manaValueNow >= skillList[selectedSkillIndex].manaCost)  // 기존 조건(쿨다운) + 마나 소모량이 일정 이상이면 스킬 실행
            {
                skillOn = true;    // 반복 스킬 사용 방지
                animator.SetTrigger(AnimationStrings.SkillTrigger1);    // 스킬 사용 캐릭터 애니메이션 실행
                manaValueNow -= skillList[selectedSkillIndex].manaCost; // 마나 소모량에 따라 감소
                StartCoroutine(SkillSpawn(selectedSkillIndex));  // 스킬 프리팹 생성
            }
            else
            {
                Debug.Log("스킬사용 A 실패");
            }
        }
    }
    public void OnSkill2(InputAction.CallbackContext context) { // S키, 부적 날리기
        if (context.started && touchingDirection.IsGrounded == true && skillOn == false) // 스킬 활성화(쿨다운이 끝나있는지) 여부 및 땅에 있는 지 확인
        {
            Debug.Log("스킬사용 S");
            selectedSkillIndex = 1;
            if (manaValueNow >= skillList[selectedSkillIndex].manaCost)  // 기존 조건(쿨다운) + 마나 소모량이 일정 이상이면 스킬 실행
            {
                skillOn = true;    // 반복 스킬 사용 방지
                animator.SetTrigger(AnimationStrings.SkillTrigger2);    // 스킬 사용 캐릭터 애니메이션 실행
                manaValueNow -= skillList[selectedSkillIndex].manaCost; // 마나 소모량에 따라 감소
                StartCoroutine(SkillSpawn(selectedSkillIndex));  // 스킬 프리팹 생성
            }
            else
            {
                Debug.Log("스킬사용 S 실패");
            }
        }
    }
    public void OnSkill3(InputAction.CallbackContext context) { // D키, 지옥귀 소환
        selectedSkillIndex = 2; 
        if (context.started && touchingDirection.IsGrounded == true && skillOn == false) // 스킬 활성화(쿨다운이 끝나있는지) 여부 및 땅에 있는 지 확인
        {
            print("스킬 사용 D");
            selectedSkillIndex = 1;
            if (manaValueNow >= skillList[selectedSkillIndex].manaCost)  // 기존 조건(쿨다운) + 마나 소모량이 일정 이상이면 스킬 실행
            {
                skillOn = true;    // 반복 스킬 사용 방지
                animator.SetTrigger(AnimationStrings.SkillTrigger2);    // 스킬 사용 캐릭터 애니메이션 실행
                manaValueNow -= skillList[selectedSkillIndex].manaCost; // 마나 소모량에 따라 감소
                StartCoroutine(SkillSpawn(selectedSkillIndex));  // 스킬 프리팹 생성
            }
            else
            {
                Debug.Log("스킬사용 S 실패");
            }
        }
    }
    public void OnSkill4(InputAction.CallbackContext context) { // F키, 스킬 미정
        selectedSkillIndex = 3;
        print("스킬 사용 F");
    }
    public IEnumerator SkillSpawn(int selectedSkillIndex) { // 스킬 스폰 시스템
        // 스킬 사용 로직
        PlayerSkill playerSkill;
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);    // 스킬 스폰 지점 구하기
        yield return new WaitForSeconds(skillList[selectedSkillIndex].spawnDelay);
        if (transform.localScale.x >= 0) // 플레이어가 오른쪽을 바라보고 있을 경우 == true
        {
            skillList[selectedSkillIndex].projectilePrefab.GetComponent<SpriteRenderer>().flipX = false;
            spawnPosition = new Vector2(transform.position.x + skillList[selectedSkillIndex].spawnPosx, transform.position.y + skillList[selectedSkillIndex].spawnPosy);
            GameObject skill = Instantiate(skillList[selectedSkillIndex].projectilePrefab, spawnPosition, Quaternion.identity);
            skill.name = skillList[selectedSkillIndex].name; // 스킬 데이터에 따른 인스펙터 이름 변경
            if(skillList[selectedSkillIndex].freezePlayerPos == true)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("PlayerFreezeStop",skillList[selectedSkillIndex].duration);
            }

            Destroy(skill, skillList[selectedSkillIndex].duration);
            print(skill + "스킬 사용. 우");
        }
        else
        {
            skillList[selectedSkillIndex].projectilePrefab.GetComponent<SpriteRenderer>().flipX = true;
            spawnPosition = new Vector2(transform.position.x - skillList[selectedSkillIndex].spawnPosx, transform.position.y + skillList[selectedSkillIndex].spawnPosy);
            GameObject skill = Instantiate(skillList[selectedSkillIndex].projectilePrefab, spawnPosition, Quaternion.identity);
            skill.name = skillList[selectedSkillIndex].name; // 스킬 데이터에 따른 인스펙터 이름 변경
            
            if (skillList[selectedSkillIndex].freezePlayerPos == true)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("PlayerFreezeStop", skillList[selectedSkillIndex].duration);
            }

            Destroy(skill, skillList[selectedSkillIndex].duration);
            print(skill + "스킬 사용. 좌");
        }
        skillAudio.clip = skillList[selectedSkillIndex].skillSpawnAudioSource;  // skillAudio 자식 오브젝트에 사용할 사운드 할당 및 실행
        skillAudio.Play();

        //cameraObj.GetComponent<CameraMange>().VibrateForTime(0.5f);

        yield return new WaitForSeconds(skillList[selectedSkillIndex].cooldown); // 쿨타임 카운트
        skillOn = false;     // 스킬 재사용 가능 전환

    }
    public void PlayerFreezeStop() {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    //캐릭터가 적에게 맞아 데미지를 입고 날라가는 넉백 효과
    public void OnHit(int damage, Vector2 knockback) 
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public bool _isDash = false;
    public bool IsDash
    {
        get
        {
            return _isDash;
        }
        set
        {
            _isDash = value;
            //animator.SetBool(AnimationStrings.isDash, value);
            //animator.SetTrigger(AnimationStrings.isDash);
        }
    }
    public float CurrentDashSpeed
    {
        get
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
                            return runDashSpeed;
                        }
                        else
                        {
                            return walkDashSpeed;
                        }
                    }
                    else
                    {
                        return airDashSpeed;
                    }
                }
                else
                {
                    return idleDashSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    public float CurrentDashDuration
    {
        get {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    //땅에 있는 경우
                    if (touchingDirection.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runDashDuration;
                        }
                        else
                        {
                            return walkDashDuration;
                        }
                    }
                    else
                    {
                        return airDashDuration;
                    }
                }
                else
                {
                    return walkDashDuration;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    [SerializeField]
    public void DoDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //animator.SetBool(AnimationStrings.isDash, true);
            print("대쉬입력");
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        print("대쉬입장");
        if (DashOn == true){
            DashOn = false;
            _isDash = true;
            float originalGraviry = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * CurrentDashSpeed, 0f);
            print(touchingDirection.IsGrounded + "이다다다다");
            yield return new WaitForSeconds(CurrentDashDuration);
            rb.gravityScale = originalGraviry;
            _isDash = false;
            yield return new WaitForSeconds(dashCooltime);
            DashOn = true;
        }
        print("대쉬퇴장");
    }
    public void SkillChangeZ(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // animator.SetBool(AnimationStrings.isTeleport, true);
            Debug.Log("스킬 변경 Z");
        }
    }
    public void SkillChangeX(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //animator.SetBool(AnimationStrings.isTeleport, true);
            Debug.Log("스킬 변경 C");
        }
    }
}