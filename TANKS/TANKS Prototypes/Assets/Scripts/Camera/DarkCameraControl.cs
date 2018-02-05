using UnityEngine;

public class DarkCameraControl : CameraControl
{

	private void Update() {
		transform.Rotate (Vector3.up * Time.deltaTime * 10.0f, Space.World);
	}
       
	protected override void Awake()
    {
		base.Awake ();
		m_ScreenEdgeBuffer = 9f; 
		m_MinSize = 10.5f;
    }


}