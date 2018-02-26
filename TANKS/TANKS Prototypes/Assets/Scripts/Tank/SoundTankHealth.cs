using UnityEngine;
using UnityEngine.UI;

public class SoundTankHealth : TankHealth
{
	private bool m_InContact;

	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.gameObject.name == "Sound King Tank(Clone)") {
			m_InContact = true;
		}
	}

	void OnCollisionExit(Collision collisionInfo) {
		if (collisionInfo.gameObject.name == "Sound King Tank(Clone)") {
			m_InContact = false;
		}
	}

	public void Update() {
		if (m_InContact) {
			float damage = Time.deltaTime * 50;
			TakeDamage (damage);
		}
	}
}