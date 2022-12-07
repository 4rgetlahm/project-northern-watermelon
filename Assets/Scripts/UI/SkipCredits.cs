using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCredits : MonoBehaviour
{
    public ChangeScenes script;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            script.ChangeScene();
        }
    }
}
