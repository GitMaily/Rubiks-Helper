using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Sauvegarder : MonoBehaviour
{


    public void FaireSauvegarder(string nomSauvegarde)
    {
        SauvegardeInputs _sauvegardeInputs = new SauvegardeInputs();
        string filepath = Application.dataPath + "/"+nomSauvegarde+".json";

        string sauvegardeJson = JsonUtility.ToJson(_sauvegardeInputs, true);

        StreamWriter streamWriter = File.CreateText(filepath);
            
        streamWriter.Close();
            
        File.WriteAllText(filepath, sauvegardeJson, Encoding.UTF8);
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
