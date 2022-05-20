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
    private DateOnly dataAula = DateOnly.FromDateTime(DateTime.Now);
    private string aula = "select * from grade where inicioaula=@t, data=@o;";

    public string msgAula(string aux)
    {
        try
        {
            command = new MySqlCommand(aula, connection);

            command.Parameters.AddWithValue("@t", aux);
            command.Parameters.AddWithValue("@o", aux1)

            connection.Open();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                data += 
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

    public string inicioDaAula()
    {
        hora = TimeOnly.FromDateTime(DateTime.Now);


        string horarioDaAula;
        string aux = hora.ToString();
        string auxdata = dataAula.ToString();
        string auxdata1;

        try
        {
            command = new MySqlCommand(tempoAula, connection);
            connection.Open();

            reader = command.ExecuteReader();

            while(reader.Read())
            {
                horarioDaAula = Convert.ToString(reader["inicioaula"]);
                auxdata1 = convert.ToString(reader["data"]);

                if(aux.Length > 7)
                    if(horarioDaAula.Equals(aux.Substring(0,5)) && auxdata.Equals(auxdata1))
                    {
                        connection.Close();
                        return horarioDaAula;
                    }
                else
                {
                    if(horarioDaAula.Equals(aux.Substring(0,4)) && auxdata.Equals(auxdata1))
                    {
                        connection.Close();
                        return horarioDaAula;
                    }
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
        return null;
    }

    public void fecharDB()
    {
        connection.Close();
    }
}