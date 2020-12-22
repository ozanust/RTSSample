using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
	private Unit selectedUnit;
	private InputService inputService;
	private Camera playerCam;
	private RaycastHit hit;

	private void Start()
	{
		inputService = InputService.Instance;
		playerCam = Camera.main;

		inputService.OnMouseUp += OnSelection;
		inputService.OnRightMouseUp += OnAction;
	}

	private void OnSelection()
	{
		Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 1000))
		{
			if(hit.collider.gameObject.tag == "Unit")
			{
				if (selectedUnit != null)
					selectedUnit.Select(false);

				selectedUnit = hit.collider.gameObject.GetComponent<Unit>();
				selectedUnit.Select(true);
			}
			else
			{
				if(selectedUnit != null)
				{
					selectedUnit.Select(false);
					selectedUnit = null;
				}
			}
		}
	}

	private void OnAction()
	{
		if (selectedUnit == null)
			return;

		Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.gameObject.tag == "Ground")
			{
				selectedUnit.GoTo(hit.point);
			}
		}
	}
}
