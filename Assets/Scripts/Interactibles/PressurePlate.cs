using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour 
{
	[SerializeField] private UnityEvent _PressPlate;
	[SerializeField] private UnityEvent _ReleasePlate;

	// Pressure plate state
	enum PlateState {Pressed, Released};
	private PlateState pstate; 

	private int NumActivatorsOnPlate;

	private void Start()
	{
		pstate = PlateState.Released;
		NumActivatorsOnPlate = 0;
	}

	// The tags of every possible thing that can activate this
	[SerializeField] private string[] activatorTags;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (pstate == PlateState.Released)
		{
			Debug.Log(other.tag);
			foreach (string tag in activatorTags)
			{
				if (other.tag == tag)
				{
					Debug.Log("Pressed");
					_PressPlate?.Invoke();
					pstate = PlateState.Pressed;
					NumActivatorsOnPlate++;
					break;
				}
			}
		}
		else if (pstate == PlateState.Pressed)
		{
			foreach (string tag in activatorTags)
			{
				if (other.tag == tag)
				{
					NumActivatorsOnPlate++;
					break;
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (pstate == PlateState.Pressed)
		{
			Debug.Log("Thing left");
			foreach (string tag in activatorTags)
			{
				if (other.tag == tag)
				{
					Debug.Log("Activator leaves");
					NumActivatorsOnPlate--;
					break;
				}
			}

		}
	}

	/*private void OnTriggerStay2D(Collider2D other)
	{
		if (pstate == PlateState.Pressed)
		{
			Debug.Log(other.tag + " is current on pressure plate");
			foreach (string tag in activatorTags)
			{
				if (other.tag == tag)
				{
					NumActivatorsOnPlate++;
					break;
				}
			}
		}

	}*/



	// I put this in LateUpdate because we want OnTriggerStay2D to be run first. 
	private void LateUpdate()
	{
		if (pstate == PlateState.Pressed && NumActivatorsOnPlate == 0)
		{
			Debug.Log("released " + NumActivatorsOnPlate);
			_ReleasePlate?.Invoke();
			pstate = PlateState.Released;
		}

	}
}
