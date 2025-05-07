using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;

namespace GranieManager.ViewModels;

public partial class TournamentDetailsViewModel : ViewModelBase
{
    [ObservableProperty] private Tournament? _tournament;   
}