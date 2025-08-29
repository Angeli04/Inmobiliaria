using System.Collections.Generic;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using Inmobiliaria.Models;

public class InquilinoRepositorio : RepositorioBase
{
    public InquilinoRepositorio(IConfiguration configuration) : base(configuration) { }

    public IEnumerable<Inquilino> GetAll()
    {
        var lista = new List<Inquilino>();

        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos;";
            using (var cmd = new MySqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Inquilino
                    {
                        IdInquilino = reader.GetInt32("IdInquilino"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido = reader.GetString("Apellido"),
                        Dni = reader.GetString("Dni"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email")
                    });
                }
            }
        }

        return lista;
    }

    public Inquilino GetById(int id)
    {
        Inquilino inquilino = null;

        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos WHERE IdInquilino = @Id;";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inquilino = new Inquilino
                        {
                            IdInquilino = reader.GetInt32("IdInquilino"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email")
                        };
                    }
                }
            }
        }

        return inquilino;
    }

    public void Add(Inquilino inquilino)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = @"
                INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email)
                VALUES (@Nombre, @Apellido, @Dni, @Telefono, @Email);
                SELECT LAST_INSERT_ID();";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
                cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                cmd.Parameters.AddWithValue("@Email", inquilino.Email);

                var newId = Convert.ToInt32(cmd.ExecuteScalar());
                inquilino.IdInquilino = newId;
            }
        }
    }

    public void Update(Inquilino inquilino)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = @"
                UPDATE Inquilinos
                SET Nombre = @Nombre,
                    Apellido = @Apellido,
                    Dni = @Dni,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE IdInquilino = @IdInquilino;";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdInquilino", inquilino.IdInquilino);
                cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
                cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                cmd.Parameters.AddWithValue("@Email", inquilino.Email);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "DELETE FROM Inquilinos WHERE IdInquilino = @Id;";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public IEnumerable<Inquilino> BuscarPorApellido(string apellido)
    {
        var lista = new List<Inquilino>();

        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT IdInquilino, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos WHERE Apellido LIKE CONCAT('%', @Apellido, '%');";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Inquilino
                        {
                            IdInquilino = reader.GetInt32("IdInquilino"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email")
                        });
                    }
                }
            }
        }

        return lista;
    }
}