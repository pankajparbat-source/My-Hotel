using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
   
    public static RoomManager instance;
  
    public Queue<GameObject> ActiveRooms = new Queue<GameObject>();
  //  private HashSet<GameObject> ActiveRooms = new HashSet<GameObject>();
    private HashSet<GameObject> UnActiveRoom = new HashSet<GameObject>();
    private void Awake()
    {
        instance = this;
    }
  

    public void AddTheActiveRoom(GameObject obj)
    {
        ActiveRooms.Enqueue(obj);
    }
    public void AddUnActiveRoom(GameObject obj)
    {
       
        UnActiveRoom.Add(obj);
      
    }
    public GameObject RemoveUnRoom()
    {
      
        //if (UnActiveRoom.Count >= 0)
        //{
            var enumerator = UnActiveRoom.GetEnumerator();
            if (enumerator.MoveNext())
            {
                GameObject Obj = enumerator.Current;
               
                return Obj;
               // Debug.Log($"Removed the first GameObject: {firstGameObject.name}");
            }
           
        //}
       
        return null;
    }

    public GameObject RemoveRoom()
    {
       

        if (ActiveRooms.Count > 0) // Ensure the queue is not empty
        {
            GameObject obj= ActiveRooms.Dequeue();
           
            return obj;
        }
        else
        {
            
            return null; // Return null or handle it as needed
        }

    }
    public void AddGameObject(GameObject obj)
    {
        if (UnActiveRoom.Add(obj))
        {
           
        }
        else
        {
            
        }
    }

    // Remove a GameObject from the HashSet
    public void RemoveGameObject(GameObject obj)
    {
        if (UnActiveRoom.Remove(obj))
        {
           
        }
        else
        {
            
        }
    }

    // Check if a GameObject exists in the HashSet
    public bool ContainsGameObject(GameObject obj)
    {
        return UnActiveRoom.Contains(obj);
    }
}
