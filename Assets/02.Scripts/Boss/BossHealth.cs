using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BossHealth : MonoBehaviour
{
    protected float curHealth; //* 현재 체력
   // private bool death = false;
    public float maxHealth; //* 최대 체력
    private Animator animator;
    //public void SetHp(float amount) //*Hp설정
    //{
    //    maxHealth = amount;
    //    curHealth = maxHealth;
    //}
    public Slider HpBarSlider;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHitBox;
    [SerializeField] private GameObject gameClearObj;
    private Material originMater;
    private void Start()
    {
        //maxHealth = HpBarSlider.value;
        curHealth = maxHealth;
        animator = boss.GetComponent<Animator>();
        Debug.Log("최대 체력 : " + maxHealth);
        Debug.Log("현재 체력 : " + curHealth);
    }

    public void CheckHp() //*HP 갱신
    {
        if (HpBarSlider != null)
            HpBarSlider.value = curHealth / maxHealth;
    }
    
    public void Damage(float damage) //* 데미지 받는 함수
    {
        //if (maxHealth == 0 || curHealth <= 0) //* 이미 체력 0이하면 패스
        //{
        //    Debug.Log("이미 체력 0이하면 패스");
        //    return;
        //}
        curHealth -= damage;
        Debug.Log("보스의 현재 체력 " + curHealth);
        CheckHp(); //* 체력 갱신
        //if (curHealth < 50f && death != true && animator.GetBool("Groggy") == false)
        //{
        //    //GameObject.Find("Dueogsini").GetComponent<Animator>().SetBool("Groggy", true);
        //    animator.SetBool("Groggy",true);
        //    Debug.Log("체력이 5퍼 미만");
        //    Boss_Controller boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss_Controller>();
        //    boss.StartCoroutine(boss.Groggy());
        //}
        if (curHealth <= 0)
        {
            bossHitBox.SetActive(false);
            StopAllCoroutines();
            animator.SetBool("Death", true);
            originMater = bossHitBox.GetComponent<Hit>().originMaterial;
            boss.GetComponent<SpriteRenderer>().material = originMater;
            GameClear gameClear = gameClearObj.GetComponent<GameClear>();
            gameClear.StartCoroutine(gameClear.GameClearLogic());
            Debug.Log("코루틴 시작");
        }
    }

}
