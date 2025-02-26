using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

enum GameState { Start, End, Pause, Exploring, Interacting, Viewing }

public class GameManager : MonoBehaviour
{
    private GameState GS;

    private Player MainPlayer;
    private RotatePlayer RotPlayer;
    private Inventory PlayerInventory;

    [SerializeField]
    private GameObject[] Interactions;
    
    //need to extract this
    private PlayerControls PC;

    private void Awake()
    {
        PC = new PlayerControls();
        PC.Enable();

    }

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        RotPlayer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RotatePlayer>();
        PlayerInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        Interactions = new GameObject[GameObject.FindGameObjectsWithTag("Interactables").Length];
        for(int i = 0; i < Interactions.Length; i++)
        {
            Interactions[i] = GameObject.FindGameObjectsWithTag("Interactables")[i];
        }
        
        GS = GameState.Exploring;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GS)
        {
            case GameState.Exploring:
                MainPlayer.IsActive = true;
                RotPlayer.IsActive = true;
                
                break;
            case GameState.Interacting:
            case GameState.Viewing:
                MainPlayer.IsActive = false;
                RotPlayer.IsActive = false;
                break;
         
        }

        if(Interactions.Where(x => x.GetComponent<InteractableObject>().IsDisplayed).Any())
        {
            GS = GameState.Interacting;
        }
        else if(PlayerInventory.IsViewingInventory)
        {
            GS = GameState.Viewing;
        }
        else
        {
            GS = GameState.Exploring;
        }
        
    }
    

}
