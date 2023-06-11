﻿using ReactiveUI.Fody.Helpers;

namespace Demo.Avalonia.FluentTaskDialog;

public class AskTextBoxViewModel : ViewModelBase
{
    [Reactive]
    public string Title { get; set; } = "Title";

    [Reactive]
    public string Text { get; set; } = string.Empty;
}
