using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Helper : MonoBehaviour
{
    public GameObject PanelHelper;
    public GameObject Toggle;
    public Text indicationClic;
    public Text indicationRelache;


    public void AfficheAide()
    {
        if (Toggle.GetComponent<Toggle>().isOn)
        {
            PanelHelper.SetActive(true);
        }
        else
        {
            PanelHelper.SetActive(false);
        }
    }
    

    public void OnPointerDown()
    {
        indicationClic.gameObject.SetActive(true);
        indicationRelache.gameObject.SetActive(false);
        indicationClic.text = "Vous avez cliqué sur l'écran";
    }

    public void OnPointerUp()
    {
        indicationClic.gameObject.SetActive(false);
        indicationRelache.gameObject.SetActive(true);
        indicationRelache.text = "Vous avez relâché la souris";
    }

    void Update()
    {
        if (PanelHelper.activeSelf && Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }
        
        if (PanelHelper.activeSelf && Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }
    
}