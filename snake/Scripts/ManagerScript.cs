using Godot;
using System;

public partial class ManagerScript : Node2D
{
	Vector2 screenSize;
	PackedScene snakeTemplate;
	Node snake;
	SnakeSmoothMoveScript snakeScript;
	Node2D foodManager;
	FoodManagerScript foodScript;
	public bool shouldGrow;
	public Vector2 headPos;
	public int score;
	Viewport viewport;

	public override void _Ready()
	{
		init();
		viewport = GetViewport();
	}

	public override void _Process(double delta)
	{
		snakeScript.target = getMousePos();

		screenSize = viewport.GetVisibleRect().Size;
		snakeScript.screenSize = screenSize;
		foodScript.screenSize = screenSize;


		CheatSpawnFood();

		snakeGrow(foodScript.FoodConsumed());

	}
	private void snakeGrow(int len) {
		for (int i = 0; i < len; i++) {
			snakeScript.addToSnake(Vector2.Zero);
		}
	}
	void CheatSpawnFood() {
		if (Input.IsActionPressed("cheat")) {
			foodScript.spawnFoodItem(new Vector2(
				(2*GD.Randf()-1)*(screenSize.X-(screenSize.X/2)),
				(2*GD.Randf()-1)*(screenSize.Y-(screenSize.Y/2))
			));
		}
	}
	private Vector2 getMousePos() {
		headPos = snakeScript.headPos;
		Vector2 mousePos = viewport.GetMousePosition();
		screenSize = viewport.GetVisibleRect().Size;
		mousePos.X -= screenSize.X/2;
		mousePos.Y -= screenSize.Y/2;
		mousePos -= headPos;
		return mousePos;		
	}
	private void init() {
		shouldGrow = false;

		foodManager = GetChild<Node2D>(0);
		foodScript = foodManager as FoodManagerScript;

		snakeTemplate = GD.Load<PackedScene>("res://Scenes/Snake.tscn");
		
		snake = snakeTemplate.Instantiate();
		AddChild(snake);
		
		snakeScript = snake as SnakeSmoothMoveScript;
	}
}
