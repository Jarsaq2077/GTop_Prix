using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject textoPausa;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f; //ZA WARUDO
        textoPausa.SetActive(true);
        juegoPausado = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1f;
        textoPausa.SetActive(false);
        juegoPausado = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
