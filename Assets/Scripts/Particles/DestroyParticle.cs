using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private AudioSource drop;
    // Start is called before the first frame update
    void Start()
    {
        drop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
        //drop.Play();
    }
}
