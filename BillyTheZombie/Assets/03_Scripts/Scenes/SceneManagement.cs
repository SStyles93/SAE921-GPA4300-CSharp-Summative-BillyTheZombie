using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    /// <summary>
    /// Activates the wanted scene according to its index
    /// </summary>
    /// <param name="Index">Index of the wanted scene</param>
   public void ActivateScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
 