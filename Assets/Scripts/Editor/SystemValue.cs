using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SystemValue : EditorWindow
{
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/My Window/Open")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		SystemValue window = (SystemValue)EditorWindow.GetWindow(typeof(SystemValue));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup();
	}
}
