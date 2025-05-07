using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using GranieManager.Repository;
using GranieManager.Services;
using GranieManager.ViewModels;
using GranieManager.Views;
using GranieManager.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace GranieManager;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    public static IServiceCollection Services { get; private set; }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>(),
            };

            var databaseRepository = _serviceProvider.GetRequiredService<DatabaseRepository>();
            databaseRepository.InitializeDatabase();
        }

        base.OnFrameworkInitializationCompleted();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = """
                                  Host=localhost;
                                  Port=5432;
                                  Username="postgres";
                                  Password="postgres";
                                  Database="granie_manager_db";
                                  """;
        services.AddSingleton<IPlayerRepository, PlayerRepository>(provider => new PlayerRepository(connectionString));
        services.AddSingleton<ITournamentRepository, TournamentRepository>(provider => new TournamentRepository(connectionString));
        services.AddSingleton<ITrainingRepository, TrainingRepository>(provider => new TrainingRepository(connectionString));
        
        services.AddSingleton<IPlayerService, PlayerService>();
        services.AddSingleton<ITournamentService, TournamentService>();
        services.AddSingleton<ITrainingService, TrainingService>();
        
        services.AddTransient<PlayersViewModel>();
        services.AddTransient<TournamentsViewModel>();
        services.AddTransient<TrainingsViewModel>();
        services.AddTransient<PlayerSigningViewModel>();
        services.AddTransient<PlayerSelectionViewModel>();
        services.AddTransient<PlayerDetailsViewModel>();
        services.AddTransient<TournamentDetailsViewModel>();
        services.AddTransient<TrainingDetailsViewModel>();
        
        services.AddTransient<PlayersView>(provider =>
        {
            var view = new PlayersView();
            view.DataContext = provider.GetRequiredService<PlayersViewModel>();
            return view;
        });

        services.AddTransient<TournamentsView>(provider =>
        {
            var view = new TournamentsView();
            view.DataContext = provider.GetRequiredService<TournamentsViewModel>();
            return view;
        });

        services.AddTransient<TrainingsView>(provider =>
        {
            var view = new TrainingsView();
            view.DataContext = provider.GetRequiredService<TrainingsViewModel>();
            return view;
        });

        services.AddSingleton<MainWindowViewModel>();
        
        services.AddSingleton<DatabaseRepository>(provider => new DatabaseRepository(connectionString));

        services.AddTransient<PlayerSigning>();
        services.AddTransient<PlayerSelection>();
        services.AddTransient<PlayerDetails>();
        services.AddTransient<TournamentDetails>();
        services.AddTransient<TrainingDetails>();
    }
    
    public new static App Current => (App)Application.Current;
    public T GetService<T>() => _serviceProvider.GetRequiredService<T>();

    public App()
    {
        Services = new ServiceCollection();
        ConfigureServices(Services);
        _serviceProvider = Services.BuildServiceProvider();
    }
}