using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GranieManager.Models;
using GranieManager.Services;
using GranieManager.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace GranieManager.ViewModels;

public partial class TournamentsViewModel : ViewModelBase, IRecipient<PlayerSelectionViewModel.PlayerTournamentMessage>
{
    [ObservableProperty] private string _title = "Tournaments";
    private readonly ITournamentService _tournamentService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IPlayerService _playerService;
    [ObservableProperty] private ObservableCollection<Tournament> _tournaments = new ObservableCollection<Tournament>();
    [ObservableProperty] private Tournament? _selectedTournament;
    
    public TournamentsViewModel(ITournamentService tournamentService, IServiceProvider serviceProvider, IPlayerService playerService)
    {
        _tournamentService = tournamentService;
        _serviceProvider = serviceProvider;
        _playerService = playerService;
        LoadTournamentAsync();
        WeakReferenceMessenger.Default.Register(this);
    }

    public async Task LoadTournamentAsync()
    {
        var tournaments = await _tournamentService.GetAllTournamentAsync();
        Tournaments.Clear();
        foreach (var tournament in tournaments)
        {
            Tournaments.Add(tournament);
        }
    }

    [RelayCommand]
    private void ParticipateInTournament(Tournament? tournament)
    {
        if (tournament == null)
        {
            return;
        }
        
        var playerSelection = _serviceProvider.GetRequiredService<PlayerSelection>();

        if (playerSelection.DataContext is PlayerSelectionViewModel vm)
        {
            var playerService = _serviceProvider.GetRequiredService<IPlayerService>();
            vm.InitializeTournament(playerService, tournament);
            playerSelection.Show();
        }
    }

    public async void Receive(PlayerSelectionViewModel.PlayerTournamentMessage message)
    {
        if (message.TournamentPlayer != null && message.ChosenTournament != null)
        {
            try
            {
                await _playerService.JoinTournamentAsync(message.TournamentPlayer, message.ChosenTournament);
                await LoadTournamentAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while joining tournament: {ex.Message}");
            }
            SelectedTournament = null;
        }
    }

    [RelayCommand]
    public void ShowTournamentDetails(Tournament? tournament)
    {
        if (tournament == null)
        {
            return;
        }

        var window = App.Current.GetService<TournamentDetails>();
        if (window.DataContext is TournamentDetailsViewModel vm)
        {
            vm.Tournament = tournament;
            window.Show();
        }
    }
}