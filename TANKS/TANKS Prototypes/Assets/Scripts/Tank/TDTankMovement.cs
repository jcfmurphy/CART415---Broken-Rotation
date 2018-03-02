using UnityEngine;

public class TDTankMovement : TankMovement
{

	private Transform m_Transform;

	protected override void Start()
	{
		base.Start ();

		m_Transform = GetComponent<Transform> ();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate ();

		m_Rigidbody.AddForce (m_Transform.up * -9.8f);

		m_Rigidbody.angularVelocity = Vector3.zero;
	}

}