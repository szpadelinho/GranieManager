using System.Collections.ObjectModel;
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
    private Training _selectedTraining;
    private Window _playerSelectionWindow;
    
    [ObservableProperty] private ObservableCollection<Player> _players;
    [ObservableProperty] private Player _selectedPlayer;

    public Training SelectedTraining
    {
        set => SetProperty(ref _selectedTraining, value);
    }

    public void Initialize(IPlayerService playerService, Training selectedTraining)
    {
        _playerService = playerService;
        SelectedTraining = selectedTraining;
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
        if (SelectedPlayer != null && _playerSelectionWindow != null)
        {
            WeakReferenceMessenger.Default.Send(new TrainingsViewModel.PlayerSelectedMessage(SelectedPlayer));
            _playerSelectionWindow.Close();
        }
    }
}