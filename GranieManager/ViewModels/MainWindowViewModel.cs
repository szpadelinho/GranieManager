using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GranieManager.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GranieManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private object? _currentView;
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        CurrentView = _serviceProvider.GetRequiredService<PlayersView>();
    }

    [RelayCommand]
    private void ShowPlayersView()
    {
        CurrentView = _serviceProvider.GetRequiredService<PlayersView>();
    }
    [RelayCommand]
    private void ShowTournamentsView()
    {
        CurrentView = _serviceProvider.GetRequiredService<TournamentsView>();
    }
    
    [RelayCommand]
    private void ShowTrainingsView()
    {
        CurrentView = _serviceProvider.GetRequiredService<TrainingsView>();
    }
}