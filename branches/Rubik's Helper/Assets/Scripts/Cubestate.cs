using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubestate : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();


    public static bool autoRotating = false;
    public static bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            
            // Attacher le parent de chaque face (le petit cube) au parent du petit cube du milieu
            if (face != cubeSide[4]) // à part si c'est déjà le cas
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }    

}
