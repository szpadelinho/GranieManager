using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;
using GranieManager.Services;

namespace GranieManager.ViewModels;

public partial class TournamentsViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = "Tournaments";
    private readonly ITournamentService _tournamentService;
    [ObservableProperty] private ObservableCollection<Tournament> _tournaments = new ObservableCollection<Tournament>();

    public TournamentsViewModel(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
        LoadTournamentAsync();
    }

    public async void LoadTournamentAsync()
    {
        var tournaments = await _tournamentService.GetAllTournamentAsync();
        Tournaments.Clear();
        foreach (var tournament in tournaments)
        {
            Tournaments.Add(tournament);
        }
    }
}