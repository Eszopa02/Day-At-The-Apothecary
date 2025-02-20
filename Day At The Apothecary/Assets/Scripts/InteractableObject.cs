using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    
    private GameObject ObjectName;
    private GameObject TextDisplay;
    private GameObject Canvas;

    private PlayerControls PC;

    private bool IsCloseProximity;

    public bool IsDisplayed { get; private set; }

    private void Awake()
    {
        PC = new PlayerControls();
        PC.Enable();

        PC.Movement.Interaction.performed += DisplayCanvas;
        PC.Movement.ExitScreen.performed += CloseCanvas;
    }

    private void OnDisable()
    {
        PC.Movement.Interaction.performed -= DisplayCanvas;
        PC.Movement.ExitScreen.performed -= CloseCanvas;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(ObjectName == null)
        {
            ObjectName = transform.parent.gameObject;
        }

        if (TextDisplay == null)
        {
            //Replaced getchild(0) with find from https://discussions.unity.com/t/calling-a-gameobjects-child/710799/5
            TextDisplay = transform.Find("ControlText").gameObject;
        }

        if(Canvas == null)
        {
            Canvas = transform.Find("Canvas").gameObject;
        }

        TextDisplay.SetActive(false);
        Canvas.SetActive(false);

        IsCloseProximity = false;
        IsDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayCanvas(InputAction.CallbackContext context)
    {
        if(IsCloseProximity)
        {
            ChangeCanvasCondition(false, true);
        }
    }

    private void CloseCanvas(InputAction.CallbackContext context)
    {
        if(Canvas.activeInHierarchy)
        {
            ChangeCanvasCondition(true, false);
        }
    }

    private void ChangeCanvasCondition(bool condition1, bool condition2)
    {
        TextDisplay.SetActive(condition1);
        
        Canvas.SetActive(condition2);
        IsDisplayed = condition2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SetConditions(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SetConditions(false);
    }

    private void SetConditions(bool condition)
    {
        IsCloseProximity = condition;
        TextDisplay.SetActive(condition);
    }
}
