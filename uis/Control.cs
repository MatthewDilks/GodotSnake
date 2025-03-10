using Godot;
using System;

public partial class Control : Godot.Control
{
	PackedScene GameManager;
	Node CurrentGame;
 	ManagerScript CurrentGameScript;
 	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
 		GameManager = GD.Load<PackedScene>("snake/res://Scenes/manager_scene.tcsn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
 	void initNewGame() {
		CurrentGame = GameManager.Instantiate();
  		addChild(currentGame);
    		CurrentGameScript = CurrentGame as ManagerScript;
    	}
  	
}
