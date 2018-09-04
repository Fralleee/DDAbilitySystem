using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCaster : AbilityCaster
{
  void Update()
  {
    CastAbilities();
  }

  void CastAbilities()
  {
    for (int i = 0; i < abilities.Count; i++)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1 + i)) abilities[i].Cast(Input.GetKey(KeyCode.LeftAlt));
      if (Input.GetKeyUp(KeyCode.Alpha1 + i)) abilities[i].StopCast();
    }
  }

  public override void SelfCast(TargetAbility ability, bool parent = false)
  {
    if (parent) Instantiate(ability.prefab, transform.position, Quaternion.identity, transform);
    else
    {
      GameObject instance = Instantiate(ability.prefab, transform.position, Quaternion.identity);
      Destroy(instance, 1f);
    }
  }

  public override void TargetCast(TargetAbility ability)
  {
    if (!targeter.currentTarget) return;
    if (ability.targetTeam == AbilityTargetTeam.Friendly && targeter.currentTarget.layer != friendlyLayer) return;
    if (ability.targetTeam == AbilityTargetTeam.Enemy && targeter.currentTarget.layer != hostileLayer) return;
    GameObject instance = Instantiate(ability.prefab, targeter.currentTarget.transform.position, Quaternion.identity);
    Destroy(instance, 1f);
  }

  public override void DirectionCast(DirectionAbility ability)
  {
    if (ability.castType == AbilityCastType.Active) DirectionCastActive(ability);
    if (ability.castType == AbilityCastType.Channel) DirectionCastChannel(ability);

  }

  public override void StopChannel()
  {
    Destroy(channel);
    channel = null;
  }

  public void DirectionCastChannel(DirectionAbility ability)
  {
    channel = Instantiate(ability.prefab, transform.position + transform.forward, transform.rotation, transform);
  }

  public void DirectionCastActive(DirectionAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward, Quaternion.identity);
    instance.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
  }

}
