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
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationAutomatique.estMelanger && !RotationAutomatique.estResolu)
        {
            tempsEcoule += Time.deltaTime;
            int minutes = 0;
            minutes = Mathf.FloorToInt(tempsEcoule / 60F);
            int secondes = 0;
            secondes = Mathf.FloorToInt(tempsEcoule - minutes * 60);
            string formatTextTemps = string.Format("{0:00}:{1:00}", minutes, secondes);
            textTemps.text = formatTextTemps;
        }
        else if (RotationAutomatique.estMelanger && RotationAutomatique.estResolu)
        {
            tempsEcoule += 0f;
        }
        else
        {
            tempsEcoule = 0f;
        }
    }
}
