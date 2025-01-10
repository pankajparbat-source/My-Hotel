using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomController;

public class RoomAssiner : MonoBehaviour
{
    public delegate void CleanerInstantiat();
    public CleanerInstantiat cleanerInstantiat;
    [SerializeField] private customerController customerController;
    [SerializeField] private InputManager inputmanager;
    [SerializeField]private CleanerMoneyCollect moneyCollect;
    [SerializeField]private receptionCollect receptionCollect=null;
    [SerializeField] private LevelCollectionScript levelCollection;
    public delegate void CleanerSpeed();
    public CleanerSpeed cleanerSpeed;
    public bool isAdded = false;
    private int  _baseRoomRent=0;
    private int _thisLevelRent=0;
    private int _comparRent = 0;
    private float speed = 1.4f; 
    private GameObject _previousRoom=null;
    private GameObject _CurrentRoom=null;
    [SerializeField] GameObject leveltwoWall;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PlayerController playerController;
  
    [SerializeField] private List<GameObject> RoomList = new List<GameObject>();

    private Queue<GameObject> UpGrade1= new Queue<GameObject>();
    private Queue<GameObject> UpGrade2= new Queue<GameObject>();
    private Queue<GameObject> UpGrade3= new Queue<GameObject>();

    public LevelStates CurrentLevelstates = LevelStates.Upgrade1;

