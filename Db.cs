using System;
using MySql.Data.MySqlClient;

namespace AgendaADS;

public class Db
{
    static string connectionString = "server=localhost; user=root; database=agendaads; port=3306; password=";
    static MySqlConnection connection = new MySqlConnection(connectionString);

    static MySqlCommand? command;
    static MySqlDataReader? reader;

    private string data;
    private TimeOnly hora; 
    private DateOnly dataAula;
    private string horarioDaAula;
    private string diaDaAula;

    private string aula = "select * from grade where inicioaula=@t and data=@d;";

    public string msgAula()
    {
        try
        {
            command = new MySqlCommand(aula, connection);

            command.Parameters.AddWithValue("@t", horarioDaAula);
            command.Parameters.AddWithValue("@d", diaDaAula);
            
            connection.Open();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                data = 
                "Professor: " + Convert.ToString(reader["professor"]) + "\n" + 
                "Disciplina: " + Convert.ToString(reader["aula"]) + "\n" +
                "Inicio da aula: " + Convert.ToString(reader["inicioaula"]) + "\n" +
                "Termino da aula: " + Convert.ToString(reader["terminoaula"]) + "\n" +
                "Local da aula: " + Convert.ToString(reader["local"]) + "\n" +
                "Como chegar: " + Convert.ToString(reader["comochegar"]);
            }
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            System.Console.WriteLine("Error " + ex.Message.ToString());
        }
        finally
        {
            connection.Close();
        }
        return data;
    }

    private string tempoAula = "select inicioaula, data from grade;";

    public Boolean inicioDaAula()
    {
        hora = TimeOnly.FromDateTime(DateTime.Now);
        dataAula = DateOnly.FromDateTime(DateTime.Now);

        
        string aux = hora.ToString();
        string auxdata = dataAula.DayOfWeek.ToString().ToLower();

        try
        {
            command = new MySqlCommand(tempoAula, connection);
            connection.Open();

            reader = command.ExecuteReader();

            while(reader.Read())
            {
                horarioDaAula = Convert.ToString(reader["inicioaula"]);                
                diaDaAula = Convert.ToString(reader["data"]);

                    if(horarioDaAula.Equals(aux) && auxdata.Equals(diaDaAula) )
                    {
                        connection.Close();
                        return true;
                    }
            }
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            System.Console.WriteLine("Error " + ex.Message.ToString());
        }
        finally
        {
            connection.Close();
        }
        return false;
    }

    public void fecharDB()
    {
        connection.Close();
    }
}