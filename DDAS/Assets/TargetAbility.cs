using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Target")]
public class TargetAbility : Ability
{
  public GameObject prefab;
  public override void Cast(bool selfCast = false)
  {
    if (owner) owner.TargetCast(this, selfCast);
    else Debug.LogWarning("No owner on Ability: " + name);
  }
}
