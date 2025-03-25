using Godot;

public partial class SceneControl : Node
{
	[Export] private string nextScenePath = "res://NextScene.tscn"; 

	public override void _Ready()
	{
		Button nextButton = GetNode<Button>("NextButton"); 
		Button quitButton = GetNode<Button>("QuitButton"); 
		
		nextButton.Pressed += OnNextButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
	}

	private void OnNextButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/manager_scene.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit(); 
	}
}
