using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleaningTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    private void OnEnable()
    {
        agent=gameObject.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
