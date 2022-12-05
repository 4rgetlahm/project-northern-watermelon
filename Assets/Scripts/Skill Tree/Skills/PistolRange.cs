using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRange : Skill
{
    private static int BaseSkillId = 5;
    public PistolRange() :
    base(
        SkillType.PistolOnHit,
        "Range",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(10, 0, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new IncreasedProjectilesRange();
    }
}
