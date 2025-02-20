using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

enum GameState { Start, End, Pause, Exploring, Interacting }

public class GameManager : MonoBehaviour
{
    private GameState GS;

    private Player MainPlayer;
    private RotatePlayer RotPlayer;

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
                MainPlayer.IsActive = false;
                RotPlayer.IsActive = false;
                break;
         
        }

        if(Interactions.Where(x => x.GetComponent<InteractableObject>().IsDisplayed).Any())
        {
            GS = GameState.Interacting;
        }
        else
        {
            GS = GameState.Exploring;
        }
        
    }
    

}
