using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PointAbility : Ability
{
  public GameObject prefab;
  public override void Cast(bool selfCast = false)
  {
    if (owner) owner.PointCast(this);
    else Debug.LogWarning("No owner on Ability: " + name);
  }
}
