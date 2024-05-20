using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Scarecrow;

public class Boss_Move : StateMachineBehaviour
{
    public float speed = 1.5f;
    public float attackRange = 6f;
    bool isRandomTriggered = false;

    
    Animator animator;
    Transform player;
    Rigidbody2D rb;
    Boss boss;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        isRandomTriggered = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (!isRandomTriggered && Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            isRandomTriggered = true; // 랜덤 변수가 설정되었음을 표시
            int randomValue = Random.Range(0, 3);
            Debug.Log(randomValue.ToString());

            switch (randomValue)
            {
                case 0:
                    animator.SetTrigger("HV_P");
                    break;
                case 1:
                    animator.SetTrigger("THUNDER_P");
                    break;
                case 2:
                    animator.SetTrigger("STOMP_P");
                    break;
                default:
                    break;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("HV_P");
        animator.ResetTrigger("THUNDER_P");
        animator.ResetTrigger("STOMP_P");
        isRandomTriggered = false; // 랜덤 변수가 설정되었음을 
    }
   
}
