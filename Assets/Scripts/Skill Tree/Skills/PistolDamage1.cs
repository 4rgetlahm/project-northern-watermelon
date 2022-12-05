using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDamage1 : Skill
{
    private static int BaseSkillId = 4;
    public PistolDamage1() :
    base(
        SkillType.PistolOnHit,
        "dmg1",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(10, 0, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IncreasePistolDamage();
    }
}
