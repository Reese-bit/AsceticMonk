using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float attackRange = 3f;

    private Transform player;
    private Rigidbody2D rb;
    private NewBoss boss;
    private Vector2 target;
    private Vector2 newPos;
    private int currentAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<NewBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentAttack = Random.Range(1, 4);
        boss.LookAtPlayer();

        var position = rb.position;
        target = new Vector2(player.position.x, position.y);
        newPos = Vector2.MoveTowards(position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position,rb.position) <= attackRange)
        {
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

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
