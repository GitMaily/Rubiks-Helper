using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    public GameObject Panel;
    //private Text myText;
    public GameObject Toggle;
    public PivotRotation pivotRotation;

    //public RubiksFaces rubiksFaces;
    public Text indication;
    public void AfficheAide()
    {
        if (Toggle.GetComponent<Toggle>().isOn)
        {
            Panel.SetActive(true);
            if (pivotRotation.IsFunctionRun()==false)
            {
                indication.text = "L'aide s'affiche";
            }
            else 
            {
                indication.text = "La fonction est en cours d'utilisation";
            }
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}