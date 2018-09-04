using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : MonoBehaviour
{
  [SerializeField] float movementModifier = 0.25f;
  IMotor motor;
  void Awake()
  {
    motor = GetComponentInParent<IMotor>();
  }

  void Start()
  {
    if(motor != null) motor.ApplyModifier(movementModifier);
  }

}