    private void Start()
    {
        CurrentLevelstates = LevelStates.Upgrade1;
        for (int i = 0; i < RoomList.Count; i++)
        {
            Debug.Log("Room name "+ RoomList[i]);
            UpGrade1.Enqueue(RoomList[i]);
        }
    }
    private void OnEnable()
    {
        moneyCollect.cleanerAssin += RoomAsiner;
        
        inputmanager.inUpdate += CallOnUpdate;
    }
    private void OnDisable()
    {
        moneyCollect.cleanerAssin -= RoomAsiner;
       
        inputmanager.inUpdate -= CallOnUpdate;
    }
    public void AddedFirstRoom()
    {
        StartCoroutine(StartCorouting());
    }
    public IEnumerator StartCorouting()
    {
        yield return new WaitForSeconds(2);
        GameObject room = UpGrade1.Dequeue();
        _previousRoom = room;
        _CurrentRoom = room;
        RoomController roomController = room.GetComponent<RoomController>();
        roomController.SetRoomrent(_thisLevelRent);
        roomController.ColliderEnable();
        roomController.RoomAdded += Roomadded;
        roomController.CleanRoomColliderDisable();
        roomController._moneyCollider.DisableCollider();
        roomController.CanvaDisable();
        _thisLevelRent += _baseRoomRent;
       
        roomManager.AddTheActiveRoom(room);
        UpGrade2.Enqueue(room);
    }
    public void CallOnUpdate()
    {
        if(_previousRoom)
        {
            if (_previousRoom.GetComponent<RoomController>().roomStates == RoomController.RoomStates.IsEmpty)
            {
                
                RoomUpdatetion();
                //  RoomUpdatetion();
                _previousRoom.GetComponent<RoomController>().roomStates = RoomController.RoomStates.None;

            }
        }
      
    
    }
  
   
    public void RoomUpdatetion()
    {
        if ((CurrentLevelstates == LevelStates.Upgrade1)&&(!isAdded))
        {
          
           

            if (UpGrade1.Count >0)
                {
                isAdded = true;
                if(_previousRoom)
                {
                    _previousRoom.GetComponent<RoomController>().RoomAdded -= Roomadded;
                }
               
                    GameObject room = UpGrade1.Dequeue();
                   _previousRoom = null;
                   _previousRoom = _CurrentRoom;
                _CurrentRoom = null;
                _CurrentRoom = room;
                if (!_previousRoom)
                {
                    _previousRoom = _CurrentRoom;
                }
                   
                   
                    RoomController roomController = room.GetComponent<RoomController>();
                    roomController.RoomAdded += Roomadded;
                    roomController.CleanRoomColliderDisable();
                    roomController.SetRoomrent(_thisLevelRent);
                    roomController.ShowTextRent();
                    roomController.CanvaEnable();
                    roomController.ColliderEnable();
                    roomController._moneyCollider.EnableCollider();
                    _thisLevelRent += _baseRoomRent;
                    _comparRent = _thisLevelRent;
                Debug.Log("Room name " + _CurrentRoom);
                if (UpGrade1.Count==2&&receptionCollect)
                    {
                   // customerController.ProcessNextObject();
                    receptionCollect.EnableCollider();
                    receptionCollect.EnableCleanerCanva();
                    receptionCollect.SetMoney(_thisLevelRent);
                    receptionCollect.RemaningMoneyRent();
                   

                }
                }
                else
                {
                levelManager._reviousRoomRent = _thisLevelRent;
                customerController.ProcessNextObject();
                
                    moneyCollect.EnableCollider();
                    moneyCollect.EnableCleanerCanva();
                    moneyCollect.SetMoney(_thisLevelRent);
                    moneyCollect.RemaningMoneyRent();
                    moneyCollect.AssingSpeed(speed);
                    speed += 0.2f;
                   // yield return new WaitForSeconds(2);
                    CurrentLevelstates = LevelStates.Upgrade2;



              
               
            }


        }
        else if (CurrentLevelstates == LevelStates.Upgrade2&&(!isAdded ))
        {
           
            if (UpGrade2.Count >0)
            {
                isAdded = true;
                _previousRoom.GetComponent<RoomController>().RoomAdded -= Roomadded;
                    GameObject room = UpGrade2.Dequeue();
                    _previousRoom = null;
                    _previousRoom = _CurrentRoom;
                    _CurrentRoom = null;
                    _CurrentRoom = room;
                    RoomController roomController = room.GetComponent<RoomController>();
                   
                    roomController.RoomAdded += Roomadded;
                    roomController.SetRoomrent(_thisLevelRent);
                    roomController.CanvaEnable();
                    roomController._moneyCollider.EnableCollider();
                    roomController.ColliderEnable();
                  //  roomController.CleanRoomColliderDisable();
                    roomController.ShowTextRent();
                    _thisLevelRent += _baseRoomRent;
                    _comparRent = _thisLevelRent;
                   
                }
                else
                {
                    customerController.ProcessNextObject();
                    moneyCollect.EnableCleanerCanva();
                    moneyCollect.SetMoney(_thisLevelRent);
                    moneyCollect.RemaningMoneyRent();
                  //  moneyCollect.AssingSpeed(2);
                    moneyCollect.EnableCollider();
                    moneyCollect.AssingSpeed(speed);
                    speed += 0.2f;
                  //  yield return new WaitForSeconds(2);
                    CurrentLevelstates = LevelStates.Upgrade3;
                }
        }
        else if (CurrentLevelstates == LevelStates.Upgrade3&& (!isAdded ))
        {
            
            if (UpGrade3.Count > 0)
                {
                isAdded = true;
                _previousRoom.GetComponent<RoomController>().RoomAdded -= Roomadded;
                     GameObject room = UpGrade3.Dequeue();
                    _previousRoom = null;
                    _previousRoom = _CurrentRoom;
                    _CurrentRoom=null;
                    _CurrentRoom = room;
                 
                    RoomController roomController = room.GetComponent<RoomController>();
                    roomController.RoomAdded += Roomadded;
                    roomController.SetRoomrent(_thisLevelRent);
                    roomController.CanvaEnable();
                    roomController.ColliderEnable();
                   // roomController.CleanRoomColliderDisable();
                    roomController.ShowTextRent();
                    roomController._moneyCollider.EnableCollider();
                    _thisLevelRent += _baseRoomRent;
                    _comparRent = _thisLevelRent;
                   // spawnCustomer?.Invoke();
                }
                else
                {

                    moneyCollect.EnableCleanerCanva();
                    moneyCollect.SetMoney(_thisLevelRent);
                    moneyCollect.RemaningMoneyRent();
                    moneyCollect.EnableCollider();
                    moneyCollect.AssingSpeed(speed);
                    speed += 0.2f;
                _thisLevelRent += _baseRoomRent;
                levelCollection.EnableCollider();
                levelCollection.EnableCleanerCanva();
                levelCollection.SetMoney(_thisLevelRent);
                CurrentLevelstates = LevelStates.none;
                // yield return new WaitForSeconds(2);

            }
          
        }
      
       

    }

    public void AssinLevelRent(int rent)
    {
        _thisLevelRent = rent;
        _baseRoomRent = rent;
    }
    public enum LevelStates
    {
        Upgrade1, Upgrade2, Upgrade3,none
    }
    private void Roomadded()
    {
       
        roomManager.AddTheActiveRoom(_CurrentRoom);
        if (CurrentLevelstates == LevelStates.Upgrade1)
        {
          
            UpGrade2.Enqueue(_CurrentRoom);
        }
        else if(CurrentLevelstates == LevelStates.Upgrade2)
        {
         
            UpGrade3.Enqueue(_CurrentRoom);
        }
        isAdded = false;
    }
    public void LevelUpgrade1()
    {
        leveltwoWall.SetActive(false);
       // levelManager.UpgredLevel();
    }
    public void CleanerRoom()
    {
        moneyCollect.RoomAssiner();
    }
    public void RoomAsiner()
    {
        for (int i = 0; i < RoomList.Count; i++)
        {
            RoomList[i].GetComponent<RoomController>().cleaner = moneyCollect.AssinCleaner();
        }
    }

}

