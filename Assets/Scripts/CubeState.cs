using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>(9);
    public List<GameObject> back = new List<GameObject>(9);
    public List<GameObject> up = new List<GameObject>(9);
    public List<GameObject> down = new List<GameObject>(9);
    public List<GameObject> left = new List<GameObject>(9);
    public List<GameObject> right = new List<GameObject>(9);

    public static bool autoRotating = false;
    public static bool started = false;
    public static bool autoRotatingResoudre = false;
    public static bool startedResoudre = false;
    public void PickUp(List<GameObject> cubeSide)
    {
        
        // Pour régler les problèmes de bugs d'affichage des petits cube quand la vitesse de rotation est trop élevée pour la machine
        //StartCoroutine(PauseBug());
        foreach (GameObject face in cubeSide)
        {
            
            // Attacher le parent de chaque face (le petit cube) au parent du petit cube du milieu
            if (face != cubeSide[4]) // à part si c'est déjà le cas
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }

    }


    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }

    }
    
    IEnumerator PauseBug()
    {
        Debug.Log("Starting PauseBug coroutine...");

        yield return new WaitForSeconds(1f);
        Debug.Log("PauseBug coroutine finished.");

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
