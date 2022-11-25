using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAnimationStateController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private SmarterAI ai;
    private bool isWalking;
    private NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("IsWalking", true);
        } else
        {
           animator.SetBool("IsWalking", false);
        }
    }
}
