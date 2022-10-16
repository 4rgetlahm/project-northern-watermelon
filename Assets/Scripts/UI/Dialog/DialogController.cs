using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogController : MonoBehaviour
{

    private Action button1Action;
    private Action button2Action;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text button1Text;
    [SerializeField] private TMP_Text button2Text;

    public void Setup(string title, string description, string button1Text, Action button1Action, string button2Text, Action button2Action)
    {
        this.titleText.text = title;
        this.descriptionText.text = description;
        this.button1Text.text = button1Text;
        this.button2Text.text = button2Text;
        this.button1Action = button1Action;
        this.button2Action = button2Action;
    }


    public void Button1Pressed()
    {
        button1Action.Invoke();
        Destroy(gameObject);
    }

    public void Button2Pressed()
    {
        button2Action.Invoke();
        Destroy(gameObject);
    }

}