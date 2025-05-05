using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;
using GranieManager.Repository;

namespace GranieManager.Services;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;

    public TrainingService(ITrainingRepository trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }


    public async Task<IEnumerable<Training>> getAllTrainingsAsync()
    {
        return await _trainingRepository.GetAllAsync();
    }

    public async Task<Training> getTrainingByIdAsync(int id)
    {
        return await _trainingRepository.GetByIdAsync(id);
    }
}