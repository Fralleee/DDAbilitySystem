using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

  protected override bool TryCast(Ability ability, bool selfCast = false)
  {
    bool foundTarget = targeter.FindTarget(ability);
    return ability.Test(targeter.currentTarget, selfCast);
  }

  public override void TargetCast(TargetAbility ability, bool selfCast = false)
  {
    if (selfCast)
    {
      Instantiate(ability.prefab, transform.position, Quaternion.identity, transform);
      return;
    }
    GameObject instance = Instantiate(ability.prefab, targeter.currentTarget.transform.position, Quaternion.identity);
  }

  public override void PointCast(PointAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, targeter.currentTarget.transform.position, Quaternion.identity);
  }

  public override void DirectionCast(DirectionAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward, Quaternion.identity);
  }
}
