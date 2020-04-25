using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;

	[SerializeField]
	private float crouchSpeed = 50f;
	private float moveSpeed = 100f;
	private float runSpeed = 200f;
	private int movementType;
	[SerializeField]
	private float turnSpeed = 100f;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var movement = new Vector3(horizontal, 0, vertical);



		if (Input.GetKey(KeyCode.LeftShift)) { characterController.SimpleMove(movement * Time.deltaTime * runSpeed); movementType = 3; }
		else if (Input.GetKey(KeyCode.LeftControl)) { characterController.SimpleMove(movement * Time.deltaTime * crouchSpeed); movementType = 2; }
		else { characterController.SimpleMove(movement * Time.deltaTime * moveSpeed); movementType = 1; }

		animator.SetFloat("Speed", movement.magnitude);
		animator.SetInteger("Type", movementType);

		if (movement.magnitude > 0)
		{
			Quaternion newDirection = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
		}





	}
}
