﻿/*
*
*	Scene Editor Class
*	==================
*
*	Created: 	2016/11/21
*	Class Name: SCR_SceneEditor
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	The purpose of this class is to allow for object manipulation in the scene, 
*				and also for storing all of the current scene objects.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Scene editor IS A game object, therefore inherits from it. */
public class SCR_SceneEditor : MonoBehaviour 
{

	/* Attributes. */
	[SerializeField]	private List<SCR_PersistentObject> sceneObjects = null;		/* The current list of scene objects. */

	[Header ("Object Manipulation")]
	[SerializeField]	private Vector3 translationSpeed = Vector3.zero; 			/* Used to determine how fast objects will move for translations in each axis. */
	[SerializeField]	private Vector3 scaleIncrement = Vector3.zero; 				/* Used to determine how fast objects scale in each axis. */

	private enum TransformState 													/* Defining transform states for the user. */
	{
		translation,	/* The user is translating an object. */
		rotation, 		/* The user is rotating an object. */
		scale 			/* The user is scaling an object. */
	};
	private TransformState currentTransformState = TransformState.translation;	 	/* Used to determine what transform state the user is currently in for object manipulation. */

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	private void Awake()
	{

		/* Initialising our attributes. */
		sceneObjects = new List<SCR_PersistentObject>();

	}

	/*
	*
	*	Overview
	*	--------
	*	This function will spawn in any primitive object we want at a 
	*	specified spawn location.
	*
	*	Params
	*	------
	*	PrimitiveType primtiveType 	- 	This is the current primitive type of the object
	*									that the user wishes to spawn (cube, sphere etc).
	*
	*	Vector3 spawnPosition 		- 	This is the current spawn position of the object
	*									that the user wishes to spawn.
	*
	*/
	public void SpawnObject(PrimitiveType primitiveType, Vector3 spawnPosition)
	{

		/* Creating a temporary instance of a cube game object. */
        GameObject tempGameObject = GameObject.CreatePrimitive(primitiveType);

		/* Creating a temporary instance of a persistent object and adding it onto the temporary game object. */
		SCR_PersistentObject tempPersistentObject = tempGameObject.AddComponent<SCR_PersistentObject>();
		tempPersistentObject.transform.position = spawnPosition;
		tempPersistentObject.transform.parent = GameObject.Find(name).transform;
		tempPersistentObject.ObjectType = primitiveType;
		tempPersistentObject.ID = sceneObjects.Count;

		/* Add the cube into the list of game objects. */
        sceneObjects.Add(tempPersistentObject);

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual rotation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being rotated.
	*
	*/
	private void CheckRotation(SCR_PersistentObject persistentObject)
	{}

	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual scale values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being scaled.
	*
	*/
	private void CheckScale(SCR_PersistentObject persistentObject)
	{

		/* Initialising local attributes. */
		Vector3 tempScale = persistentObject.transform.localScale;

		/* If the I key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller in front of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller in front of the left past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.I))
		{

			/* Set the value of the temporary scale attribute. */ 
			tempScale.Set(tempScale.x, tempScale.y, tempScale.z + scaleIncrement.z);

		}

		/* If the J key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller to the left of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller to the left of the left hand controller past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.J))
		{

			/* Set the value of the temporary scale attribute. */ 
			tempScale.Set(tempScale.x - scaleIncrement.x, tempScale.y, tempScale.z);

		}

