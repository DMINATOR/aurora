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

    private TextEdit TextChatHistory;
    private LineEdit LineEditUserInput;
    private Button ButtonSend;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        TextOllamaPath = GetNode<TextEdit>($"%{nameof(TextOllamaPath)}");
        TextOllamaEndpoint = GetNode<TextEdit>($"%{nameof(TextOllamaEndpoint)}");
        TextChatHistory = GetNode<TextEdit>($"%{nameof(TextChatHistory)}");
        LineEditUserInput = GetNode<LineEdit>($"%{nameof(LineEditUserInput)}");
        ButtonSend = GetNode<Button>($"%{nameof(ButtonSend)}");
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
        if (_session == null || string.IsNullOrWhiteSpace(LineEditUserInput.Text))
            return;

        var userMessage = LineEditUserInput.Text.Trim();
        
        AppendToChat($"You: {userMessage}\n");
        LineEditUserInput.Text = string.Empty;

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
        TextChatHistory.Text += text;
        TextChatHistory.ScrollVertical = int.MaxValue;
    }
}
