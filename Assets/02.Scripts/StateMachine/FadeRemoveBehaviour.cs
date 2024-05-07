using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour //스프라이트 페이드 처리 및 애니메이션 종료 후 게임 오브젝트 제거
{
    public float fadeTime = 0.5f; // 페이드 시간(초 단위)
    private float timeElapsed = 0f; // 페이드 진행 시간 누적 값
    SpriteRenderer spriteRenderer;
    GameObject objToRemove; // 페이드 처리 할 게임 오브젝트
    Color startColor; //스프라이트 색상

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //애니메이션 시작될 때
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f; // 페이드 누적 시간 초기화
        spriteRenderer = animator.GetComponent<SpriteRenderer>(); 
        startColor = spriteRenderer.color; //페이드 시작 시 색상 저장
        objToRemove = animator.gameObject; //제거할 오브젝트 저장
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime; //페이드 누적 시간 증가

        float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime)); //페이드를 위해 새로운 알파 값 계산

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha); //스프라이트의 알파 값을 새로운 값으로 설정

        if(timeElapsed > fadeTime) //페이드 시간 초과시 게임 오브젝트 제거
        {
            Destroy(objToRemove);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
