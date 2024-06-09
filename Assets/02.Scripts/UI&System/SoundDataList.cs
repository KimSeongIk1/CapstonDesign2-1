using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 작성자 : 김장후
[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Object/SoundData", order = int.MaxValue)]
public class SoundDataList : ScriptableObject
{
    [Header("System")]
    public AudioClip[] backGround;  // 배경음악
    public AudioClip sceneChange;   // 씬 넘어갈 때 음악


    [Header("Player")]
    [TextArea(3, 5)]
    [SerializeField] private string playerInfo; // 설명용 툴팁
    // 스킬처럼 모션과 시전음이 다를 경우 플레이어의 사용 모션 클립은 0번칸에 두기

    public AudioClip[] playerMove;    // 걷기/달리기
    public AudioClip[] playerATK;     // 일반 공격
    public AudioClip[] playerAirATK;  // 공중 공격
    public AudioClip[] playerSkill1;  // 스킬 1 
    public AudioClip[] playerSkill2;  // 스킬 2
    public AudioClip[] playerSkill3;  // 스킬 3
    public AudioClip[] playerDash;  // 대쉬
    public AudioClip[] playerHit; // 피격
    public AudioClip playerDeath; // 사망

    private void Awake() {
        playerInfo = "스킬처럼 모션과 시전음이 다를 경우 플레이어의 사용 모션 클립은 0번칸에 두기";
    }
}
