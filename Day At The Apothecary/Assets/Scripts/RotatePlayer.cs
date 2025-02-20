using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Code from Eden Szopas and from Unity Ace 
public class RotatePlayer : MonoBehaviour, IPlayer
{
    public Transform MainPlayer;
    public float MouseSensitivity = 2f;
    float CameraVerticalRotation = 0f;

    public bool IsActive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = MainPlayer == null ? GameObject.FindGameObjectWithTag("Player").transform : MainPlayer;
        MouseSensitivity = MouseSensitivity == 0 ? 0.25f : MouseSensitivity;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActive)
        {
            PlayerRotation();
        }
        
    }
    
    private void PlayerRotation()
    {
        //https://stackoverflow.com/questions/64327135/how-to-convert-input-getaxismouse-x-or-input-getaxismouse-y-to-the-new-i
        //Helped with converting mouse detection from new Input System
        float inputX = Mouse.current.delta.x.ReadValue() * MouseSensitivity;
        float inputY = Mouse.current.delta.y.ReadValue() * MouseSensitivity;

        CameraVerticalRotation -= inputY;
        CameraVerticalRotation = Mathf.Clamp(CameraVerticalRotation, -12f, 50f);
        transform.localEulerAngles = Vector3.right * CameraVerticalRotation;

        MainPlayer.Rotate(Vector3.up * inputX);
    }
}
