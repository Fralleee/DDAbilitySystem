using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICaster : AbilityCaster
{
  float nextCast;
  float castRate = 2f;
  Dictionary<int, float> nextCasts = new Dictionary<int, float>();
  [SerializeField] AbilityRequirement[] requirements;

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
    if (foundTarget && requirements.All(x => x.Test(this, ability, targeter.currentTarget, selfCast)))
    {
      bool result = ability.Test(RequirementType.Casting, targeter.currentTarget, selfCast);
      return result;
    }
    return false;
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
    Vector3 position = targeter.currentTarget.transform.position;
    GameObject instance = Instantiate(ability.prefab, new Vector3(position.x, 0, position.z), Quaternion.identity);
  }

  public override void DirectionCast(DirectionAbility ability)
  {
    GameObject instance = Instantiate(ability.prefab, transform.position + transform.forward, transform.rotation);
  }
}
