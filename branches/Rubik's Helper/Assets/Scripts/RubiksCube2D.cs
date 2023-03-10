using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubiksCube2D : MonoBehaviour
{

    public Transform transformU;
    public Transform transformD;
    public Transform transformF;
    public Transform transformB;
    public Transform transformL;
    public Transform transformR;
    
    RubiksFaces _rubiksFaces;
    Cubestate cubeState;
    //private int layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        //_rubiksFaces = FindObjectOfType<RubiksFaces>();
        //cubeState = FindObjectOfType<Cubestate>();

    }

    // Update is called once per frame
    void Update()
    {
        
        /*List<GameObject> raycastHitFaces = new List<GameObject>();
        Vector3 raycast = transformF.transform.position;
        RaycastHit raycastHit;
        
        if(Physics.Raycast(raycast,transformF.right, out raycastHit, Mathf.Infinity,layerMask))
        {
            Debug.DrawRay(raycast, transformF.right * raycastHit.distance, Color.yellow);
            raycastHitFaces.Add(raycastHit.collider.gameObject);
            print(raycastHit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawRay(raycast,transformF.right * 1000, Color.green);
        }

        _rubiksFaces.front = raycastHitFaces;*/
    }

    public void Set()
    {
        cubeState = FindObjectOfType<Cubestate>();
        
        UpdateMap(cubeState.front, transformF);
        //UpdateMap(cubeState.back, transformB);
        //UpdateMap(cubeState.left, transformL);
        //UpdateMap(cubeState.right, transformR);
        //UpdateMap(cubeState.up,transformU);
        //UpdateMap(cubeState.down, transformD);
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1,0.5f,0,1);
            }

            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
