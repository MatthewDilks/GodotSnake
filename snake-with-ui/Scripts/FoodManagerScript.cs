using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class FoodManagerScript : Node2D
{
	public Vector2 screenSize;
	List<Node2D> foodItems;
	ManagerScript managerScript;
	PackedScene foodTemplate = GD.Load<PackedScene>("res://Scenes/food_items.tscn");
	public override void _Ready()
	{
		foodItems = new List<Node2D>();
		Node2D manager = GetParent<Node2D>();
		managerScript = manager as ManagerScript;

	}
	public override void _Process(double delta)
	{
		Vector2 headPos = managerScript.headPos;
		for (int i = 0; i < foodItems.Count; i++) {
			if ((foodItems[i].Position-headPos).Length() < 40) {
				managerScript.shouldGrow = true;
				
				onFoodCollision(i);
				break;
			}
		}
	}
	public void spawnFoodItem(Vector2 pos) {

		Node2D newThing = (Node2D)foodTemplate.Instantiate();
		newThing.Position = pos;

		AddChild(newThing);
		foodItems.Add(newThing);
	}
	public int FoodConsumed(Vector2 headPos) {
		int foodNearby = 0;
		for (int i = 0; i < foodItems.Count; i++) {
			if ((foodItems[i].Position-headPos).Length() < 40) {
				
				onFoodCollision(i);
				foodNearby++;
			}
		}
		return foodNearby;
	}
	public List<Vector2> nearestFood(List<Vector2> pos) {
		if (foodItems.Count < 1) {
			return pos;
		}

		int[] closestIndex = new int[pos.Count];
		float[] closestDist = new float[pos.Count];
		for (int i = 0; i < pos.Count; i++) {
			closestDist[i] = 999999;
		}

		for (int i = 0; i < foodItems.Count; i++) {
			for (int j = 0; j < pos.Count; j++) {
				float dist = getRelativePos(pos[j],foodItems[i].Position).Length();
				if (dist <= closestDist[j]) {
					closestIndex[j] = i;
					closestDist[j] = dist;
				}
			}
		}
		List<Vector2> results = new List<Vector2>();
		for (int i = 0; i < pos.Count; i++) {
			results.Add(foodItems[closestIndex[i]].Position);
		}
		return results;
	}
	private Vector2 getRelativePos(Vector2 a, Vector2 b) {
		return b - a;
	}
	void onFoodCollision(int index) {
		spawnFoodItem(new Vector2(
			(2*GD.Randf()-1)*(screenSize.X-(screenSize.X/2)),
			(2*GD.Randf()-1)*(screenSize.Y-(screenSize.Y/2))
		));

		foodItems[index].QueueFree();
		foodItems.RemoveAt(index);
	}
}
