using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour // 불 애니메이션 파라미터 제어
{
    public string boolName; //불 파라미터 이름
    public bool updateOnState; //애니메이션 상태가 진입하거나 종료될 때 파라미터 값 업데이트 여부
    public bool updateOnStateMachine;//스테이트 머신이 진입하거나 종료될 때 파라미터 값 업데이트 여부
    public bool valueOnEnter, valueOnExit;//애니메이션 상태 또는 스테이트 머신 진입할 때 혹은 종료될 때 파라미터 값


    //애니메이션 상태가 진입할 때 호출
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState) // 변수가 true 일 경우 파라미터 값을 온엔터 값으로 설정
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }


    //애니메이션 상태 종료될 때 호출
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState) // 변수가 true 일 경우 엑시트 값으로 설정
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    //스테이트 머신 진입 시 호출
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }

    //스테이트 머신 종료 시 호출
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }
}
