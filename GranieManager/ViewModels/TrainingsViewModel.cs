using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GranieManager.Models;
using GranieManager.Services;

namespace GranieManager.ViewModels;

public partial class TrainingsViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = "Trainings";
    private readonly ITrainingService _trainingService;
    [ObservableProperty] private ObservableCollection<Training> _trainings = new ObservableCollection<Training>();

    public TrainingsViewModel(ITrainingService trainingService)
    {
        _trainingService = trainingService;
        LoadTrainingAsync();
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
}