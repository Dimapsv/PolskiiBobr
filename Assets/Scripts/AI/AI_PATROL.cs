using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AI_PATROL : MonoBehaviour
{
    private NavMeshAgent AI_Agent;
    private GameObject Player;
    
    public Transform[] WayPoints;
    public int Current_Patch;

    public enum AI_State { Patrol, Stay, Chase };
    public AI_State AI_Enemy;

    void Start()
    {
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (AI_Enemy == AI_State.Patrol)
        {
            AI_Agent.speed = 5;
            AI_Agent.isStopped = false;
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
            float Patch_Dist = Vector3.Distance(WayPoints[Current_Patch].transform.position, gameObject.transform.position);
            if (Patch_Dist < 2)
            {
                Current_Patch++;
                Current_Patch = Current_Patch % WayPoints.Length;
            }
        }
        if (AI_Enemy == AI_State.Stay)
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            AI_Agent.isStopped = true;
        }
        if (AI_Enemy == AI_State.Chase)
        {
            AI_Agent.speed = 12;
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            AI_Agent.SetDestination(Player.transform.position);
        }

    }
}
