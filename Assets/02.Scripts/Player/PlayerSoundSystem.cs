using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundSystem : MonoBehaviour
{

    [SerializeField] private enum PlayerMotion { ATK1 = 0, ATK2, ATK3, AIRATK1, AIRATK2, Skill1, Skill2, Skill3, Dash }
    [SerializeField] private enum PlayerMove { Walk, Run }
    [SerializeField] private enum PlayerSkillMain { ATK1 = 0, ATK2, ATK3, AIRATK1, AIRATK2, Skill1, Skill2, Skill3 }

    [Header("AudioSource")]
    [SerializeField] private AudioSource motionAudio; // 플레이어의 액티브한 모션 사운드
    [SerializeField] private AudioSource moveAudio;   // 플레이어의 이동 사운드
    [SerializeField] private AudioSource SkillAudio;  // 플레이어의 일반공격 & 스킬 사운드. 플레이어의 일반 공격은 인스펙터에서 할당해주도록 하자.

    [Header("Other")]
    [SerializeField] private SoundDataList soundList;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerSC;
    [SerializeField] private PlayerSkillystem playerSKill;
    [SerializeField] private TouchingDirection touchingDirection;

    [Header("Flag")]
    [SerializeField] bool isMovingNow;
    [SerializeField] bool isMove;
    [SerializeField] bool isRun;
    private void Awake() {
        player = GameObject.Find("Player");
        playerSC = player.GetComponent<PlayerController>();
        playerSKill = player.GetComponent<PlayerSkillystem>();
        touchingDirection = player.GetComponent<TouchingDirection>();
    }
    private void Update() {
        MoveSound();
    }
    private void MoveSound() {
        isMovingNow = playerSC.moveInput.x != 0 && touchingDirection.IsGrounded;
        isRun = playerSC._isRun;

        if (isMovingNow && !isMove)// 입력값이 0이 아닐 경우 (걷기)
        {
            isMove = true;
            MoveSoundChange();
        }
        else if(playerSC.moveInput.x == 0 && isMove || !touchingDirection.IsGrounded)// 입력값이 0이 아닐 경우
        {
            moveAudio.Stop();
            isMove = false;
        }
            
/*
        if(!playerSC._isRun) // 이동 중이고, 뛰지 않는 도중이라면 (걷는다면)
            moveAudio.clip = soundList.playerMove[(int)PlayerMove.Walk];
        else                // 그게 아니라면 (달리기 중이라면)
            moveAudio.clip = soundList.playerMove[(int)PlayerMove.Run];
*/
    }
    private void MoveSoundChange() {
       /* if (isWalk)
        {
            if (playerSC._isRun)
            {
                isWalk = false;
                moveAudio.Stop();
                moveAudio.clip = soundList.playerMove[(int)PlayerMove.Run];
                moveAudio.Play();
            }
        }
        else
        {
            if (!playerSC._isRun)
            {
                isWalk = true;
                moveAudio.clip = soundList.playerMove[(int)PlayerMove.Walk];
            }
        }*/
    }
    private void AttackSound() { 
    }
}
