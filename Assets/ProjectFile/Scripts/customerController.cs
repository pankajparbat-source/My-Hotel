using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class customerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> customersList = new List<GameObject>();
    private Queue<GameObject> CustomerInHotels = new Queue<GameObject>();
    // Start is called before the first frame update
    [SerializeField] private Transform _basePosition;
    [SerializeField] private Transform destination;
    [SerializeField] private RoomManager roomManager;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Awake()
    {
        foreach(var custo in customersList)
        {
            CustomerInHotels.Enqueue(custo);
        }
    }
    private void Start()
    { 
        ProcessNextObject();
    }
     public void ProcessNextObject()
     {
        if (CustomerInHotels.Count > 0)
        {
            
            GameObject nextObj=  Instantiate(CustomerInHotels.Dequeue(), _basePosition.position, Quaternion.identity);
            customer customer = nextObj.GetComponent<customer>();
            customer.GetComponent<customer>().AssingScript(roomManager);
            customer.SetBasePosition(_basePosition);
        }
        else
        {
           
        }
     }
    public void RemoveFromHotel()
    {

    }
}
