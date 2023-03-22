using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Se souvenir du volume
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    public void MettreAJourVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");

    }
}
