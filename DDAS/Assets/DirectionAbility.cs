using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DirectionAbility : Ability
{
  public GameObject prefab;
  public override void Setup(AbilityCaster caster)
  {
    base.Setup(caster);
  }

  public override void Cast(bool selfCast = false)
  {
    owner.DirectionCast(this);
  }

  public override void StopCast()
  {
    if(castType == AbilityCastType.Channel)
    {
      owner.StopChannel();
    }
  }

}
