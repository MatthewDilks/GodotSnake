using Godot;
using System;

public partial class Example : Node
{
	PackedScene GameManager;
	Node Game;
	public override void _Ready()
	{
		GameManager = GD.Load<PackedScene>("res://Scenes/manager_scene.tscn");
		
		Game = GameManager.Instantiate();
		AddChild(Game);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
