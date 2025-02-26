using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemState { PlaceHolder, Random, Potion, Tool, Nature}

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName, itemDescription;

    [SerializeField]
    private Sprite itemImage;

    //[SerializeField]
    public string ItemName { get => itemName;  private set { itemName = value; } }
    public string ItemDescription { get => itemDescription; private set { itemDescription = value; } }

    [SerializeField]
    public Sprite ItemImage { get => itemImage; private set { itemImage = value; } }

    [SerializeField]
    private ItemState ItemType;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Item";
        if(ItemType == ItemState.PlaceHolder)
        {
            ItemName = "What Am I?";
            ItemDescription = "Who knows what I am, all I know I exist";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
