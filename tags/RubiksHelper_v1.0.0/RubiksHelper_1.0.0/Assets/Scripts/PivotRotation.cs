using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    
    private Vector3 localForward;
    private Vector3 mouseRef;
    bool dragging = false;
    
    private bool autoRotating = false;
    private Quaternion targetQuaternion;
    private float speed = 300f;
    
    private float sensibility = 0.4f;
    private Vector3 rotation;
    
    private Readcube readCube;
    private Cubestate cubeState;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<Readcube>();
        cubeState = FindObjectOfType<Cubestate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
            dragging = false;
            RotateToRightAngle();
           }
           
        }
        if (autoRotating)
        {
           AutoRotate();
         }
    }
    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        // Créer un vecteur pour faire tourner
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }
    
    private void SpinSide(List<GameObject> side)// utilise la souris pour faire tourner a modifier
        {
            // réinitialiser la rotation
            rotation = Vector3.zero;
    
            // la position de la souris actuelle moins celle de la dernière position
            Vector3 mouseOffset = (Input.mousePosition - mouseRef);        
    
    
            if (side == cubeState.up)
            {
                rotation.y = (mouseOffset.x + mouseOffset.y) * sensibility * 1;
            }
            if (side == cubeState.down)
            {
                rotation.y = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            }
            if (side == cubeState.left)
            {
                rotation.z = (mouseOffset.x + mouseOffset.y) * sensibility * 1;
            }
            if (side == cubeState.right)
            {
                rotation.z = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            }
            if (side == cubeState.front)
            {
                rotation.x = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            }
            if (side == cubeState.back)
            {
                rotation.x = (mouseOffset.x + mouseOffset.y) * sensibility * 1;
            }
            // rotate
            transform.Rotate(rotation, Space.Self);
    
            mouseRef = Input.mousePosition;
        }
        
        public void RotateToRightAngle()
            {
                Vector3 vec = transform.localEulerAngles;
                // on arrondit à l'angle 90 le plus proche
                vec.x = Mathf.Round(vec.x / 90) * 90;
                vec.y = Mathf.Round(vec.y / 90) * 90;
                vec.z = Mathf.Round(vec.z / 90) * 90;
        
                targetQuaternion.eulerAngles = vec;
                autoRotating = true;
            }
        
        
         private void AutoRotate()
            {
                dragging = false;
                var step = speed * Time.deltaTime;
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
                // if within one degree, set angle to target angle and end the rotation
                if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
                {
                    transform.localRotation = targetQuaternion;
                    // dégrouper les petits cubes
                   // cubeState.PutDown(activeSide, transform.parent);
                    readCube.ReadState();
                    //Cubestate.autoRotating = false;
                    autoRotating = false;
                    dragging = false;                                                               
                }
            } 



}
