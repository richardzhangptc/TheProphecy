using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateRelay : MonoBehaviour
{
    [SerializeField] private List<PressurePlate> listOfPressurePlates = new List<PressurePlate>();
	[SerializeField] private UnityEvent _relayActivation;
	[SerializeField] private UnityEvent _relayDeactivation;
    private bool active = false;

    private bool allTriggered = false;

    private void Update()
    {
        allTriggered = true;
        foreach (PressurePlate pp in listOfPressurePlates)
        {
            if (pp.pstate == PressurePlate.PlateState.Released)
            {
                allTriggered = false;
            }
        }

        if (allTriggered == true)
        {
            active = true;
            _relayActivation?.Invoke();
        }
        else
        {
            _relayDeactivation?.Invoke();
            active = false;
        }
    }
}
