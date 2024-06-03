using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject clearCamera;
    [SerializeField] private GameObject bossUI;
    [SerializeField] private GameObject boss;
    private Animator animator;

    private void Start()
    {
        animator = boss.GetComponent<Animator>();
    }
    public IEnumerator GameClearLogic()
    {
        
        bossUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        mainCamera.SetActive(false);
        clearCamera.SetActive(true);
        animator.SetTrigger("Die"); //* 체력이 0 이하라 죽음
        yield return new WaitForSeconds(3f);
        clearCamera.SetActive(false);
        mainCamera.SetActive(true);
        clearPanel.SetActive(true);
    }
}
