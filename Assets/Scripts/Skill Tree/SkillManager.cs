using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private Skill skill;

    [SerializeField]
    public GameObject upgradeButton;

    public void BuySkill()
    {
        if (!HasPrerequisites(skill))
        {
            Dialog.Show("Skill not available", "You do not have the prerequisites for this skill", "Ok", null, "Ok", null);
            return;
        }
        if (!skill.Cost.CanAfford())
        {
            Dialog.Show("Not enough currency", "You do not have enough currency to buy this skill", "Ok", null, "Ok", null);
            //EIK TU NX ARNAI BENT ISTESTUOK SAVO SISTEMA
            return;
        }
        CurrencyController.RedSouls -= skill.Cost.costRedSouls;
        CurrencyController.BlueSouls -= skill.Cost.costBlueSouls;
        CurrencyController.YellowSouls -= skill.Cost.costYellowSouls;
        CurrencyController.Eyece -= skill.Cost.costEyece;
        CurrencyController.FingerBurn -= skill.Cost.costFingerBurn;

        SkillTree.AddSkill(skill);
        upgradeButton.SetActive(false);
        //Dialog.Show("Skill bought", "You have bought the skill " + skill.Name, "Ok", null, "Ok", null);
    }

    public bool HasPrerequisites(Skill skill)
    {
        foreach (Skill prerequisite in skill.Prerequisites)
        {
            if (!SkillTree.HasSkill(prerequisite))
            {
                return false;
            }
        }
        return true;
    }
}
