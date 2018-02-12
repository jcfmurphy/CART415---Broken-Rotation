using UnityEngine;

public class SoundTankMovement : MonoBehaviour
{
	public int m_PlayerNumber = 1;         
	public float m_Speed = 12f;            
	public float m_TurnSpeed = 180f;       
	public float m_PitchRange = 0.2f;
	public ParticleSystem m_LeftDustTrail;
	public ParticleSystem m_RightDustTrail;
	public ProcSoundTank ProcSound;


	protected string m_MovementAxisName;     
	protected string m_TurnAxisName;         
	protected Rigidbody m_Rigidbody;
	protected float m_MovementInputValue;    
	protected float m_TurnInputValue;        
	protected float m_OriginalPitch;         


	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}


	private void OnEnable ()
	{
		m_Rigidbody.isKinematic = false;
		m_MovementInputValue = 0f;
		m_TurnInputValue = 0f;
	}


	private void OnDisable ()
	{
		m_Rigidbody.isKinematic = true;
		ProcSound.Reset ();
	}


	protected virtual void Start()
	{
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
		m_TurnAxisName = "Horizontal" + m_PlayerNumber;

	}


	protected virtual void Update()
	{
		// Store the player's input and make sure the audio for the engine is playing.

		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		ProcSound.engineOn = CheckEngine ();

		//update dust trail particle systems
		if (Mathf.Abs (m_MovementInputValue) > 0.1f || Mathf.Abs (m_TurnInputValue) > 0.1f) {
			UpdateParticleSystem (m_LeftDustTrail, true);
			UpdateParticleSystem (m_RightDustTrail, true);
		} else {
			UpdateParticleSystem (m_LeftDustTrail, false);
			UpdateParticleSystem (m_RightDustTrail, false);
		}

	}


	protected bool CheckEngine()
	{
		// Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

		if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f) {
			return false;
		} else {
			return true;
		}
	}


	private void FixedUpdate()
	{
		// Move and turn the tank.

		Move();
		Turn();
	}


	protected virtual void Move()
	{
		// Adjust the position of the tank based on the player's input.

		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}


	protected virtual void Turn()
	{
		// Adjust the rotation of the tank based on the player's input.

		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}

	protected void UpdateParticleSystem (ParticleSystem particleSystem, bool shouldBePlaying) {
		if (particleSystem.isPlaying && !shouldBePlaying) {
			particleSystem.Stop ();
		} else if (!particleSystem.isPlaying && shouldBePlaying) {
			particleSystem.Play ();
		}
	}
}