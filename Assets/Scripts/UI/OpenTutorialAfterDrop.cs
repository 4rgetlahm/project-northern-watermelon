using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorialAfterDrop : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;

    public void Update()
    {
        if (CurrencyController.RedSouls >= 1)
            Open();
    }

    public void Open()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Close()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;

        this.gameObject.SetActive(false);

    }
}
