using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
	private CharacterController jugador;
	private Animator animator;
	private Rigidbody rb;

	[SerializeField]
	public float moveSpeed;
	public int movementType;
	[SerializeField]
	private float turnSpeed = 100f;
	private Transform camara;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		jugador = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var movJugador = new Vector3(horizontal , 0, vertical);

		//Animacion para morir
		if (movementType==4){animator.Play("Muerte");}

		//Movimiento al correr
		if (Input.GetKey(KeyCode.LeftShift)) {
			movementType = 3;
			moveSpeed = 200f;
		}
		//Movimiento al agacharse
		else if (Input.GetKey(KeyCode.LeftControl)) { 
			movementType = 2;
			moveSpeed = 50f;
		}
		//Movimiento al caminar
		else {
			movementType = 1;
			moveSpeed = 100f;
		}

		jugador.SimpleMove(movJugador * Time.deltaTime * moveSpeed);

		animator.SetFloat("Speed", movJugador.magnitude);
		animator.SetInteger("Type", movementType);

		if (movJugador.magnitude > 0)
		{
			Quaternion newDirection = Quaternion.LookRotation(movJugador);
			transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
		}

	}

}
