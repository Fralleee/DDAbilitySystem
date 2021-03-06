﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITargeter : MonoBehaviour, ITargeter
{
  AbilityCaster abilityCaster;
  public GameObject currentTarget { get; set; }
  public GameObject mainTarget { get; set; }
  public GameObject objective { get; set; }
  public string targetName;

  void Awake()
  {
    abilityCaster = GetComponent<AbilityCaster>();
  }

  void Update()
  {
    RotateTowardsTarget();
  }

  void RotateTowardsTarget()
  {
    if (currentTarget)
    {
      Vector3 targetPostition = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
      transform.LookAt(targetPostition);
    }
  }

  public bool FindTarget(Ability ability)
  {
    currentTarget = null;
    targetName = "Mr Nobody";
    LayerMask targetLayer = ability.targetTeam == AbilityTargetTeam.Enemy ? 1 << abilityCaster.hostileLayer : 1 << abilityCaster.friendlyLayer;
    Collider[] colliders = Physics.OverlapSphere(transform.position, ability.range, targetLayer);
    if (colliders.Length > 0)
    {
      Collider col = colliders.FirstOrDefault(x => ability.Test(RequirementType.Target, x.gameObject));
      if (col)
      {
        GameObject target = col.gameObject;
        currentTarget = target;
        targetName = target.name;
        RotateTowardsTarget();
        return true;
      }
    }
    return false;
  }
}
