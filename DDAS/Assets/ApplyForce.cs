using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
  [SerializeField] float force = 10f;
	void Start ()
  {
    GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
  }
}
