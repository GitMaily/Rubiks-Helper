using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public float vitesse = 5f;
    public Camera camera;
    public GameObject rotationCible;
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

    public void boutonRealigner()
    {
    
        Vector3 vecInitial = transform.eulerAngles;
        vecInitial.x = Mathf.Round(vecInitial.x / 90) * 90;
        vecInitial.y = Mathf.Round(vecInitial.y / 90) * 90;
        vecInitial.z = Mathf.Round(vecInitial.z / 90) * 90;

        // Debug.Log("x =" + vecInitial.x);
        // Debug.Log("y =" + vecInitial.y);
        // Debug.Log("z =" + vecInitial.z);

        transform.eulerAngles = vecInitial;
        //transform.rotation = new Quaternion(vecInitial.x,vecInitial.y,vecInitial.z,vitesse);
        // Debug.Log("apres x =" + vecInitial.x);
        // Debug.Log("apres y =" + vecInitial.y);
        // Debug.Log("apres z =" + vecInitial.z);
        // print(transform.rotation);

        //transform.eulerAngles += new Vector3(vecInitial.x, vecInitial.y, vecInitial.z) * Time.deltaTime * vitesse;
        
        //rotationCible.transform.rotation = transform.rotation.eulerAngles.x


        //transform.rotation = Quaternion.Lerp(transform.rotation, rotationCible.transform.rotation, Time.deltaTime * vitesse);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationCible.transform.rotation, vitesse);
        //transform.eulerAngles = vecFin;
        //transform.rotation = Quaternion.Lerp (transform.rotation, new Quaternion.Euler(90, transform.rotation.y, tramsform.rotation.z), Time.deltaTime * speed);
        //print("is clicked");
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
            
            //boutonRealigner();
            /*if (transform.rotation != rotationCible.transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationCible.transform.rotation, vitesse);

            }*/
          
        }
        
    }
}
