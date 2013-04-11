using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour {
	
	public GUIStyle style;
	public GUIStyle titleStyle;
	bool instructions;
	
	string instMessage 		= "The goal of this game is to reach the top of the path, \n" +
							"while avoiding the evil bunnies who will try to knock you down.\n" +
							"You can create a limited number of blocks to help you climb,\n" +
							"indicated by the number in the top left corner of the screen, \n" +
							"and can destroy them at will, allowing you to place more blocks.\n\n" +
							"Creating blocks allows you to affect more than just your movement abilities\n" +
							"if you create a block in the way of an enemy, that enemy will be pushed by the\n" +
							"block.  If there is another block on the other side of the enemy, the enemy\n" +
							"will be killed instead.  Use these methods to take the advantage!\n\n" +
							"Controls:\n" +
							"W/Up Arrow: Move Forward\n" +
							"S/Down Arrow: Move Backward\n" +
							"A/Left Arrow: Move Left\n" +
							"D/Right Arrow: Move Right\n" +
							"Mouse Move: Look Around\n" +
							"Left Click (on any block): Create Block\n" +
							"Right Click (on a block you've created): Destroy Block";
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
			GUI.Box(new Rect(Screen.width/4, (Screen.height/2)-170, Screen.width/2, 340), instMessage);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+130, Screen.width/7, 30), backButtonText))
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
