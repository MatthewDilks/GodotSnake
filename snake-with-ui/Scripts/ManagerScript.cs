using Godot;
using System;
using System.Collections.Generic;

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
	[Export] public Texture2D playerTexture;

	public override void _Ready()
	{
		init();
		viewport = GetViewport();

		for (int i = 0; i < 50; i++) {
			foodScript.spawnFoodItem(new Vector2(
				(2*GD.Randf()-1)*(screenSize.X-(screenSize.X/2)),
				(2*GD.Randf()-1)*(screenSize.Y-(screenSize.Y/2))
			));
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit")) {
			GetTree().ChangeSceneToFile("control.tscn");
		}
		
		snakeScript.target = getMousePos();

		screenSize = viewport.GetVisibleRect().Size;
		snakeScript.screenSize = screenSize;
		foodScript.screenSize = screenSize;


		// CheatSpawnFood();

		snakeGrow(snakeScript, foodScript.FoodConsumed(headPos));

		// if (snakeScript.snakeSelfCollideCheck()) {
		// 	QueueFree();
		// }
	}
	public int getFoodConsumed(Vector2 pos) {
		return foodScript.FoodConsumed(pos);
	}
	public List<Vector2> getNearestFood(List<Vector2> pos) {
		return foodScript.nearestFood(pos);
	}
	public void snakeGrow(SnakeSmoothMoveScript snake, int len) {
		for (int i = 0; i < len; i++) {
			snake.addToSnake();
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

		snakeScript = snake as SnakeSmoothMoveScript;
		snakeScript.collision = false;
		snakeScript.texture = playerTexture;

		AddChild(snake);
	}
}
