using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GranieManager.Models;
using GranieManager.Services;

namespace GranieManager.ViewModels;

public partial class PlayerSigningViewModel : ViewModelBase
{
    private readonly IPlayerService _playerService;
    private Window _playerSigningWindow;
    
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _skill;
    [ObservableProperty] private string _stress;
    [ObservableProperty] private string _fatigue;
    [ObservableProperty] private string _game;

    public PlayerSigningViewModel(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    public void SetWindow(Window window)
    {
        _playerSigningWindow = window;
    }

    [RelayCommand]
    private async Task AddPlayerToDatabase()
    {
        if (string.IsNullOrWhiteSpace(Name) ||
            !int.TryParse(Skill, out var skill) || skill < 0 ||
            !int.TryParse(Stress, out var stress) || stress < 0 ||
            !int.TryParse(Fatigue, out var fatigue) || fatigue < 0 ||
            string.IsNullOrWhiteSpace(Game))
        {
            Debug.WriteLine("Invalid player data");
            return;
        }

        var newPlayer = new Player
        {
            Name = Name,
            Skill = skill,
            Stress = stress,
            Fatigue = fatigue,
            Game = Game,
            Points = 0,
            Money = 0
        };
        
        await _playerService.AddPlayerAsync(newPlayer);
        
        WeakReferenceMessenger.Default.Send(new PlayerAddedMessage());
        
        _playerSigningWindow.Close();
    }
    
    public class PlayerAddedMessage{}
}