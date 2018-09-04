using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbilityCaster : MonoBehaviour
{
  public int hostileLayer;
  public int friendlyLayer;
  [SerializeField] protected List<Ability> abilities;
  protected ITargeter targeter;
  protected GameObject channel;

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
      SelfCast((TargetAbility)instance, true);
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

  public abstract void SelfCast(TargetAbility ability, bool parent = false);
  public abstract void TargetCast(TargetAbility ability);
  public abstract void DirectionCast(DirectionAbility ability);
  public abstract void StopChannel();

}
