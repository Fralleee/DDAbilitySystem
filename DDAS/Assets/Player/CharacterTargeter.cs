using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterTargeter : MonoBehaviour, ITargeter
{
  AbilityCaster abilityCaster;
  public GameObject currentTarget { get; set; }
  public GameObject mainTarget { get; set; }
  public GameObject objective { get; set; }

  void Awake()
  {
    abilityCaster = GetComponent<AbilityCaster>();
  }

  void Update()
  {
    TargetSetter();
  }

  void TargetSetter()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      GameObject target = null;
      RaycastHit hit;
      Ray ray = new Ray(transform.position, transform.forward);
      if (Physics.Raycast(ray, out hit, 24f, 1 << abilityCaster.hostileLayer | 1 << gameObject.layer))
      {
        target = hit.transform.gameObject;
        SetAsCurrentTarget(target);
      }
    }
  }

  void SetAsCurrentTarget(GameObject target)
  {
    if (currentTarget) currentTarget.GetComponent<CurrentTarget>().SetDefault();
    currentTarget = target;
    currentTarget.GetComponent<CurrentTarget>().SetAsTarget();
  }

  public bool FindTarget(Ability ability)
  {
    return false;
  }
}
