using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static RoomController;

public class customer : MonoBehaviour
{
    [SerializeField]  RoomManager roomManager;
    [SerializeField] Animator animator;
    [SerializeField] private Transform _reseption;
    [SerializeField] private Transform _BasePosition;
   
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Collider capsulCollider;
    private GameObject _assinedRoom=null;
    private Transform _previousPosition;
    private Quaternion _previousRotation;
    private bool isBed = true;
    private bool isSwan = false;
    public void SetBasePosition(Transform _basePosition)
    {
        _BasePosition = _basePosition;
    }
    public void AssinRoom(GameObject room)
    {
        _assinedRoom = room;
    }
    private void Awake()
    {
       // roomManager=GetComponent<RoomManager>();
    }
    private void OnEnable()
    {
        if (roomManager == null)
        {
            roomManager = GetComponent<RoomManager>();
        }
      
        agent = GetComponent<NavMeshAgent>();   
        rb=GetComponent<Rigidbody>();
        capsulCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        animator.SetBool("walk",true);
        agent.destination = _reseption.position;
       
        agent.speed = 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TargetPosition" && isBed&&_assinedRoom)
        {
            isSwan=true;
            isBed = false;
            capsulCollider.enabled = false;
            animator.SetBool("walk", false);
            agent.isStopped = true;
            agent.destination = agent.transform.position;
            agent.enabled = false;
            
            RoomController roomController = _assinedRoom.GetComponent<RoomController>();
           
            Transform SlepingPosition = roomController.GetslepPosition();
            roomController.roomStates = RoomStates.IsFull;
            roomController.LightOn();
            _previousPosition = gameObject.transform;
            _previousRotation = gameObject.transform.rotation;
            transform.SetParent(SlepingPosition);
            transform.localPosition = new Vector3(-1.65f, 0.45f, -0.4f);
            Quaternion quaternion = SlepingPosition.rotation;
            transform.localRotation = Quaternion.Euler(-90, quaternion.y, -90);
            rb.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(ReasinDestination());
        }
        else if (other.gameObject.tag == "TargetPosition" && !isBed)
        {
            isBed = true;
            agent.destination = _BasePosition.position;
          
        }
        else if(other.gameObject.tag == "SwanPosition"&& isSwan)
        {
            isSwan = false;
            agent.destination = _reseption.position;
        }
    }
  
   
    public IEnumerator ReasinDestination()
    {
        yield return new WaitForSeconds(5);
        if(_BasePosition)
        {
            capsulCollider.enabled = true;
            gameObject.transform.SetParent(null);

            RoomController roomController = _assinedRoom?.GetComponent<RoomController>();
            if (roomManager == null)
            {
               
               
            }
            roomController.LightOff();
            roomManager.AddUnActiveRoom(_assinedRoom);
            transform.position = roomController.GetBedPosition().position;
            transform.rotation = Quaternion.Euler(_previousRotation.x, _previousRotation.y, _previousRotation.z);
            animator.SetBool("walk", true);
            agent.enabled = true;
            yield return new WaitForSeconds(1);
            roomController.roomStates = RoomStates.underCleanness;
            roomController.CleanRoomColliderEnable();
            roomController.EnableCanvasUnderCleaness();

        }
        else
        {
           
        }
    }
    public void AssingDestination(Transform _BasePosition)
    {
        agent.destination=transform.position;
    }
   public void AssingScript(RoomManager roomMan)
    {
        roomManager=roomMan;
    }
}
