using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent navAgent;
	[SerializeField]
	private GameObject selectionIndicator;

	private Camera playerCam;

	public NavMeshAgent NavAgent => navAgent;

	public Action OnDestinationReached;

	private void Start()
	{
		playerCam = Camera.main;
	}

	public void GoTo(Vector3 destination)
	{
		navAgent.SetDestination(destination);
		navAgent.speed = 10;
	}

	public void Select(bool isSelected)
	{
		selectionIndicator.SetActive(isSelected);
	}

	private void LateUpdate()
	{
		if(selectionIndicator.activeInHierarchy)
			selectionIndicator.transform.LookAt(playerCam.transform);
	}

	private void FixedUpdate()
	{
		if (navAgent.speed > 0)
		{
			if (Vector3.Distance(transform.position, navAgent.destination) < 0.01f)
			{
				OnDestinationReached?.Invoke();
			}
		}
	}
}
