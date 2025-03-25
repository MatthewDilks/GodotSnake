using Godot;

public partial class SceneControl : Node
{
	[Export] private string nextScenePath = "res://NextScene.tscn"; 

	public override void _Ready()
	{
		Button playAgainButton = GetNode<Button>("PlayAgain"); 
		Button quitGameButton = GetNode<Button>("QuitGame"); 
		
		playAgainButton.Pressed += OnPlayAgainButtonPressed;
		quitGameButton.Pressed += OnQuitGameButtonPressed;
	}

	private void OnPlayAgainButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/manager_scene.tscn");
	}

	private void OnQuitGameButtonPressed()
	{
		GetTree().Quit();
	}
}
