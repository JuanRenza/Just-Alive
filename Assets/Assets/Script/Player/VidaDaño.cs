using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class VidaDaño : MonoBehaviour
{
	private Animator animator;
	private CharacterController jugador;
	public int vida = 100;
	public bool invencible = false;
	public float tiempo_invencible = 1f;
	public float tiempo_frenado = 0.2f;

	private void start(){
		jugador = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animator.Play("Daño");
	}


	public void restarVida(int cantidad) 
	{
		if(!invencible && vida > 0)
		{
			vida -= cantidad;
			StartCoroutine(Invulnerabilidad());

			if (vida <= 0){GetComponent<MovimientoJugador>().movementType = 4;}

		}
	}
	

	IEnumerator Invulnerabilidad()
	{
		invencible = true;
		yield return new WaitForSeconds(tiempo_invencible);
		invencible = false;
	}
}
