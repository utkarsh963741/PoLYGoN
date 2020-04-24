using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    [SerializeField]
    public int index=1;

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(index);
        }
    }
}
