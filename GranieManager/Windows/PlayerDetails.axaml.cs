using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranieManager.ViewModels;

namespace GranieManager.Windows;

public partial class PlayerDetails : Window
{
    public PlayerDetails()
    {
        InitializeComponent();
        DataContext = App.Current.GetService<PlayerDetailsViewModel>();
    }
}