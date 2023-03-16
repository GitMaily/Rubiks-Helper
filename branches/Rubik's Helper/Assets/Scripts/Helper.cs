using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Helper : MonoBehaviour
{
    public GameObject PanelHelper;
    public GameObject Toggle;
    public Text indicationClic;
    public Text indicationRelache;

    public RotationAutomatique rAuto;

    public List<string> cheminInverse;

    public static int etape;

    public Button precedent;
    public Button suivant;

    public bool clicPrec=false;
    public bool clicSuiv=false;

    private List<string> listeMemoire = new List<string>();

    private PivotRotation pivotRotation;

    public Stack<string> rotationStack;

    
    void Start()
    {
        pivotRotation = FindObjectOfType<PivotRotation>();
    }

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
    

    /*public void OnPointerDown()
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
    }*/
    
    public void Precedent()
    {
        etape--;
        //ListHelper();
        /*if (RotationAutomatique.moveList.Count == 0)
        {
            List<string> listeMemoire = rAuto.CheminInverse();
            if (listeMemoire.Count > 0)
            {
               indicationRelache.text = listeMemoire[rAuto.etape--]; 
            }
            
        }*/

        //clicPrec = false;
        /*if (RotationAutomatique.moveList.Count == 0)
        {
            indicationRelache.text = RotationAutomatique.moveList[0];
            if (i > RotationAutomatique.moveList.Count)
            {
                indicationRelache.text = RotationAutomatique.moveList[RotationAutomatique.moveList.Count - 1];
            }
            else
            {
                indicationRelache.text = RotationAutomatique.moveList[i++];
            }
        }*/


    }
    
    public void Suivant()
    {

        etape++;
        etape++;
        //ListHelper();
        /*if (RotationAutomatique.moveList.Count == 0)
        {
            List<string> listeMemoire = rAuto.CheminInverse();
            if (listeMemoire.Count > 0)
            {
               indicationRelache.text = listeMemoire[rAuto.etape++]; 
            }

        }*/

        //clicSuiv = false;


        /*if (RotationAutomatique.moveList.Count == 0)
        {
            indicationRelache.text = RotationAutomatique.moveList[0];
            if (i < 0)
            {
                indicationRelache.text = RotationAutomatique.moveList[0];
            }
            else
            {
                indicationRelache.text = RotationAutomatique.moveList[i--];
            }
        }*/
    }

    public void ListHelper()
    {
        if (RotationAutomatique.moveList.Count == 0)
        {
            //etape = 0;
            indicationRelache.text = "Le mélange est terminé";
            listeMemoire = rAuto.CheminInverse();
            if (listeMemoire.Count > 0)
            {
                if (etape <= 0)
                {
                    etape = 0;
                    precedent.interactable = false;
                    suivant.interactable = true;
                    indicationRelache.text = listeMemoire[0];
                }
                else if(etape>=listeMemoire.Count)
                {
                    etape = listeMemoire.Count;
                    precedent.interactable = true;
                    suivant.interactable = false;
                    indicationRelache.text = listeMemoire[listeMemoire.Count-1];
                }
                else
                {
                    precedent.interactable = true;
                    suivant.interactable = true;
                    indicationRelache.text = listeMemoire[etape];
                }
                
            }
        }
    }
    /*indicationRelache.text =listeMemoire[0] +"on est ici";
    if (clicPrec == true)
    {
        etape=etape--;
        if (etape < 0)
        {
            precedent.interactable = false;
            indicationRelache.text = "Nous sommes à la première étape";
        }
        else if (etape >= 0)
        {
            indicationRelache.text = listeMemoire[etape] +" precedent";
        }
        
    }
    if (clicSuiv == true)
    {
        etape = etape++;
        if (etape >listeMemoire.Count)
        {
            suivant.interactable = false;
            indicationRelache.text = "Nous sommes à la dernière étape";
        }
        else if (etape >= 0)
        {
            indicationRelache.text = listeMemoire[etape] + " suivant";
        }
    }
    
}
else
{
    indicationRelache.text = "ListMemoire est de taille 0";
}*/
        //}
        /*else
        {
            indicationRelache.text = "Le mélange n'est pas terminé";
        }*/
        
        /*etape = 0;
        if (RotationAutomatique.moveList.Count == 0)
        {
            List<string> listeMemoire = rAuto.CheminInverse();
            if (listeMemoire.Count > 0 && clicPrec==false && clicSuiv==false)
            {
                indicationRelache.text = listeMemoire[0];
            }

            if (clicPrec == true)
            {
                etape = etape--;
                if ((etape) <=0)
                {
                    precedent.interactable = false;
                    indicationRelache.text = listeMemoire[0];
                }
                else
                {
                    indicationRelache.text = listeMemoire[etape];
                }
                
            }

            if (clicSuiv == true)
            {
                etape = etape++;
                if ((etape) > listeMemoire.Count)
                {
                    suivant.interactable = false;
                    indicationRelache.text = listeMemoire[listeMemoire.Count-1];
                }
                else
                {
                    indicationRelache.text = listeMemoire[etape];
                }

            }
        }
        /*else
        {
            indicationRelache.text = "Aucune rotation n'a été faite";
        }

        return listeMemoire;
    }
    else
    {
        indicationRelache.text = "Mélange en cours";
        return null;
    }*/
        
        /*if (RotationAutomatique.moveList.Count == 0)
        {
            List<string> listeMemoire = rAuto.CheminInverse();
            if (listeMemoire.Count > 0)
            {
                // Afficher l'élément correspondant à la variable etape dans la liste
                if (etape >= 0 && etape < listeMemoire.Count)
                {
                    indicationRelache.text = listeMemoire[etape];
                }
                else
                {
                    indicationRelache.text = "Aucune rotation n'a été faite";
                }
                //etape++;
            }
            else
            {
                indicationRelache.text = "Aucune rotation n'a été faite";
            }
        }
        
    }*/

    void Update()
    {
        /*if (PanelHelper.activeSelf && Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }
        
        if (PanelHelper.activeSelf && Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
        ListHelper();*/
        
        
        // Déterminer si l'utilisateur a fait la bonne rotation à l'étape courante
        /*List<string> rotationEffectuee = pivotRotation.DeterminerRotationManuelle();
        if (rotationEffectuee.Count > 0)
        {
            Debug.Log("rotationEffectuee : "+rotationEffectuee[0]);

        }
        if (listeMemoire.Count > 0 && rotationEffectuee.Count > 0)
        {
            Debug.Log("listeMemoire.Count > 0 && rotationEffectuee.Count > 0////// rotation was :"+rotationEffectuee);
            if (rotationEffectuee.Count == 1)
            {
                if (listeMemoire[etape] == rotationEffectuee[0])
                {
                    Debug.Log("Manipulation correcte");
                    // Incrémenter l'étape pour procéder automatiquement à la prochaine rotation
                    etape++;
                }
                else
                {
                    // Pas la bonne manipulation, insérer la rotation dans la listeMemoire
                    Debug.Log("Pas la bonne manip");
                    listeMemoire.Insert(etape,rotationEffectuee[0]);

                }
            }

            if (rotationEffectuee.Count == 2)
            {
                if (listeMemoire[etape] == rotationEffectuee[0] && listeMemoire[etape] == rotationEffectuee[1] )
                {
                    Debug.Log("Manipulation correcte (2* quelque chose)");
                    // Incrémenter 2* l'étape pour procéder automatiquement à la prochaine rotation
                    etape++;
                    etape++;
                }
                else
                {
                    // Pas la bonne manipulation, insérer la rotation dans la listeMemoire
                    Debug.Log("Pas la bonne manip");
                    listeMemoire.Insert(etape,rotationEffectuee[0]);
                    listeMemoire.Insert(etape,rotationEffectuee[0]);

                } 
            }

           
        }*/
        //ListHelper();


        // if (RotationAutomatique.pileRotations.Count > 0)
        // {
        //     rotationStack = RotationAutomatique.InverserPile(RotationAutomatique.pileRotations);
        //     
        //     
        // }

        //RotationCorrecte();
    }

    
    
    private void afficherListeMelange(List<string> moves, string message)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        sb.Append(message + "\n");
        foreach(string nomRotation in moves)
        {
            sb.Append(nomRotation);
            if (i != moves.Count-1)
            {
                sb.Append(", ");
            }
            i++;
        }
        
        Debug.Log(sb.ToString());
    }
    
    private void afficherPile(Stack<string> movesPile, string message)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        sb.Append(message + "\n");
        foreach(string nomRotation in movesPile)
        {
            sb.Append(nomRotation);
            if (i != movesPile.Count-1)
            {
                sb.Append(", ");
            }
            i++;
        }
        
        Debug.Log(sb.ToString());
    }
}