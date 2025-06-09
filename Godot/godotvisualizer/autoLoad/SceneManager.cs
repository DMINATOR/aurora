using Godot;
using System;

/// <summary>
/// Used to switch between scenes
/// </summary>
public partial class SceneManager : Node
{
    [Export]
    public PackedScene MainScene;

    [Export]
    public PackedScene AiScene;


    public void ChangeToMainScene()
    {
        GetTree().ChangeSceneToPacked(MainScene);
    }

    public void ChangeToAiScene()
    {
        GetTree().ChangeSceneToPacked(AiScene);
    }
}

public static class SceneManagerExtensions
{
    // Retrieves an instance of a scene manager  
    public static SceneManager GetGlobalSceneManager(this Node node)
    {
        return node.GetNode<SceneManager>($"/root/{nameof(SceneManager)}");
    }
}
