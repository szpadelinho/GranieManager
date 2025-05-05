using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;
using GranieManager.Repository;

namespace GranieManager.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ITournamentService _tournamentRepository;
    private readonly ITrainingRepository _trainingRepository;
    private readonly Random _random = new Random();

    public PlayerService(IPlayerRepository playerRepository, ITournamentService tournamentRepository,
        ITrainingRepository trainingRepository)
    {
        _playerRepository = playerRepository;
        _tournamentRepository = tournamentRepository;
        _trainingRepository = trainingRepository;
    }

    public async Task<IEnumerable<Player>> GetAllPlayersAsync()
    {
        return await _playerRepository.GetAllAsync();
    }

    public async Task<Player> GetPlayerByIdAsync(int id)
    {
        return await _playerRepository.GetByIdAsync(id);
    }

    public async Task AddPlayerAsync(Player player)
    {
        await _playerRepository.AddAsync(player);
    }

    public async Task UpdatePlayerAsync(Player player)
    {
        await _playerRepository.UpdateAsync(player);
    }

    public async Task DeletePlayerAsync(int id)
    {
        await _playerRepository.DeleteAsync(id);
    }

    public async Task TrainPlayerAsync(Player player, Training training)
    {
        player.Skill += training.SkillIncrease;
        player.Fatigue += Math.Min(100, player.Fatigue + training.FatigueIncrease);
        player.Stress = Math.Min(100, player.Stress + training.StressIncrease);
        await _playerRepository.UpdateAsync(player);
    }

    public async Task JoinTournamentAsync(Player player, Tournament tournament)
    {
        if (player.Skill >= tournament.MinSkillRequired)
        {
            int randomNumber = _random.Next(1, 201);

            if (randomNumber <= player.Skill)
            {
                player.Points += 100;
                player.Money += tournament.Prize;
            }
            else
            {
                player.Points -= 10;
            }
            
            player.Fatigue += 10;
            player.Stress += 10;
        
            await _playerRepository.UpdateAsync(player);
        }
        else
        {
            throw new Exception($"Player {player.Skill} is out of range");
        }
    }
}