using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityTargetTeam { All, Enemy, Friendly, Self, None }
public enum AbilityCastType { Active, Passive, Toggle, Channel }
public enum AbilityTargetType { Target, Direction, Location }
public enum AbilityRequirements { RequiresTarget, RequiresLineOfSight, RequiresMovement }

public abstract class Ability : ScriptableObject
{
  public AbilityTargetTeam targetTeam = AbilityTargetTeam.Enemy;
  public AbilityCastType castType = AbilityCastType.Active;
  public AbilityTargetType targetType = AbilityTargetType.Target;
  public AbilityRequirements[] requirements;
  public Image image;
  public AbilityCaster owner;
  public float range = 15f;
  public virtual void Setup(AbilityCaster caster) { owner = caster; }
  public abstract void Cast(bool selfCast = false);
  public abstract void StopCast();
}
