using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTarget : MonoBehaviour
{
  [SerializeField] Color selectedColor;
  new Renderer renderer;
  Color defaultColor;

  void Awake()
  {
    renderer = GetComponent<Renderer>();
    defaultColor = renderer.material.color;
  }

  public void SetAsTarget() { renderer.material.color = selectedColor; }
  public void SetDefault() { renderer.material.color = defaultColor; }
}
