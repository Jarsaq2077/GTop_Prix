using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public Text countdownText; // UI Text donde aparece "3, 2, 1, GO"
    public PlayerMovement playerCar;
    public CarController npcCar;


    void Start()
    {
        // Al iniciar, bloquear movimiento
        playerCar.canMove = false;
        npcCar.canMove = false;


        // Iniciar la corrutina del conteo
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

        countdownText.gameObject.SetActive(false); // ocultar texto



        playerCar.canMove = true;
        npcCar.canMove = true;

    }
}
