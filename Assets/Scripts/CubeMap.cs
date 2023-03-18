using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    CubeState cubeState2;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;

    public Transform mapHelper;
    public void Set()
    {
        cubeState2 = FindObjectOfType<CubeState>();
        
        
        UpdateMap(cubeState2.front, front);
        UpdateMap(cubeState2.back, back);
        UpdateMap(cubeState2.left, left);
        UpdateMap(cubeState2.right, right);
        UpdateMap(cubeState2.up,up);
        UpdateMap(cubeState2.down, down);

        MapHelper();
        
    }
    
    public void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }

            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1,0.5f,0,1);
            }
            i++;


        }

    }
    
    /// <summary>
    /// Aide visuelle : va afficher la face 2D de la face à tourner
    /// </summary>
    public void MapHelper()
    {
        // Savoir quelle est la face à afficher
        if (RotationAutomatique.pileAide.Count > 0)
        {
            string peekMove = RotationAutomatique.pileAide.Peek();
            
            // Enlever le "'"
            if (peekMove.Contains("'"))
            {
                peekMove = peekMove.Replace("'", "");
            }

            switch (peekMove)
            {
                case "U": UpdateMap(cubeState2.up, mapHelper);
                    break;
                case "D": UpdateMap(cubeState2.down, mapHelper);
                    break;
                case "L": UpdateMap(cubeState2.right, mapHelper);
                    break;
                case "F": UpdateMap(cubeState2.front, mapHelper);
                    break;
                case "R": UpdateMap(cubeState2.left, mapHelper);
                    break;
                case "B": UpdateMap(cubeState2.back, mapHelper);
                    break;
            }


        }

        

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
