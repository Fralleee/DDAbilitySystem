using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour, IMotor
{
  public bool useGravity = true;
  public float movementSpeed = 1f;

  public float fallMultiplier = 2.5f;
  public float jumpPower = 50f;
  CharacterController controller;
  Vector3 movement;
  float vSpeed;
  float currentX;
  float movementSpeedModifier = 1f;

  void Start()
  {
    controller = GetComponent<CharacterController>();
  }

  void Update()
  {
    currentX += Input.GetAxis("Mouse X");

    movement = Vector3.zero;
    vSpeed += Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    movement.y = vSpeed;
    controller.Move(movement * Time.deltaTime);

    float movementVertical = Input.GetAxisRaw("Vertical");
    float movementHorizontal = Input.GetAxisRaw("Horizontal");
    float movementMultiplier = movementVertical < 0 ? 0.5f : 1;

    movement = new Vector3(movementHorizontal, 0, movementVertical) * movementMultiplier;
    movement = transform.TransformDirection(movement);
    movement *= movementSpeed * movementSpeedModifier;
    movement = Vector3.ClampMagnitude(movement, movementSpeed * movementSpeedModifier);

    if (controller.isGrounded)
    {
      vSpeed = -1;
      if (Input.GetButtonDown("Jump")) vSpeed = jumpPower;
    }

    if (useGravity) vSpeed += Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    movement.y = vSpeed;
    controller.Move(movement * Time.deltaTime);
    Quaternion rotation = Quaternion.Euler(0, currentX, 0);
    transform.rotation = rotation;
  }

  public void ApplyModifier(float value)
  {
    movementSpeedModifier += value;
  }

}
