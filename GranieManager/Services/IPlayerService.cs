using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;

namespace GranieManager.Services;

public interface IPlayerService
{
    Task<IEnumerable<Player>> GetAllPlayersAsync();
    Task<Player> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task DeletePlayerAsync(int id);
    Task TrainPlayerAsync(Player player, Training training);
    Task JoinTournamentAsync(Player player, Tournament tournament);
}