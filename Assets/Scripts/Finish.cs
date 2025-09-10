using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameManager gameHandler;
    public AudioSource nextLap;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player character has touched the line.");
            gameHandler.StopTimer();
            nextLap.volume = 0.5f;
            nextLap.Play();
        }
    }
}