using AuroraLib.AI;
using AuroraLib.AI.Models;
using Godot;
using OllamaSharp.Models.Chat;
using System;

public partial class AiScene : Control
{
    private AiSession _session;

    private TextEdit TextOllamaPath;

    private TextEdit TextOllamaEndpoint;
    
    private TextEdit _chatHistory;
    private LineEdit _userInput;
    private Button _sendButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        TextOllamaPath = GetNode<TextEdit>("%TextOllamaPath");
        TextOllamaEndpoint = GetNode<TextEdit>("%TextOllamaEndpoint");
        _chatHistory = GetNode<TextEdit>("Window/TabContainer/TabPlayground/VBoxContainerChat/ChatHistory");
        _userInput = GetNode<LineEdit>("Window/TabContainer/TabPlayground/VBoxContainerChat/HBoxContainerInput/UserInput");
        _sendButton = GetNode<Button>("Window/TabContainer/TabPlayground/VBoxContainerChat/HBoxContainerInput/SendButton");
        // No direct event hookup; use Godot's signal connection in the scene file
    }

    private void ButtonStartServerPressed()
    {
        var config = new AiSessionConfig
        {
            PathToServerExecutable = TextOllamaPath.Text,
            Model = new Phi4AiModel(),
            ServerEndpoint = TextOllamaEndpoint.Text,
            ErrorSink = (message) =>
            {
                if (message != null)
                {
                    GD.PrintErr($"[AI]: {message}");
                }
            },
            OutputSink = (message) =>
            {
                if (message != null)
                {
                    GD.Print($"[AI]: {message}");
                }
            },
            TokenReceivedSink = (token) =>
            {
                if (token != null)
                {
                    // token do later
                    //GD.Print($"[AI]: {message}");
                }
            }
        };

        GD.Print($"[AI]: Starting session");
        _session = new AiSession(config);
    }

    private void ButtonStopServerPressed()
    {
        GD.Print($"[AI]: Stopping session");
        _session.Dispose();
    }

    // Rename OnSendButtonPressed to ButtonSendPressed for consistency
    private void ButtonSendPressed()
    {
        if (_session == null || string.IsNullOrWhiteSpace(_userInput.Text))
            return;
        var userMessage = _userInput.Text.Trim();
        AppendToChat($"You: {userMessage}\n");
        _userInput.Text = string.Empty;
        try
        {
            var response = _session.SendMessage(userMessage);
            AppendToChat($"Llama: {response}\n");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[AI][Chat] Error: {ex.Message}\n{ex.StackTrace}");
            AppendToChat($"[Error]: {ex.Message}\n");
        }
    }

    private void AppendToChat(string text)
    {
        _chatHistory.Text += text;
        _chatHistory.ScrollVertical = int.MaxValue;
    }
}
