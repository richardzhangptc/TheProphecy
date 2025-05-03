using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

	private bool hasMonster;
	public bool HasMonster
	{
		get { return hasMonster; }
	}

	private bool hasOracle;
	public bool HasOracle
	{
		get { return hasOracle; }
	}

	void Start()
	{
		hasOracle = false;
		hasMonster = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Monster")
		{
			hasMonster = true;
		}
		else if (other.tag == "Oracle")
		{
			hasOracle = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Monster")
		{
			hasMonster = false;
		}
		else if (other.tag == "Oracle")
		{
			hasOracle = false;
		}
	}

}
