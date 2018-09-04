using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
  GameObject currentTarget { get; set; }
  GameObject mainTarget { get; set; }
  GameObject objective { get; set; }
  bool FindTarget(Ability ability);
}
