using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnEnemigo : MonoBehaviour
{
    public GameObject enemigo;
    public int numeroZombies;
    private int i = 0;
    public Transform respawn;

    // Update is called once per frame
    void Update()
    {
        if (i < numeroZombies)
        {
            Instantiate(enemigo, respawn.position, respawn.rotation);
            i = i + 1;
        }
    }
}
