using UnityEngine;

public abstract class AbilityRequirement : ScriptableObject
{
  public abstract bool Test(AbilityCaster caster, Ability ability, GameObject target, bool useSelfCast = false);
}
