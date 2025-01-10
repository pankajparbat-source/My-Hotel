using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cleaner : MonoBehaviour
{
    public CleanerStat cleanerStat = CleanerStat.None;
    [SerializeField] private RoomManager roomManager;
    private GameObject Room=null;
    private RoomController roomController=null;
    private NavMeshAgent navMeshAgent;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void RoomAssiner(GameObject room)
    {
        if(room)
        {
            Debug.Log("room is assined " + room.name);
            Room = room;
            roomController = room.GetComponent<RoomController>();
        }
        else
        {
            Debug.Log("not working");
            cleanerStat = CleanerStat.NotWorking;
        }
       
        
    }
    private void Update()
    {
        if(roomController)
        {
            if (!roomController.cleanessCollider1.iscolled)
            {
                navMeshAgent.destination = roomController.cleanessCollider1.PositionOfCollider().position;
            }
            else if (!roomController.cleanessCollider2.iscolled)
            {
                navMeshAgent.destination = roomController.cleanessCollider2.PositionOfCollider().position;
            }
            else if (!roomController.cleanessCollider3.iscolled)
            {
                navMeshAgent.destination = roomController.cleanessCollider3.PositionOfCollider().position;
            }
          
        }
        if (cleanerStat == CleanerStat.NotWorking)
        {
            Debug.Log("assin new room to the cleaner");
            RoomAssiner(roomManager.RemoveUnRoom());
        }
       

    }
    private void OnTriggerEnter(Collider other)
    {
      
    }
}
public enum CleanerStat
{
  None , Working,NotWorking
}
