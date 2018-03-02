using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour {

	private Quaternion m_TargetRotation;
	private float m_RotateSpeed = 10f;
	private Transform m_Transform;


	// Use this for initialization
	void Start () {
		m_Transform = this.gameObject.GetComponent<Transform> ();
		m_TargetRotation = Quaternion.LookRotation(Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.forward);
		} else if (Input.GetKeyDown ("2")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.left);
		} else if (Input.GetKeyDown ("3")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.back);
		} else if (Input.GetKeyDown ("4")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.right);
		} else if (Input.GetKeyDown ("5")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.up);
		} else if (Input.GetKeyDown ("6")) {
			m_TargetRotation = Quaternion.LookRotation(Vector3.down);
		}
	}

	void FixedUpdate() {
		m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, m_TargetRotation, Time.deltaTime * m_RotateSpeed);
	}
}
