using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainmenu; 
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OpenMainMenu(){
        mainmenu.SetActive(true);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void PlayGame1(){
        SceneManager.LoadScene("Nivel1");
    }
    public void PlayGame2(){
        SceneManager.LoadScene(2);
    }
}
