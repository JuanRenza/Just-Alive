using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemiga : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    float dist;
    public float distanciaPerseguir = 200;
    private bool perseguir = false;
    private Animator anim;
    VidaDaño vidaPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        vidaPlayer = player.GetComponent(typeof(VidaDaño)) as VidaDaño;

     
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        if(dist <= distanciaPerseguir)
        {
            perseguir = true;

        }
  
        if(perseguir == true)
        {
            agent.SetDestination(player.transform.position);
        }

        if(Vector3.Distance(player.transform.position, transform.position) > 1 && vidaPlayer.vida > 0)
        {
            anim.SetFloat("VelX", 0);
            anim.SetFloat("VelY", 1);
        }
        
            if (Vector3.Distance(player.transform.position, transform.position) > 1 && vidaPlayer.vida == 0)
            {
                //anim.SetFloat("VelX", 0);
                anim.SetFloat("VelY", 0);
                anim.SetFloat("Velx",Random.Range(-1f, 0f));
                
            }
        
              
                if(Vector3.Distance(player.transform.position, transform.position) <= 1.5f && vidaPlayer.vida > 0)
                {
                    anim.SetFloat("VelX", 1);
                    anim.SetFloat("Vely", 0);
                }
     
            if (Vector3.Distance(player.transform.position, transform.position) <= 1 && vidaPlayer.vida == 0)
            {
                anim.SetFloat("VelX", 0);
                anim.SetFloat("VelY", 0.5f);
                perseguir = false;
            }
        

                if(perseguir == false)
        {
            distanciaPerseguir = 0;
        }
                
                
                   
                
            
            
        
    }
}
