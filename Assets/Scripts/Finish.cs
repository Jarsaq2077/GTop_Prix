using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameManager gameHandler;

    private void OnTriggerEnter(Collider other)
    {
        gameHandler.StopTimer();
    }
}