using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class LevelCollectionScript : MonoBehaviour
{

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject _wall;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private TextMeshProUGUI RentText;
    [SerializeField] private Canvas _moneyCollect;
    private GameObject _cleaner = null;
    private Coroutine timerCoroutine;
    private Collider moneycollider;
    private bool isInstanciate = true;
    private int totalMoney = 0;
    private int previousPrice = 0;

    public delegate void LevelUpgrade();
    public LevelUpgrade levelUpgrade;
    private void Awake()
    {
        moneycollider = GetComponent<Collider>();
        DisableCollider();
        DisableCleanerCanva();
    }
    private void OnEnable()
    {
      
    }
    private void OnDisable()
    {
       
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
            SwanLevel();
           

        }

    }
    public void SwanLevel()
    {
      
        _wall.SetActive(false);
        levelManager.Upgred();
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
        
        _moneyCollect.enabled = true;
    }
    public void DisableCleanerCanva()
    {
      
        _moneyCollect.enabled = false;
    }
}
