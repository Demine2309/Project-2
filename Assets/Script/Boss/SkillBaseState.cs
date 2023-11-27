using UnityEngine;

public abstract class SkillBaseState
{
    abstract public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
    abstract public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
    abstract public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex);
}
