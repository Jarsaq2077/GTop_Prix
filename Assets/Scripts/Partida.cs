using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida : MonoBehaviour
{
    public GameManager gameHandler;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player character has touched the line.");
            gameHandler.StartTimer();
        }
    }
}