﻿using UnityEngine;
using System;
using System.Collections;

[Serializable]
// Persistent object IS A game object, therefore inherits from it.
public class SCR_PersistentObject : MonoBehaviour
{

	// Attributes.
	private PrimitiveType primitiveType;	// Accessing what primitive type we are using.
	[SerializeField]	private int id;		// Accessing the ID number of the object.

	// Getters/Setters.
	public Transform ObjectTransform
	{
		get { return transform; }
	}

	// This will allow us to get/set the primitive type of the game object.
	public PrimitiveType ObjectType
	{
		get { return primitiveType; }
		set { primitiveType = value; }
	}

	// This will allow us to get/set the current ID number.
	public int ID
	{
		get { return id; }
		set { id = value; }
	}

}
