using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   
{
    public float horizotalMove;
    public float verticalMove;
    public CharacterController player;
    public float playerSpeed;
    private Vector3 playerInput;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForward; //Camara frente
    private Vector3 camRight; //Camra lado


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizotalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizotalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1); //Valor (Distancia) maximo entre un valor y otro.

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer); //El personaje gira hacia el sitio donde nos movemos

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);
    }

    void camDirection() //mover el personaje en direccion a la camara
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized; //Valor normalizado de los valores
        camRight = camRight.normalized; //Valor normalizado de los valores
    }

    //Skills del Jugador
    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    //Funcion Gravedad
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}
