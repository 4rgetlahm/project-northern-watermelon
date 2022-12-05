using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolSpeed1 : Skill
{
    private static int BaseSkillId = 2;
    public PistolSpeed1() :
    base(
        SkillType.PistolPassive,
        "Speed1",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(5, 0, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IncreasePistolShoot();
    }
}
