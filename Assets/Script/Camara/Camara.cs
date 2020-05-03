using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField]
    public Transform jugador;
    [SerializeField]
    private Vector3 posicionRelativa;
    [Range(0.01f,1.0f)]
    public float smoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        posicionRelativa = transform.position-jugador.position;

        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = jugador.position + posicionRelativa ;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        
    }
}

