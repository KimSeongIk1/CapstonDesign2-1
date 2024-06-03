using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCoolDown : MonoBehaviour
{
    public Image[] SkillIcon = new Image[4];
    public SkillData[] skillList;// 사용 가능한 스킬 목록(미사용)
    public PlayerController player;
    private void Awake() {
        for(int i = 0; i < skillList.Length; i++)
        {
            skillList[i] = player.skillList[i];
        }
    }
    void Update()
    {
        if (player.skillOn)
            StartCoroutine(SkillCoolDown(player.selectedSkillIndex));
    }
    public IEnumerator SkillCoolDown(int skillImdex) {
        print(skillImdex+"이야이야이");
        float nowtime = skillList[skillImdex].cooldown;
        SkillIcon[skillImdex].fillAmount = 0;
        if (nowtime > 0){
            nowtime -= Time.deltaTime;
            SkillIcon[skillImdex].fillAmount = (nowtime / skillList[skillImdex].cooldown);
        }
        yield return null;
    }
}
