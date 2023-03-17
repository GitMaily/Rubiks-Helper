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
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
