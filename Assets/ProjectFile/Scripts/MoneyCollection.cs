using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCollection : MonoBehaviour
{
    [SerializeField] private RoomController roomController;
    int totalMony = 0;
    private Collider moneycollider;

    private bool isWaiting = false;
  
    private Coroutine timerCoroutine;
    public bool iscolled = false;

    private void Awake()
    {
        moneycollider = GetComponent<Collider>();
        DisableCollider();
    }

    public void EnableCollider()
    {
        
        moneycollider.enabled = true;
    }
    public void DisableCollider()
    {
        moneycollider.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Owner")  ))
        {
            timerCoroutine = StartCoroutine(StartTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Owner"))
        {
            // Stop the timer and preserve remaining time
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            isWaiting = false;
        }
    }

    private IEnumerator StartTimer()
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.01f);
      
        if (PlayerController.instance.GetMoney() > 0) {
            int var = PlayerController.instance.GetMoney() - 1;
            totalMony++;
            PlayerController.instance.Money(var );
            roomController._comapar -= 1 ;
            FPS.instance.CashinHand();
            roomController.RemaningMoneyRent();
           
        }

        if (roomController._comapar == 0)
        {
            roomController.DisableWall();
            roomController.EnableRoom();
            roomController.CallDelegate();
            DisableCollider();
            roomController.CanvaDisable();
        }

    }
}
