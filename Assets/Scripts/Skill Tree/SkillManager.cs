using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private Skill skill;

    public void BuySkill()
    {
        if (!HasPrerequisites(skill))
        {
            Dialog.Show("Skill not available", "You do not have the prerequisites for this skill", "Ok", null, "Ok", null);
        }
        if (!skill.Cost.CanAfford())
        {
            Dialog.Show("Not enough currency", "You do not have enough currency to buy this skill", "Ok", null, "Ok", null);
        }
        CurrencyController.RedSouls -= skill.Cost.costRedSouls;
        CurrencyController.BlueSouls -= skill.Cost.costBlueSouls;
        CurrencyController.YellowSouls -= skill.Cost.costYellowSouls;
        CurrencyController.Eyece -= skill.Cost.costEyece;
        CurrencyController.FingerBurn -= skill.Cost.costFingerBurn;

        SkillTree.AddSkill(skill);
        Dialog.Show("Skill bought", "You have bought the skill " + skill.Name, "Ok", null, "Ok", null);
    }

    private bool HasPrerequisites(Skill skill)
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
