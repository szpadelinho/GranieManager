using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;

namespace GranieManager.ViewModels;

public partial class TrainingDetailsViewModel : ViewModelBase
{
    [ObservableProperty] private Training _training;
}