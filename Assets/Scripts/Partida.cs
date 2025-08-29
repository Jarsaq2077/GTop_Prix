using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida : MonoBehaviour
{
    public GameManager gameHandler;

    private void OnTriggerEnter(Collider other)
    {
        gameHandler.StartTimer();
    }
}