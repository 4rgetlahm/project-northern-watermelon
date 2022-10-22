using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PathfinderMovement pathfinderMovement = GetComponent<PathfinderMovement>();
        pathfinderMovement.Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
