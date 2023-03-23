using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patterns : MonoBehaviour
{

    
    public Text indicationPhrase;

    public Stack<string> pileRecommencer = new Stack<string>();

    public static bool isSuperflip;
    public static List<string> TheSuperflip = new List<string>()
    {
        "U", "R", "R", "F", "B", "R", "B", "B", "R", "U", "U", "L", "B", "B", "R", "U'", "D'", "R", "R", "F", "R'", "L", "B", "B", "U", "U", "F", "F"
    };

    public static bool isTetris;
    public static List<string> Tetris = new List<string>()
    {
        "L", "R", "F", "B", "U'", "D'", "L'", "R'"
    };

    public string nomMotifCourant;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isSuperflip)
        {
            nomMotifCourant = "Superflip";
            TheSuperflip.Reverse();
            RotationAutomatique.pileAide = new Stack<string>(TheSuperflip);

        }
        
        if (isTetris)
        {
            nomMotifCourant = "Tetris";
            Tetris.Reverse();
            RotationAutomatique.pileAide = new Stack<string>(Tetris);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationAutomatique.pileAide.Count == 0)
        {
            indicationPhrase.text = nomMotifCourant+" terminé !";
        }

        // if (pileRecommencer.Count > 0)
        // {
        //     RotationAutomatique.
        // }
        //

    }

    public void boutonRecommencer()
    {

        // if (RotationAutomatique.pileAide.Count > 0)
        // {
        //     Debug.Log("Finir avant de recommencer");
        // }
        // else
        // {
        //     // Résoudre le cube avant de recommencer le pattern
        //     
        //     if (isSuperflip)
        //     {
        //         List<string> superflipInverse = new List<string>() {"R", "L", "D", "U", "B'", "F'", "R'", "L'"};
        //         superflipInverse.Reverse();
        //         RotationAutomatique.clicResoudre = true;
        //         RotationAutomatique.pileAide = new Stack<string>(superflipInverse) ;
        //
        //         if (RotationAutomatique.pileAide.Count == 0)
        //         {
        //             RotationAutomatique.pileAide = new Stack<string>(TheSuperflip);
        //
        //         }
        //
        //     }
        //
        //     if (isTetris)
        //     {
        //         
        //         List<string> tetrisInverse = new List<string>() {"R", "L", "D", "U", "B'", "F'", "R'", "L'"};
        //         tetrisInverse.Reverse();
        //         RotationAutomatique.clicResoudre = true;
        //         RotationAutomatique.pileAide = new Stack<string>(tetrisInverse) ;
        //         
        //         // if (RotationAutomatique.pileAide.Count == 0)
        //         // {
        //         //     RotationAutomatique.pileAide = new Stack<string>(Tetris);
        //         //
        //         // }
        //
        //     }
        // }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
