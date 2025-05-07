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

public partial class TrainingsViewModel : ViewModelBase, IRecipient<PlayerSelectionViewModel.PlayerTrainedMessage>
{
    [ObservableProperty] private string _title = "Trainings";
    private readonly ITrainingService _trainingService;
    private readonly IPlayerService _playerService;
    [ObservableProperty] private Training _selectedTraining;
    [ObservableProperty] private ObservableCollection<Training> _trainings = new ObservableCollection<Training>();

    public TrainingsViewModel(ITrainingService trainingService, IPlayerService playerService)
    {
        _trainingService = trainingService;
        _playerService = playerService;
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
            vm.InitializeTraining(App.Current.GetService<IPlayerService>(), SelectedTraining);
            playerSelectionWindow.Show();
        }
    }

    public async void Receive(PlayerSelectionViewModel.PlayerTrainedMessage message)
    {
        Debug.WriteLine($"Received training {message.ChosenTraining.Type} for player {message.TrainedPlayer.Name}");

        if (SelectedTraining != null && message.TrainedPlayer != null && message.ChosenTraining != null)
        {
            if (SelectedTraining.Id == message.ChosenTraining.Id)
            {
                await _playerService.TrainPlayerAsync(message.TrainedPlayer, message.ChosenTraining);
                SelectedTraining = null;
                
            }
            else
            {
                Debug.WriteLine("Training is corrupted");
            }
        }
    }
}