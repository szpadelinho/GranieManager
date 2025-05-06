using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranieManager.ViewModels;

namespace GranieManager.Windows;

public partial class PlayerSigning : Window
{
    public PlayerSigning()
    {
        InitializeComponent();
        DataContext = App.Current.GetService<PlayerSigningViewModel>();

        if (DataContext is PlayerSigningViewModel vm)
        {
            vm.SetWindow(this);
        }
    }
}