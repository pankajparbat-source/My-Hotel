using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CleanerMoneyCollect : MonoBehaviour
{
    [SerializeField] private RoomAssiner roomAssiner;
    [SerializeField] private GameObject CleanerSpwanPosition;
    [SerializeField] private GameObject prefabCleaner;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private TextMeshProUGUI RentText ;
    [SerializeField] private Canvas _moneyCollect;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    private GameObject _cleaner = null;
    private Coroutine timerCoroutine;
    private Collider moneycollider;
    private bool isInstanciate = true;
    private int totalMoney = 0;
    private int previousPrice = 0;


    public delegate void CleanerAssin();
    public CleanerAssin cleanerAssin;
    private void Awake()
    {
        moneycollider = GetComponent<Collider>();
        DisableCollider();
        DisableCleanerCanva();
    }
    private void OnEnable()
    {
        roomAssiner.cleanerInstantiat += EnableCollider;
    }
    private void OnDisable()
    {
        roomAssiner.cleanerInstantiat -= EnableCollider;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Owner"))
        {
            timerCoroutine = StartCoroutine(StartTimer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Owner"))
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            //isWaiting = false;
        }
    }
    private IEnumerator StartTimer()
    {
      //  isWaiting = true;
        yield return new WaitForSeconds(0.01f);
        if (PlayerController.instance.GetMoney() > 0)
        {
            int var = PlayerController.instance.GetMoney() - 1;
            PlayerController.instance.Money(var);
            previousPrice -= 1;
            FPS.instance.CashinHand();
            RemaningMoneyRent();
        }
        if (previousPrice == 0)
        {
            DisableCleanerCanva();
            DisableCollider();
            if(_cleaner)
            {
                
            }
            else
            {
                SwanCleaner();
            }
           
        }

    }
    public void SwanCleaner()
    {
        isInstanciate = true;
        _cleaner = Instantiate(prefabCleaner, CleanerSpwanPosition.transform.position, Quaternion.identity, gameObject.transform);
        navMeshAgent = _cleaner.GetComponent<NavMeshAgent>();
        cleanerAssin?.Invoke();
        GameObject room = roomManager.RemoveUnRoom();
        if(room)
        {
            _cleaner.GetComponent<Cleaner>().cleanerStat = CleanerStat.Working;
            _cleaner.GetComponent<Cleaner>().RoomAssiner(room);
            RoomController roomController = room.GetComponent<RoomController>();
            roomController.cleanessCollider1.PositionOfCollider();
            if (!roomController.cleanessCollider1.iscolled)
            {
                Debug.Log("room name " + room.name);
                navMeshAgent.destination = roomController.cleanessCollider1.PositionOfCollider().position;
            }
            else if (!roomController.cleanessCollider2.iscolled)
            {
                Debug.Log("room name " + room.name);
                navMeshAgent.destination = roomController.cleanessCollider2.PositionOfCollider().position;
            }
            else if (!roomController.cleanessCollider3.iscolled)
            {
                Debug.Log("room name " + room.name);
                navMeshAgent.destination = roomController.cleanessCollider3.PositionOfCollider().position;
            }
        }
        else
        {
            _cleaner.GetComponent<Cleaner>().cleanerStat = CleanerStat.NotWorking;
            navMeshAgent.destination = moneycollider.transform.position;
        }
        
       
    }
    public void DisableCollider()
    {
        moneycollider.enabled = false;
    }
    public void SetMoney(int Buyprice)
    {
        totalMoney = Buyprice;
        previousPrice = Buyprice;
    }
    public void AssingSpeed(float speed)
    {
        if(_cleaner)
        {
            _cleaner.GetComponent<NavMeshAgent>().speed = speed;
        }
       
    }
    public void EnableCollider()
    {
      
        moneycollider.enabled = true;
    }
    public void RemaningMoneyRent()
    {
       
        RentText.text = previousPrice.ToString();
    }
    public void EnableCleanerCanva()
    {
        Debug.Log("canvas Enable");
        _moneyCollect.enabled= true;
    }
    public void DisableCleanerCanva()
    {
        Debug.Log("canvas Disable");
        _moneyCollect.enabled = false;
    }
    public void RoomAssiner()
    {
        if(_cleaner)
        {
            _cleaner.GetComponent<Cleaner>().RoomAssiner(roomManager.RemoveUnRoom());
        }
    }
    public GameObject AssinCleaner()
    {
        return _cleaner;
    }
}
