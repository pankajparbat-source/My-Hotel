using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void FunctionInUpdate();
    public FunctionInUpdate inUpdate;

    public delegate void cameramove();
    public cameramove cameram;

    public delegate void PlayerMovement();
    public PlayerMovement playerMovement;


    public delegate void StartAnimation();
    public StartAnimation startAnimation;

    public delegate void EndtAnimation ();
    public EndtAnimation endtAnimation;

    private int currentTouchCount=0;
    private int previouseTouch=0;


    public delegate void StartDrag(Touch touch);
    public StartDrag startDrag;

    // this delegates for camera
    public delegate void SingleTouch(Touch touch);
    public SingleTouch singleTouch;

    public delegate void SingleTouchDrag(Touch touch);
    public SingleTouch singleTouchDrag;
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        inUpdate?.Invoke();
        if (Input.touchCount > 0)
        {
            Touch zeroTouch = Input.GetTouch(0);
            if (currentTouchCount != previouseTouch)
            {
                singleTouch?.Invoke(zeroTouch);
            }
            switch (zeroTouch.phase)
            {
                case TouchPhase.Began:
                    startAnimation?.Invoke();
                    startDrag?.Invoke(zeroTouch);
                    singleTouch?.Invoke(zeroTouch);
                    break;

                case TouchPhase.Moved:
                    playerMovement?.Invoke();
                    singleTouchDrag?.Invoke(zeroTouch);
                    break;

               
                case TouchPhase.Stationary:
                    playerMovement?.Invoke();
                    break;
                case TouchPhase.Ended:
                    endtAnimation?.Invoke();
                    break;
                case TouchPhase.Canceled:
                   
                    break;
            }
        }
    }
    private void LateUpdate()
    {
        cameram?.Invoke ();
    }
    

}
