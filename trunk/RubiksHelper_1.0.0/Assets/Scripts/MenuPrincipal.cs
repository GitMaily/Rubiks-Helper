using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void commencerUnePartie()
    {
        //MenuSauvegarde.NumeroSauvegarde = 0;

        SceneManager.LoadScene("Scenes/PartieRubiksCube");
    }
    
    public void quitterApplication()
    {
        EditorApplication.isPlaying=false;
        Application.Quit();
    }
}
