using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolIceBuff : Skill
{
    private static int BaseSkillId = 3;
    public PistolIceBuff() :
    base(
        SkillType.PistolOnHit,
        "Ice",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(0, 2, 0, 1, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IceWeaponBuff();
    }
}
