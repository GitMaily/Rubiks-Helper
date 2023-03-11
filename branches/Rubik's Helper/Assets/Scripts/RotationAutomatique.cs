using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RotationAutomatique : MonoBehaviour
{
    public static List<string> moveList = new List<string>();
    public static List<string> moveListMemoire = new List<string>();
    
    private List<string> allMoves = new List<string>()
    {
        "U", "D", "L", "R", "B", "F",
        "U'", "D'", "L'", "R'", "B'", "F'"

    };
    
    private CubeState cubeState;
    private ReadCube readCube;

    public static bool estResolu = true;
    public static int nbRotationAuto = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();

    }

    // Update is called once per frame
    void Update()
    {
        verificationEtat();
        
        //afficherListeMelange(MoveListInverse());
        if (moveList.Count > 0 && CubeState.autoRotating == false && CubeState.started)
        {
            DoMove(moveList[0]);
            nbRotationAuto++;
            // enlever la première rotation
            moveList.Remove(moveList[0]);
        }
        /*if (moveListMemoire.Count > 0 && CubeState.autoRotatingResoudre == false && CubeState.startedResoudre)
        {
            DoMove(moveListMemoire[0]);
            // enlever la première rotation
            moveListMemoire.Remove(moveListMemoire[0]);
        }*/
    }

    public void verificationEtat()
    {
        if (nbRotationAuto > 0)
        {
            estResolu = false;
        }
        else
        {
            estResolu = true;
        }
    }
    
    void DoMove(string move)
    {
        // réinitialiser la lecture du cube
        readCube.ReadState();
        CubeState.autoRotating = true;
        CubeState.autoRotatingResoudre = true;

        if (move == "U")
        {
            RotateSide(cubeState.up,-90);
        }

        if (move == "U'")
        {
            RotateSide(cubeState.up,90);

        }

        if (move == "D")
        {
            RotateSide(cubeState.down,-90);

        }
        if (move == "D'")
        {
            RotateSide(cubeState.down,90);

        }
        if (move == "R")
        {
            RotateSide(cubeState.left,-90);

        }
        if (move == "R'")
        {
            RotateSide(cubeState.left,90);

        }
        if (move == "L")
        {
            RotateSide(cubeState.right,-90);

        }
        if (move == "L'")
        {
            RotateSide(cubeState.right,90);

        }
        if (move == "B")
        {
            RotateSide(cubeState.back,-90);

        }
        if (move == "B'")
        {
            RotateSide(cubeState.back,90);

        }
        if (move == "F")
        {
            RotateSide(cubeState.front,-90);

        }
        if (move == "F'")
        {
            RotateSide(cubeState.front,90);

        }

    }

    void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }

    public void BoutonMelanger()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10, 30);
        string derniereRotation = "";
        
        for (int i = 0; i < shuffleLength; i++)
        {
            //Debug.Log("pour "+i+" :");
            //afficherListeMelange(moves);

            int randomMove = Random.Range(0, allMoves.Count);
            string selection = allMoves[randomMove];
            bool estPossible = true;

            if (i != 0 && derniereRotation != selection &&
                (derniereRotation.Contains(selection) || selection.Contains(derniereRotation)))
            {
                i--;
                estPossible = false;
                //afficherListeMelange(moves);
                //Debug.Log("normalement : "+selection);


            }
            
            if (estPossible)
            {
               moves.Add(selection);
               derniereRotation = selection; 
            }
            /*else
            {
                Debug.Log("après le test");

                afficherListeMelange(moves);
            }*/
            
        }
        
        moveList = moves;
        Debug.Log("Liste de rotation du mélange :");
        afficherListeMelange(moves);
        for (int i = 0; i < moves.Count; i++)
        {
            moveListMemoire.Add(moves[i]); 
        }
        //moveListMemoire = moveList;
        Debug.Log("Liste de rotation de moveListMemoire :");
        afficherListeMelange(moveListMemoire);
        Debug.Log("taille moveListMemoire: "+moveListMemoire.Count);
        //testerRotationCote();

        /*
        for (int i = 0; i < moveList.Count-1;i++)
        {
            if (moveList[i] != moveList[i+1] && (moveList[i].Contains(moveList[i+1]) || moveList[i+1].Contains(moveList[i]) || (i < moveList.Count-2 && moveList[i+2].Contains(moveList[i+1]))))
            {
                int randomMove = 0;
                do
                {
                    randomMove = Random.Range(0, allMoves.Count);
                    
                } while ( allMoves[randomMove] == (moveList[i]));
                
                moveList[i] = allMoves[randomMove];
            }
        }
        */


    }
    
    /// <summary>
    /// Convertit la liste du mélange pour obtenir le chemin inverse
    /// </summary>
    /// <returns></returns>
    public List<string> CheminInverse()
    {
        List<string> listeCheminInverse = new List<string>();
        List<string> listeCheminInverse2 = new List<string>();

        // Mettre la liste du mélange dans l'ordre décroissant
        Debug.Log("count :"+moveListMemoire.Count);
        for (int i = moveListMemoire.Count-1; i >= 0; i--)
        {
            Debug.Log("i = "+i);
            listeCheminInverse.Add(moveListMemoire[i]);
        }
        Debug.Log("Affichage listeCheminInverse normalement décroissant");
        afficherListeMelange(listeCheminInverse);
        
        // Inverser chaque rotation de la liste
        listeCheminInverse2 = InverserListeMove(listeCheminInverse);
        Debug.Log("Affichage listeCheminInverse normalement décroissant et inversée");
        afficherListeMelange(listeCheminInverse2);

        return listeCheminInverse2;
    }

    /// <summary>
    /// Convertit la liste des rotations du mélange dans l'ordre décroissant puis rend chaque rotation en son inverse
    /// </summary>
    /// <returns>
    /// La liste inversée du mélange : on obtient le chemin inverse du mélange
    /// </returns>
    public List<string> MoveListInverse()
    {
        Debug.Log("movelistinverse running");
        List<string> moveInverses = new List<string>();
        Debug.Log("Liste moveListMemoire avant");
        afficherListeMelange(moveListMemoire);
        // Convertir en une pile
        Queue<string> pileMemoire = new Queue<string>(moveListMemoire);
        // Initialiser une autre pile vide
        Queue<string> nouvellePile = new Queue<string>();

        int taillePileMemoire = pileMemoire.Count;
        //Debug.Log("taille de la pile mémoire :"+taillePileMemoire);
        // Empiler la pileMemoire dans la nouvellePile
        for (int i = 0; i < taillePileMemoire; i++)
        {
            string moveCourant = pileMemoire.Dequeue(); // dépiler la pileMemoire
            nouvellePile.Enqueue(moveCourant); // empiler dans la nouvellePile
        }
        
        //Debug.Log("taille de la nouvelle Pile:"+ nouvellePile.Count);

        moveInverses = nouvellePile.ToList();
        
        Debug.Log("Liste moveInverses avant l'inversement :");
        afficherListeMelange(moveInverses);
        
        InverserListeMove(moveInverses);
        Debug.Log("Liste moveInverses après inversement :");
        afficherListeMelange(moveInverses);
        
        return moveInverses;

    }

    /// <summary>
    /// Convertit chaque rotation d'une liste en son inverse
    /// </summary>
    private List<string> InverserListeMove(List<string> moveListDecroissant)
    {
        for (int i = 0; i < moveListDecroissant.Count; i++)
        {
            if (moveListDecroissant[i].Contains("'"))
            {
                moveListDecroissant[i] = moveListDecroissant[i].Replace("'", "");
            }
            else
            {
                moveListDecroissant[i] = moveListDecroissant[i] + "'";
            }
        }

        /*Debug.Log("entrée dans inverserListeMove avant l'inversement");
        afficherListeMelange(moveListDecroissant);
        foreach (string move in moveListDecroissant)
        {
            if (move.Contains("'"))
            {
                
                Debug.Log("move contains ' avant :"+move);
                move.Replace("'", "");
                Debug.Log("move contains ' apres :"+move);

            }
            else
            {
                Debug.Log("move avant :"+move);

                move.Append('"');
                Debug.Log("move apres :"+move);

            }
        }
        Debug.Log("entrée dans inverserListeMove apres l'inversement");
        afficherListeMelange(moveListDecroissant);*/
        return moveListDecroissant;
    }
    /// <summary>
    /// Résout le Rubik's Cube en utilisant le chemin inverse du mélange réalisé
    /// </summary>
    public void BoutonResoudre()
    {
        Debug.Log("bouton resoudre cliqué");
        //List<string> moveListInverse = MoveListInverse();
        List<string> moveListInverse = CheminInverse();

        //afficherListeMelange(moveListInverse);
        moveList = moveListInverse;
        
        moveListMemoire.Clear();
    }

    public void testerRotationCote()
    {
        moveList.AddRange(new List<string>(){"U'", "U", "L'", "L", "B", "B"});
        
    }

    private void afficherListeMelange(List<string> moves)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
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
    
    
    
    
}
