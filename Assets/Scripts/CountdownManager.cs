using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CountdownManager : MonoBehaviour
{
    public Text countdownText; 

    public PlayerMovement playerCar;
    public List<CarController> npcCars = new List<CarController>(); 

    void Start()
    {
        // al iniciar bloquear movimiento del jugador
        if (playerCar != null)
            playerCar.canMove = false;

        // bloquear movimiento de todos los bots
        foreach (var bot in npcCars)
        {
            if (bot != null)
                bot.canMove = false;
        }

        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);

        
        if (playerCar != null)
            playerCar.canMove = true;

      
        foreach (var bot in npcCars)
        {
            if (bot != null)
                bot.canMove = true;
        }
    }
}
