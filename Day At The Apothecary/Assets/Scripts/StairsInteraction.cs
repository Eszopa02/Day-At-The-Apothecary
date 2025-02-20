using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StairsInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform TargetPosition;

    private GameObject MainPlayer;

    private GameObject TextDisplay;
    private PlayerControls PC;

    private bool IsCloseProximity;

    private void Awake()
    {
        PC = new PlayerControls();
        PC.Enable();

        PC.Movement.Interaction.performed += ChangePosition;
        
    }

    private void OnDisable()
    {
        PC.Movement.Interaction.performed -= ChangePosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (TextDisplay == null)
        {
            TextDisplay = transform.Find("ControlText").gameObject;
        }

        TextDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangePosition(InputAction.CallbackContext context)
    {
        if (IsCloseProximity)
        {
            MainPlayer.transform.position = TargetPosition.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainPlayer = other.gameObject;
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
