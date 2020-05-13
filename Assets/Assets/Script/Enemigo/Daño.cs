using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class Daño : MonoBehaviour
{

    public int cantidad = 10;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            other.GetComponent<VidaDaño>().restarVida(cantidad);
        } 
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player") 
        {
            other.GetComponent<VidaDaño>().restarVida(cantidad);
        } 
    }

}
