using Godot;
using System;
using System.Collections.Generic;

public partial class AiSnakeScript : Node
{
	ManagerScript manager;
	PackedScene snakeTemplate;
	List<Node> snakeNodes;
	List<SnakeSmoothMoveScript> snakes;
	
	public override void _Ready()
	{
		Init();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		updateSnakeTargets();
		if (snakes.Count < 5) {
			AddSnake();
		}
	}
	void Init() {
		manager = GetParent<Node2D>() as ManagerScript;

		snakeNodes = new List<Node>();
		snakes = new List<SnakeSmoothMoveScript>();

		snakeTemplate = GD.Load<PackedScene>("res://Scenes/Snake.tscn");
		
		for (int i = 0; i < 5; i++) {
			snakes.Add(AddSnake());
		}
	}
	
	void updateSnakeTargets() {
		List<Vector2> headPositions = new List<Vector2>();
		for (int i = 0; i < snakes.Count; i++) {
			headPositions.Add(snakes[i].headPos);
		}
		List<Vector2> targets = manager.getNearestFood(headPositions);

		for (int i = 0; i < snakes.Count; i++) {
			snakes[i].target = targets[i] - snakes[i].headPos + randVec()*15;

			manager.snakeGrow(snakes[i],manager.getFoodConsumed(snakes[i].headPos));
			if (snakes[i].snakeSelfCollideCheck()) {
				snakeNodes[i].QueueFree();
				snakeNodes.RemoveAt(i);
			}
		}
	}
	private Vector2 randVec() {
		return new Vector2(GD.Randf()*2-1,GD.Randf()*2-1);
	}

	private SnakeSmoothMoveScript AddSnake() {
		Node temp = snakeTemplate.Instantiate();

		snakeNodes.Add(temp);
		AddChild(temp);
		snakes.Add(temp as SnakeSmoothMoveScript);
		snakes[snakes.Count-1].collision = false;
		return temp as SnakeSmoothMoveScript;
	}
}
