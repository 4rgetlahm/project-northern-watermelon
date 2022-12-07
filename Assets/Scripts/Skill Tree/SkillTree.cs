using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillTree
{
    private static List<Skill> Skills = new List<Skill>();

    public static void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public static void RemoveSkill(Skill skill)
    {
        Skills.Remove(skill);
    }

    public static bool HasSkill(Skill skill)
    {
        return Skills.Contains(skill);
    }

    public static List<Skill> GetSkills()
    {
        return Skills;
    }

    public static void RemoveAllSkills()
    {
        Skills = new List<Skill>();
    }

    public static List<IWeaponBuff> GetWeaponBuffs(SkillType skillType)
    {
        List<IWeaponBuff> weaponBuffs = new List<IWeaponBuff>();
        foreach (Skill skill in Skills)
        {
            if (skill.SkillType == skillType)
            {
                weaponBuffs.Add(skill.WeaponBuff);
            }
        }
        return weaponBuffs;
    }
}
