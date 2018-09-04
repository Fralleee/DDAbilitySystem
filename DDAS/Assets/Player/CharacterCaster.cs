using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCaster : AbilityCaster
{
  void Update()
  {
    for (int i = 0; i < abilities.Count; i++)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1 + i))
      {
        bool result = TryCast(abilities[i], Input.GetKey(KeyCode.LeftAlt));
        if(result) abilities[i].Cast(Input.GetKey(KeyCode.LeftAlt));
      }
    }
  }

  protected override bool TryCast(Ability ability, bool selfCast = false)
  {
    return ability.Test(targeter.currentTarget, selfCast);
  }

  public override void TargetCast(TargetAbility ability, bool selfCast)
  {
    if (selfCast)
    {
      Instantiate(ability.prefab, transform.position, Quaternion.identity, transform);
      return;
    }
    if (!targeter.currentTarget) return;
    if (ability.targetTeam == AbilityTargetTeam.Friendly && targeter.currentTarget.layer != friendlyLayer) return;
    if (ability.targetTeam == AbilityTargetTeam.Enemy && targeter.currentTarget.layer != hostileLayer) return;
    GameObject instance = Instantiate(ability.prefab, targeter.currentTarget.transform.position, Quaternion.identity);
  }

  public override void PointCast(PointAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward * ability.range, Quaternion.identity);
  }

  public override void DirectionCast(DirectionAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward, Quaternion.identity);
  }

}
