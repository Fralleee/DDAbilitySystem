using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
  [SerializeField] Text attackersText;
  [SerializeField] Text targetsText;

  void Awake()
  {
    var attackers = GameObject.FindGameObjectsWithTag("Caster");
    var targets = GameObject.FindGameObjectsWithTag("Target");
    attackersText.text = "Attackers: " + attackers.Length;
    targetsText.text = "Targets: " + targets.Length;
  }
}
