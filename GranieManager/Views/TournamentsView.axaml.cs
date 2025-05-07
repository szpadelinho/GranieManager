using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GranieManager.Models;
using GranieManager.ViewModels;

namespace GranieManager.Views;

public partial class TournamentsView : UserControl
{
    public TournamentsView()
    {
        InitializeComponent();
    }

    private void OnTournamentDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is TournamentsViewModel vm &&
            sender is ListBox listBox &&
            listBox.SelectedItem is Tournament tournament &&
            vm.ShowTournamentDetailsCommand.CanExecute(tournament))
        {
            vm.ShowTournamentDetailsCommand.Execute(tournament);
        }
    }
}