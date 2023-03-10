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
    
    public void Set()
    {
        cubeState2 = FindObjectOfType<CubeState>();
        
        UpdateMap(cubeState2.front, front);
        UpdateMap(cubeState2.back, back);
        UpdateMap(cubeState2.left, left);
        UpdateMap(cubeState2.right, right);
        UpdateMap(cubeState2.up,up);
        UpdateMap(cubeState2.down, down);
    }
    
    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = new Color(1,0.5f,0,1);
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
                map.GetComponent<Image>().color = Color.red;
            }
            i++;


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
