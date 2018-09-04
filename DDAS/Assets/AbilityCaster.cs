using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbilityCaster : MonoBehaviour
{
  [HideInInspector] public int hostileLayer;
  [HideInInspector] public int friendlyLayer;

  [SerializeField] protected List<Ability> abilities;
  protected ITargeter targeter;

  protected virtual void Awake()
  {
    hostileLayer = LayerMask.NameToLayer("Targets");
    friendlyLayer = gameObject.layer;
    targeter = GetComponent<ITargeter>();
  }
  protected virtual void Start()
  {
    HandlePassiveAbilities();
    abilities = HandleAbilities();
  }

  void HandlePassiveAbilities()
  {
    foreach (Ability ability in abilities.Where(x => x.castType == AbilityCastType.Passive))
    {
      Ability instance = Instantiate(ability);
      instance.Setup(this);
      TargetCast((TargetAbility)instance, true);
    }
  }
  List<Ability> HandleAbilities()
  {
    List<Ability> copies = new List<Ability>();
    foreach (Ability ability in abilities.Where(x => x.castType != AbilityCastType.Passive))
    {
      Ability instance = Instantiate(ability);
      instance.Setup(this);
      copies.Add(instance);
    }
    return copies;
  }

  protected abstract bool TryCast(Ability ability, bool selfCast = false);
  public abstract void TargetCast(TargetAbility ability, bool selfCast = false);
  public abstract void PointCast(PointAbility ability);
  public abstract void DirectionCast(DirectionAbility ability);

}
