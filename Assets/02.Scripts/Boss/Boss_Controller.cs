using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Controller : MonoBehaviour
{


    Animator animator;
    private int speedx = 3; // 돌진 속도
    Rigidbody2D rigid2D;
    public GameObject cameraObj;
    public GameObject player;
    private int DIRECTION = 2;
    private bool ready = false;
    private Vector2 playerPos;
    private SpriteRenderer sprite;

    //번개 패턴 관련
    private GameObject backSprite; //번개 패턴의 배경 오브젝트
    private SpriteRenderer backSpriteAlpha; //위 배경의 알파값을 조절하기 위한 변수

    //패턴 프리팹
    public GameObject[] objPrefab;

    //패턴 시작 관련 변수
    [SerializeField] private int startPatternNum; //첫번째로 시작할 패턴
    [SerializeField] private int patternRange; //패턴의 개수를 지정
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        backSprite = GameObject.Find("ThunderBackGround");
        backSpriteAlpha = backSprite.GetComponent<SpriteRenderer>();
        NextPatternPlay(startPatternNum);
    }

    //플레이어 방향을 보는 로직
    void LookPlayer()
    {
        DIRECTION = (player.GetComponent<Transform>().position.x < transform.position.x ? -1 : 1); //player와 자신(보스)의 x좌표를 비교해서 적당한 상수를 DIRECTION에 저장한다.
        float scale = transform.localScale.z;
        transform.localScale = new Vector3(DIRECTION * -1 * scale, scale, scale); //DIRECTION변수를 이용해서 player쪽을 바라보도록한다.
    }

    // 0.돌진 패턴
    public bool isBroken = false;
    [SerializeField] private GameObject[] rushObj;
    [SerializeField] private GameObject rushRange;
    IEnumerator Rush()
    {
        Debug.Log("돌진 패턴 사용");

        LookPlayer();//플레이어 방향을 향함
        animator.SetBool("RushReady", true);//돌진 준비 애니메이션 재생
        //tartCoroutine(EffectRange(rushRange)); //공격 범위 표시
        yield return StartCoroutine(EffectRange(rushRange));

        //돌진 시작
        animator.SetBool("RushReady", false);
        animator.SetBool("Rush", true);
        isBroken = false;
        rushObj[0].SetActive(true); //피격 박스 활성화
        rushObj[1].SetActive(true); //이펙트 활성화
    
        while (!isBroken)
        {
            yield return new WaitForSeconds(0.1f);
            if (speedx > rigid2D.velocity.x * DIRECTION) //AddForce를 이용해서 자연스럽게 움직이도록 하되 speedx보다 빠르지 않도록한다.
            {
                rigid2D.AddForce(transform.right * DIRECTION * 1000);

            }
            cameraObj.GetComponent<CameraMange>().Dolmpulse(); // 카메라 흔들림

        }

        yield return new WaitForSeconds(2);

        rushObj[0].SetActive(false); //피격 박스 비활성화
        rushObj[1].SetActive(false); //이펙트 비활성화

        animator.SetBool("Rush", false);
        yield return new WaitForSeconds(3);

        NextPatternPlay(Random.Range(0, patternRange)); //랜덤한 다음 패턴 실행
    }

    // 1. 할퀴기(가로) 패턴
    [SerializeField] GameObject[] horizontalObj;
    [SerializeField] GameObject horizontalRange;
    IEnumerator Horizontal()
    {
        Debug.Log("할퀴기(가로) 패턴");
        LookPlayer();
        yield return new WaitForSeconds(1);

        StartCoroutine(EffectRange(horizontalRange)); //공격 범위 보이기
        yield return StartCoroutine(EffectRange(horizontalRange)); //EffectRange 코루틴을 끝날때까지 대기
        animator.SetTrigger("Horizontal");
        yield return new WaitForSeconds(3);

        NextPatternPlay(Random.Range(0, patternRange));
    }
    // 2.할퀴기(세로) 패턴
    [SerializeField] private GameObject[] verticalObj;
    [SerializeField] private GameObject[] verticalRange;
    IEnumerator Vertical()
    {
        Debug.Log("할퀴기(세로) 패턴");
        LookPlayer();
        yield return new WaitForSeconds(1);

        
        playerPos = player.transform.position;
        StartCoroutine(EffectRange(verticalRange[0]));      
        yield return StartCoroutine(EffectRange(verticalRange[1])); //EffectRange 코루틴을 끝날때까지 대기
        animator.SetTrigger("Vertical");
        //effectRangePrefab[0].transform.position = playerPos;
        //verticalObj[0].transform.position = playerPos; //할퀴기 오브젝트가 플레이어 위치로 이동
        yield return new WaitForSeconds(3f);
        NextPatternPlay(Random.Range(0, patternRange));
    }

    // 3.번개 패턴
    [SerializeField] private GameObject[] thunderEffectObj; //번개 패턴 이펙트
    [SerializeField] private GameObject[] thunderObj; //번개 오브젝트
    [SerializeField] private GameObject[] thunderRange; //공격 범위
    IEnumerator Thunder()
    {
        Debug.Log("번개 패턴");
        thunderEffectObj[1].SetActive(true);
        thunderEffectObj[2].SetActive(true);
        // 알파 값을 변경하여 배경을 어둡게 하는 로직
        do
        {
            Color color = backSpriteAlpha.color;
            color.a += Time.deltaTime * 0.5f;
            backSpriteAlpha.color = color;
            yield return null;
        } while (backSpriteAlpha.color.a <= 0.5f);
        animator.SetTrigger("Thunder");
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(EffectRange(thunderRange[0]));
            StartCoroutine(EffectRange(thunderRange[1]));
        }
        
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
        {
            thunderObj[i].SetActive(true);
            thunderObj[i + 3].SetActive(true);
            yield return new WaitForSeconds(1f);
        }


        for (int i = 0; i < 3; i++)
        {
            thunderObj[i].SetActive(false);
            thunderObj[i + 3].SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        // 알파 값을 변경하여 배경을 밝게 하는 로직
        do
        {
            Color color = backSpriteAlpha.color;
            color.a -= Time.deltaTime * 0.5f;
            backSpriteAlpha.color = color;
            yield return null;
        } while (backSpriteAlpha.color.a > 0f);

        thunderEffectObj[1].SetActive(false);
        thunderEffectObj[2].SetActive(false);
        yield return new WaitForSeconds(3f);
        NextPatternPlay(Random.Range(0, patternRange));
    }

    // 4.짓밟기 패턴
    [SerializeField] private GameObject stompObj;
    [SerializeField] private GameObject stompRange;
    IEnumerator Stomp() 
    {
        Debug.Log("짓밟기 패턴");
        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(EffectRange(stompRange));
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("Stomp");
            yield return new WaitForSeconds(0.2f);
            stompObj.SetActive(true);
            cameraObj.GetComponent<CameraMange>().Dolmpulse();
            yield return new WaitForSeconds(1);
        }


        //cameraObj.GetComponent<CameraMange>().CameraShake();
        stompObj.SetActive(false);
        yield return new WaitForSeconds(3);

        NextPatternPlay(Random.Range(0, patternRange));
    }
    // 5.순간이동 패턴
    IEnumerator Teleport()
    {
        Debug.Log("텔레포트 패턴");
        effectObj[3].SetActive(true);
        do
        {
            Color color = sprite.color;
            color.a -= Time.deltaTime * 1f;
            sprite.color = color;
            yield return null;
        } while (sprite.color.a >= 0f);
        effectObj[3].SetActive(false);
        yield return new WaitForSeconds(2);
        effectObj[4].SetActive(true);
        effectObj[5].SetActive(true);
        playerPos = player.transform.position;
        gameObject.transform.position = playerPos;
        do
        {
            Color color = sprite.color;
            color.a += Time.deltaTime * 1.5f;
            sprite.color = color;
            yield return null;
        } while (sprite.color.a <= 1f);
        effectObj[4].SetActive(false);
        effectObj[5].SetActive(false);
        effectObj[6].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        effectObj[6].SetActive(false);
        yield return new WaitForSeconds(2);
        NextPatternPlay(Random.Range(0, patternRange));
    }

    // 6.발악 패턴 . 보스 체력 5퍼 이하시 패턴
    [SerializeField] private GameObject[] groggyObj;
    [SerializeField] private GameObject[] groggyRange;
    public IEnumerator Groggy()
    {
        yield return new WaitForSeconds(2f);
        //패턴 시작
        for (int i = 0; i < 3; i++)
        {
            if (ready != true) yield return null;
            StartCoroutine(EffectRange(groggyRange[0]));
            yield return new WaitForSeconds(2.5f);
            groggyObj[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            groggyObj[i].SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        NextPatternPlay(6);

    }
    // 다음으로 시작되는 패턴을 지정해주는 로직
    public void NextPatternPlay(int nextPattern)
    {
        switch (nextPattern)
        {
            case 0:
                StartCoroutine(Rush());
                break;
            case 1:
                StartCoroutine(Horizontal());
                break;
            case 2:
                StartCoroutine(Vertical());
                break;
            case 3:
                StartCoroutine(Thunder());               
                break;
            case 4:
                StartCoroutine(Stomp());
                break;
            case 5:
                StartCoroutine(Teleport());
                break;
            case 6:
                StartCoroutine(Groggy());
                break;
        }
    }

    //공격 범위 표시
    IEnumerator EffectRange(GameObject rangeObject)
    {
        
        Debug.Log("공격범위 표시");
        float fadeCount = 0;
        Color rangeColor = rangeObject.GetComponent<SpriteRenderer>().color;
        rangeColor.a = 0;

        rangeObject.SetActive(true);
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            rangeObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0,fadeCount);

        }
        while (fadeCount >=0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            rangeObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, fadeCount);

        }
        yield return new WaitForSeconds(0.5f);

        ready = true;

    }


    [SerializeField] private GameObject[] effectObj;
    public void EnableObj(int num)
    {
        effectObj[num].SetActive(true);
    }


}
