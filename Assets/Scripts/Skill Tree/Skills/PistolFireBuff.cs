using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFireBuff : Skill
{
    private static int BaseSkillId = 1;
    public PistolFireBuff() :
    base(
        SkillType.PistolOnHit,
        "Fire",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(0, 0, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new BleedWeaponBuff();
    }
}
