using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameManager gameHandler;
    public AudioSource nextLap;
    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player character has touched the final line.");
            gameHandler.StopTimer();
            nextLap.volume = 0.5f;
            nextLap.Play();
            if (count >= 2)
            {
                SceneManager.LoadScene("Nivel 2");
            }
            else
            {
                count++;
            }
        }
    }
    
}