using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedFace : MonoBehaviour
{
    
    private CubeState cubeState;
    private ReadCube readCube;
    private int layerMask = 1 << 6;

    
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.autoRotating && transform.childCount == 27)
        {
       
            // Lire l'état courant du cube
            readCube.ReadState();

            // envoyer un raycast à partir de la souris pour voir si une face est touchée
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                // Faire une liste de tous les côtés (liste de face GameObjects)
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };
                
                // Si la face touchée existe dans un côté
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        // Les assembler
                        cubeState.PickUp(cubeSide);
                        // commencer la logique de la rotation
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}