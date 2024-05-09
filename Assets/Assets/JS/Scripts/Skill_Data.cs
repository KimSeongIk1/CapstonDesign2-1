using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class Skill_Data : ScriptableObject
{
    public GameObject[] skillList;
    public string skillName; // 스킬 이름
    public Sprite skillIcon; // 스킬 아이콘
    public float cooldown; // 스킬 쿨다운 시간
    public GameObject projectilePrefab; // 발사체 프리팹 (있을 경우)
    public float damage; // 공격력
    public float range; // 공격 범위
}
