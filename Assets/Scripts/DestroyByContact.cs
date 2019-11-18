using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gamecontroller;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gamecontroller = gameControllerObject.GetComponent<GameController>();
        }
        if(gamecontroller == null)
        {
            Debug.Log("Cannt find 'GameController' Script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        

        if(other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gamecontroller.GameOver();
        }
        gamecontroller.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
