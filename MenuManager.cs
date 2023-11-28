using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public void OnButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonExit()
    {

         Application.Quit();

    }
}
