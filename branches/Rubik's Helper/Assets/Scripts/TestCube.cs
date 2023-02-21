using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MouseButton = Unity.VisualScripting.MouseButton;

public class TestCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Rotate(0,90,0);

        }



        MouseEnterEvent mouseEnterEvent = new MouseEnterEvent();
        GameObject.FindWithTag("FirstCubeTag");
        
        if (Input.GetMouseButtonDown(0))
        {
            this.transform.Rotate(0,90,0);

        }
        
    }
}
