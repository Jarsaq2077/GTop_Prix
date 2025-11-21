using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public Text LapText;
    public GameManager gameHandler;
    public AudioSource nextLap;
    private int count = 0;
    private int laps = 0;
    private int currentIndex = 0;
    

    private void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentIndex == 1)
        {
            laps = 2;
        }else if (currentIndex == 2)
        {
            laps = 1;
        }else if(currentIndex == 3)
        {
            laps = 3;
        }
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player character has touched the final line.");
            gameHandler.StopTimer();
            nextLap.volume = 0.5f;
            nextLap.Play();
            count++;
            LapText.text = (count + 1).ToString() + " / " + laps.ToString();
            if (count >= laps)
            {                
                if(currentIndex < 3)
                {
                    SceneManager.LoadScene(currentIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(4);
                }
                
            }
        }
    }
    
}