using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_summon : MonoBehaviour
{
    private GameObject player;
    public SkillData skillData;
    public Vector2 playerDistance;

    private void Awake() {
        player = DataManager.Instance.Player;
    }

    private void FixedUpdate() {
        playerDistance = this.transform.position - player.transform.position;
    }
    void Update()
    {
        
    }
}
