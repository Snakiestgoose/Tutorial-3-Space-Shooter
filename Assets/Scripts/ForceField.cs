using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{

    public PlayerController playerController;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" ||  other.tag == "Boundary")
        {
            return;
        }

        playerController.ffCollision();
    }


}
