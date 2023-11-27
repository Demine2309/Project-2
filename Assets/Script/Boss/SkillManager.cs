using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    SkillBaseState currentSkill;

    JumpAttack jump = new JumpAttack();
    SpitAttack spit = new SpitAttack();
    SwipeAttack swipe = new SwipeAttack();

    private void Start()
    {

    }

    private void Update()
    {

    }
}
