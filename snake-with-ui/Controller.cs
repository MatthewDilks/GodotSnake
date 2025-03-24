using Godot;
using System;

public partial class Controller : Control
{
	// PackedScene GameManager;
	// Node CurrentGame;
 	// ManagerScript CurrentGameScript;
	Button button;
	Button exit;
	
	public override void _Ready()
	{
        	button = GetNode<Button>("VBoxContainer/Button");
		exit = GetNode<Button>("VBoxContainer/Button3");
		
		exit.Pressed += exitMenu;
		button.Pressed += initNewGame;
	}

	public override void _Process(double delta)
	{
	}
	void exitMenu() {
		GetTree().Quit();
	}
 	void initNewGame() {
		GetTree().ChangeSceneToFile("res://Scenes/manager_scene.tscn");
    	}
}
