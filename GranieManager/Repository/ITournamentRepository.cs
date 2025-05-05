using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;

namespace GranieManager.Repository;

public interface ITournamentRepository
{
    Task<IEnumerable<Tournament>> GetAllAsync();
    Task<Tournament> GetByIdAsync(int id);
    Task AddAsync(Tournament tournament);
    Task UpdateAsync(Tournament tournament);
    Task DeleteAsync(int tournamentId);
}