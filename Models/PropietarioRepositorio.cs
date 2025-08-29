using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

public class PropietarioRepositorio : RepositorioBase
{
    public PropietarioRepositorio(IConfiguration configuration) : base(configuration) { }

    public IEnumerable<Propietario> GetAll()
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios";
        return connection.Query<Propietario>(sql).ToList();
    }

    public Propietario GetById(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios WHERE Id = @Id";
        return connection.QueryFirstOrDefault<Propietario>(sql, new { Id = id });
    }

    public void Add(Propietario propietario)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = @"INSERT INTO Propietarios (DNI, Nombre, Apellido, Telefono, Email)
                    VALUES (@DNI, @Nombre, @Apellido, @Telefono, @Email);
                    SELECT LAST_INSERT_ID();";
        var newId = connection.ExecuteScalar<int>(sql, propietario);
        propietario.Id = newId;
    }

    public void Update(Propietario propietario)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = @"UPDATE Propietarios
                    SET DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email
                    WHERE Id = @Id";
        connection.Execute(sql, propietario);
    }

    public void Delete(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = "DELETE FROM Propietarios WHERE Id = @Id";
        connection.Execute(sql, new { Id = id });
    }

    public Propietario BuscarPorDni(string dni)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios WHERE DNI = @Dni LIMIT 1";
        return connection.QueryFirstOrDefault<Propietario>(sql, new { Dni = dni });
    }
}
