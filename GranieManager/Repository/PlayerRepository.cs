using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GranieManager.Models;
using Npgsql;

namespace GranieManager.Repository;

public class PlayerRepository : IPlayerRepository
{
    private readonly string _connectionString;

    public PlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Player>("SELECT * FROM Players");
        }
    }

    public async Task<Player> GetByIdAsync(int playerId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Player>("SELECT * FROM Players WHERE Id = @Id", new { Id = playerId });
        }
    }

    public async Task AddAsync(Player player)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("INSERT INTO Players (Name, Skill, Stress, Fatigue, Points, Game, Money) VALUES (@Name, @Skill, @Stress, @Fatigue, @Points, @Game, @Money)", player);
        }
    }

    public async Task UpdateAsync(Player player)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("UPDATE Players SET Name = @Name, Skill = @Skill, Stress = @Stress, Fatigue = @Fatigue, Points = @Points, Game = @Game, Money = @Money WHERE Id = @Id", player);
        }
    }

    public async Task DeleteAsync(int playerId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("DELETE FROM Players WHERE Id = @Id", new { Id = playerId });
        }
    }
}