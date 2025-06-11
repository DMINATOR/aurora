using AuroraLib.AI;
using AuroraLib.AI.Models;
using Godot;
using OllamaSharp.Models.Chat;
using System;
using System.Linq;

public partial class AiScene : Control
{
    private AiSession _session;

    private TextEdit TextOllamaPath;

    private TextEdit TextOllamaEndpoint;

    private TextEdit TextChatHistory;
    private LineEdit LineEditUserInput;
    private Button ButtonSend;
    private Button ButtonRefreshProcesses;

    private Tree TreeProcesses;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TextOllamaPath = GetNode<TextEdit>($"%{nameof(TextOllamaPath)}");
        TextOllamaEndpoint = GetNode<TextEdit>($"%{nameof(TextOllamaEndpoint)}");
        TextChatHistory = GetNode<TextEdit>($"%{nameof(TextChatHistory)}");
        LineEditUserInput = GetNode<LineEdit>($"%{nameof(LineEditUserInput)}");
        ButtonSend = GetNode<Button>($"%{nameof(ButtonSend)}");
        ButtonRefreshProcesses = GetNode<Button>($"%{nameof(ButtonRefreshProcesses)}");
        TreeProcesses = GetNode<Tree>($"%{nameof(TreeProcesses)}");

        // Set column widths for process list
        TreeProcesses.SetColumnCustomMinimumWidth(0, 60);   // PID
        TreeProcesses.SetColumnCustomMinimumWidth(1, 200);  // Name
        TreeProcesses.SetColumnCustomMinimumWidth(2, 110);  // Action

        PopulateOllamaProcessList();
    }

    private void PopulateOllamaProcessList()
    {
        TreeProcesses.Clear();

        var root = TreeProcesses.GetRoot();

        if (root == null)
        {
            root = TreeProcesses.CreateItem();
        }

        var processes = AuroraLib.AI.AiServer.GetOllamaProcesses();

        foreach (var process in processes)
        {
            if( process == null || process.HasExited)
                continue;

            var item = TreeProcesses.CreateItem(root);
            item.SetText(0, process.Id.ToString()); // PID
            item.SetText(1, process.ProcessName + ".exe"); // Name
            item.SetText(2, "❌ Terminate"); // Action
        }
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
                    AppendToChatDeferred(token);
                }
            }
        };

        GD.Print($"[AI]: Starting session");
        _session = new AiSession(config);
        PopulateOllamaProcessList();
    }

    private void ButtonStopServerPressed()
    {
        GD.Print($"[AI]: Stopping session");
        _session.Dispose();
        PopulateOllamaProcessList();
    }

    private async void ButtonSendPressed()
    {
        if (_session == null || string.IsNullOrWhiteSpace(LineEditUserInput.Text))
            return;

        var userMessage = LineEditUserInput.Text.Trim();
        LineEditUserInput.Text = string.Empty;

        AppendToChatDeferred($"You: {userMessage}\n");
        TextChatHistory.Text += "\n"; // Add a new line for better readability

        try
        {
            try
            {
                var response = await _session.SendMessageAsync(userMessage);
                GD.PrintErr($"[AI][Chat] Response: '{response}'");
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[AI][Chat] Error: {ex.Message}\n{ex.StackTrace}");
                AppendToChatDeferred($"[Error]: {ex.Message}\n");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[AI][Chat] Task Error: {ex.Message}\n{ex.StackTrace}");
            AppendToChatDeferred($"[Error]: {ex.Message}\n");
        }
    }

    private void ButtonRefreshProcessesPressed()
    {
        PopulateOllamaProcessList();
    }

    // Call through deferred only
    private void AppendToChatRaw(string text)
    {
        TextChatHistory.Text += text;
        TextChatHistory.ScrollVertical = int.MaxValue;
    }

    private void AppendToChatDeferred(string text)
    {
          CallDeferred(nameof(AppendToChatRaw), text);
    }

    private void OnProcessCellSelected()
    {
        var selectedItem = TreeProcesses.GetSelected();
        if (selectedItem == null)
            return;

        var actionText = selectedItem.GetText(2);
        if (actionText == "❌ Terminate")
        {
            var pid = int.Parse(selectedItem.GetText(0));
            var process = System.Diagnostics.Process.GetProcessById(pid);
            if (process != null && !process.HasExited)
            {
                process.Kill();
                GD.Print($"[AI]: Terminated process {pid}");
                CallDeferred(nameof(PopulateOllamaProcessList));
            }
        }
    }
}
