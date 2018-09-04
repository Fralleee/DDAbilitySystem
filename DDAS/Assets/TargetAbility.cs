using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TargetAbility : Ability
{
  public GameObject prefab;
  public override void Setup(AbilityCaster caster)
  {
    base.Setup(caster);
  }

  public override void Cast(bool selfCast = false)
  {
    if (owner)
    {
      if (selfCast) owner.SelfCast(this);
      else owner.TargetCast(this);
    }
    else Debug.LogWarning("No owner on Ability: " + name);
  }

  public override void StopCast()
  {
    if (castType == AbilityCastType.Channel)
    {
      owner.StopChannel();
    }
  }

  //public override bool Test(GameObject target)
  //{
  //  if (!target) return false;
  //  if (targetTeam == AbilityTargetTeam.Friendly && target.layer == owner.friendlyLayer) return true;
  //  if (targetTeam == AbilityTargetTeam.Enemy && target.layer == owner.hostileLayer) return true;
  //  return false;
  //}
}
