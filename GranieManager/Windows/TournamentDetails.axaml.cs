using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranieManager.ViewModels;

namespace GranieManager.Windows;

public partial class TournamentDetails : Window
{
    public TournamentDetails()
    {
        InitializeComponent();
        DataContext = App.Current.GetService<TournamentDetailsViewModel>();
    }
}