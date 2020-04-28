using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class VidaDaño : MonoBehaviour
{
	private Animator animator;
	public int vida = 100;
	public bool invencible = false;
	public float tiempo_invencible = 1f;
	public float tiempo_frenado = 0.2f;

	private void start(){
		animator = GetComponent<Animator>();
	}


	public void restarVida(int cantidad) 
	{
		if(!invencible && vida > 0)
		{
			vida -= cantidad;
			StartCoroutine(Invulnerabilidad());
			StartCoroutine(FrenarVelocidad());
		}
	}

	IEnumerator Invulnerabilidad()
	{
		invencible = true;
		yield return new WaitForSeconds(tiempo_invencible);
		invencible = false;
	}

		IEnumerator FrenarVelocidad()
	{
		var velocidadactual = GetComponent<MovimientoJugador>().moveSpeed;
		GetComponent<MovimientoJugador>().moveSpeed = 0;
		yield return new WaitForSeconds(tiempo_frenado);
		GetComponent<MovimientoJugador>().moveSpeed = velocidadactual;
	}
}
