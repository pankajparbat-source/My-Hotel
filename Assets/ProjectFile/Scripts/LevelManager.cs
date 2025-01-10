using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int level = 1;
    public int _roomRent = 20;
    public int _reviousRoomRent = 0;
    public static LevelManager instance;
    private int _levelIndex = 0;
    [SerializeField] private List<GameObject> LevelList = new List<GameObject>();
  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
       
        LevelList[_levelIndex].SetActive(true);
        LevelList[_levelIndex].GetComponent<RoomAssiner>().AssinLevelRent(_roomRent)  ;
        LevelList[_levelIndex].GetComponent<RoomAssiner>().AddedFirstRoom();
       
        _levelIndex ++;
    }

    public IEnumerator UpgredLevel()
    {
        
        LevelList[_levelIndex].SetActive(true);
        
        RoomAssiner roomAssiner = LevelList[_levelIndex].GetComponent<RoomAssiner>();
        roomAssiner.AssinLevelRent(_reviousRoomRent);
        roomAssiner.isAdded = false;
        yield return new WaitForSeconds(0.1f);
        roomAssiner.RoomUpdatetion();
       
        _levelIndex++;
    }
    public void Upgred()
    {

     StartCoroutine( UpgredLevel());
    }
}
