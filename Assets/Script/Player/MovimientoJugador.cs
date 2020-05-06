using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class MovimientoJugador : MonoBehaviour
{
    CharacterController jugador;
	private Animator animator;
	private Rigidbody rb;
	private Camera mainCamera;

    public float speed;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
	public int movementType;
	public float moveSpeed;
	[SerializeField]
	public float sensibilidadCamara = 1.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		jugador = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		mainCamera = Camera.main;

		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		movimiento();
		movimientoCamara();

		if(Input.GetKeyDown(KeyCode.Escape)){Cursor.lockState=CursorLockMode.None;}
		
    }

	void Paso(){
        FindObjectOfType<AudioManager>().Play("Paso");
    }

	void movimiento(){
		
		//Animacion para morir
		if (movementType==4){animator.Play("Muerte");}
		//Movimiento al correr
		if (Input.GetKey(KeyCode.LeftShift)) {
			movementType = 3;
			speed = 7.0f;
		}
		//Movimiento al agacharse
		else if (Input.GetKey(KeyCode.LeftControl)) { 
			movementType = 2;
			speed = 2.5f;
		}
		//Movimiento al caminar
		else {
			movementType = 1;
			speed = 3.0f;
		}

        if (jugador.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
		
		animator.SetFloat("Speed", moveDirection.magnitude);
		animator.SetInteger("Type", movementType);

        moveDirection.y -= gravity * Time.deltaTime;

		//Posicionar la direcion del movimiento a la del juego
		moveDirection = transform.TransformDirection(moveDirection);
        jugador.Move(moveDirection * Time.deltaTime);
		movementType = 0;
	}

	void movimientoCamara(){

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		//Mirar a los lados
		Vector3 rotacionY = transform.localEulerAngles;
		rotacionY.y += mouseX * sensibilidadCamara;
		transform.localRotation = Quaternion.AngleAxis(rotacionY.y,Vector3.up);

		//Mirar arriba y abajo
		Vector3 rotacionX = mainCamera.gameObject.transform.localEulerAngles;
		rotacionX.x -= mouseY * sensibilidadCamara;
		mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(rotacionX.x,Vector3.right);

	}
}
