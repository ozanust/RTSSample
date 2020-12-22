using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoManager<InputService>
{
	public Action OnMouseUp;
	public Action OnRightMouseUp;
	public Action OnMouseHeld;
	public Action<float> MouseHeldDuration;

	private float mouseHeldTimer = 0f;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonUp(0))
		{
			OnMouseUp?.Invoke();
			if(mouseHeldTimer > Constants.EPSILON)
			{
				MouseHeldDuration?.Invoke(mouseHeldTimer);
				mouseHeldTimer = 0;
			}
		}

		if (Input.GetMouseButtonUp(1))
		{
			OnRightMouseUp?.Invoke();
		}

		if (Input.GetMouseButton(0))
		{
			mouseHeldTimer += Time.deltaTime;
			OnMouseHeld?.Invoke();
		}
    }
}
