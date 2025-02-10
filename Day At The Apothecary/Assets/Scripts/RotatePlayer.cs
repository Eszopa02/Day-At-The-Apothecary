using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    private float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RotationSpeed = RotationSpeed == 0 ? 10 : RotationSpeed;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotate();
    }
    
    //might have to redo this section to better follow mouse =_=
    private void PlayerRotate()
    {
        /*
         Some help from:
        https://stackoverflow.com/questions/68700056/how-to-check-if-my-mouse-pointer-in-the-left-right-part-of-my-screen-on-unity-c

         */
        var temp = new Vector2();
        if (Input.mousePosition.x < Screen.width / 3f) //left
        {
            temp.y = -1;
        }
        else if(Input.mousePosition.x > (Screen.width / 3f) * 2) // => right half
        {   
            temp.y = 1;
        }
        else
        {
            temp.y = 0;
        }

        /*
        if(Input.mousePosition.y < Screen.height / 3f) //down
        {
            temp.x = 1;
        }
        else if(Input.mousePosition.y > (Screen.width / 3f) * 2)
        {
            temp.x = -1;
        }*/
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + temp.x, transform.eulerAngles.y + temp.y, 0);
    }
}
