using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour 
{
	[SerializeField] private UnityEvent _activationEvent;

	// The tags of every possible thing that can activate this
	[SerializeField] private string[] activatorTags;

	private void OnTriggerEnter2D(Collider2D other)
	{
		foreach (string tag in activatorTags)
		{
			if (other.tag == tag)
			{
				_activationEvent?.Invoke();
				break;
			}
		}
	}
}
