﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using HanumanInstitute.MvvmDialogs.Wpf;
using HanumanInstitute.MvvmDialogs.Wpf.FrameworkDialogs;
using Ookii.Dialogs.Wpf;

namespace Demo.CustomSaveFileDialog;

public class CustomSaveFileDialog : FrameworkDialogBase<SaveFileDialogSettings, string?>
{
    private readonly SaveFileDialogSettings settings;
    private readonly VistaSaveFileDialog saveFileDialog;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomSaveFileDialog"/> class.
    /// </summary>
    /// <param name="settings">The settings for the save file dialog.</param>
    /// <param name="appSettings">Application-wide settings configured on the DialogService.</param>
    public CustomSaveFileDialog(SaveFileDialogSettings settings, AppDialogSettings appSettings) :
        base(settings, appSettings)
    {
        this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

        saveFileDialog = new VistaSaveFileDialog
        {
            CheckFileExists = settings.CheckFileExists,
            CheckPathExists = settings.CheckPathExists,
            CreatePrompt = settings.CreatePrompt,
            DefaultExt = settings.DefaultExtension,
            FileName = settings.InitialFile,
            Filter = SyncFilters(settings.Filters),
            InitialDirectory = settings.InitialDirectory,
            OverwritePrompt = settings.OverwritePrompt,
            Title = settings.Title
        };
    }

    /// <summary>
    /// Encodes the list of filters in the Win32 API format:
    /// "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
    /// </summary>
    /// <param name="filters">The list of filters to encode.</param>
    /// <returns>A string representation of the list compatible with Win32 API.</returns>
    private static string SyncFilters(List<FileFilter> filters)
    {
        StringBuilder result = new StringBuilder();
        foreach (var item in filters)
        {
            // Add separator.
            if (result.Length > 0)
            {
                result.Append('|');
            }

            // Get all extensions as a string.
            var extDesc = item.ExtensionsToString();
            // Get name including extensions.
            var name = item.NameToString(extDesc);
            // Add name+extensions for display.
            result.Append(name);
            // Add extensions again for the API.
            result.Append("|");
            result.Append(extDesc);
        }
        return result.ToString();
    }

    /// <summary>
    /// Opens a save file dialog with specified owner.
    /// </summary>
    /// <param name="owner">
    /// Handle to the window that owns the dialog.
    /// </param>
    /// <returns>
    /// true if user clicks the OK button; otherwise false.
    /// </returns>
    public override async Task<string?> ShowDialogAsync(WindowWrapper owner)
    {
        if (owner == null) throw new ArgumentNullException(nameof(owner));

        var result = await owner.Ref.RunUiAsync(() => saveFileDialog.ShowDialog(owner.Ref));
        return result == true ? saveFileDialog.FileName : null;
    }

    /// <summary>
    /// Opens a save file dialog with specified owner.
    /// </summary>
    /// <param name="owner">
    /// Handle to the window that owns the dialog.
    /// </param>
    /// <returns>
    /// true if user clicks the OK button; otherwise false.
    /// </returns>
    public override string? ShowDialog(WindowWrapper owner)
    {
        if (owner == null) throw new ArgumentNullException(nameof(owner));

        var result = saveFileDialog.ShowDialog(owner.Ref);
        return result == true ? saveFileDialog.FileName : null;
    }
}
