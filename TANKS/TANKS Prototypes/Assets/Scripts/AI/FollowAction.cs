using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Follow")]
public class FollowAction : Action {

	[HideInInspector] public Transform followTarget;

	private void Start()
	{
		GameObject followObject = GameObject.Find ("Sound King Tank");
		followTarget = followObject.transform;
	}

	public override void Act (StateController controller)
	{
		Follow (controller);
	}

	private void Follow (StateController controller) {
		controller.navMeshAgent.destination = followTarget.position;
		controller.navMeshAgent.isStopped = false;
	}
}
