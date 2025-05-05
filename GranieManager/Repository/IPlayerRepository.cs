using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;

namespace GranieManager.Repository;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetAllAsync();
    Task<Player> GetByIdAsync(int playerId);
    Task AddAsync(Player player);
    Task UpdateAsync(Player player);
    Task DeleteAsync(int playerId);
}