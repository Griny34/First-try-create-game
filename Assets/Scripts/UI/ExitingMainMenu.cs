using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitingMainMenu : MonoBehaviour
{
    public void ExitMenu()
    {
        SceneManager.LoadScene(GameConfig.MainMenuIndex);
    }
}
