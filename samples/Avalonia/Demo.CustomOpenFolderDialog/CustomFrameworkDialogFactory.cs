﻿using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs.Avalonia;

namespace Demo.CustomOpenFolderDialog;

public class CustomFrameworkDialogFactory : FrameworkDialogFactory
{
    public override IFrameworkDialog<TResult> Create<TSettings, TResult>(TSettings settings, AppDialogSettingsBase appSettings)
    {
        var s2 = (AppDialogSettings)appSettings;
        return settings switch
        {
            OpenFolderDialogSettings s => (IFrameworkDialog<TResult>)new CustomOpenFolderDialog(s),
            _ => base.Create<TSettings, TResult>(settings, appSettings)
        };
    }
}
