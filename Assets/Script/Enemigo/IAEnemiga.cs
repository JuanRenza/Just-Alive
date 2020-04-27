using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemiga : MonoBehaviour
{
    [SerializeField]
    public GameObject Target;
    [SerializeField]
    public NavMeshAgent agent;
    [SerializeField]
    public float distance;


   

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Target.transform.position, transform.position) < distance)
        {

            agent.SetDestination(Target.transform.position);
            agent.speed = 5;

        }
        else
        {
            agent.speed = 0;

        }




        
    }
}
