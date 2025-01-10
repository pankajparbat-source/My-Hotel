using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static  PlayerController instance;
    private int PlayerMoney ;
    private float rotationSpeed =3f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private InputManager inputManager;
    private float _speed = 1f;
    private Vector2 lastTouchPosition;

    private GameObject _currentRoom=null;
    private GameObject _previousRoom=null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        inputManager.playerMovement += PlayerMovement;
        inputManager.singleTouch += SingleTouchBegan;
        inputManager.singleTouchDrag += singleTouchMoved;
    }
    private void OnDisable()
    {
        inputManager.playerMovement -= PlayerMovement;
        inputManager.singleTouch -= SingleTouchBegan;
        inputManager.singleTouchDrag -= singleTouchMoved;
    }
    public void PlayerMovement()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position+new Vector3(0,4,0), new Vector3(0, 2, 0) + transform.forward, out hit,4f, layerMask))
        {
           
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), new Vector3(0, 2, 0) + transform.forward * 4, Color.black);
            gameObject.transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
     if(collision.gameObject.tag=="Table")
        {

        }
    }
    public void SingleTouchBegan(Touch touch)
    {
      lastTouchPosition = touch.position;
    }

    public void singleTouchMoved(Touch touch)
    {
        float deltaX = touch.position.x - lastTouchPosition.x;
        float deltay = touch.position.y - lastTouchPosition.y;
        Vector2 direction= new Vector2(-deltaX, deltay).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, angle+0f,0f );
        transform.rotation = targetRotation;
    }
    public void SetMoney(int money)
    {
        PlayerMoney = PlayerMoney+ money;
        Debug.Log(PlayerMoney);
    }

    public void Money(int money)
    {
        PlayerMoney = money;
        Debug.Log(PlayerMoney);
    }
    public int GetMoney()
    {
     //   Debug.Log(PlayerMoney);
        return PlayerMoney ;
    }
    
}
