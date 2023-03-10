using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    private float vitesse = 10f;
    private float vitesseRotationAuto = 300f;
    public Camera camera;
    private Quaternion targetQuaternion;
    private bool autoRotating = false;
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
        
    }
}
