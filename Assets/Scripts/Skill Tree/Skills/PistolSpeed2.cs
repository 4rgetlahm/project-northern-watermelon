using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolSpeed2 : Skill
{
    private static int BaseSkillId = 6;
    public PistolSpeed2() :
    base(
        SkillType.PistolPassive,
        "Speed2",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(10, 1, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IncreasePistolShoot();
    }
}
