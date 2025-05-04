using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

	private bool isLocked;
	private Animator myAnim;
	
	[SerializeField] private Sprite lockedSprite;
	[SerializeField] private Sprite unLockedSprite;
	private SpriteRenderer spriteRenderer;

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
		myAnim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			spriteRenderer.sprite = lockedSprite;
		}
		isLocked = true;
		hasOracle = false;
		hasMonster = false;
		GetComponent<BoxCollider2D>().isTrigger = false;
	}

	public void Unlock()
	{
		if (spriteRenderer != null && unLockedSprite != null)
		{
			spriteRenderer.sprite = unLockedSprite;
			Debug.Log("OPEN");
		}
		GetComponent<BoxCollider2D>().isTrigger = true;
		isLocked = false;
	}

	public void Lock()
	{
		if (spriteRenderer != null && lockedSprite != null)
		{
			spriteRenderer.sprite = lockedSprite;
		}
		GetComponent<BoxCollider2D>().isTrigger = false;
		isLocked = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!isLocked)
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
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (!isLocked)
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

}
