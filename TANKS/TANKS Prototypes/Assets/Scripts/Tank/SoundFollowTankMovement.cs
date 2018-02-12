using UnityEngine;

public class SoundFollowTankMovement : SoundTankMovement
{
	Transform m_PlayerTransform;

	protected override void Start()
	{
		UpdateParticleSystem (m_LeftDustTrail, true);
		UpdateParticleSystem (m_RightDustTrail, true);

		GameObject kingTank = GameObject.Find ("King Sound Tank");

		m_PlayerTransform = kingTank.transform;
	}


	protected override void Update()
	{
		ProcSound.engineOn = CheckEngine ();


	}

	protected override void Move()
	{
		// Adjust the position of the tank based on the player's position.

		Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}


	protected override void Turn()
	{
		// Adjust the rotation of the tank based on the player's position.

		transform.LookAt (m_PlayerTransform);
	}
		
}