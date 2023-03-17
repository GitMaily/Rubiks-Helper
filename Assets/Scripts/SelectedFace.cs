using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedFace : MonoBehaviour
{
    
    private CubeState cubeState;
    private ReadCube readCube;
    private PivotRotation pivotRotation;
    private Rotation rotation;
    private int layerMask = 1 << 6;

    public GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        pivotRotation = FindObjectOfType<PivotRotation>();
        rotation = FindObjectOfType<Rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.autoRotating && transform.childCount == 27 && !pivotRotation.dragging && !rotation.swiping)
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
                        temp = cubeSide[4];

                        // commencer la logique de la rotation
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (temp.name == "U" || temp.name == "D")
            {

                temp.transform.parent.Rotate(Vector3.up, 90f);
               
            }
            if (temp.name == "F" || temp.name == "B")
            {
                temp.transform.parent.Rotate(Vector3.left, 90f);
                
            }

            if (temp.name == "L" || temp.name == "R")
            {
                temp.transform.parent.Rotate(Vector3.back, 90f);

            }
        } 
    }
}