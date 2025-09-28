using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}
