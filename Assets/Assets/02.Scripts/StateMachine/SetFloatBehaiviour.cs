using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaiviour : StateMachineBehaviour
{
  // 변수 이름 (애니메이션 파라미터 이름)
  public string floatName;

  // 업데이트 조건 플래그들
  public bool updateOnStateEnter;  // 상태에 진입할 때 업데이트
  public bool updateOnStateExit;   // 상태를 벗어날 때 업데이트
  public bool updateOnStateMachineEnter;  // 상태 머신에 진입할 때 업데이트
  public bool updateOnStateMachineExit; // 상태 머신을 벗어날 때 업데이트

  // 상태 진입/종료 시 설정할 값
  public float valueOnEnter;
  public float valueOnExit;

  // 상태에 진입할 때 호출
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (updateOnStateEnter)
    {
      animator.SetFloat(floatName, valueOnEnter);
    }
  }

  // 상태를 벗어날 때 호출
  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (updateOnStateExit)
    {
      animator.SetFloat(floatName, valueOnExit);
    }
  }

  // 상태 머신에 진입할 때 호출
  public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
  {
    if (updateOnStateMachineEnter)
      animator.SetFloat(floatName, valueOnEnter);
  }

  // 상태 머신을 벗어날 때 호출
  public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
  {
    if (updateOnStateMachineExit)
      animator.SetFloat(floatName, valueOnExit);
  }
}