using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SkillIcon : MonoBehaviour
{
    [SerializeField]
    public Sprite skillImage;
    [SerializeField]
    public string skillNameText;
    [SerializeField]
    public string skillDescText;
    [SerializeField]
    public SkillManager skillManager;
    [SerializeField]
    public Skill skill;

    [SerializeField]
    public TMP_Text nameObj;
    [SerializeField]
    public TMP_Text descObj;
    [SerializeField]
    public Image iconObj;
    [SerializeField]
    public GameObject upgradeButton;
    [SerializeField]
    public GameObject costUI;

    public bool isBought = false;
    public bool isActivated = false;

    [SerializeField]
    public Image currentSprite;
    [SerializeField]
    public Sprite deactivatedIcon;
    [SerializeField]
    public Sprite boughtIcon;
    [SerializeField]
    public Sprite normalIcon;

    public void Update()
    {
        //check if skill was bought - turn yellow
        //check if skill cannot be bought - turn gray
        if (SkillTree.HasSkill(skill))
        {
            costUI.SetActive(false);
            currentSprite.sprite = boughtIcon;
            isBought = true;
            return;
        }
        if (skillManager.HasPrerequisites(skill))
        {
            currentSprite.sprite = normalIcon;
            isActivated = true;
            return;
        }
        
    }

    public void PressSkillButton()
    {
        nameObj.text = skillNameText;
        descObj.text = skillDescText;
        iconObj.sprite = skillImage;

        //check if skill has been bought
        if(isBought)
        {
            upgradeButton.SetActive(false);
            return;
        }

        //check if previous skills were bought to unlock it
        if (!isActivated)
        {
            upgradeButton.SetActive(false);
            return;
        }

        if (!skill.Cost.CanAfford())
        {
            upgradeButton.SetActive(false);
            return;
        }

        upgradeButton.SetActive(true);
        upgradeButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { skillManager.BuySkill(); });
    }
}
