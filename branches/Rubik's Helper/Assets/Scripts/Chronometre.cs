using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometre : MonoBehaviour
{
    /// <summary>
    /// Temps écoulé
    /// </summary>
    public float tempsEcoule = 0f;
    /// <summary>
    /// Texte pour le chrono
    /// </summary>
    public Text textTemps;
    
    public static string scoreFinal;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreFinal = null;
        if (RotationAutomatique.estMelanger && !RotationAutomatique.estResolu)
        {
            tempsEcoule += Time.deltaTime;
            string formatTextTemps = FormatTextTemps(tempsEcoule);
            textTemps.text = formatTextTemps;
            scoreFinal = null;
        }
        else if (RotationAutomatique.estResolu)
        {
            tempsEcoule += 0f;
            scoreFinal = FormatTextTemps(tempsEcoule);
        }
        else
        {
            tempsEcoule = 0f;
        }
    }
    
    private string FormatTextTemps(float temps)
    {
        int minutes = Mathf.FloorToInt(tempsEcoule / 60F);
        int secondes = Mathf.FloorToInt(tempsEcoule - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, secondes);
    }
}
