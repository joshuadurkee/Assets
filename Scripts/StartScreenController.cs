using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour {
	
	public GUIStyle style;
	public GUIStyle titleStyle;
	bool instructions;
	
	string instMessage 		= "The goal of this game is to " +
							"reach the top of the path, \n" +
							"while avoiding the evil bunnies " +
							"who will try to knock you down.\n" +
							"You can create a limited number a blocks to help you climb,\n" +
							"And destroy them at will\n\n" +
							"Controls:\n" +
							"W: Move Forward\n" +
							"S: Move Backward\n" +
							"A: Strafe Left\n" +
							"D: Strafe Right\n" +
							"Mouse Move: Look Around\n" +
							"Left Click (on a block): Create Block\n" +
							"Right Click (on a block): Destroy Block";
	string gameTitle 		= "Blocks & Bunnies";
	string startButtonText 	= "Start";
	string instButtonText 	= "Instructions";
	string backButtonText	= "Back";
	
	// Use this for initialization
	void Start () {
		//guiText.font = font;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI ()
	{
		if (instructions)
		{
			GUI.Box(new Rect(Screen.width/4, (Screen.height/2)-125, Screen.width/2, 250), instMessage);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+80, Screen.width/7, 30), backButtonText))
				instructions = false; 
		}
		else
		{
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), gameTitle);
			if (GUI.Button(new Rect(3*Screen.width/7, 6*Screen.height/13, Screen.width/7, Screen.height/13), startButtonText))
				Application.LoadLevel(1);
			if (GUI.Button(new Rect(3*Screen.width/7, 7.25f*Screen.height/13, Screen.width/7, Screen.height/13), instButtonText))
				instructions = true;
		}
	}
}
