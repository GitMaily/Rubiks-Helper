using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCube2D : MonoBehaviour
{

    public Transform transformU;
    public Transform tranformD;
    public Transform transformF;
    public Transform tranformB;
    public Transform transformL;
    public Transform tranformR;
    
    RubiksFaces _rubiksFaces;

    private int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        _rubiksFaces = FindObjectOfType<RubiksFaces>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        List<GameObject> raycastHitFaces = new List<GameObject>();
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

        _rubiksFaces.front = raycastHitFaces;
    }
}
