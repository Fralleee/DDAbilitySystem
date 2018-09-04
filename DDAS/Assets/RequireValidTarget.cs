using UnityEngine;

[CreateAssetMenu]
public class RequireValidTarget : AbilityRequirement
{
  public override bool Test(AbilityCaster caster, Ability ability, GameObject target, bool useSelfCast = false)
  {
    if (useSelfCast && ability.targetTeam == AbilityTargetTeam.Friendly) return true;
    if (!target || ability.targetLayer != target.layer) return false;
    return true;
  }
}
