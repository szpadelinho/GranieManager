using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GranieManager.Models;
using GranieManager.ViewModels;
using GranieManager.Windows;

namespace GranieManager.Views;

public partial class PlayersView : UserControl
{
    public PlayersView()
    {
        InitializeComponent();
        Debug.WriteLine($"PlayersView DataContext: {DataContext}");
    }

    private void OnPlayerDoubleTapped(object? sender, TappedEventArgs tappedEventArgs)
    {
        if (DataContext is PlayersViewModel vm &&
            sender is ListBox listBox &&
            listBox.SelectedItem is Player player &&
            vm.ShowPlayersDetailsCommand.CanExecute(player))
        {
            vm.ShowPlayersDetailsCommand.Execute(player);
        }
    }
}