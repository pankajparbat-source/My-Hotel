using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static RoomController;
using static UnityEngine.UI.GridLayoutGroup;

public class Reseption : MonoBehaviour
{
    public static Receptional receptional = Receptional.Owner;
    private string collidedobj = "Owner";
    [SerializeField] private PlayerController controller;
    [SerializeField] private FPS fPS;
    private int reseptionCounter=0;
    private bool isCostomer=false;
    private bool isOwner=false;
    private GameObject CollidedCustomer=null;
    private GameObject Owner = null;
    [SerializeField] private RoomManager roomManager;
    private bool flag=false;
 
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Receptionist" && !isOwner)
        {
            flag = true;
            collidedobj = "bhb";
           
            Owner = collision.gameObject;
            isOwner = true;
           
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
      
        if (collision.gameObject.tag == "Customer" && !isCostomer)
        {

            isCostomer = true;
            CollidedCustomer = collision.gameObject;
           
        }
        if (collision.gameObject.tag == collidedobj && !isOwner)
        {
            
            Owner = collision.gameObject;
            isOwner = true;
        }

        if ((isOwner && isCostomer))
        {
           StartCoroutine( Setdestination(CollidedCustomer));
        }
    }
  

    private void Update()
    {
        if (CollidedCustomer)
        {
            if ((Vector3.Distance(transform.position, CollidedCustomer.transform.position)) > 1.5f)
            {
                isCostomer = false;
            }

        }
        if (Owner)
        {

            if (Vector3.Distance(transform.position, Owner.transform.position) > 1.5f&&!flag)
            {
               
                isOwner = false;
            }
        }

    }
    private IEnumerator Setdestination(GameObject Customer)
    {
        yield return new WaitForSeconds(0.01f);
        GameObject room = roomManager.RemoveRoom();
        if(room)
        {
            NavMeshAgent agent = Customer.GetComponent<NavMeshAgent>();
            customer customerScript = Customer.GetComponent<customer>();
            customerScript.AssinRoom(room);
            RoomController roomController = room.GetComponent<RoomController>();
            roomController.roomStates = RoomStates.IsFull;
            agent.destination = roomController.GetBedPosition().position;
            reseptionCounter = roomController.GetRoomRent();
            controller.SetMoney(reseptionCounter);
            fPS.CashinHand();
            Debug.Log(isCostomer + " " + isOwner);
            isCostomer = isOwner = false;
        }
      
    }
}
public enum Receptional
{
   None, Owner, reception
}
