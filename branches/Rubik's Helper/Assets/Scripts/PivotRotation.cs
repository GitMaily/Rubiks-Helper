using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    
    public Transform Up;
    public Transform Down;
    public Transform Left;
    public Transform Right;
    public Transform Front;
    public Transform Back;
    
    private List<GameObject> activeSide;
    
    private Vector3 localForward;
    private Vector3 mouseRef;
    public bool dragging = false;
    
    private bool autoRotating = false;
    private Quaternion targetQuaternion;
    private float speed = 300f;
    
    private float sensibility = 0.25f;
    private Vector3 rotation;
    
    private ReadCube readCube;
    private CubeState cubeState;
    
    Vector3 rotationInitiale;
    Vector3 rotationFinale;
    
    Quaternion rotationInitialeQuaternion;
    Quaternion rotationFinaleQuaternion;

    private bool rotationAjoutee = false;
    private bool rotationManuelleAjoutee = false;

    public bool isFunctionRunning = false;
    
    public static List<string> facesSelectionnees = new List<string>();
    public static List<string> listeRotationsManuelles = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    { 
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        
    }

    // Update is called once per frame
    void Update()
    {
        string nomMove = "";

        if (dragging)
        {
            nomMove = SpinSide(activeSide);
            
            if (!rotationAjoutee)
            {
                facesSelectionnees.Add(nomMove);
                //Debug.Log("nomMove ajouté :"+nomMove);
                //Debug.Log("Affichage de facesSelectionnees");
                //afficherListe(facesSelectionnees);
                rotationAjoutee = true;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
                rotationAjoutee = false;
                //nomMove = DeterminerRotationManuelle();

            }
        }
        if (autoRotating)
        {
            
            //Input.ResetInputAxes();
            AutoRotate();
            //DeterminerRotationManuelle();

            
        }
        //DeterminerRotationManuelle();

    
        /*AjouterRotationCheminInverseOuNon();
        
        Debug.Log("Affichage de listeRotationsManuelles");
        afficherListe(listeRotationsManuelles);
        rotationManuelleAjoutee = true;*/
        
        //listeRotationsManuelles.Add(DeterminerRotationManuelle());

        /*if (!rotationManuelleAjoutee)
        {
            listeRotationsManuelles.Add(DeterminerRotationManuelle());
            Debug.Log("affichage listeRotationsManuelles dans méthode");
            afficherListe(listeRotationsManuelles);

            rotationManuelleAjoutee = true;
        }*/

    }
    
    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        // Créer un vecteur pour faire tourner
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }
    
    /// <summary>
    /// Fait tourner une face avec la souris
    /// </summary>
    /// <param name="side"></param>
    private string SpinSide(List<GameObject> side)// utilise la souris pour faire tourner 
    {
        
        isFunctionRunning = true;
        // réinitialiser la rotation
        rotation = Vector3.zero;

        // la position de la souris actuelle moins celle de la dernière position
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);
        float rotationAmount = (mouseOffset.x + mouseOffset.y) * sensibility;

        string nomMove = "";

        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensibility * 1;


            nomMove = "U";

            //Debug.Log("rotation.y = "+rotation.y+", mouseOffset = "+mouseOffset);
            
            
            
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            
            nomMove = "D";

            /*if (!rotationAjoutee)
            {
                RotationAutomatique.AjouterListeMemoire(nomMove);
                rotationAjoutee = true;
            }*/
        }
        if (side == cubeState.left)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            
            nomMove = "R";

            /*if (!rotationAjoutee)
            {
                RotationAutomatique.AjouterListeMemoire(nomMove);
                rotationAjoutee = true;
            }*/
        }
        if (side == cubeState.right)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensibility * 1;
            
            nomMove = "L";

            /*if (!rotationAjoutee)
            {
                RotationAutomatique.AjouterListeMemoire(nomMove);
                rotationAjoutee = true;
            }*/
        }
        if (side == cubeState.front)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensibility * -1;
            
            nomMove = "F";

            /*if (!rotationAjoutee)
            {
                RotationAutomatique.AjouterListeMemoire(nomMove);
                rotationAjoutee = true;
            }*/
        }
        if (side == cubeState.back)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensibility * 1;
            
            nomMove = "B";

            /*if (!rotationAjoutee)
            {
                RotationAutomatique.AjouterListeMemoire(nomMove);
                rotationAjoutee = true;
            }*/
        }
        

        if (!rotationAjoutee)
        {
            rotationInitiale = transform.rotation.eulerAngles;
            rotationInitialeQuaternion = transform.rotation;

        }
        // rotate
        transform.Rotate(rotation, Space.Self);
        
        //var rotationFinale = transform.rotation.eulerAngles;


        mouseRef = Input.mousePosition;
        isFunctionRunning = false;

        return nomMove;
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
        //isFunctionRunning = true;
        dragging = false;
        var step = speed * Time.deltaTime;
        //step = Mathf.Clamp(step, 0f, 1f);
        
        //Debug.Log("active side :"+activeSide[4]);
        //Vector3 initialRotation = activeSide[4].transform.localEulerAngles;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
        //Vector3 currentRotation = activeSide[4].transform.localEulerAngles;
        
        
        // si à moins de 1°, régler l'angle sur l'angle cible et terminer la rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;

            // Vérifier si il y a bien eu une rotation
            rotationFinale = transform.rotation.eulerAngles;
            rotationFinaleQuaternion = transform.rotation;

            //if (RotationAutomatique.clicResoudre == false)
            {
                List<string> rotationManuelle = DeterminerRotationManuelle();
                if (!rotationManuelle.Equals(""))
                {
                    listeRotationsManuelles.AddRange(rotationManuelle);

                }
                

            }

            //Debug.Log("listeROtationsManuelles dans autorotate, count ="+listeRotationsManuelles.Count);
            //afficherListe(listeRotationsManuelles);
            
            //DeterminerRotationManuelle();

            /*Debug.Log("rotationInitiale dans autoRotate"+ rotationInitiale);
            Debug.Log("rotationFinale dans autoRotate"+ rotationFinale);
            
            if (rotationInitiale == rotationFinale)
            {
                Debug.Log("pas de rotation");
                facesSelectionnees.Remove(facesSelectionnees[facesSelectionnees.Count-1]);
            }
            else
            {
                // Il y a eu une rotation
                // Déterminer l'angle de la rotation
                Debug.Log("Il y a eu une rotation");
                
                Quaternion rotationEffectueeQuaternion = rotationFinaleQuaternion * Quaternion.Inverse(rotationInitialeQuaternion);

                var rotationEffectueeEuler = rotationEffectueeQuaternion.eulerAngles; // 270 = sens horaire, 90 = sens anti-horaire

                Debug.Log("rotationEffectueeQuaternion =" +rotationEffectueeQuaternion);
                Debug.Log("Angles d'Euler de la rotation effectuée : " + rotationEffectueeEuler);
                
                

            }*/
            
            // dégrouper les petits cubes
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            // réinitialiser les booléens qui permettaient d'affirmer la manipulation
            CubeState.autoRotating = false;
            autoRotating = false;
            dragging = false;                                                               
        }
        //isFunctionRunning = false;
    }

    public bool IsFunctionRun()
    {
        return isFunctionRunning;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);
        
        // Trouver l'axe pour faire une rotation
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        
        autoRotating = true;
    }

    public void AjouterRotationCheminInverseOuNon()
    {
        listeRotationsManuelles.AddRange(DeterminerRotationManuelle());
        Debug.Log("affichage listeRotationsManuelles dans méthode");
        afficherListe(listeRotationsManuelles);
        rotationManuelleAjoutee = true;

        //return DeterminerRotationManuelle();
    }

    /// <summary>
    /// Va déterminer la rotation effectuée manuellement
    /// </summary>
    public List<string> DeterminerRotationManuelle()
    {
        List<string> rotationManuelleEffectuee = new List<string>();
        //string rotationManuelleEffectuee = "";

        float epsilon = 0.0001f;

        if (Vector3.Distance(rotationInitiale, rotationFinale) < epsilon) //if (rotationInitiale == rotationFinale)
            
        {
            // Vérifier qu'il y a bien eu une rotation

            //Debug.Log("pas de rotation");
            if (facesSelectionnees.Count > 0)
            {
                //facesSelectionnees.Remove(facesSelectionnees[^1]); // équivalent à facesSelectionnees[facesSelectionnees.Count-1]

            }

        }
        else
        {
            // Il y a eu une rotation
            //string derniereRotation = facesSelectionnees[^1];
            //RotationAutomatique.AjouterListeMemoire(derniereRotation);
            // Déterminer l'angle de la rotation
            //Debug.Log("Il y a eu une rotation");

            Quaternion rotationEffectueeQuaternion = rotationFinaleQuaternion * Quaternion.Inverse(rotationInitialeQuaternion);

            var rotationEffectueeEuler = rotationEffectueeQuaternion.eulerAngles; // 270 = sens horaire, 90 = sens anti-horaire

            //Debug.Log("rotationEffectueeQuaternion =" + rotationEffectueeQuaternion);
            Debug.Log("Angles d'Euler de la rotation effectuée : " + rotationEffectueeEuler);


            if (facesSelectionnees.Count > 0)
            {
                switch (facesSelectionnees[^1])
                {
                    
                    case "U":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                    case "F":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                    case "R":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                    case "B":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                    case "L":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                    case "D":
                        rotationManuelleEffectuee = DeterminerSensRotation(rotationEffectueeEuler, facesSelectionnees[^1]);
                        break;
                }

                //RotationAutomatique.AjouterListeMemoire(rotationEffectuee);
            }


            if (facesSelectionnees.Count > 0)
            {
                facesSelectionnees.Remove(facesSelectionnees[^1]);

            }

        }

        return rotationManuelleEffectuee;
    }
    
    
    private List<string> DeterminerSensRotation(Vector3 rotationEffectueeEuler, string nomMove)
    {
        List<string> sensRotations = new List<string>();
        
        // Seulement si l'orientation (rotation) du cube est à (0,0,0) ? J'ai rien dit
        //if (transform.parent.rotation.eulerAngles == Vector3.zero)
        {
            float epsilon = 0.0001f;

            // Cas pour U en haut
            if (Mathf.Abs(rotationEffectueeEuler.y - 270.0f) < epsilon) // Correspond aussi à 3 fois la même rotation
            {
                // Ajouter un ' pour donner l'inverse
                Debug.Log("Rotation 270 = anti-clockwise");
                nomMove = nomMove + "'";
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);
                

            }
            else if(Mathf.Abs(rotationEffectueeEuler.y - 90.0f) < epsilon)
            {
                Debug.Log("Rotation 90 = clockwise");
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);

            }
            else if(Mathf.Abs(rotationEffectueeEuler.y - 180.0f) < epsilon) // Correspond à 2 fois la même rotation
            {
                Debug.Log("Rotation 180 = clockwise"); 
                // Ajouter le mouvement une nouvelle fois
                sensRotations.Add(nomMove);
                sensRotations.Add(nomMove);

                Debug.Log("Ajout de 2*"+nomMove);

            }
            
            // Cas pour F à gauche
            if (Mathf.Abs(rotationEffectueeEuler.x - 270.0f) < epsilon)
            {
                // Ajouter un ' pour donner l'inverse
                Debug.Log("Rotation 270 = anti-clockwise");
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);

            }
            else if(Mathf.Abs(rotationEffectueeEuler.x - 90.0f) < epsilon)
            {
                Debug.Log("Rotation 90 = clockwise");
                nomMove = nomMove + "'";
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);

            }
            else if(Mathf.Abs(rotationEffectueeEuler.x - 180.0f) < epsilon) // Correspond à 2 fois la même rotation
            {
                Debug.Log("Rotation 180 = clockwise"); 
                // Ajouter le mouvement une nouvelle fois
                sensRotations.Add(nomMove);
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de 2*"+nomMove);

                
            }
            // Cas pour R à droite
            if (Mathf.Abs(rotationEffectueeEuler.z - 270.0f) < epsilon)
            {
                // Ajouter un ' pour donner l'inverse
                //Debug.Log("Rotation 270 = anti-clockwise");
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);

            }
            else if(Mathf.Abs(rotationEffectueeEuler.z - 90.0f) < epsilon)
            {
                //Debug.Log("Rotation 90 = clockwise");
                nomMove = nomMove + "'";
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de "+nomMove);

            }
            else if(Mathf.Abs(rotationEffectueeEuler.z - 180.0f) < epsilon) // Correspond à 2 fois la même rotation
            {
                Debug.Log("Rotation 180 = clockwise"); 
                // Ajouter le mouvement une nouvelle fois
                sensRotations.Add(nomMove);
                sensRotations.Add(nomMove);
                Debug.Log("Ajout de 2* "+nomMove);

                
            }
            
        }

        return sensRotations;
        //return nomMove;

    }
    
    

    private void afficherListe(List<string> moves)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        foreach(string nomRotation in moves)
        {
            sb.Append(nomRotation);
            if (i != moves.Count-1)
            {
                sb.Append(", ");
            }
            i++;
        }
        
        Debug.Log(sb.ToString());
    }


}
