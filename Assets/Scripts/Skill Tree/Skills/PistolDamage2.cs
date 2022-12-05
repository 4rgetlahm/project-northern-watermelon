using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDamage2 : Skill
{
    private static int BaseSkillId = 7;
    public PistolDamage2() :
    base(
        SkillType.PistolOnHit,
        "dmg2",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(15, 1, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IncreasePistolDamage();
    }
}
