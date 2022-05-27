using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public InputField nameInputField;

    public static string nameInputValue;

    public Button playButton;

    public void ShowControls()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        nameInputValue = nameInputField.text;
        SceneManager.LoadScene("Controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        SceneManager.LoadScene("GameOver");
    }

   public static void StartMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        SceneManager.LoadScene("Level1");
    }

    public static void PlayGame2()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        SceneManager.LoadScene("Level2");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level2"));
    }

    public static void Victory()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        SceneManager.LoadScene("Victory");
    }

    public static void Leaderboard()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        SceneManager.LoadScene("Leaderboard");
    }

    public void NameFieldOnValueChanged()
    {
        if (nameInputField.text.Length > 0)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }
}
