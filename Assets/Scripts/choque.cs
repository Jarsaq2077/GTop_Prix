using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class choque : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Image healthBar;
    public GameObject fireParticles;
    public Camera mainCamera;  

    private Rigidbody rb;
    private PlayerMovement movementScript;

    [Header("Sonido de choque")]
    public AudioSource audioSource;
    public AudioClip crashClip;

    public CanvasGroup gameOverCanvas;
    public float fadeDuration = 1f;
    public float showDuration = 5f;


    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        movementScript = GetComponent<PlayerMovement>();
        UpdateHealthUI();

        if (fireParticles != null)
            fireParticles.SetActive(false);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null && crashClip != null)
        {
            audioSource.PlayOneShot(crashClip);
        }

        TakeDamage(1);
        Debug.Log("Chocó contra: " + collision.gameObject.name);
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER - el coche quedó destruido");
        StartCoroutine(ShowGameOver());

        if (movementScript != null)
        {
            movementScript.canMove = false;
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.useGravity = false;
            rb.isKinematic = true;
        }

        if (fireParticles != null)
        {
            fireParticles.SetActive(true);
        }

        if (mainCamera != null)
        {
            Vector3 offset = new Vector3(0, 10f, -6f);
            mainCamera.transform.position = transform.position + offset;
            mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }
    IEnumerator ShowGameOver()
    {
        if (gameOverCanvas == null)
            yield break;

        // 1. Fade IN
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            gameOverCanvas.alpha = t / fadeDuration;
            yield return null;
        }

        gameOverCanvas.alpha = 1;

        // 2. Esperar en pantalla
        float waitTime = 0;
        while (waitTime < showDuration)
        {
            // si presiona R antes, salir al menú
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }

            waitTime += Time.deltaTime;
            yield return null;
        }

        // 3. Fade OUT
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            gameOverCanvas.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        SceneManager.LoadScene(0);
        gameOverCanvas.alpha = 0;
    }

}
