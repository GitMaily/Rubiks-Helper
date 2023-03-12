using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    private float vitesse = 10f;
    private float vitesseCubeEspace = 200f;
    private float vitesseRotationAuto = 300f;
    public Camera camera;
    private Quaternion targetQuaternion;
    private bool autoRotating = false;
    
    // Les touches utilisées pour faire tourner le Rubik's Cube
    /*public KeyCode rotateUp;
    public KeyCode rotateDown;
    public KeyCode rotateLeft;
    public KeyCode rotateRight;
        */
    GameObject pivot;
    
    Vector2 firstPressPos;
    Vector2 secondPresPos;
    Vector2 currentSwipe;

    private bool rotationCubeEnCours = false;
    
    public GameObject target;

    #region Rotation dans l'espace avec clic droit

    public void rotationClicDroit()
    {
        
        float rotationX = Mathf.Round(Input.GetAxis("Mouse X") * vitesse);
        float rotationY = Mathf.Round(Input.GetAxis("Mouse Y") * vitesse);
        
        Vector3 droite = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 haut = Vector3.Cross(transform.position - camera.transform.position , droite);

        transform.rotation = Quaternion.AngleAxis(-rotationX, haut) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotationY, droite) * transform.rotation;
        

    }

    public void rotationClicDroitSansAxeZ()
    {
        float rotationX = Mathf.Round(Input.GetAxis("Mouse X") * vitesse);
        float rotationY = Mathf.Round(Input.GetAxis("Mouse Y") * vitesse);
        
        Vector3 droite = camera.transform.right;
        Vector3 haut = camera.transform.up;

        transform.RotateAround(transform.position, droite, -rotationY);
        transform.RotateAround(transform.position, haut, rotationX);
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        //transform.rotation = Quaternion.Euler(mouseDelta.y,-mouseDelta.x,0) * transform.rotation;
    }
    

    public void calculerRealignement()
    {
        Vector3 vecInitial = transform.localEulerAngles;
        vecInitial.x = Mathf.Round(vecInitial.x / 90) * 90;
        vecInitial.y = Mathf.Round(vecInitial.y / 90) * 90;
        vecInitial.z = Mathf.Round(vecInitial.z / 90) * 90;
        
        targetQuaternion.eulerAngles = vecInitial;
        autoRotating = true;
    }
   

    private void AutoRealigner()
    {
        var step = vitesseRotationAuto * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
        // si à moins de 1°, régler l'angle sur l'angle cible et terminer la rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            target.transform.rotation = transform.rotation;
            autoRotating = false;
            rotationCubeEnCours = false;

        }
    } 
    #endregion
    /*public void boutonRealigner()
    {
    
        Vector3 vecInitial = transform.eulerAngles;
        vecInitial.x = Mathf.Round(vecInitial.x / 90) * 90;
        vecInitial.y = Mathf.Round(vecInitial.y / 90) * 90;
        vecInitial.z = Mathf.Round(vecInitial.z / 90) * 90;

        transform.eulerAngles = vecInitial;
        
        var step = vitesse * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
    }*/
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = new GameObject("Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            rotationCubeEnCours = true;

            rotationClicDroit();

        }

        if (Input.GetMouseButtonUp(2))
        {
            calculerRealignement();
        }
        if (autoRotating)
        {
            AutoRealigner();
        }
        /*else
        {
            if (!autoRotating && !rotationCubeEnCours && transform.rotation != target.transform.rotation)
            {
                var step = vitesseCubeEspace * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            } 
        }*/
        

        //boutonRotation();
        //Swipe();
        RotationSwipe();


    }

    #region RotationBoutons

   /* public void boutonRotation()
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
    }*/
    
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
    #endregion


    public void RotationSwipe()
    {
        if (!autoRotating && !rotationCubeEnCours)
        {
            Swipe();
            SwipeFleches();
            if (transform.rotation != target.transform.rotation)
            {
                var step = vitesseCubeEspace * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            } 
        }
    }

    private bool ToucheClavierFleche()
    {
        // pour l'instant juste gauche et droite
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //print(firstPressPos); 
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            secondPresPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPresPos.x - firstPressPos.x, secondPresPos.y - firstPressPos.y);
            
            // mettre la magnitude du vecteur à 1 (on souhaite juste la direction, pas tout le vecteur)
            currentSwipe.Normalize();
            if (LeftSwipe(currentSwipe) || Input.GetKeyDown(KeyCode.LeftArrow))
            {

                target.transform.Rotate(0,90,0,Space.World);
            }
            else if (RightSwipe(currentSwipe) || Input.GetKeyDown(KeyCode.RightArrow))
            {

                target.transform.Rotate(0,-90,0,Space.World);
            }
            else if (UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(90,0,0,Space.World);
            }
            else if (UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0,0,-90,Space.World);
            }
            else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0,0,90,Space.World);
            }
            else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(-90,0,0,Space.World);
            }
            
            
        }
        
    }
    void SwipeFleches()
    {
        if (ToucheClavierFleche())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                target.transform.Rotate(0,0,-90,Space.World);
            }
            
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                target.transform.Rotate(-90,0,0,Space.World);
            }
            
            
            
        }
        
    }
    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    } 
    
    
    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    } 
    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    } 
    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    } 
    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    } 
}
