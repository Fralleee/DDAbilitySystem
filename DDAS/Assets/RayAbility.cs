using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAbility : MonoBehaviour
{
  public LineRenderer laserLineRenderer;
  public float laserWidth = 0.1f;
  public float laserMaxLength = 5f;

  void Start()
  {
    Vector3[] initLaserPositions = { Vector3.zero, Vector3.zero };
    laserLineRenderer.SetPositions(initLaserPositions);
    laserLineRenderer.startWidth = laserWidth;
    laserLineRenderer.endWidth = laserWidth;
  }

  void Update()
  {
    ShootLaserFromTargetPosition(transform.position, transform.forward, laserMaxLength);
  }


  void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
  {
    Ray ray = new Ray(targetPosition, direction);
    RaycastHit raycastHit;
    Vector3 endPosition = targetPosition + (length * direction);
    if (Physics.Raycast(ray, out raycastHit, length))
    {
      endPosition = raycastHit.point;
    }
    laserLineRenderer.SetPosition(0, targetPosition);
    laserLineRenderer.SetPosition(1, endPosition);
  }
}
