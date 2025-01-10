using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform playerTransfrom;
    [SerializeField] private InputManager inputManager;
    private Vector3 offsetvalue;
    private float speed = 5f;
    private void Awake()
    {
      
        offsetvalue = transform.position-playerTransfrom.position;
    }
    private void OnEnable()
    {
        inputManager.cameram += PlayerFollow;
    }
    private void OnDisable()
    {
        inputManager.cameram -= PlayerFollow;
    }

    private void PlayerFollow()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransfrom.position + offsetvalue, speed * Time.deltaTime);
        //gameObject.transform.position = playerTransfrom.position+offsetvalue;
    }
}
