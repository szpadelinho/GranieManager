using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GranieManager.Models;
using GranieManager.ViewModels;

namespace GranieManager.Views;

public partial class TrainingsView : UserControl
{
    public TrainingsView()
    {
        InitializeComponent();
    }
    
    private void OnTrainingDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is TrainingsViewModel vm &&
            sender is ListBox listBox &&
            listBox.SelectedItem is Training training &&
            vm.ShowTrainingDetailsCommand.CanExecute(training))
        {
            vm.ShowTrainingDetailsCommand.Execute(training);
        }
    }
}