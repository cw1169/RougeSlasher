using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Main Game");
    }

//Function to close the application
    // public void QuitApplication(){
    //     Application.Quit();
    //     #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false;
    //     #endif
    // }
}
