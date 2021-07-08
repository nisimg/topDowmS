using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Controller : MonoBehaviour 
{
    Animator anim;
    NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
     
       if (agent.velocity.sqrMagnitude > 0)
        {
            anim.SetBool("InMove", true);
        }
        else
        {
            anim.SetBool("InMove", false);
        }
    }

   
}