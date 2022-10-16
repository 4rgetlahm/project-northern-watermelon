using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance { get; private set; }

    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public DialogController ShowDialog(string title, string description, string button1Text, Action button1Action, string button2Text, Action button2Action)
    {
        GameObject dialog = Instantiate(dialogBox, canvas.transform);
        DialogController dialogController = dialog.GetComponent<DialogController>();
        dialogController.Setup(title, description, button1Text, button1Action, button2Text, button2Action);
        dialogBox.SetActive(true);
        return dialogController;
    }

    public static void Show(string title, string description, string button1Text, Action button1Action, string button2Text, Action button2Action)
    {
        Instance.ShowDialog(title, description, button1Text, button1Action, button2Text, button2Action);
    }

}
