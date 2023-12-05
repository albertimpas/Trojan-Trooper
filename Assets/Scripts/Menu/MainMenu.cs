using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public void Easy()
    {
        SceneManager.LoadScene(1); // Load the scene with index 1
    }

    public void Medium()
    {
        SceneManager.LoadScene(2); // Load the scene with index 1
    }

    public void Hard()
    {
        SceneManager.LoadScene(3); // Load the scene with index 1
    }
}
