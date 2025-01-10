using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator m_Controller;
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private Rigidbody m_Rigidbody;
    private void OnEnable()
    {
       m_InputManager.startAnimation += walkStart;
        m_InputManager.endtAnimation += walkEnd;
        m_InputManager.playerMovement += Onsationary;
    }
    private void OnDisable()
    {
        m_InputManager.endtAnimation -= walkEnd;
        m_InputManager.startAnimation -= walkStart;
        m_InputManager.playerMovement -= Onsationary;
    }
    public void walkStart()
    {
      
        m_Controller.SetBool("walk",true);
        m_Rigidbody.constraints =  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    
}
    public void walkEnd()
    {
      
        m_Controller.SetBool("walk", false);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    public void Onsationary()
    {
        m_Rigidbody.constraints=RigidbodyConstraints.FreezeRotationY|
            RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezePositionY;
    }
}
