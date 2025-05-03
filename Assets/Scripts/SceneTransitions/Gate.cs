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
			Debug.Log("Monster in");
			hasMonster = true;
		}
		else if (other.tag == "Oracle")
		{
			Debug.Log("Oracle in");
			hasOracle = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Monster")
		{
			Debug.Log("Monster out");
			hasMonster = false;
		}
		else if (other.tag == "Oracle")
		{
			Debug.Log("Oracle out");
			hasOracle = false;
		}
	}

}
