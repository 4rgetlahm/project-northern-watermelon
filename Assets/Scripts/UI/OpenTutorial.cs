using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;

    [SerializeField]
    public GameObject otherTutorial;

    public float waitFor = 2f;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTime + waitFor <= Time.time)
        {
            canvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Close()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        
        if(otherTutorial != null)
            otherTutorial.SetActive(true);

        this.gameObject.SetActive(false);

    }
}
