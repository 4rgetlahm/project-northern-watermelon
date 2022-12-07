using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;

    [SerializeField]
    public GameObject otherTutorial;

    public bool close = true;

    public float waitFor = 2f;
    private float startTime;

    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!activated && startTime + waitFor <= Time.time)
        {
            activated = true;
            canvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Close()
    {
        if(close)
            canvas.SetActive(false);
        Time.timeScale = 1f;
        
        if(otherTutorial != null)
            otherTutorial.SetActive(true);

        if(close)
            this.gameObject.SetActive(false);

    }
}
