using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAutomatique : MonoBehaviour
{
    public static List<string> moveList = new List<string>();

    private readonly List<string> allMoves = new List<string>()
    {
        "U", "D", "L", "R", "B", "F",
        "U'", "D'", "L'", "R'", "B'", "F'"

    };
    
    private CubeState cubeState;

    private ReadCube readCube;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();

    }

    // Update is called once per frame
    void Update()
    {
        if (moveList.Count > 0 && CubeState.autoRotating == false && CubeState.started)
        {
            DoMove(moveList[0]);
            // enlever la première rotation
            moveList.Remove(moveList[0]);
        }
    }

    void DoMove(string move)
    {
        // réinitialiser la lecture du cube
        readCube.ReadState();
        CubeState.autoRotating = true;
        
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
        Debug.Log(shuffleLength);
        
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            
        }
        
        moveList = moves;
        //testerRotationCote();
        afficherListeMelange();
    }

    public void testerRotationCote()
    {
        moveList.AddRange(new List<string>(){"U", "D", "L", "R", "B", "F"});
        
    }

    private void afficherListeMelange()
    {
        foreach(string nomRotation in moveList)
        {
            Debug.Log(nomRotation);
        }
    }
    
    
}
