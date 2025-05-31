using AuroraLib;
using Godot;

public partial class MainScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var cl = new Class1();
        GD.Print($"Info: MainScene is ready = {cl.GetMessage()}");         // Standard log
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
