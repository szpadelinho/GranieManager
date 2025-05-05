using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranieManager.Models;

namespace GranieManager.Services;

public interface ITrainingService
{
    Task<IEnumerable<Training>> getAllTrainingsAsync();
    Task<Training> getTrainingByIdAsync(int id);
}