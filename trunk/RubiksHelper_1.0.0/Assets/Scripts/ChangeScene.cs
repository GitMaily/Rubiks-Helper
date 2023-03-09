using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change_scene(int ID_Scene)
    {
        SceneManager.LoadScene(ID_Scene);
    }
}
