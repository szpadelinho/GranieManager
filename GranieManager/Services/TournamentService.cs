using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;
using GranieManager.Repository;

namespace GranieManager.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<IEnumerable<Tournament>> GetAllTournamentAsync()
    {
        return await _tournamentRepository.GetAllAsync();
    }

    public async Task<Tournament> GetTournamentByIdAsync(int id)
    {
        return await _tournamentRepository.GetByIdAsync(id);
    }
}