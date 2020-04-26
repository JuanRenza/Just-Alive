using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController jugador;
	private Animator animator;
	private Rigidbody rb;

	[SerializeField]
	private float crouchSpeed = 50f;
	private float moveSpeed = 100f;
	private float runSpeed = 200f;
	private int movementType;
	[SerializeField]
	private float turnSpeed = 100f;
	private Transform camara;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		jugador = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var movJugador = new Vector3(horizontal , 0, vertical);


		if (Input.GetKey(KeyCode.LeftShift)) {
			jugador.SimpleMove(movJugador * Time.deltaTime * runSpeed); movementType = 3;
		}
		else if (Input.GetKey(KeyCode.LeftControl)) { 
			jugador.SimpleMove(movJugador * Time.deltaTime * crouchSpeed); movementType = 2; 
		}
		else { 
			jugador.SimpleMove(movJugador * Time.deltaTime * moveSpeed); movementType = 1; 
		}

		animator.SetFloat("Speed", movJugador.magnitude);
		animator.SetInteger("Type", movementType);

		if (movJugador.magnitude > 0)
		{
			Quaternion newDirection = Quaternion.LookRotation(movJugador);
			transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
		}

	}

}
