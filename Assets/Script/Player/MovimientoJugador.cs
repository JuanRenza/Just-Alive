using UnityEngine;
using System.Collections;

public class MovimientoJugador : MonoBehaviour
{
    CharacterController jugador;
	private Animator animator;
	private Rigidbody rb;
	private Camera mainCamera;

	[SerializeField]
    public float speed = 6.0f;
	[SerializeField]
    public float alturaSalto = 8.0f;
	[SerializeField]
    public float gravity = 20.0f;
	private Vector3 direccion;


	public int movementType;
	[SerializeField]
	public float sensibilidadCamara = 1.5f;

    

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
		movementType = 0;

		//Movimiento al caminar
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			movementType = 1;
			speed = 3.0f;
		}
		//Movimiento al agacharse
		if (Input.GetKey(KeyCode.LeftControl)) { 
			movementType = 2;
			speed = 2.5f;
		}
		//Movimiento al correr
		if (Input.GetKey(KeyCode.LeftShift)) {
			movementType = 3;
			speed = 7.0f;
		}

		if (jugador.isGrounded)
        {

			float Horizontal = Input.GetAxis("Horizontal");
			float Vertical = Input.GetAxis("Vertical");
            direccion = new Vector3(Horizontal, 0.0f, Vertical);
			direccion.Normalize();
            direccion *= speed;

			//Posicionar la direcion del movimiento a la del juego
			direccion = transform.TransformDirection(direccion);

            if (Input.GetButton("Jump"))
            {
                direccion.y = alturaSalto;
            }
        }

        direccion.y -= gravity * Time.deltaTime;
        jugador.Move(direccion * Time.deltaTime);

		animator.SetFloat("Speed", direccion.magnitude);
		animator.SetInteger("Type", movementType);
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
		//rotacionX.x = Mathf.Clamp(rotacionX.x,-90f,90f);
		rotacionX.x -= mouseY * sensibilidadCamara;
		mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(rotacionX.x,Vector3.right);

	}
}
