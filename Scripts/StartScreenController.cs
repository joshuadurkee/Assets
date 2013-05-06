using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour {
	
	public GUIStyle style;
	public GUIStyle titleStyle;
	int instructions;
	
	string instMessage1		=	"The goal of this game is to reach the top\n" +
								"of the path, while avoiding the evil bunnies \n" +
								"who will try to knock you down.  You can \n" +
								"create a limited number of blocks to help you\n" +
								"climb, indicated by the number of cube charges\n" +
								"displayed in the top left corner of the screen.\n" +
								"You can destroy any blocks you have created,\n" +
								"allowing you to place more blocks.";
	string instMessage2		=	"In order to create a block, you must point the\n" +
							 	"crosshair in the center of the screen at the\n" +
							 	"side of an existing block, and you must also be\n" +
								"close enough to the block the crosshair is\n" +
								"pointing at.\n\n" +
								"If both these requirements are met, the\n" +
								"crosshair will be a bluish color.\n" +
								"If not, the crosshair will be a reddish color.";
	string instMessage3		=	"Creating blocks allows you to affect more than\n" +
								"just your movement abilities if you create a\n" +
								"block in the way of an enemy, that enemy will\n" +
								"be pushed by the block.  If there is another\n" +
								"block on the other side of the enemy, the enemy\n" +
								"will be killed instead.  Use these methods to\n" +
								"take the advantage!";
	string instMessage4		=	"The fancy white particle effect signals a cube\n" +
	 							"resetter.  A cube resetter will remove all blocks\n" +
	 							"you have created, and refund all your cube charges.\n" +
	 							"They will also act as checkpoints, and you will\n" +
	 							"respawn at them if you are pushed or fall off\n" +
	 							"the platforms.\n\n" +
								"The blue particle effect signals the end of the\n" +
								"level, touch it to win!";
	string controlsMessage	=	"Controls:\n" +
								"W/Up Arrow: Move Forward\n" +
								"S/Down Arrow: Move Backward\n" +
								"A/Left Arrow: Move Left\n" +
								"D/Right Arrow: Move Right\n" +
								"Mouse Move: Look Around\n" +
								"Left Click (any block): Create Block\n" +
								"Right Click (a block you've created): Destroy Block\n" +
								"Escape: Pause/Unpause the game";
	string gameTitle 		= "Blocks & Bunnies";
	string startButtonText 	= "Start";
	string instButtonText 	= "Instructions";
	string nextButtonText	= "Next";
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		Screen.lockCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI ()
	{
		switch (instructions)
		{
		case 0:		
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), gameTitle);
			if (GUI.Button(new Rect(3*Screen.width/7, 6*Screen.height/13, Screen.width/7, Screen.height/13), startButtonText))
				Application.LoadLevel(1);
			if (GUI.Button(new Rect(3*Screen.width/7, 7.25f*Screen.height/13, Screen.width/7, Screen.height/13), instButtonText))
				instructions = 1;
			break;
		case 1:
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), instMessage1);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+60, Screen.width/7, 30), nextButtonText))
				instructions++;
			break;
		case 2:
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), instMessage2);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+60, Screen.width/7, 30), nextButtonText))
				instructions++;
			break;
		case 3:
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), instMessage3);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+60, Screen.width/7, 30), nextButtonText))
				instructions++;
			break;
		case 4:
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), instMessage4);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+60, Screen.width/7, 30), nextButtonText))
				instructions = 5;
			break;
		case 5:
			GUI.Box(new Rect(3*Screen.width/8, Screen.height/3, Screen.width/4, Screen.height/3), controlsMessage);
			if (GUI.Button(new Rect(3*Screen.width/7, (Screen.height/2)+60, Screen.width/7, 30), nextButtonText))
				instructions = 0;
			break;
		}
	}
}
