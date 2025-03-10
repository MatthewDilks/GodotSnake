using Godot;
using System;
using System.Collections.Generic;

public partial class SnakeSmoothMoveScript : Node
{
	// public
	public Vector2 headPos;
	public Vector2 headDirection;
	public Vector2 target;
	public Vector2 screenSize;
	// export
	[Export] float ideal = 25;
	[Export] float speed = 400;
	[Export] int startLen = 15;
	[Export] float TurnLimit = 0.5f;
	[Export] float turnSpeed = 5f;

	// private
	private List<MeshInstance2D> circles;

	public override void _Ready()
	{
		headDirection = new Vector2();
		headPos = new Vector2();

		// Mov = new Vector2(0,0);
		circles = new List<MeshInstance2D>();

		for (int i = 0; i < startLen; i++) {
			addToSnake(new Vector2(i*25,0));
		}
	}
	public override void _Process(double delta)
	{
		headPos = circles[0].Position;

		LimitMouse((float)delta);
		
		passThroughSnake();
	}



	// public functions
	public void addToSnake(Vector2 pos) {
		MeshInstance2D circle = new MeshInstance2D();
		circle.Mesh = new SphereMesh();
		circle.Position = pos;
		circle.Scale = new Vector2(50,50);
		AddChild(circle);
		circles.Add(circle);
	}
	

	// private functions
	private void passThroughSnake() {
		for (int j = 0; j < circles.Count-1; j++) {
			normaliseDist(j);
		}
	}
	private void normaliseDist(int index) {
		if (index > circles.Count-2 || index < 0) {
			return;
		}
		Vector2 distVec = getRelativePos(circles[index].Position,circles[index+1].Position);

		Vector2 finalCoord = (distVec.Normalized() * ideal) + circles[index].Position;

		circles[index+1].Position = finalCoord;
	}
	private void LimitMouse(float delta) {
		// mouse.X -= screenSize.X/2;
		// mouse.Y -= screenSize.Y/2;
		// mouse -= circles[0].Position;

		Vector2 headDir = getHeadDir().Normalized();
		
		float cross = target.Cross(headDir);
		bool clockwise = cross < 0;

		if (getAlignment(target,getHeadDir()) < TurnLimit) {
			if (clockwise) {
				circles[0].Rotate(-turnSpeed * delta);
			}
			else {
				circles[0].Rotate(turnSpeed * delta);
			}
		}
		
		circles[0].MoveLocalY(speed*delta);
	}
	private Vector2 getHeadDir() {
		return getRelativePos(circles[0].Position,circles[1].Position);
	}
	private Vector2 getRelativePos(Vector2 a, Vector2 b) {
		return b - a;
	}
	private float getAlignment(Vector2 a, Vector2 b) {
		return a.Normalized().Dot(b.Normalized());
	}
}
