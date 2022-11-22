using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Loads a new scene
    /// </summary>
    /// <param name="ButtonScene"> The name of the scene to change to </param>
    public void ButtonMoveScene(string ButtonScene)
    {
        SceneManager.LoadScene(ButtonScene);
    }

}