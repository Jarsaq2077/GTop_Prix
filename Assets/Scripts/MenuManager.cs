using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainmenu;
    public AudioSource bgm;
    public float volumen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        bgm.loop = true;
        bgm.volume = volumen;
        bgm.Play();       

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayGame1();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayGame2();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayGame3();
    }
    
    public void OpenMainMenu(){
        mainmenu.SetActive(true);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void BackMenu(){
        SceneManager.LoadScene(0);
    }
    public void PlayGame1(){
        SceneManager.LoadScene(1);
    }
    public void PlayGame2(){
        SceneManager.LoadScene(2);
    }
    public void PlayGame3(){
        SceneManager.LoadScene(3);
    }
}
