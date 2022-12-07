using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    //public EnemyHealth script;
    public GameObject canvas;
    public GameObject boss;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("no");
        if (boss == null)
        {
            Debug.Log("yes");
            canvas.SetActive(true) ;
        }
    }
}
