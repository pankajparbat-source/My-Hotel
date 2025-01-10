using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class receptionCollect : MonoBehaviour
{
    [SerializeField] private Reseption reseption;
    [SerializeField] private RoomAssiner roomAssiner;
    [SerializeField] private Transform _destination;
    [SerializeField] private GameObject prefabCleaner;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private TextMeshProUGUI RentText;
    [SerializeField] private Canvas _moneyCollect;
    private GameObject _cleaner = null;
    private Coroutine timerCoroutine;
    private Collider moneycollider;
    private bool isInstanciate = true;
    private int totalMoney = 0;
    private int previousPrice = 0;

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
            SwanCleaner();
            Reseption.receptional = Receptional.reception;

        }

    }
    public void SwanCleaner()
    {
        isInstanciate = true;
        _cleaner = Instantiate(prefabCleaner, transform.position, Quaternion.identity, gameObject.transform);
        NavMeshAgent navMeshAgent = _cleaner.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 1.5f;
        navMeshAgent.destination = _destination.position;
       
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
        if (_cleaner)
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
        _moneyCollect.enabled = true;
    }
    public void DisableCleanerCanva()
    {
        Debug.Log("canvas Disable");
        _moneyCollect.enabled = false;
    }
}
