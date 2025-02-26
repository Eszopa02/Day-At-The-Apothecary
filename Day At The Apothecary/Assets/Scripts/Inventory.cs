using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> MainInventory = new List<Item>();

    [SerializeField]
    private List<GameObject> InventorySlots = new List<GameObject>();

    private PlayerControls PC;

    [SerializeField]
    private GameObject FoundItem;
    private GameObject UIInventory;

    private bool CanAddToInventory, CanViewInventory;
    public bool IsViewingInventory { get; private set; }    

    private void Awake()
    {
        PC = new PlayerControls();
        PC.Enable();

        PC.Movement.AccessInventory.performed += ViewInventory;
        PC.Movement.Interaction.performed += AddToInventory;
    }

    private void OnDisable()
    {
        PC.Movement.AccessInventory.performed -= ViewInventory;
        PC.Movement.Interaction.performed -= AddToInventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (UIInventory == null)
        {
            UIInventory = transform.Find("Canvas").gameObject;
        }

        UIInventory.SetActive(false);
        CanViewInventory = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            //Debug.Log("Close to item");
            CanAddToInventory = true;
            if(FoundItem == null)
            {
                FoundItem = other.transform.parent.gameObject;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanAddToInventory = false;
        if(FoundItem != null)
        {
            FoundItem = null;
        }
    }

    private void AddToInventory(InputAction.CallbackContext context)
    {
        if(CanAddToInventory)
        {
            MainInventory.Add(FoundItem.GetComponent<Item>());
            FoundItem.SetActive(false);
            FoundItem = null;

            UpdateInventory();
        }
        
    }

    private void ViewInventory(InputAction.CallbackContext context)
    {
        CanViewInventory = !CanViewInventory;
        UIInventory.SetActive(CanViewInventory);
        IsViewingInventory = CanViewInventory;
        //Debug.Log("View inventory");
    }

    private void UpdateInventory()
    {
        for(int i = 0; i < MainInventory.Count; i++)
        {
            InventorySlots[i].transform.Find("Panel").transform.Find("ItemIcon").GetComponent<Image>().sprite = MainInventory[i].ItemImage;
            InventorySlots[i].transform.Find("Panel").transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = MainInventory[i].ItemName;
        }
    }
}
