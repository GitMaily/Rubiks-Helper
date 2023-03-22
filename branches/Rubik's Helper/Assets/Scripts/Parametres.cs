using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Parametres : MonoBehaviour
{
    SauvegardeInputs sauvegardeInputs;

    public GameObject panelGeneral;
    public GameObject panelControles;
    public GameObject panelVideo;
    
    public GameObject musicSlider;
    private float sliderValue = 0.0f;
    
    public GameObject pleinEcran;

    public bool initialized;
    public Slider mouseSensitivitySlider;
    
    public GameObject selectedGeneral;
    public GameObject selectedControles;
    public GameObject selectedVideo;
    
    
    public GameObject saviezVous;
    public GameObject zenMode;
    public GameObject inverserSouris;

    public GameObject assignerPause;
    public GameObject assignerHoraire;
    public GameObject assignerAntiHoraire;
    public static bool pauseClicked;
    public static bool horaireClicked;
    public static bool antiHoraireClicked;

    
    
    private static string pauseKey;
    public GameObject panelAssignement;
    public void BoutonControles()
    {
        panelGeneral.SetActive(false);
        panelVideo.SetActive(false);
        panelControles.SetActive(true);
        
        selectedControles.SetActive(true);
        selectedGeneral.SetActive(false);
        selectedVideo.SetActive(false);
        
    }
    
    public void BoutonGeneral()
    {
        panelGeneral.SetActive(true);
        panelVideo.SetActive(false);
        panelControles.SetActive(false);
        
        selectedControles.SetActive(false);
        selectedGeneral.SetActive(true);
        selectedVideo.SetActive(false);

    }
    
    public void BoutonVideo()
    {
        panelGeneral.SetActive(false);
        panelVideo.SetActive(true);
        panelControles.SetActive(false);
        
        selectedControles.SetActive(false);
        selectedGeneral.SetActive(false);
        selectedVideo.SetActive(true);
    }
    
    
    public void MusicSlider (){
        //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.SetFloat("Volume", musicSlider.GetComponent<Slider>().value);
    }
    
    #region Sensibilité de la souris
    
    public void SetMouseSensitivity(float val)
    {
        if (!this.initialized)
        {
            return;
        }
        if (!Application.isPlaying)
        {
            return;
        }
        PlayerPrefs.SetFloat("Sensitivity", val);
        Debug.Log("Set sensitivity to " + val);
    }

    public float vitesse = 12f;
    
    public void Sensitivity()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

    }

    public float mouseSensitivity = 100f;
    
    
    public void MouseLook()
    {
        PlayerPrefs.SetFloat("CurrentSens", mouseSensitivity);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    }

    public void AjusterVitesse(float newVitesse)
    {
        mouseSensitivity = newVitesse * 10;
    }
    
    #endregion
    
    
    public void BoutonPleinEcran (){
        if(Screen.fullScreen){
            pleinEcran.GetComponent<TMP_Text>().text = "Activer";
        }
        else if(!Screen.fullScreen){
            pleinEcran.GetComponent<TMP_Text>().text = "Désactiver";
        }
        Screen.fullScreen = !Screen.fullScreen;

        
    }
    
    public void BoutonLeSaviezVous (){
        if(PlayerPrefs.GetInt("Saviez-vous")==0){
            PlayerPrefs.SetInt("Saviez-vous",1);
            saviezVous.GetComponent<TMP_Text>().text = "Désactiver";
        }
        else if(PlayerPrefs.GetInt("Saviez-vous")==1){
            PlayerPrefs.SetInt("Saviez-vous",0);
            saviezVous.GetComponent<TMP_Text>().text = "Activer";
        }
    }
    
    public void BoutonZen (){
        if(PlayerPrefs.GetInt("Zen")==0){
            PlayerPrefs.SetInt("Zen",1);
            zenMode.GetComponent<TMP_Text>().text = "Désactiver";
        }
        else if(PlayerPrefs.GetInt("Zen")==1){
            PlayerPrefs.SetInt("Zen",0);
            zenMode.GetComponent<TMP_Text>().text = "Activer";
        }
    }
    
    
    public void BoutonInverser (){
        if(PlayerPrefs.GetInt("Inverser")==0){
            PlayerPrefs.SetInt("Inverser",1);
            inverserSouris.GetComponent<TMP_Text>().text = "Inverser";
        }
        else if(PlayerPrefs.GetInt("Inverser")==1){
            PlayerPrefs.SetInt("Inverser",0);
            inverserSouris.GetComponent<TMP_Text>().text = "Inverser";
        }
    }
    public string DeterminerKey(GameObject assigner)
    {
        string key = "";

        foreach(KeyCode kCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kCode))
            {
                key = kCode.ToString();
                panelAssignement.SetActive(false);
                break;

            }
        }
        assigner.GetComponent<TMP_Text>().text = key;
        return key;
    }
    
    public void BoutonAssignerPause()
    {
        // string key = "";
        panelAssignement.SetActive(true);
        pauseClicked = true;
        // key = DeterminerKey(assignerPause);
        // assignerPause.GetComponent<TMP_Text>().text = key;

    }
    
    public void BoutonAssignerHoraire()
    {
        panelAssignement.SetActive(true);
        horaireClicked = true;

    }
    
    public void BoutonAssignerAntiHoraire()
    {
        panelAssignement.SetActive(true);
        antiHoraireClicked = true;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");

        }

        // mouseSensitivity = PlayerPrefs.GetFloat("currentSens", 100);
        // mouseSensitivitySlider.value = mouseSensitivity / 10;
        
        // check full screen
        if(Screen.fullScreen){
            pleinEcran.GetComponent<TMP_Text>().text = "Désactiver";
        }
        else{
            pleinEcran.GetComponent<TMP_Text>().text = "Activer";
        }
        
        // Vérifier options "Le saviez-vous ?"
        if(PlayerPrefs.GetInt("Saviez-vous")==0){
            saviezVous.GetComponent<TMP_Text>().text = "Activer";
        }
        else{
            saviezVous.GetComponent<TMP_Text>().text = "Désactiver";
        }
        
        // Vérifier options "Zen"
        if(PlayerPrefs.GetInt("Zen")==0){
            zenMode.GetComponent<TMP_Text>().text = "Activer";
        }
        else{
            zenMode.GetComponent<TMP_Text>().text = "Désactiver";
        }
        
        // Vérifier options "Inverser"
        if(PlayerPrefs.GetInt("Inverser")==0){
            inverserSouris.GetComponent<TMP_Text>().text = "Activer";
        }
        else{
            inverserSouris.GetComponent<TMP_Text>().text = "Désactiver";
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Assigner bouton pause
        if(PlayerPrefs.HasKey("PauseKey"))
        {
            assignerPause.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("PauseKey");

        }
        else
        {
            assignerPause.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Echap");

        }
        
        // Assigner bouton horaire
        if(PlayerPrefs.HasKey("HoraireKey"))
        {
            assignerHoraire.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("HoraireKey");

        }
        else
        {
            assignerHoraire.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("A");

        }
        
        // Assigner bouton anti-horaire
        if(PlayerPrefs.HasKey("Anti-HoraireKey"))
        {
            assignerAntiHoraire.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Anti-HoraireKey");

        }
        else
        {
            assignerAntiHoraire.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Z");

        }
        // assignerHoraire.GetComponent<TMP_Text>().text = "A";
        // assignerAntiHoraire.GetComponent<TMP_Text>().text = "Z";

        sliderValue = musicSlider.GetComponent<Slider>().value;

    }
}
