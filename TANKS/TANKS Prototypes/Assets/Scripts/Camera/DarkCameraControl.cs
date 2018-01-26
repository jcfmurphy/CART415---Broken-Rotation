using UnityEngine;

public class DarkCameraControl : CameraControl
{
       
	protected override void Awake()
    {
		base.Awake ();
		m_ScreenEdgeBuffer = 9f; 
		m_MinSize = 10.5f;
    }


}