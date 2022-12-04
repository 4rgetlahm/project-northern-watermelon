using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillCost : MonoBehaviour
{
    public int costRedSouls;
    public int costBlueSouls;
    public int costYellowSouls;
    public int costEyece;
    public int costFingerBurn;

    public SkillCost(int costRedSouls, int costBlueSouls, int costYellowSouls, int costEyece, int costFingerBurn)
    {
        this.costRedSouls = costRedSouls;
        this.costBlueSouls = costBlueSouls;
        this.costYellowSouls = costYellowSouls;
        this.costEyece = costEyece;
        this.costFingerBurn = costFingerBurn;
    }

    public bool CanAfford()
    {
        return (costRedSouls <= CurrencyController.RedSouls &&
                costBlueSouls <= CurrencyController.BlueSouls &&
                costYellowSouls <= CurrencyController.YellowSouls &&
                costEyece <= CurrencyController.Eyece &&
                costFingerBurn <= CurrencyController.FingerBurn);
    }
}
