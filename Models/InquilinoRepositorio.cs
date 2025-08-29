
using Dapper;
using Inmobiliaria.Models;
using MySqlConnector;

public class InquilinoRepositorio : RepositorioBase
{
    public InquilinoRepositorio(IConfiguration configuration) : base(configuration) { }

    public IEnumerable<Inquilino> GetAll()
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos;";
        return conn.Query<Inquilino>(sql).ToList();
    }

    public Inquilino GetById(int id)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos WHERE IdInquilino = @Id;";
        return conn.QueryFirstOrDefault<Inquilino>(sql, new { Id = id });
    }

    public void Add(Inquilino inquilino)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = @"
                INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email)
                VALUES (@Nombre, @Apellido, @Dni, @Telefono, @Email);
                SELECT LAST_INSERT_ID();";
        var newId = conn.ExecuteScalar<int>(sql, inquilino);
        inquilino.IdInquilino = newId;
    }

    public void Update(Inquilino inquilino)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = @"
                UPDATE Inquilinos
                SET Nombre = @Nombre,
                    Apellido = @Apellido,
                    Dni = @Dni,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE IdInquilino = @IdInquilino;";
        conn.Execute(sql, inquilino);
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = "DELETE FROM Inquilinos WHERE IdInquilino = @Id;";
        conn.Execute(sql, new { Id = id });
    }

    public IEnumerable<Inquilino> BuscarPorApellido(string apellido)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos WHERE Apellido LIKE CONCAT('%', @Apellido, '%');";
        return conn.Query<Inquilino>(sql, new { Apellido = apellido }).ToList();
    }
}