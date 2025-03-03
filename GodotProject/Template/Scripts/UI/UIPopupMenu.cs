using Godot;
using System;

namespace Template;

public partial class UIPopupMenu : Control
{
    public event Action OnOpened;
    public event Action OnClosed;
    public event Action OnMainMenuBtnPressed;

    public WorldEnvironment WorldEnvironment { get; private set; }

    VBoxContainer vbox;
    PanelContainer menu;
    public UIOptions Options;

    public override void _Ready()
    {
        Global.Services.Add(this);
        TryFindWorldEnvironmentNode();

        menu = GetNode<PanelContainer>("%Menu");
        vbox = GetNode<VBoxContainer>("%Navigation");

        Options = Game.LoadPrefab<UIOptions>(Prefab.UIOptions);
        AddChild(Options);
        Options.Hide();
        Hide();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (Game.Console.Visible)
            {
                Game.Console.ToggleVisibility();
                return;
            }

            if (Options.Visible)
            {
                Options.Hide();
                menu.Show();
            }
            else
            {
                Visible = !Visible;
                GetTree().Paused = Visible;

                if (Visible)
                {
                    OnOpened?.Invoke();
                    // todo: pause the game
                }
                else
                {
                    OnClosed?.Invoke();
                }
            }
        }
    }

    void TryFindWorldEnvironmentNode()
    {
        Node node = GetTree().Root.FindChild("WorldEnvironment", 
            recursive: true, owned: false);

        if (node is not null and WorldEnvironment worldEnvironment)
            WorldEnvironment = worldEnvironment;
    }

    void _on_resume_pressed() => Hide();

    void _on_options_pressed()
    {
        Options.Show();
        menu.Hide();
    }

    void _on_main_menu_pressed()
    {
        OnMainMenuBtnPressed?.Invoke();
        GetTree().Paused = false;
        Game.SwitchScene(Scene.UIMainMenu);
    }

    async void _on_quit_pressed() => await GetNode<Global>("/root/Global").QuitAndCleanup();
}

