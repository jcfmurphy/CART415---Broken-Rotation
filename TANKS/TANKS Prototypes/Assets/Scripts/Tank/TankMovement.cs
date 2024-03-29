using UnityEngine;

public class TankMovement : MonoBehaviour
{
	public int m_PlayerNumber = 1;         
	public float m_Speed = 12f;            
	public float m_TurnSpeed = 180f;       
	public AudioSource m_MovementAudio;    
	public AudioClip m_EngineIdling;       
	public AudioClip m_EngineDriving;      
	public float m_PitchRange = 0.2f;
	public ParticleSystem m_LeftDustTrail;
	public ParticleSystem m_RightDustTrail;


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


	protected virtual void OnDisable ()
	{
		m_Rigidbody.isKinematic = true;
	}


	protected virtual void Start()
	{
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
		m_TurnAxisName = "Horizontal" + m_PlayerNumber;

		m_OriginalPitch = m_MovementAudio.pitch;
	}


	protected virtual void Update()
	{
		// Store the player's input and make sure the audio for the engine is playing.

		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		EngineAudio ();

		//update dust trail particle systems
		if (Mathf.Abs (m_MovementInputValue) > 0.1f || Mathf.Abs (m_TurnInputValue) > 0.1f) {
			UpdateParticleSystem (m_LeftDustTrail, true);
			UpdateParticleSystem (m_RightDustTrail, true);
		} else {
			UpdateParticleSystem (m_LeftDustTrail, false);
			UpdateParticleSystem (m_RightDustTrail, false);
		}

	}


	protected virtual void EngineAudio()
	{
		// Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

		if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f) {
			if (m_MovementAudio.clip == m_EngineDriving) {
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		} else {
			if (m_MovementAudio.clip == m_EngineIdling) {
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
	}


	protected virtual void FixedUpdate()
	{
		// Move and turn the tank.

		Move();
		Turn();
	}


	private void Move()
	{
		// Adjust the position of the tank based on the player's input.

		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}


	private void Turn()
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