using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMiniJeux : MonoBehaviour
{

    public void BoutonRetour()
    {
        SceneManager.LoadScene("MenuUI");
    }


    public void BoutonSixTrous()
    {
        Patterns.isTetris = false;
        Patterns.isSuperflip = true;
        SceneManager.LoadScene("PartieMiniJeux");
    }
    
    public void BoutonTetris()
    {
        Patterns.isSuperflip = false;
        Patterns.isTetris = true;
        SceneManager.LoadScene("PartieMiniJeux");
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
