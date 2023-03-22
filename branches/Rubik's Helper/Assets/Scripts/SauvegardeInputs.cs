using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[Serializable]
public class SauvegardeInputs : MonoBehaviour
{

    public static string pauseKey;
    public static string horaireKey;
    public static string antiHoraireKey;

    public GameObject assignerPause;
    public GameObject assignerHoraire;
    public GameObject assignerAntiHoraire;

    public GameObject panelAssignement;

   
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
    public string DeterminerPauseKey()
    {
        // string key = "";

        foreach(KeyCode kCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kCode))
            {
                pauseKey = kCode.ToString();
                assignerPause.GetComponent<TMP_Text>().text = pauseKey;
                Parametres.pauseClicked = false;
                panelAssignement.SetActive(false);
                PlayerPrefs.SetString("PauseKey", pauseKey);

                break;

            }
        }

        return pauseKey;
    }
    
    public string DeterminerHoraireKey()
    {

        foreach(KeyCode kCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kCode))
            {
                horaireKey = kCode.ToString();
                panelAssignement.SetActive(false);
                assignerHoraire.GetComponent<TMP_Text>().text = horaireKey;
                
                PlayerPrefs.SetString("HoraireKey", horaireKey);
                Parametres.horaireClicked = false;

                break;

            }
        }
        return horaireKey;
    }
    
    public string DeterminerAntiHoraireKey()
    {

        foreach(KeyCode kCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kCode))
            {
                antiHoraireKey = kCode.ToString();
                
                
                panelAssignement.SetActive(false);
                
                assignerAntiHoraire.GetComponent<TMP_Text>().text = antiHoraireKey;

                PlayerPrefs.SetString("Anti-HoraireKey", antiHoraireKey);
                Parametres.antiHoraireClicked = false;

                break;

            }
        }
        return antiHoraireKey;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // DeterminerKey();
        if (Parametres.pauseClicked)
        {
            pauseKey = DeterminerPauseKey();

        }
        if (Parametres.horaireClicked)
        {
            horaireKey = DeterminerHoraireKey();

        }
        if (Parametres.antiHoraireClicked)
        {
            antiHoraireKey = DeterminerAntiHoraireKey();

        }
      

    }
}
