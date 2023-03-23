using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    /*
    public static string score1;
    public static string score2;
    public static string score3;
    public Text _score1;
    public Text _score2;
    public Text _score3;
    */
    public static string[] scores = { "00:00","00:00","00:00" };
    public Text[] scoresTexts;

    //public static int numSauvegarde;
    
    //private string scoreSauvegarde;

    //private Chronometre chrono;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationAutomatique.estResolu)
        {
            ComparaisonScore(scores, Chronometre.scoreFinal);
            /*
            //scoreSauvegarde = chrono.scoreFinal;
            bool placer = false;

            for (int i = 0; i < scores.Length && !placer; i++)
            {
                if (scores[i] == null)
                {
                    //scores[i] = scoreSauvegarde;
                    scores[i] = Chronometre.scoreFinal;
                    placer = true;
                }
            }

            if (!placer)
            {
                //ComparaisonScore(scores, scoreSauvegarde);
                ComparaisonScore(scores, Chronometre.scoreFinal);
            }
            
            if (score1 == null)
            {
                score1 = scoreSauvegarde;
            }
            else if (score2 == null)
            {
                score2 = scoreSauvegarde;
            }
            else if (score3 == null)
            {
                score3 = scoreSauvegarde;
            }
            else
            {
                if (TempsEnFloat(score1) < TempsEnFloat(scoreSauvegarde))
                {
                    score1 = scoreSauvegarde;
                }
                else
                {
                    if (TempsEnFloat(score2) < TempsEnFloat(scoreSauvegarde))
                    {
                        score2 = scoreSauvegarde;
                    }
                    else
                    {
                        if (TempsEnFloat(score3) < TempsEnFloat(scoreSauvegarde))
                        {
                            score3 = scoreSauvegarde;
                        }
                    }
                }
            }*/
        }

        for (int i = 0; i < scores.Length; i++)
        {
            scoresTexts[i].text = scores[i];
        }

        RotationAutomatique.estResolu = false;
    }

    private float TempsEnFloat(string tempsText)
    {
        string[] tempsSplit = tempsText.Split(':');
        int minutes = int.Parse(tempsSplit[0]);
        int secondes = int.Parse(tempsSplit[1]);
        float tempsF = minutes * 60 + secondes;
        return tempsF;
    }

    private void ComparaisonScore(string[] scores, string scoreFinal)
    {
        float _scoreFinal = TempsEnFloat(scoreFinal);
        float[] _scores = new float[scores.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            _scores[i] = TempsEnFloat(scores[i]);
        }

        //int numSauvegarde = 0;
        bool changer = false;
        string tempScore;
        for (int i = 0; i < _scores.Length && !changer; i++)
        {
            if (_scores[i] != 0f)
            {
                if (_scoreFinal < _scores[i])
                {
                    for (int j = scores.Length-1; j != i; j--)
                    {
                        scores[j] = scores[j - 1];
                    }

                    scores[i] = scoreFinal;
                    changer = true;
                }
            }
            else
            {
                scores[i] = scoreFinal;
                changer = true;
            }
        }
    }
    
    public void BoutonRetour()
    {
        SceneManager.LoadScene("MenuUI");
    }
}
