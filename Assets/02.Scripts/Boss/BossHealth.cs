using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BossHealth : MonoBehaviour
{
    protected float curHealth; //* 현재 체력
    private bool death = false;
    public float maxHealth; //* 최대 체력
    private Animator animator;
    //public void SetHp(float amount) //*Hp설정
    //{
    //    maxHealth = amount;
    //    curHealth = maxHealth;
    //}
    public Slider HpBarSlider;
    private void Start()
    {
        //maxHealth = HpBarSlider.value;
        curHealth = maxHealth;
        //Debug.Log("amount : " + amount);
        animator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
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
            death = true;
            animator.SetBool("Groggy", false);
            //animator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
            animator.SetTrigger("Die"); //* 체력이 0 이하라 죽음
            StartCoroutine(GameClear());
            Debug.Log("코루틴 시작");
        }
    }
    [SerializeField] private GameObject clearPanel;
    IEnumerator GameClear()
    {
        yield return new WaitForSeconds(3f);
        clearPanel.SetActive(true);
    }
}
