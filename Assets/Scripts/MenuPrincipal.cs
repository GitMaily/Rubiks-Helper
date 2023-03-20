using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menuJouer;
    public GameObject menuQuitter;
    
    public Animator CameraObject;

    public void commencerUnePartie()
    {
        //MenuSauvegarde.NumeroSauvegarde = 0;

        SceneManager.LoadScene("Scenes/PartieRubiksCube");
    }
    
    public void BoutonJouer(){
        menuQuitter.SetActive(false);
        menuJouer.SetActive(true);
    }

    public void BoutonParametres()
    {
        CameraObject.SetFloat("Animer",1);

    }

    public void BoutonRevenirMenuPrincipal()
    {
        CameraObject.SetFloat("Animer",0);

    }

    public void BoutonQuitter()
    {
        menuQuitter.SetActive(true);
        //if(extrasMenu) extrasMenu.SetActive(false);
    }


    public void RevenirApplication()
    {
        menuQuitter.SetActive(false);

    }
    public void quitterApplication()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BoutonQuitter();
            menuJouer.SetActive(false);
            if (menuQuitter.activeSelf)
            {
                menuQuitter.SetActive(false);
            }
        }
    }
}
