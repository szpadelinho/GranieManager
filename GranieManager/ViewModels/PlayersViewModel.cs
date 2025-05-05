using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;
using GranieManager.Services;

namespace GranieManager.ViewModels;

public partial class PlayersViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = "Players";
    private readonly IPlayerService _playerService;
    [ObservableProperty] private ObservableCollection<Player> _players = new ObservableCollection<Player>();

    public PlayersViewModel(IPlayerService playerService)
    {
        _playerService = playerService;
        LoadPlayersAsync();
    }

    private async Task LoadPlayersAsync()
    {
        var players = await _playerService.GetAllPlayersAsync();
        Players.Clear();
        foreach (var player in players)
        {
            Players.Add(player);
        }
    }
}