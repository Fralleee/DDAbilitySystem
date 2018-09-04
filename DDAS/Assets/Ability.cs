using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityTargetTeam { Enemy, Friendly }
public enum AbilityCastType { Active, Passive, Toggle, Channel }
public enum AbilityTargetType { Target, Direction, Point }

public abstract class Ability : ScriptableObject
{
  public AbilityTargetTeam targetTeam = AbilityTargetTeam.Enemy;
  public AbilityCastType castType = AbilityCastType.Active;
  public AbilityTargetType targetType = AbilityTargetType.Target;
  public AbilityRequirement[] requirements;
  public Image image;
  public float range = 15f;

  protected float nextTest;
  protected float testRate;

  [HideInInspector] public AbilityCaster owner;
  [HideInInspector] public int environmentLayer;
  [HideInInspector] public int targetLayer;

  public void Setup(AbilityCaster caster)
  {
    owner = caster;
    environmentLayer = LayerMask.NameToLayer("Environment");
    targetLayer = targetTeam == AbilityTargetTeam.Enemy ? owner.hostileLayer : owner.friendlyLayer;
  }
  public abstract void Cast(bool selfCast = false);
  public bool Test(GameObject target, bool selfCast = false)
  {
    return requirements.All(x => x.Test(owner, this, target, selfCast));
  }
}
