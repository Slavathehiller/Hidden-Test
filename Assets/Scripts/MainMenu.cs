using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameButtonClick()
    {
        SceneManager.LoadScene(Scenes.MAIN_SCENE);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
