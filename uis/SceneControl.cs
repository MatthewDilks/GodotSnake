using Godot;

public partial class SceneControl : Node
{
	[Export] private string nextScenePath = "res://NextScene.tscn"; // Set in editor or manually

	public override void _Ready()
	{
		Button nextButton = GetNode<Button>("NextButton"); // Button to switch scenes
		Button quitButton = GetNode<Button>("QuitButton"); // Button to quit
		
		nextButton.Pressed += OnNextButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
	}

	private void OnNextButtonPressed()
	{
		GetTree().ChangeSceneToFile(nextScenePath);
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit(); // Closes the application
	}
}
