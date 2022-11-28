using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBleedMkOne : Skill
{
    private static int BaseSkillId = 1;
    public PistolBleedMkOne() :
    base(
        SkillType.PistolOnHit,
        "Pistol Bleed Mk. One",
        "Bleed enemies with your pistol. 1 Damage per second for 3 seconds.",
        new SkillCost(1, 0, 0, 0, 0))
    {
        Id = BaseSkillId;
        WeaponBuff = new BleedWeaponBuff();
    }
}
