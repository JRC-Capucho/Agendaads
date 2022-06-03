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


private string addAula = "INSERT INTO grade(professor, aula, inicioaula, local, comochegar, data, terminoaula) VALUES (@pro,@dis,@ini,@loc,@com,@dia,@term)";
    public void adicionarAula(String professor, String disciplina,String inicioaula,String terminoaula, String localAula, String comoChegar, String diaSemana)
    {
        try
        {
            command = new MySqlCommand(addAula, connection);

            command.Parameters.AddWithValue("@pro", professor);
            command.Parameters.AddWithValue("@dis", disciplina);
            command.Parameters.AddWithValue("@ini", inicioaula);
            command.Parameters.AddWithValue("@term", terminoaula);
            command.Parameters.AddWithValue("@loc", localAula);
            command.Parameters.AddWithValue("@com", comoChegar);
            command.Parameters.AddWithValue("@dia", diaSemana);
            
            connection.Open();
            command.ExecuteNonQuery();

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            System.Console.WriteLine("Error " + ex.Message.ToString());
        }
        finally
        {
            MessageBox.Show("Registrado no banco de dados com sucesso!");
            connection.Close();
        }
    }

    private string attAula =
    "update grade set professor=@pro, aula=@dis, inicioaula=@ini, terminoaula=@term, local=@loc, comochegar=@com, data=@dia where aula=@au";

    public void atualizarAula(String professor, String disciplina,String inicioaula,String terminoaula, String localAula, String comoChegar, String diaSemana)
    {
        try
        {
            command = new MySqlCommand(attAula, connection);

            command.Parameters.AddWithValue("@pro", professor);
            command.Parameters.AddWithValue("@dis", disciplina);
            command.Parameters.AddWithValue("@au", disciplina);
            command.Parameters.AddWithValue("@ini", inicioaula);
            command.Parameters.AddWithValue("@term", terminoaula);
            command.Parameters.AddWithValue("@loc", localAula);
            command.Parameters.AddWithValue("@com", comoChegar);
            command.Parameters.AddWithValue("@dia", data);
            
            connection.Open();
            command.ExecuteNonQuery();

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            System.Console.WriteLine("Error " + ex.Message.ToString());
        }
        finally
        {
            MessageBox.Show("Alteração no banco de dados com sucesso!");
            connection.Close();
        }
    }


    private string eraseAula = 
    "delete from grade where aula=@dis";

    public void excluirAula(String disciplina)
    {
        try
        {
            command = new MySqlCommand(eraseAula, connection);

            command.Parameters.AddWithValue("@dis", disciplina);
            
            connection.Open();
            command.ExecuteNonQuery();

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            System.Console.WriteLine("Error " + ex.Message.ToString());
        }
        finally
        {
            MessageBox.Show("Apagou do banco de dados com sucesso!");
            connection.Close();
        }
    }


    private string searchAula =
    "select * from grade where aula=@dis";
    public String[] buscarAula(String disciplina)
    {
        String[] data = new String[7];

        try
        {
            command = new MySqlCommand(searchAula, connection);

            command.Parameters.AddWithValue("@dis", disciplina);

            connection.Open();
            

            reader = command.ExecuteReader();

            while(reader.Read())
            {
                data[0] = Convert.ToString(reader["professor"]); 
                data[1] = Convert.ToString(reader["aula"]); 
                data[2] =  Convert.ToString(reader["inicioaula"]);
                data[3] = Convert.ToString(reader["terminoaula"]); 
                data[4] = Convert.ToString(reader["local"]);
                data[5] = Convert.ToString(reader["comochegar"]);
                data[6] = Convert.ToString(reader["data"]);
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




    public void fecharDB()
    {
        connection.Close();
    }
}