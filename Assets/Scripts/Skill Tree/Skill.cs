using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    PistolPassive,
    PistolOnHit,
    AssaultRiflePassive,
    AssaultRifleOnHit
}

public class Skill : MonoBehaviour
{
    public int Id { get; protected set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SkillCost Cost { get; set; }
    public IWeaponBuff WeaponBuff { get; set; }
    public List<Skill> Prerequisites = new List<Skill>();
    public SkillType SkillType { get; set; }
    public Skill(SkillType skillType, string name, string description, SkillCost cost, IWeaponBuff weaponBuff = null)
    {
        SkillType = skillType;
        Name = name;
        Description = description;
        Cost = cost;
        WeaponBuff = weaponBuff;
    }

    public void AddPrerequisite(Skill skill)
    {
        Prerequisites.Add(skill);
    }

    public void RemovePrerequisite(Skill skill)
    {
        Prerequisites.Remove(skill);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        if (!(obj is Skill))
        {
            return false;
        }
        Skill compare = (Skill)obj;
        return Id == compare.Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
