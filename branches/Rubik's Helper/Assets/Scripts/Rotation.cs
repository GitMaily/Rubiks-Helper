using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    private float vitesse = 10f;
    private float vitesseRotationAuto = 300f;
    public Camera camera;
    private Quaternion targetQuaternion;
    private bool autoRotating = false;
    
    // Les touches utilisées pour faire tourner le Rubik's Cube
        public KeyCode rotateUp;
        public KeyCode rotateDown;
        public KeyCode rotateLeft;
        public KeyCode rotateRight;
        
        GameObject pivot;
    
    public void rotationClicDroit()
    {
        float rotationX = Mathf.Round(Input.GetAxis("Mouse X") * vitesse);
        float rotationY = Mathf.Round(Input.GetAxis("Mouse Y") * vitesse);

        //print(rotationX);
        //print(rotationY);
        Vector3 droite = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 haut = Vector3.Cross(transform.position - camera.transform.position , droite);

        transform.rotation = Quaternion.AngleAxis(-rotationX, haut) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotationY, droite) * transform.rotation;
        
       

    }
    

    public void autoRealigner()
    {
        Vector3 vecInitial = transform.localEulerAngles;
        vecInitial.x = Mathf.Round(vecInitial.x / 90) * 90;
        vecInitial.y = Mathf.Round(vecInitial.y / 90) * 90;
        vecInitial.z = Mathf.Round(vecInitial.z / 90) * 90;
        
        targetQuaternion.eulerAngles = vecInitial;
        autoRotating = true;

    }
    
    private void AutoTurn()
    {
        var step = vitesseRotationAuto * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
        // si à moins de 1°, régler l'angle sur l'angle cible et terminer la rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            autoRotating = false;

        }
    } 
    
    public void boutonRealigner()
    {
    
        Vector3 vecInitial = transform.eulerAngles;
        vecInitial.x = Mathf.Round(vecInitial.x / 90) * 90;
        vecInitial.y = Mathf.Round(vecInitial.y / 90) * 90;
        vecInitial.z = Mathf.Round(vecInitial.z / 90) * 90;

        transform.eulerAngles = vecInitial;
        
        var step = vitesse * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = new GameObject("Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotationClicDroit();

        }

        if (Input.GetMouseButtonUp(1))
        {
            autoRealigner();
            
        }
        if (autoRotating)
        {
            AutoTurn();
        }

        boutonRotation();

    }

    public void boutonRotation()
    {
        pivot.transform.position = transform.position;
        if (Input.GetKeyDown(rotateUp))
        {
            RotateCube(Vector3.left, rotateUp);
        }
        if (Input.GetKeyDown(rotateDown))
        {
            RotateCube(Vector3.right, rotateDown);
        }
        if (Input.GetKeyDown(rotateLeft))
        {
            RotateCube(Vector3.up, rotateLeft);
        }
        if (Input.GetKeyDown(rotateRight))
        {
            RotateCube(Vector3.down, rotateRight);
        }
    }
    
    void RotateCube(Vector3 axis, KeyCode toucheClavier)
    {
        // Création d'un pivot pour faire pivoter le Rubik's Cube autour
        
        // pivot.transform.position = transform.position;

        // Positionnement du Rubik's Cube sur le pivot
        //transform.SetParent(pivot.transform);

        
        // Rotation du pivot autour de l'axe approprié
        transform.Rotate(axis, pivot.transform.position.y+ 90);
        
        //var step = vitesseRotationAuto * Time.deltaTime;
        //pivot.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);


        // Positionnement du Rubik's Cube à son emplacement d'origine
        //transform.SetParent(null);

        // Destruction du pivot
        //Destroy(pivot);
    }
}
