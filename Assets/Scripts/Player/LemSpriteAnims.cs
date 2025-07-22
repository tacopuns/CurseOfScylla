using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LemSpriteAnims : MonoBehaviour
{
    public Animator spriteAnimator;

    //bool isWalking = false;

    [SerializeField] private NavMeshAgent agent;



    void Start()
    {
        spriteAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool walking = agent.pathPending || agent.remainingDistance > agent.stoppingDistance;
        spriteAnimator.SetBool("isWalking", walking);
    }
}
