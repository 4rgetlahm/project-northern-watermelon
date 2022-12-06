using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator anim;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
}
