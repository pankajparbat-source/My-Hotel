using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using static RoomController;

public class RoomController : MonoBehaviour
{
    public delegate void RoomAddedForCleaner();
    public RoomAddedForCleaner roomAdded;
    public GameObject cleaner;
    [SerializeField] private FPS fps;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private PlayerController playerController=null;
    [SerializeField] private TextMeshProUGUI RentText=null;
    [SerializeField] private Canvas One = null;
    [SerializeField] private Canvas Two =null;
    [SerializeField] private Canvas Three = null;
    [SerializeField] private Canvas _moneyCollect =null;
    [SerializeField] private GameObject Bed = null;
    [SerializeField] private GameObject _Position = null;
    [SerializeField] private GameObject _wall = null;
    [SerializeField] private GameObject _room = null;
    [SerializeField] private Canvas UnderCleaness;
    [SerializeField] private Canvas Light = null;

    public cleanessCollider cleanessCollider1 = null;
    public cleanessCollider cleanessCollider2 = null;
    public cleanessCollider cleanessCollider3 = null;
    public MoneyCollection _moneyCollider = null;

    public RoomStates roomStates = RoomStates.None;
    private Collider canvasCollider=null;
    public int RoomRent=0;
    public int _comapar = 0;
    public bool IsAdded = false;
    public event Action RoomAdded;
    public int RoomTip = 0;

    private void Awake()
    {
        canvasCollider = GetComponent<Collider>();
        ColliderEnable();
        DisableCanvasUnderCleaness();
    }
  
    private void OnEnable()
    {
      
    }

    public void EnableCanvasUnderCleaness()
    {
        UnderCleaness.enabled = true;
    }
    public void DisableCanvasUnderCleaness()
    {
        UnderCleaness.enabled = false;
    }
    public void CanvaEnable()
    {
       
        _moneyCollect.enabled = true;
    }

    public void CanvaDisable()
    {
        _moneyCollect.enabled = false;
    }
    public void ShowTextRent()
    {
      
        RentText.text =RoomRent.ToString();
    }
    public void RemaningMoneyRent()
    {
        RentText.text = _comapar.ToString();
    }
    private void Update()
    {
        if(cleanessCollider1.iscolled && cleanessCollider2.iscolled && cleanessCollider3.iscolled)
        {
           
            roomManager.RemoveGameObject(this.gameObject);
            roomStates = RoomStates.Clean;
            roomManager.AddTheActiveRoom(this.gameObject);
            roomStates = RoomStates.IsEmpty;
            DisableCanvasUnderCleaness();
            playerController.SetMoney(RoomTip);
            fps.CashinHand();
            if(cleaner)
            {
                
                //  GameObject room = ;
                cleaner.GetComponent<Cleaner>().cleanerStat = CleanerStat.Working;
                cleaner.GetComponent<Cleaner>().RoomAssiner(roomManager.RemoveUnRoom());
            }
           
           
            cleanessCollider1.iscolled = cleanessCollider2.iscolled = cleanessCollider3.iscolled = false;
           
        }
    }
    public void UpgradeRoom()
    {

    }
    public Transform GetBedPosition()
    {
        return Bed.transform;
    }
    public Transform GetslepPosition()
    {
        return _Position.transform;
    }
    public void SetRoomrent(int rent)
    {
       
        RoomRent = rent;
        _comapar = rent;
        RoomTip = rent /10;
    }
    public int GetRoomRent()
    {
        return RoomRent;
    }
    public void EnableWall()
    {
        if(_wall)
        {
            _wall.SetActive(true);
        }
       
    }
    public void EnableRoom()
    {
        
      _room.SetActive(true);
    }
    public void DisableWall()
    {
        if (_wall != null)
        {
            _wall.SetActive(false);
        }
        
    }
    public void DisableRoom()
    {
       _room.SetActive(false);
    }

    public void CleanRoomColliderEnable()
    {
        cleanessCollider1.EnableCollider();
        cleanessCollider2.EnableCollider();
        cleanessCollider3.EnableCollider();
    }
    public void CleanRoomColliderDisable()
    {
       
        cleanessCollider1.DisableCollider();
        cleanessCollider2.DisableCollider();
        cleanessCollider3.DisableCollider();
    }

    public void CallDelegate()
    {
        RoomAdded?.Invoke();
    }

    public void ColliderEnable()
    {
       
        canvasCollider.enabled = true;
    }
    public void ColliderDisable()
    {
        canvasCollider.enabled = false;
    }
    public void RoomEmpty()
    {

    }
    public enum RoomStates
    {
        None, IsFull,underCleanness ,Clean,IsEmpty
    }
    public void LightOn()
    {
        Light.enabled = true;
    }
    public void LightOff()
    {
        Light.enabled = false;
    }
   
}
