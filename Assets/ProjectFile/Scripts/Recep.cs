using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Recep : MonoBehaviour
{
   private Rigidbody rd;
   private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        rd = gameObject.GetComponent<Rigidbody>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ReceptionalistPosion")
        {
           
            navMeshAgent.destination = gameObject.transform.position;
            navMeshAgent.isStopped = true;
            rd.constraints = RigidbodyConstraints.FreezeAll;


        }
      
    }
  
}
