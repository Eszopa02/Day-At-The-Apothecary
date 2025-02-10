using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IEntity
{
    private PlayerControls PC;
    private InputAction Move;

    [SerializeField]
    private float movementSpeed;

    public Vector3 Direction { get; set; }
    public float MovementSpeed { get => movementSpeed; 
        set 
        { 
            movementSpeed = value;
        } 
    }
    public float DefaultSpeed { get; set; }
    
   
    private void Awake()
    {
        PC = new PlayerControls();
        PC.Enable();
        
        Move = PC.Movement.Directions;
        Move.Enable();

        //PC.Movement.Directions.performed += PlayerMove;
    }

    private void OnDisable()
    {
        Move.Disable();
        //PC.Movement.Directions.performed -= PlayerMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        MovementSpeed = MovementSpeed == 0 ? 5 : MovementSpeed; //checks and assigns if value is 0
        DefaultSpeed = MovementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
    }

    //Moves the player based on which buttons are pressed and move to 
    //correct vector (x and z)
    private void PlayerMove()
    {
        var temp = Move.ReadValue<Vector3>();   
        
        //Might be redunant and not needed (the line below)
        Direction = temp;
        
        var dir = new Vector3(Direction.x, 0, Direction.z);

        transform.Translate(dir * MovementSpeed * Time.fixedDeltaTime);
    }

   
   
}

//Main component for any entity that share specific properties
public interface IEntity
{
    public Vector3 Direction { get;set;}
    public float MovementSpeed { get; set; }
    public float DefaultSpeed { get; set; }
}