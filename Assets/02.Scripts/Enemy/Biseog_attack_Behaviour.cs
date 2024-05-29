using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biseog_attack_Behaviour : StateMachineBehaviour
{
    public GameObject Tal; // 탈깨비 프리팹 변수
    private int talCount = 0; // 생성된 탈깨비의 개수를 추적하기 위한 변수

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // attack 진입(enter) 시 탈깨비 생성
        if (Tal != null && talCount < 3)
        {
            Transform SpawnPoint = animator.transform.Find("SpawnPoint");

            if (SpawnPoint != null)
            {
                GameObject instance = Instantiate(Tal, SpawnPoint);
                instance.name = "Talkkaebie"; // 생성된 오브젝트의 이름을 "Talkkaebie"로 설정
                instance.transform.position = SpawnPoint.position; // 탈깨비를 스폰포인트 위치로 이동

                // 부모의 스케일을 적용하여 자식의 스케일을 조정
                instance.transform.localScale = Vector3.one;

                talCount++; // 생성된 탈깨비 개수 증가
            }
            else
            {
                Debug.LogError("SpawnPoint를 찾을 수 없습니다.");
            }
        }
        else if (Tal == null)
        {
            Debug.LogError("탈깨비 프리팹을 할당하지 않았습니다.");
        }
        else
        {
            Debug.LogWarning("탈깨비는 3개까지만 생성됩니다.");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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