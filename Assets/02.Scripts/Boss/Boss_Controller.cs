using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Controller : MonoBehaviour
{


    Animator animator;
    private int speedx = 3; // ���� �ӵ�
    Rigidbody2D rigid2D;
    public GameObject cameraObj;
    public GameObject player;
    private int DIRECTION = 2;
    private bool ready = false;
    private Vector2 playerPos;
    private SpriteRenderer sprite;

    //���� ���� ����
    private GameObject backSprite; //���� ������ ��� ������Ʈ
    private SpriteRenderer backSpriteAlpha; //�� ����� ���İ��� �����ϱ� ���� ����


    //���� ���� ���� ����
    [SerializeField] private int startPatternNum; //ù��°�� ������ ����
    [SerializeField] private int patternRange; //������ ������ ����
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        backSprite = GameObject.Find("ThunderBackGround");
        backSpriteAlpha = backSprite.GetComponent<SpriteRenderer>();
        NextPatternPlay(startPatternNum);
    }

    //�÷��̾� ������ ���� ����
    void LookPlayer()
    {
        DIRECTION = (player.GetComponent<Transform>().position.x < transform.position.x ? -1 : 1); //player�� �ڽ�(����)�� x��ǥ�� ���ؼ� ������ ����� DIRECTION�� �����Ѵ�.
        float scale = transform.localScale.z;
        transform.localScale = new Vector3(DIRECTION * -1 * scale, scale, scale); //DIRECTION������ �̿��ؼ� player���� �ٶ󺸵����Ѵ�.
    }

    // 0.���� ����
    public bool isBroken = false;
    [SerializeField] private GameObject[] rushObj;
    [SerializeField] private GameObject rushRange;
    IEnumerator Rush()
    {
        Debug.Log("���� ���� ���");

        LookPlayer();//�÷��̾� ������ ����
        animator.SetBool("RushReady", true);//���� �غ� �ִϸ��̼� ���
        //tartCoroutine(EffectRange(rushRange)); //���� ���� ǥ��
        yield return StartCoroutine(EffectRange(rushRange));

        //���� ����
        animator.SetBool("RushReady", false);
        animator.SetBool("Rush", true);
        isBroken = false;
        rushObj[0].SetActive(true); //�ǰ� �ڽ� Ȱ��ȭ
        rushObj[1].SetActive(true); //����Ʈ Ȱ��ȭ

        while (!isBroken)
        {
            yield return new WaitForSeconds(0.1f);
            if (speedx > rigid2D.velocity.x * DIRECTION) //AddForce�� �̿��ؼ� �ڿ������� �����̵��� �ϵ� speedx���� ������ �ʵ����Ѵ�.
            {
                rigid2D.AddForce(transform.right * DIRECTION * 1000);

            }
            cameraObj.GetComponent<CameraMange>().Dolmpulse(); // ī�޶� ��鸲

        }

        yield return new WaitForSeconds(2);

        rushObj[0].SetActive(false); //�ǰ� �ڽ� ��Ȱ��ȭ
        rushObj[1].SetActive(false); //����Ʈ ��Ȱ��ȭ

        animator.SetBool("Rush", false);
        yield return new WaitForSeconds(3);

        NextPatternPlay(Random.Range(0, patternRange)); //������ ���� ���� ����
    }

    // 1. ������(����) ����
    [SerializeField] GameObject[] horizontalObj;
    [SerializeField] GameObject horizontalRange;
    IEnumerator Horizontal()
    {
        Debug.Log("������(����) ����");
        LookPlayer();
        yield return new WaitForSeconds(1);

        StartCoroutine(EffectRange(horizontalRange)); //���� ���� ���̱�
        yield return StartCoroutine(EffectRange(horizontalRange)); //EffectRange �ڷ�ƾ�� ���������� ���
        animator.SetTrigger("Horizontal");
        horizontalObj[0].SetActive(true);
        yield return new WaitForSeconds(3);

        NextPatternPlay(Random.Range(0, patternRange));
    }
    // 2.������(����) ����
    [SerializeField] private GameObject[] verticalObj;
    [SerializeField] private GameObject[] verticalRange;
    IEnumerator Vertical()
    {
        Debug.Log("������(����) ����");
        LookPlayer();
        yield return new WaitForSeconds(1);


        playerPos = player.transform.position;
        StartCoroutine(EffectRange(verticalRange[0]));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(EffectRange(verticalRange[1])); //EffectRange �ڷ�ƾ�� ���������� ���       
        animator.SetTrigger("Vertical");
        yield return new WaitForSeconds(0.5f);
        verticalObj[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        verticalObj[1].SetActive(true);
        //effectRangePrefab[0].transform.position = playerPos;
        //verticalObj[0].transform.position = playerPos; //������ ������Ʈ�� �÷��̾� ��ġ�� �̵�
        yield return new WaitForSeconds(3f);
        NextPatternPlay(Random.Range(0, patternRange));
    }

    // 3.���� ����
    [SerializeField] private GameObject[] thunderEffectObj; //���� ���� ����Ʈ
    [SerializeField] private GameObject[] thunderObj; //���� ������Ʈ
    [SerializeField] private GameObject[] thunderRange; //���� ����
    IEnumerator Thunder()
    {
        Debug.Log("���� ����");
        
        thunderEffectObj[1].SetActive(true);
        thunderEffectObj[2].SetActive(true);
        // ���� ���� �����Ͽ� ����� ��Ӱ� �ϴ� ����
        do
        {
            Color color = backSpriteAlpha.color;
            color.a += Time.deltaTime * 0.5f;
            backSpriteAlpha.color = color;
            yield return null;
        } while (backSpriteAlpha.color.a <= 0.5f);
        animator.SetTrigger("Thunder");
        thunderEffectObj[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
        {
            animator.SetTrigger("Thunder");
            StartCoroutine(EffectRange(thunderRange[i]));
            StartCoroutine(EffectRange(thunderRange[i+1]));
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
        // ���� ���� �����Ͽ� ����� ��� �ϴ� ����
        thunderEffectObj[0].SetActive(false);
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

    // 4.����� ����
    [SerializeField] private GameObject stompObj;
    [SerializeField] private GameObject stompRange;
    IEnumerator Stomp()
    {
        Debug.Log("����� ����");
        for (int i = 0; i < 3; i++)
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
    // 5.�����̵� ����
    [SerializeField] private GameObject[] teleportObj;
    IEnumerator Teleport()
    {
        Debug.Log("�ڷ���Ʈ ����");
        teleportObj[0].SetActive(true);
        do
        {
            Color color = sprite.color;
            color.a -= Time.deltaTime * 1f;
            sprite.color = color;
            yield return null;
        } while (sprite.color.a >= 0f);
        teleportObj[0].SetActive(false);
        yield return new WaitForSeconds(2);
        teleportObj[1].SetActive(true);
        teleportObj[2].SetActive(true);
        playerPos = player.transform.position;
        gameObject.transform.position = playerPos;
        do
        {
            Color color = sprite.color;
            color.a += Time.deltaTime * 1.5f;
            sprite.color = color;
            yield return null;
        } while (sprite.color.a <= 1f);
        teleportObj[1].SetActive(false);
        teleportObj[2].SetActive(false);
        teleportObj[3].SetActive(true);
        
        //teleportObj[3].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        teleportObj[3].SetActive(false);
        // teleportObj[3].SetActive(false);
        yield return new WaitForSeconds(2);
        NextPatternPlay(Random.Range(0, patternRange));
    }

    // 6.�߾� ���� . ���� ü�� 5�� ���Ͻ� ����
    [SerializeField] private GameObject[] groggyObj;
    [SerializeField] private GameObject[] groggyRange;
    public IEnumerator Groggy()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("Groggy", false);
        //���� ����
        for (int i = 0; i < 3; i++)
        {
            
            if (ready != true) yield return null;
            StartCoroutine(EffectRange(groggyRange[i]));
            yield return new WaitForSeconds(2.5f);
            animator.SetTrigger("GroggyAttack");
            groggyObj[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            groggyObj[i].SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        NextPatternPlay(6);

    }
    // �������� ���۵Ǵ� ������ �������ִ� ����
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

    //���� ���� ǥ��
    IEnumerator EffectRange(GameObject rangeObject)
    {

        Debug.Log("���ݹ��� ǥ��");
        float fadeCount = 0;
        Color rangeColor = rangeObject.GetComponent<SpriteRenderer>().color;
        rangeColor.a = 0;

        rangeObject.SetActive(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            rangeObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, fadeCount);

        }
        while (fadeCount >= 0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            rangeObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, fadeCount);

        }
        yield return new WaitForSeconds(0.5f);

        ready = true;

    }


    //[SerializeField] private GameObject[] effectObj;
    //public void EnableObj(int num)
    //{
    //    effectObj[num].SetActive(true);
    //}

    public void DestroyObj()
    {
        Destroy(this.gameObject);
        
    }
}
