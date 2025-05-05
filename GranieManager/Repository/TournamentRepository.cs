using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GranieManager.Models;
using Npgsql;

namespace GranieManager.Repository;

public class TournamentRepository : ITournamentRepository
{
    private readonly string _connectionString;

    public TournamentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Tournament>> GetAllAsync()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Tournament>("SELECT * FROM tournaments");
        }
    }

    public async Task<Tournament> GetByIdAsync(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Tournament>("SELECT * FROM tournaments WHERE id = @id", new { id });
        }
    }

    public async Task AddAsync(Tournament tournament)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("INSERT INTO Tournaments (Name, Entry, Prize, MinSkillRequired) VALUES (@Name, @Entry, @Prize, @MinSkillRequired)", tournament);
        }
    }

    public async Task UpdateAsync(Tournament tournament)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("UPDATE Tournaments SET Name = @Name, Entry = @Entry, Prize = @Prize, MinSkillRequired = @MinSkillRequired WHERE Id = @Id", tournament);
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("DELETE FROM Tournaments WHERE Id = @Id", new { Id = id });   
        }
    }
}