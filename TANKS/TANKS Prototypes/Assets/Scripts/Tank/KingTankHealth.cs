using UnityEngine;
using UnityEngine.UI;

public class KingTankHealth : TankHealth
{

	public GameObject m_Crown;

	private float[] m_samples = new float[1024];
	private GameObject[] m_SoundTanks;
	private float m_LowVolCutoff = -25f;
	private float m_HighVolCutoff = 0f;
	private float m_DamageMultiplier = 1.6f;

	public void start() {

	}

	public void Update() {
		m_SoundTanks = GameObject.FindGameObjectsWithTag ("SoundTanks");

		float damage = 0;

		foreach (GameObject soundTank in m_SoundTanks) {
			float tempVolume = GetVolume (soundTank);
			damage += GetDamage (tempVolume);
		}

		TakeDamage (damage);
		print (damage);
	}


	public float GetVolume(GameObject soundTank) {
		AudioSource tempAudio = soundTank.GetComponent<AudioSource> ();
		tempAudio.GetOutputData (m_samples, 0);
		float sqrsum = 0;

		for (int i = 0; i < m_samples.Length; i++) {
			sqrsum += m_samples [i] * m_samples [i];
		}

		float rms = Mathf.Sqrt (sqrsum / 1024);
		float dbv = 20f * Mathf.Log10 (rms / 0.1f);

		return dbv;
	}


	public float GetDamage(float tempVolume) {
		if (tempVolume <= m_LowVolCutoff) {
			return 0f;
		} else if (tempVolume <= m_HighVolCutoff) {
			float converted = (tempVolume - m_LowVolCutoff) / (m_HighVolCutoff - m_LowVolCutoff) * Time.deltaTime * m_DamageMultiplier;
			return converted;
		} else {
			return Time.deltaTime;
		}
	}


	protected override void OnDeath ()
	{
		m_Crown.transform.parent = null;
		Rigidbody tempRigid = m_Crown.GetComponent<Rigidbody> ();
		tempRigid.useGravity = true;
		tempRigid.AddTorque (Vector3.forward * 50f);

		base.OnDeath ();
	}
}