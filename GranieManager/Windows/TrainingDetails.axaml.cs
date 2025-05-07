using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranieManager.ViewModels;

namespace GranieManager.Windows;

public partial class TrainingDetails : Window
{
    public TrainingDetails()
    {
        InitializeComponent();
        DataContext = App.Current.GetService<TrainingDetailsViewModel>();
    }
}