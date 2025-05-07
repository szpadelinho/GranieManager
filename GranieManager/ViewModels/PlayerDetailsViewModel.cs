using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;

namespace GranieManager.ViewModels;

public partial class PlayerDetailsViewModel : ViewModelBase
{
    [ObservableProperty] private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}