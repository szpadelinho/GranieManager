using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranieManager.ViewModels;

namespace GranieManager.Windows;

public partial class PlayerSelection : Window
{
    public PlayerSelection()
    {
        InitializeComponent();
        DataContext = App.Current.GetService<PlayerSelectionViewModel>();
        
        if (DataContext is PlayerSelectionViewModel vm)
        {
            vm.SetWindow(this);
        }
    }
}