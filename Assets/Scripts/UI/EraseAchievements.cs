using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseAchievements : MonoBehaviour
{

    public void Erase()
    {
        CurrencyController.ResetCurrency();
        SkillTree.RemoveAllSkills();
    }
}
