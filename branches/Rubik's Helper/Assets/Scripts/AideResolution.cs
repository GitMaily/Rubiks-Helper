using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AideResolution : MonoBehaviour
{

    public static int iterateurEtape;

    public Text nbEtape;
    public Text indicationRotation;

    private static List<string> listePile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationAutomatique.pileAide.Count == 0 && iterateurEtape == 0)
        {
            indicationRotation.text = "Vous pouvez naviguer ici";
            nbEtape.text = iterateurEtape.ToString();

        }

        if (iterateurEtape != 0 && RotationAutomatique.pileAide.Count < iterateurEtape)
        {
            iterateurEtape = RotationAutomatique.pileAide.Count;
            indicationRotation.text = listePile[iterateurEtape];
            nbEtape.text = iterateurEtape.ToString();


        }
        
    }

    public void BoutonPrecedent()
    {
        listePile = RotationAutomatique.pileAide.ToList();
        // listePile.Reverse();

        Debug.Log("bouton précédent cliqué");
        //iterateurEtape = RotationAutomatique.pileAide.Count-1;

        if (RotationAutomatique.pileAide.Count > 0 && iterateurEtape > 0)
        {
            iterateurEtape--;
            nbEtape.text = iterateurEtape.ToString();
        }

        if (iterateurEtape == 0)
        {
            indicationRotation.text = "";
            nbEtape.text = iterateurEtape.ToString();


        }
        else
        {
            indicationRotation.text = listePile[iterateurEtape-1];

        }

    }

    public void BoutonSuivant()
    {
        listePile = RotationAutomatique.pileAide.ToList();
        // listePile.Reverse();
        Debug.Log("bouton suivant cliqué");
        //iterateurEtape = RotationAutomatique.pileAide.Count-1;

        if (RotationAutomatique.pileAide.Count > 0 && iterateurEtape < RotationAutomatique.pileAide.Count && iterateurEtape != RotationAutomatique.pileAide.Count)
        {
            iterateurEtape++;
            nbEtape.text = iterateurEtape.ToString();
            indicationRotation.text = listePile[iterateurEtape-1];

        }
        


    }

    public void NavigationPile()
    {
        // Convertir la pile en liste
        List<string> listePile = RotationAutomatique.pileAide.ToList();
        
        // Initialiser l'itérateur pour que la prochaine rotation à effectuer s'affiche (tête de pile)
        //iterateurEtape = RotationAutomatique.pileAide.Count-1;

        //nbEtape.text = iterateurEtape.ToString();
        // indicationRotation.text = listePile[iterateurEtape];

    }
    
    
}
