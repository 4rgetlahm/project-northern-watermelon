using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBleedMkTwo : Skill
{
    private static int BaseSkillId = 2;
    public PistolBleedMkTwo() :
    base(
        SkillType.PistolOnHit,
        "Pistol Bleed Mk. Two",
        "Bleed enemies with your pistol. 1 Additional Damage per second for 3 seconds.",
        new SkillCost(1, 0, 0, 0, 0))
    {
        WeaponBuff = new BleedWeaponBuff();
        this.AddPrerequisite(new PistolBleedMkOne());
        Id = BaseSkillId;
    }
}
