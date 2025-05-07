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

namespace GranieManager.ViewModels;

public partial class PlayersViewModel : ViewModelBase, IRecipient<PlayerSigningViewModel.PlayerAddedMessage>
{
    [ObservableProperty] private string _title = "Players";
    private readonly IPlayerService _playerService;
    [ObservableProperty] private ObservableCollection<Player> _players = new ObservableCollection<Player>();
    [ObservableProperty] private Player? _selectedPlayer;
    
    public PlayersViewModel(IPlayerService playerService)
    {
        _playerService = playerService;
        LoadPlayersAsync();
        
        WeakReferenceMessenger.Default.Register(this);
    }

    private async Task LoadPlayersAsync()
    {
        Debug.WriteLine("LoadPlayersAsync() is called");
        try
        {
            var players = await _playerService.GetAllPlayersAsync();
            Debug.WriteLine(players);
            Players.Clear();
            foreach (var player in players)
            {
                Players.Add(player);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PlayersViewModel loading Players error!!: {ex.Message}");
        }
    }

    [RelayCommand]
    private void PlayerSigningWindow()
    {
        var playerSigning = new PlayerSigning();
        playerSigning.Show();
    }
    
    public async void Receive(PlayerSigningViewModel.PlayerAddedMessage message)
    {
        await LoadPlayersAsync();
    }
    
    [RelayCommand]
    private void ShowPlayersDetails(Player? player)
    {
        if (player is null)
        {
            return;
        }

        var window = App.Current.GetService<PlayerDetails>();
        
        if(window.DataContext is PlayerDetailsViewModel vm)
        {
            vm.Player = player;
            window.Show();
        }
    }
}