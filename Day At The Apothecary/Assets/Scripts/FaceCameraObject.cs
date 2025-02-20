using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraObject : MonoBehaviour
{
    void Update()
    {
        // help from https://discussions.unity.com/t/how-to-make-a-3d-tmp-text-always-face-to-the-camera/876552/3
        this.transform.LookAt(Camera.main.transform);
        transform.RotateAround(transform.position, transform.up, 180f); 

    }
}
