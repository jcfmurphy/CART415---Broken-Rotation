using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour {

	private float m_YRotateValue;
	private float m_XRotateValue;
	private float m_RotateSpeed = 180f;
	private Transform m_Transform;


	// Use this for initialization
	void Start () {
		m_Transform = this.gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_XRotateValue = Input.GetAxis ("Vertical2");
		m_YRotateValue = Input.GetAxis ("Horizontal2");

		float xTurn = m_XRotateValue * m_RotateSpeed * Time.deltaTime;
		float yTurn = m_YRotateValue * m_RotateSpeed * Time.deltaTime;

		Vector3 turnRotation = new Vector3 (xTurn, yTurn, 0f);

		m_Transform.Rotate (turnRotation, Space.World);
	}
}
