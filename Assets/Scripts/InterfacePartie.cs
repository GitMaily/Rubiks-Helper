using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfacePartie : MonoBehaviour
{
    public AudioSource hoverSound;

    public void boutonRetour()
    {
        SceneManager.LoadScene("MenuUI");

    }

    public void boutonRealigner()
    {
    
        var vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;
        
        print("bouton cliqué");
    }
    
    public void SonHover(){
        hoverSound.Play();
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
