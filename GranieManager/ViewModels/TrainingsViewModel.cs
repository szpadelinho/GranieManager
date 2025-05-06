using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GranieManager.Models;
using GranieManager.Services;
using GranieManager.Windows;

namespace GranieManager.ViewModels;

public partial class TrainingsViewModel : ViewModelBase, IRecipient<TrainingsViewModel.PlayerSelectedMessage>
{
    [ObservableProperty] private string _title = "Trainings";
    private readonly ITrainingService _trainingService;
    [ObservableProperty] private Training _selectedTraining;
    [ObservableProperty] private ObservableCollection<Training> _trainings = new ObservableCollection<Training>();

    public TrainingsViewModel(ITrainingService trainingService)
    {
        _trainingService = trainingService;
        LoadTrainingAsync();
        WeakReferenceMessenger.Default.Register(this);
    }

    public async void LoadTrainingAsync()
    {
        var trainings = await _trainingService.getAllTrainingsAsync();
        Trainings.Clear();
        foreach (var training in trainings)
        {
            Trainings.Add(training);
        }
    }

    [RelayCommand]
    private void OpenPlayerSelectionWindow()
    {
        if (SelectedTraining == null)
        {
            Debug.WriteLine("No selected training");
            return;
        }
        
        Debug.WriteLine($"Selected training: {SelectedTraining.Type}");
        
        var playerSelectionWindow = App.Current.GetService<PlayerSelection>();
        
        if (playerSelectionWindow == null)
        {
            Debug.WriteLine("PlayerSelection window is null");
            return;
        }
        
        if(playerSelectionWindow.DataContext is PlayerSelectionViewModel vm)
        {
            Debug.WriteLine("Selected player selection window is called");
            vm.Initialize(App.Current.GetService<IPlayerService>(), SelectedTraining);
            playerSelectionWindow.Show();
        }
    }

    public void Receive(PlayerSelectedMessage message)
    {
        if (SelectedTraining != null && message.SelectedPlayer != null)
        {
            Debug.WriteLine("Received player selected training");
            Train(SelectedTraining, message.SelectedPlayer);
            SelectedTraining = null;
        }
    }

    private async void Train(Training training, Player player)
    {
        
    }

    public class PlayerSelectedMessage
    {
        public Player SelectedPlayer { get; }

        public PlayerSelectedMessage(Player selectedPlayer)
        {
            SelectedPlayer = selectedPlayer;
        }
    }
}