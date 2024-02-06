using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    LuEnemy luEnemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        luEnemy = animator.GetComponent<LuEnemy>();
        luEnemy.MoveToRandomWaypoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!luEnemy.agent.pathPending)
        {
            if (luEnemy.agent.remainingDistance <= luEnemy.agent.stoppingDistance)
            {
                if (!luEnemy.agent.hasPath || luEnemy.agent.velocity.sqrMagnitude == 0f)
                {
                    luEnemy.MoveToRandomWaypoint();
                    Debug.Log("Path Ended and wil random");
                }
            }
        }

        if(luEnemy.distanceToPlayer < luEnemy.playerFollowRange)
        {
            animator.SetBool("isChase", true);
            animator.SetBool("isPatrol", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}