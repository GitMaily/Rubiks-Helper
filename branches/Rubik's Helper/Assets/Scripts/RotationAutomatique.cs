using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RotationAutomatique : MonoBehaviour
{
    /// <summary>
    /// La liste de rotation automatique à effectuer
    /// </summary>
    public static List<string> moveList = new List<string>();
    /// <summary>
    /// La liste mémoire du mélange
    /// </summary>
    public static List<string> moveListMemoire = new List<string>();
    /// <summary>
    /// Liste des rotations effectuées manuellement
    /// </summary>
    public static List<string> manualListMemoire = new List<string>();

    /// <summary>
    /// La pile des rotations effectuées (pour le mélange)
    /// </summary>
    public static Stack<string> pileRotations = new Stack<string>();
    /// <summary>
    /// La pile des rotations effectuées stockée en mémoire
    /// </summary>
    public static Stack<string> pileRotationsMemoire = new Stack<string>();
    /// <summary>
    /// La pile du chemin inverse
    /// </summary>
    public static Stack<string> pileCheminInverse = new Stack<string>();
    
    /// <summary>
    /// La pile des rotations effectuées en miroir
    /// </summary>
    public static Stack<string> pileRotationMiroir = new Stack<string>();
    
    /// <summary>
    /// La pile du mélange
    /// </summary>
    public static Stack<string> pileMelanger = new Stack<string>();
    
    /// <summary>
    /// Une pile pour l'aide
    /// </summary>
    public static Stack<string> pileAide = new Stack<string>();
    
    private List<string> allMoves = new List<string>()
    {
        "U", "D", "L", "R", "B", "F",
        "U'", "D'", "L'", "R'", "B'", "F'"

    };
    
    private CubeState cubeState;
    private ReadCube readCube;

    public static bool clicResoudre = false;
    public static int nbRotationAuto = 0;
    public static bool enResolution;
    public Helper h1;
    public int etape=0;
    
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        moveListMemoire.Clear();
        manualListMemoire.Clear();
        pileAide.Clear();
        pileRotations.Clear();
        pileRotationsMemoire.Clear();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        /*
         // Méthode avec listes
         if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            DoMove(moveList[0]);
            nbRotationAuto++;
            // enlever la première rotation
            moveList.Remove(moveList[0]);
        }
        else if (moveList.Count == 0)
        {
            h1.ListHelper();
            enResolution = false;
            
        }*/
        
        /*
         * Méthode avec piles en implémentant l'aide
         */
        
        // Mélange automatique
        if (pileMelanger.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            string move = DoMove(pileMelanger.Pop());
            pileRotations.Push(move);
            pileAide.Push(InverserMove(move)); 
        }
        else if (moveList.Count == 0)
        {
            //h1.ListHelper();
            enResolution = false;
        }
        
        // Résolution automatique
        /*if (pileCheminInverse.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            h1.indicationRelache.text = pileCheminInverse.Peek();
            DoMove(pileCheminInverse.Pop());
            pileAide.Pop();

            //nbRotationAuto++;
            
        }
        else if (pileCheminInverse.Count == 0)
        {
            //h1.ListHelper();
            enResolution = false;
            
        }*/
        
        // Résolution automatique avec pileAide
        if (clicResoudre && pileAide.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            DoMove(pileAide.Pop());

            if (pileAide.Count == 0)
            {
                clicResoudre = false;
            }
        }
        else if (pileAide.Count == 0)
        {
            enResolution = false;
            clicResoudre = false;

            
        }

        rotationsManuelles();
    }
    
    public void BoutonMelanger()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10, 15);
        string derniereRotation = "";
        
        for (int i = 0; i < shuffleLength; i++)
        {

            int randomMove = Random.Range(0, allMoves.Count);
            string selection = allMoves[randomMove];
            
            
            // Supprimer les rotations qui s'annulent
            bool estPossible = true;
            if (i != 0 && derniereRotation != selection &&
                (derniereRotation.Contains(selection) || selection.Contains(derniereRotation)))
            {
                i--;
                estPossible = false;

            }
            
            if (estPossible)
            {
                moves.Add(selection);
                pileMelanger.Push(selection);
                pileRotationsMemoire.Push(selection);
                derniereRotation = selection; 
            }
        }
        //moveList = moves;
        moveList.AddRange(moves);
        Debug.Log("Liste de rotation du mélange :");
        afficherListeMelange(moves);

        //rotationsManuelles();
        
        // Ajouter la liste du mélange dans la liste mémoire des rotations
        for (int i = 0; i < moves.Count; i++)
        {
            moveListMemoire.Add(moves[i]); 
        }
    }
    
    /// <summary>
    /// Résout le Rubik's Cube en utilisant le chemin inverse du mélange réalisé
    /// </summary>
    public void BoutonResoudre()
    {
        enResolution = true;
        clicResoudre = true;
        List<string> moveListInverse = CheminInverse();
        List<string> listeFinale = new List<string>();
        CheminInversePile();

        InverserListeRotationsManuelles();
        
        // Ajouter la liste manuelle à la fin de la liste de résolution
        if (manualListMemoire.Count > 0)
        {
            Debug.Log("afficher manualListMemoire");
            afficherListeMelange(manualListMemoire);
            listeFinale.AddRange(manualListMemoire);
        }
        
        listeFinale.AddRange(moveListInverse);
        moveList = listeFinale;
        

        //Debug.Log("Affichage de moveList");
        //afficherListeMelange(moveList);
        
        PivotRotation.listeRotationsManuelles.Clear();
        moveListMemoire.Clear();
        manualListMemoire.Clear();
        pileRotations.Clear();
    }

    string DoMove(string move)
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

        return move;
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
        //Debug.Log("side of the cube: "+side[4].transform.parent.ToString()+", angle : "+angle);
    }

    public void rotationsManuelles()
    {
        // Vérifier s'il existe des rotations manuelles avant le mélange
        if (PivotRotation.listeRotationsManuelles.Count > 0)
        {
            // Ajouter au début des rotations effectuées
            moveListMemoire.AddRange(PivotRotation.listeRotationsManuelles);
            
            // Vider
            PivotRotation.listeRotationsManuelles.Clear();
            manualListMemoire.Clear();

        }
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
        // Debug.Log("count :"+moveListMemoire.Count);
        for (int i = moveListMemoire.Count-1; i >= 0; i--)
        {
            listeCheminInverse.Add(moveListMemoire[i]);
        }
        // Debug.Log("Affichage listeCheminInverse normalement décroissant");
        //afficherListeMelange(listeCheminInverse);
        
        // Inverser chaque rotation de la liste
        listeCheminInverse2 = InverserListeMove(listeCheminInverse);
        // Debug.Log("Affichage listeCheminInverse normalement décroissant et inversée");
        // afficherListeMelange(listeCheminInverse2);
        
        return listeCheminInverse2;
    }

    /// <summary>
    /// Convertit la pile des rotations effectuées par le chemin inverse pour résoudre le Rubik's Cube
    /// L'insère dans pileCheminInverse
    /// </summary>
    /// <returns>La pile du chemin inverse</returns>
    public Stack<string> CheminInversePile()
    {
        Stack<string> tempQueue = new Stack<string>();

        // Inverser et obtenir une nouvelle pile temp dans l'ordre décroissant
        while (pileRotations.Count > 0)
        {
            string poppedRotation = pileRotations.Pop();

            if (poppedRotation.Contains("'"))
            {
                poppedRotation = poppedRotation.Replace("'", "");
            }
            else
            {
                poppedRotation += "'";
            }
            
            tempQueue.Push(poppedRotation);
        }
        
        // afficherPile(tempQueue, "Affichage de tempQueue");
        // Empiler dans la pile finale du chemin inverse

        // pileCheminInverse = tempQueue;
        
        while (tempQueue.Count > 0)
        {
            pileCheminInverse.Push(tempQueue.Pop());
        }
        // afficherPile(pileCheminInverse, "Affichage de pileCheminInverse");

        //pileAide = pileCheminInverse;
        
        return pileCheminInverse;


    }

    /// <summary>
    /// Inverse la rotation entrée en paramètre. Ajoute un "'" si c'est pas déjà le cas, le supprime si c'est le cas
    /// </summary>
    public static string InverserMove(string move)
    {
        if (move.Contains("'"))
        {
            move = move.Replace("'", "");
        }
        else
        {
            move += "'";
        }

        return move;
    }
    
    /// <summary>
    /// Inverse la pile entrée en paramètre de sorte à ce qu'elle donne le chemin inverse de cette pile pour résoudre le Rubik's Cube
    /// </summary>
    /// <param name="rotationsPile"></param>
    /// <returns> Le chemin inverse de la pile </returns>
    public static Stack<string> InverserCheminPile(Stack<string> rotationsPile)
    {
        Stack<string> tempStack = new Stack<string>();
        Stack<string> finalStack = new Stack<string>();

        // Inverser et obtenir une nouvelle pile temp dans l'ordre décroissant
        while (rotationsPile.Count > 0)
        {
            string poppedRotation = rotationsPile.Pop();

            if (poppedRotation.Contains("'"))
            {
                poppedRotation = poppedRotation.Replace("'", "");
            }
            else
            {
                poppedRotation += "'";
            }
            
            tempStack.Push(poppedRotation);
        }
        
        
        while (tempStack.Count > 0)
        {
            finalStack.Push(tempStack.Pop());
        }
        
        
        
        return finalStack;
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

        return moveListDecroissant;
    }

    

    public void testerRotationCote()
    {
        moveList.AddRange(new List<string>(){"U'", "U", "L'", "L", "B", "B"});
        
    }

    public void InverserListeRotationsManuelles() //List<string> moveList
    {
        
        List<string> moveListManuelle = PivotRotation.listeRotationsManuelles;
        
        List<string> moveListDecroissant = new List<string>();
        
        for (int i = moveListManuelle.Count-1; i >= 0; i--)
        {
            moveListDecroissant.Add(moveListManuelle[i]);
        }
        
        manualListMemoire.AddRange(InverserListeMove(moveListDecroissant));
        
        //Debug.Log("Affichage de manualListMemoire dans Inversement");
        afficherListeMelange(manualListMemoire);
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
