using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class SkillsOfBoss
{
    abstract public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
    abstract public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
    abstract public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
}
