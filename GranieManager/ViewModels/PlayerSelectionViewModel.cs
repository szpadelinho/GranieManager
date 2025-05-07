using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GranieManager.Models;
using GranieManager.Services;

namespace GranieManager.ViewModels;

public partial class PlayerSelectionViewModel : ViewModelBase
{
    private IPlayerService _playerService;
    private ITrainingService _trainingService;
    private ITournamentService _tournamentService;
    private Training? _selectedTraining;
    private Tournament? _selectedTournament;
    private Window? _playerSelectionWindow;
    
    [ObservableProperty] private ObservableCollection<Player> _players;
    [ObservableProperty] private Player? _selectedPlayer;

    public Training? SelectedTraining
    {
        set => SetProperty(ref _selectedTraining, value);
    }
    
    public Tournament? SelectedTournament
    {
        set => SetProperty(ref _selectedTournament, value);
    }

    public PlayerSelectionViewModel(IPlayerService playerService, ITrainingService trainingService, ITournamentService tournamentService)
    {
        _playerService = playerService;
        _trainingService = trainingService;
        _tournamentService = tournamentService;
    }

    public void InitializeTraining(IPlayerService playerService, Training selectedTraining)
    {
        _playerService = playerService;
        SelectedTraining = selectedTraining;
        SelectedTournament = null;
        LoadPlayersAsync();
    }
    
    public void InitializeTournament(IPlayerService playerService, Tournament selectedTournament)
    {
        _playerService = playerService;
        SelectedTournament = selectedTournament;
        SelectedTraining = null;
        LoadPlayersAsync();
    }

    public void SetWindow(Window window)
    {
        _playerSelectionWindow = window;
    }

    private async void LoadPlayersAsync()
    {
        var players = await _playerService.GetAllPlayersAsync();
        Players = new ObservableCollection<Player>(players);
    }

    [RelayCommand]
    private void SelectPlayer()
    {
        Debug.WriteLine("SelectPlayerCommand executed");
        
        if (SelectedPlayer != null && _playerSelectionWindow != null)
        {
            if (_selectedTraining != null)
            {
                WeakReferenceMessenger.Default.Send(new PlayerTrainedMessage(SelectedPlayer, _selectedTraining));
                _playerSelectionWindow.Close();
            }
            else if (_selectedTournament != null)
            {
                WeakReferenceMessenger.Default.Send(new PlayerTournamentMessage(SelectedPlayer, _selectedTournament));
                _playerSelectionWindow.Close();
            }
        }
        else
        {
            Debug.WriteLine("No player or training selected");
        }
    }

    public class PlayerTrainedMessage
    {
        public Player TrainedPlayer { get; set; }
        public Training ChosenTraining { get; set; }

        public PlayerTrainedMessage(Player trainedPlayer, Training chosenTraining)
        {
            TrainedPlayer = trainedPlayer;
            ChosenTraining = chosenTraining;
        }
    }

    public class PlayerTournamentMessage
    {
        public Player TournamentPlayer { get; set; }
        public Tournament ChosenTournament { get; set; }

        public PlayerTournamentMessage(Player tournamentPlayer, Tournament chosenTournament)
        {
            TournamentPlayer = tournamentPlayer;
            ChosenTournament = chosenTournament;
        }
    }
}