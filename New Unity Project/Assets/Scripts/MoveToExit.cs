using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToExit : MonoBehaviour
{
    GameObject destination;
    NavMeshAgent agent;
    void Start()
    {
        destination = GameObject.Find("Finish");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
           Destroy(gameObject);
           Debug.Log("Has Collided");
        }
    }
}


