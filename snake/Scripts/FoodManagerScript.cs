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
	public int FoodConsumed() {
		int foodNearby = 0;
		Vector2 headPos = managerScript.headPos;
		for (int i = 0; i < foodItems.Count; i++) {
			if ((foodItems[i].Position-headPos).Length() < 40) {
				
				onFoodCollision(i);
				foodNearby++;
			}
		}
		return foodNearby;
	}
	void onFoodCollision(int index) {
		// foodItems[index].Position = new Vector2(GD.Randf()*screenSize.X,GD.Randf()*screenSize.Y);
		foodItems[index].QueueFree();
		foodItems.RemoveAt(index);
	}
}
