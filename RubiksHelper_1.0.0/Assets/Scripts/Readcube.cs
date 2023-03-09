using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readcube : MonoBehaviour
{
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;
    
    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();  
    private int layerMask = 1 << 6; // pour les faces du cube, il faut ajouter un layer à la 6e place dans l'éditeur

    public GameObject emptyGO;

    Cubestate cubeState;
    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();
        cubeState = FindObjectOfType<Cubestate>();
        ReadState();
        Cubestate.started = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
        {
            // utilisé pour nommer les rays, pour bien vérifier qu'ils soient dans le bon ordre
            int rayCount = 0;
            List<GameObject> rays = new List<GameObject>();
            // Créer 9 rays devant chaque cube pour une face
            // le ray 0 en haut à gauche et le ray 8 en bas à droite
            //  |0|1|2|
            //  |3|4|5|
            //  |6|7|8|
    
            for (int y = 1; y > -2; y--)
            {
                for (int x = -1; x < 2; x++)
                {
                    Vector3 startPos = new Vector3( rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                    GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                    rayStart.name = rayCount.ToString();
                    rays.Add(rayStart);
                    rayCount++;
                }
            }
            rayTransform.localRotation = Quaternion.Euler(direction);
            return rays;
    
        }


    void SetRayTransforms()
            {
                // Ajouter tous les ray necessaires dans la liste, pointés vers le cube.
                upRays = BuildRays(tUp, new Vector3(90, 90, 0));
                downRays = BuildRays(tDown, new Vector3(270, 90, 0));
                leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
                rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
                frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
                backRays = BuildRays(tBack, new Vector3(0, 270, 0));
            }
            

     public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform){
                List<GameObject> face = new List<GameObject>();
                
                        foreach (GameObject rayStart in rayStarts)
                        {
                            Vector3 ray = rayStart.transform.position;
                            RaycastHit hit;
                
                            // Vérifier si le ray croise un objet du même layerMask "Faces" (6)
                            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
                            {
                                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                                face.Add(hit.collider.gameObject);
                                //print(hit.collider.gameObject.name);
                            }
                            else
                            {
                                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
                            }
                        }
                        return face;
         }
         

    public void ReadState()
             {
                 cubeState = FindObjectOfType<Cubestate>();
         
                 // mettre l'état de chaque position des faces pour savoir la couleur de chaque position
                 cubeState.up = ReadFace(upRays, tUp);
                 cubeState.down = ReadFace(downRays, tDown);
                 cubeState.left = ReadFace(leftRays, tLeft);
                 cubeState.right = ReadFace(rightRays, tRight);
                 cubeState.front = ReadFace(frontRays, tFront);
                 cubeState.back = ReadFace(backRays, tBack);
         
                
         
             }

}
