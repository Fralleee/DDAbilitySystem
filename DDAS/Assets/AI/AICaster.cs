using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICaster : AbilityCaster
{
  float nextCast;
  float castRate = 2f;
  Dictionary<int, float> nextCasts = new Dictionary<int, float>();

  protected override void Start()
  {
    base.Start();
    for (int i = 0; i < abilities.Count; i++)
    {
      nextCasts.Add(i, 0f);
    }
  }

  void Update()
  {
    for (int i = 0; i < abilities.Count; i++)
    {
      if (Time.time > nextCasts[i])
      {
        nextCasts[i] = Time.time + castRate;
        bool result = TryCast(abilities[i]);
        if (result)
        {
          abilities[i].Cast();
          break;
        }
      }
    }
  }

  public bool TryCast(Ability ability)
  {
    return targeter.FindTarget(ability);
  }

  public override void StopChannel()
  {
    Destroy(channel);
    channel = null;
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
    Vector3 direction = (targeter.currentTarget.transform.position - transform.position).normalized;
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward, Quaternion.identity);
    instance.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
  }
}