		/* If the K key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller behind the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller behind of the left hand controller past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.K))
		{

			/* Set the value of the temporary scale attribute. */
			tempScale.Set(tempScale.x, tempScale.y, tempScale.z - scaleIncrement.z);

		}

		/* If the L key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller to the right of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller to the right of the left hand controller past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.L))
		{

			/* Set the value of the temporary scale attribute. */ 
			tempScale.Set(tempScale.x + scaleIncrement.x, tempScale.y, tempScale.z);

		}

		/* If the U key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller below the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller below the left hand controller past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.U))
		{

			/* Set the value of the temporary scale attribute. */ 
			tempScale.Set(tempScale.x, tempScale.y - scaleIncrement.y, tempScale.z);

		}

		/* If the O key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller above the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller above the left hand controller past Xcm will constantly increment by the scale increment attribute. */
		if(Input.GetKey(KeyCode.O))
		{

			/* Set the value of the temporary scale attribute. */ 
			tempScale.Set(tempScale.x, tempScale.y + scaleIncrement.y, tempScale.z);

		}

		/* Set the new scale of the object. */ 
		persistentObject.transform.localScale = tempScale;

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual translation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being translated.
	*
	*/
	private void CheckTranslations(SCR_PersistentObject persistentObject)
	{

		/* If the I key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller in front of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller in front of the left past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.I))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(0.0f, 0.0f, translationSpeed.z);

		}

		/* If the J key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller to the left of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller to the left of the left hand controller past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.J))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(-translationSpeed.x, 0.0f, 0.0f);

		}

		/* If the K key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller behind the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller behind of the left hand controller past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.K))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(0.0f, 0.0f, -translationSpeed.z);

		}

		/* If the L key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller to the right of the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller to the right of the left hand controller past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.L))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(translationSpeed.x, 0.0f, 0.0f);

		}

		/* If the U key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller below the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller below the left hand controller past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.U))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(0.0f, -translationSpeed.y, 0.0f);

		}

		/* If the O key has been pressed. */
		/* VR Equivalent: Holding the left hand controller trigger and right hand controller trigger and moving one controller above the other above a threshold distance. */
		/* For example, holding both triggers, and holding the right controller above the left hand controller past Xcm will constantly increment by the translation increment attribute. */
		if(Input.GetKey(KeyCode.O))
		{

			/* Set the new position of the object. */ 
			persistentObject.transform.Translate(0.0f, translationSpeed.y, 0.0f);

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will be used to check the current user transform state and 
	*	provide the correct transform manipulation response with user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being translated.
	*
	*/
	private void ManipulateObject(SCR_PersistentObject persistentObject)
	{

		/* If user is currently manipulating the position of the object. */
		if(currentTransformState == TransformState.translation)
		{

			/* Check any input for translation manipulation. */
			CheckTranslations(persistentObject);

		}
		/* Otherwise, the user is currently manipulating the rotation of the object. */
		else if(currentTransformState == TransformState.rotation)
		{

			/* Check any input for rotation manipulation. */
			CheckRotation(persistentObject);	

		}
		/* Otherwise, the user is currently manipulating the scale of the object. */
		else if(currentTransformState == TransformState.scale)
		{

			/* Check any input for scale manipulation. */
			CheckScale(persistentObject);

		}


	}

	/*
	*
	*	Overview
	*	--------
	*	This method will be responsible for handling any object manipulation
	*	in the scene, checking if the object is actually in focus first.
	*
	*/
	private void CheckForHighlightedObjects()
	{

		/* Loop through each of the scene objects.*/
		foreach(SCR_PersistentObject tempPersistentObject in sceneObjects)
		{

			/* If the current scene object is in focus (i.e. the player is aiming at this object). */
			if(tempPersistentObject.InFocus)
			{

				/* Manipulate the object based on user input. */
				ManipulateObject(tempPersistentObject);

			}

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will be responsible for checking the current transform 
	*	state of the user, allowing the user to switch what type of object
	*	manipulation they are performing.
	*
	*/
	private void CheckCurrentTransformState()
	{

		/* If the Num1 key has been pressed. */
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{

			/* If the current transform state has not been set to translation. */
			if(currentTransformState != TransformState.translation)
			{

				/* Set the current transform state to translation. */
				print("Translating.");
				currentTransformState = TransformState.translation;

			}

		}
		/* Otherwise, the Num2 key has been pressed. */
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{

			/* If the current transform state has not been set to rotation. */
			if(currentTransformState != TransformState.rotation)
			{

				/* Set the current transform state to rotation. */
				print("Rotating.");
				currentTransformState = TransformState.rotation;

			}

		}
		/* Otherwise, the Num3 key has been pressed. */
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{

			/* If the current transform state has not been set to scale. */
			if(currentTransformState != TransformState.scale)
			{

				/* Set the current transform state to scale. */
				print("Scaling.");
				currentTransformState = TransformState.scale;

			}

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	private void Update()
	{

		/* Handles updates to the user transform state. */
		CheckCurrentTransformState();

		/* Handles updates to any highlighted objects. */
		CheckForHighlightedObjects();

	}

	/* Getters. */
	/* This will allow us to get/set the current list of persistent objects in the scene. */
	public List<SCR_PersistentObject> Objects
	{
		get { return sceneObjects; }
		set { sceneObjects = value; }
	}

}