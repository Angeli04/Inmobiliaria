using System.Collections.Generic;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

public class PropietarioRepositorio : RepositorioBase
{
    public PropietarioRepositorio(IConfiguration configuration) : base(configuration) { }

    public IEnumerable<Propietario> GetAll()
    {
        var lista = new List<Propietario>();

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios";
            using (var command = new MySqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Propietario
                    {
                        Id = reader.GetInt32("Id"),
                        DNI = reader.GetString("DNI"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido = reader.GetString("Apellido"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email")
                    });
                }
            }
        }

        return lista;
    }

    public Propietario GetById(int id)
    {
        Propietario propietario = null;

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios WHERE Id = @Id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32("Id"),
                            DNI = reader.GetString("DNI"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email")
                        };
                    }
                }
            }
        }

        return propietario;
    }

    public void Add(Propietario propietario)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = @"INSERT INTO Propietarios (DNI, Nombre, Apellido, Telefono, Email)
                        VALUES (@DNI, @Nombre, @Apellido, @Telefono, @Email);
                        SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@DNI", propietario.DNI);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Email", propietario.Email);

                var newId = Convert.ToInt32(command.ExecuteScalar());
                propietario.Id = newId;
            }
        }
    }

    public void Update(Propietario propietario)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = @"UPDATE Propietarios
                        SET DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email
                        WHERE Id = @Id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", propietario.Id);
                command.Parameters.AddWithValue("@DNI", propietario.DNI);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Email", propietario.Email);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = "DELETE FROM Propietarios WHERE Id = @Id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }

    public Propietario BuscarPorDni(string dni)
    {
        Propietario propietario = null;

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var sql = "SELECT Id, DNI, Nombre, Apellido, Telefono, Email FROM Propietarios WHERE DNI = @Dni LIMIT 1";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Dni", dni);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32("Id"),
                            DNI = reader.GetString("DNI"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email")
                        };
                    }
                }
            }
        }

        return propietario;
    }
}
