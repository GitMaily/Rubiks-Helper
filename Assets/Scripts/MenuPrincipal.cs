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
    
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public GameObject selectedJouer;
    public GameObject selectedCharger;
    public GameObject selectedMiniJeux;
    //public GameObject selectedParam;
    public GameObject selectedQuitter;
    
    public void commencerUnePartie()
    {
        //MenuSauvegarde.NumeroSauvegarde = 0;

        SceneManager.LoadScene("Scenes/PartieRubiksCube");
    }
    
    public void BoutonJouer(){
        menuQuitter.SetActive(false);
        menuJouer.SetActive(true);
        
        selectedJouer.SetActive(true);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(false);
        //selectedParam.SetActive(false);
        selectedQuitter.SetActive(false);
    }

    public void BoutonColorier()
    {
        SceneManager.LoadScene("CreerRubiksCube");
        menuQuitter.SetActive(false);
        menuJouer.SetActive(false);
        
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(false);
        //selectedParam.SetActive(false);
        selectedQuitter.SetActive(false);
    }

    public void BoutonCharger()
    {
        SceneManager.LoadScene("ChargementRubik");
        
        menuQuitter.SetActive(false);
        menuJouer.SetActive(false);
        
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(true);
        selectedMiniJeux.SetActive(false);
        //selectedParam.SetActive(false);
        selectedQuitter.SetActive(false);
    }
    
    public void BoutonMiniJeux()
    {
        SceneManager.LoadScene("MenuMiniJeux");
        
        menuQuitter.SetActive(false);
        menuJouer.SetActive(false);
        
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(true);
        selectedQuitter.SetActive(false);
    }
    public void BoutonParametres()
    {
        menuQuitter.SetActive(false);
        menuJouer.SetActive(false);
        
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(false);
        selectedQuitter.SetActive(false);
        
        CameraObject.SetFloat("Animer",1);

    }

    public void BoutonRevenirMenuPrincipal()
    {
        CameraObject.SetFloat("Animer",0);

    }

    public void BoutonQuitter()
    {
        menuQuitter.SetActive(true);
        
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(false);
        //selectedParam.SetActive(false);
        selectedQuitter.SetActive(true);
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

    public void BoutonRecords()
    {
        SceneManager.LoadScene("MeilleurScore");
        
        menuQuitter.SetActive(false);
        selectedJouer.SetActive(false);
        selectedCharger.SetActive(false);
        selectedMiniJeux.SetActive(false);
        //selectedParam.SetActive(false);
        selectedQuitter.SetActive(false);
    }
    public void SonHover(){
        hoverSound.Play();
    }

    public void SonClic()
    {
        clickSound.Play();
    }


    public void UpdateVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }
    
    void Start()
    {
        
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
