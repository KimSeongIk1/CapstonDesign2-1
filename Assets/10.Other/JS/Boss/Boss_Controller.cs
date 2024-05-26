using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    private int nextPattern;
    Animator animator;
    public int speedx = 3;
    Rigidbody2D rigid2D;
    public GameObject cameraObj;
    public GameObject player;
    public int DIRECTION = 2;
    private Vector2 pos;
    private Vector2 boxSize;
    private bool ready = false;
    private Vector2 playerPos;

    //패턴 프리팹
    public GameObject[] objPrefab;
    //public GameObject horizontalObj;
    public GameObject[] verticalObj;
    [SerializeField] private int startPatternNum;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        nextPatternPlay(startPatternNum);
    }
    void lookPlayer()
    {
        DIRECTION = (player.GetComponent<Transform>().position.x < transform.position.x ? -1 : 1); //player와 자신(보스)의 x좌표를 비교해서 적당한 상수를 DIRECTION에 저장한다.
        float scale = transform.localScale.z;
        transform.localScale = new Vector3(DIRECTION * -1 * scale, scale, scale); //DIRECTION변수를 이용해서 player쪽을 바라보도록한다.
    }
    IEnumerator rush()
    {
        Debug.Log("Rush 패턴 사용");

        lookPlayer();//player 쪽을 보게한다.
        animator.SetTrigger("Earthquake"); //돌진을 준비하는 animation
        yield return new WaitForSeconds(2);
        animator.SetBool("Walk", true);
        //animator.SetTrigger("rushing"); //돌진하는 animation
        bool isBroken = false;
        objPrefab[1].SetActive(true);
        while (!isBroken)
        {
            yield return new WaitForSeconds(0.1f);
            if (speedx > rigid2D.velocity.x * DIRECTION) //AddForce를 이용해서 자연스럽게 움직이도록 하되 speedx보다 빠르지 않도록한다.
            {
                Debug.Log("돌진중");
                rigid2D.AddForce(transform.right * DIRECTION * 1000);
                
            }
            cameraObj.GetComponent<CameraMange>().Dolmpulse();
            pos = new Vector2(transform.position.x + DIRECTION * 5, transform.position.y);
            Debug.Log("pos = " + pos);
            boxSize = new Vector2(transform.position.x, -2);
            Debug.Log("boxSize = " + boxSize);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos, boxSize, 0);//자신(보스) 앞쪽에 있는 모든 충돌체들을 저장한다.
            Debug.Log("콜라이더 저장");

            //if (stop == true) //충돌체가 자신(보스)의 충돌체가 아니고 tag가 Ground일 때 멈추도록한다.
            //{
            //    Debug.Log("정지");
            //    animator.SetBool("Walk", false);
            //    rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
            //    isBroken = true;
            //    Debug.Log("충돌");
            //    break;
            //}
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Player") //충돌체가 자신(보스)의 충돌체가 아니고 tag가 Ground일 때 멈추도록한다.
                {
                    Debug.Log(collider);
                    player.GetComponent<Damageable>().Hit(50, Vector2.zero);
                    rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                    isBroken = true;
                    Debug.Log("플레이어와 충돌");
                    break;
                }
                if (collider.tag == "Wall") //충돌체가 자신(보스)의 충돌체가 아니고 tag가 Ground일 때 멈추도록한다.
                {
                    Debug.Log(collider);
                    rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                    isBroken = true;
                    Debug.Log("충돌");
                    break;
                }
            }
        }
        //animator.SetBool("stunned", true); //부딪혀서 스턴 당한 animation
        // StartCoroutine("stunCounter"); //4초후에 stunned를 false로 설정하는 coroutine을 호출한다.
        // yield return new WaitForSeconds(1.5f);
        //this.glopGenerator_1.GetComponent<GlopGenerator>().summonGlop(); //Glop(몬스터를 소환한다.)
        //this.glopGenerator_2.GetComponent<GlopGenerator>().summonGlop();
        //this.glopGenerator_3.GetComponent<GlopGenerator>().summonGlop();

        //nextPattern = EAT; // 연계를 위해서 

        yield return new WaitForSeconds(3);
        objPrefab[1].SetActive(false);
        animator.SetBool("Walk", false);
        //nextPatternPlay(1); //다음 패턴을 실행한다.
        nextPatternPlay(Random.Range(0, 4));
    }
    [SerializeField] GameObject[] horizontalObj;
    IEnumerator Horizontal()
    {
        Debug.Log("할퀴기(가로) 패턴");
        lookPlayer();
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Horizontal");
        playerPos = player.transform.position;
        effectRangePrefab[3].transform.position = playerPos;
        horizontalObj[0].transform.position = playerPos; //할퀴기 오브젝트가 플레이어 위치로 이동
        yield return new WaitForSeconds(1f);
        StartCoroutine(EffectRange(3));
        yield return new WaitForSeconds(1.5f);
        horizontalObj[0].SetActive(true);
        ready = false;
        yield return new WaitForSeconds(3);
        //nextPatternPlay(1);
        nextPatternPlay(Random.Range(0, 4));
    }
    
    IEnumerator Vertical()
    {
        Debug.Log("할퀴기(세로) 패턴");
        lookPlayer();
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Vertical");
        playerPos = player.transform.position;

        effectRangePrefab[0].transform.position = playerPos;
        verticalObj[0].transform.position = playerPos; //할퀴기 오브젝트가 플레이어 위치로 이동
        StartCoroutine(EffectRange(0));
        yield return new WaitForSeconds(1f);
        verticalObj[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        verticalObj[0].SetActive(false);

        effectRangePrefab[1].transform.position = playerPos;
        verticalObj[1].transform.position = playerPos;
        StartCoroutine(EffectRange(1));
        yield return new WaitForSeconds(1f);
        verticalObj[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        verticalObj[1].SetActive(false);

        yield return new WaitForSeconds(1);
        //nextPatternPlay(3);
        nextPatternPlay(Random.Range(0, 4));
    }
    [SerializeField] private GameObject[] thunderObj;
    [SerializeField] private Transform spawnPoint;
    IEnumerator Thunder()
    {
        Debug.Log("번개 패턴");
        animator.SetTrigger("Thunder");
        yield return new WaitForSeconds(1);
        
        for(int i = 0; i < thunderObj.Length; i++)
        {
            playerPos = player.transform.position;
            effectRangePrefab[2].transform.position = new Vector2(playerPos.x,spawnPoint.transform.position.y);
            StartCoroutine(EffectRange(2));
            animator.SetTrigger("Thunder");
            yield return new WaitForSeconds(0.25f);
            thunderObj[i].transform.position = new Vector2 (playerPos.x, spawnPoint.transform.position.y);
            thunderObj[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        nextPatternPlay(Random.Range(0, 4));
    }
    IEnumerator Stomp() // 짓밟기 패턴
    {
        Debug.Log("짓밟기");
        animator.SetTrigger("Stomp");
        objPrefab[3].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        cameraObj.GetComponent<CameraMange>().Dolmpulse();

        //cameraObj.GetComponent<CameraMange>().CameraShake();
        
        yield return new WaitForSeconds(3);
        objPrefab[3].SetActive(false);
        nextPatternPlay(Random.Range(0, 4));
    }
    void nextPatternPlay(int nextPattern)
    {
        switch (nextPattern)
        {
            case 0:
                StartCoroutine(rush()); 
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
        }
    }
    public GameObject[] effectRangePrefab; 
    //공격 범위 표시
    IEnumerator EffectRange(int effectNum)
    {
        
        Debug.Log("공격범위 표시");
        float fadeCount = 0;
        Color rangeColor = effectRangePrefab[0].GetComponent<SpriteRenderer>().color;
        rangeColor.a = 0;
        
        effectRangePrefab[effectNum].SetActive(true);
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            effectRangePrefab[effectNum].GetComponent<SpriteRenderer>().color = new Color(255,0,0,fadeCount);

        }
        effectRangePrefab[effectNum].SetActive(false);
        yield return new WaitForSeconds(1f);
        ready = true;

    }

}
