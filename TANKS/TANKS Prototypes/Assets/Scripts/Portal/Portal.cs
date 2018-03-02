using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Portal m_LinkedPortal;
	public GameObject m_Cube;

	private Transform m_Transform;
	private Transform m_LinkedTransform;


	// Use this for initialization
	void Start () {
		m_Transform = this.gameObject.transform;
		m_LinkedTransform = m_LinkedPortal.gameObject.transform;
	}

	void OnTriggerEnter (Collider tankCollider) {

		if (tankCollider.gameObject.tag == "PortalCollider") {

			WarpedBool hasWarped = tankCollider.GetComponent<WarpedBool> ();

			if (!hasWarped.GetWarped ()) {

				Transform tankTransform = tankCollider.gameObject.transform.parent.transform;
		
				Vector3 positionOffset = tankTransform.position - m_Transform.position;

				positionOffset = Quaternion.AngleAxis (90f, m_Transform.up) * positionOffset;

				tankTransform.position = m_LinkedTransform.position + positionOffset;

				tankTransform.RotateAround (tankTransform.position, m_Transform.up, 90f);

				tankTransform.position += m_Transform.up;

				hasWarped.SetWarped (true);
			}
		}
	}


	void OnTriggerExit(Collider tankCollider) {

		if (tankCollider.gameObject.tag == "PortalCollider") {

			WarpedBool hasWarped = tankCollider.GetComponent<WarpedBool> ();

			hasWarped.SetWarped (false);
		}
	}
}
