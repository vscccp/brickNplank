using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private const string ConnectionString = @"Data Source=DESKTOP-2HGT105;Initial Catalog=PlankDB;Integrated Security=True";
        private SqlConnection connection = new SqlConnection(ConnectionString);

        public bool AddUser(User user)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO userTBL (Username, Password) VALUES (@Username, @Password)", connection);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddScore(Scores score)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO scoresTBL (Username, Games_Played, max_score) VALUES (@Username, @GamesPlayed, @HighScore)", connection);
                cmd.Parameters.AddWithValue("@Username", score.Username);
                cmd.Parameters.AddWithValue("@GamesPlayed", score.GamesPlayed);
                cmd.Parameters.AddWithValue("@HighScore", score.HighScore);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM userTBL WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM userTBL", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE userTBL SET Password = @Password WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public Scores GetScoresByUsername(string username)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM scoresTBL WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Scores
                    {
                        Username = reader["Username"].ToString(),
                        GamesPlayed = Convert.ToInt32(reader["Games_Played"]),
                        HighScore = Convert.ToInt32(reader["max_score"])
                    };
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Scores> GetAllScores()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM scoresTBL ORDER BY max_score DESC", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Scores> scoresList = new List<Scores>();
                while (reader.Read())
                {
                    scoresList.Add(new Scores
                    {
                        Username = reader["Username"].ToString(),
                        GamesPlayed = Convert.ToInt32(reader["Games_Played"]),
                        HighScore = Convert.ToInt32(reader["max_Score"])
                    });
                }
                return scoresList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateScores(Scores score)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE scoresTBL SET Games_Played = @GamesPlayed, max_score = @HighScore WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", score.Username);
                cmd.Parameters.AddWithValue("@GamesPlayed", score.GamesPlayed);
                cmd.Parameters.AddWithValue("@HighScore", score.HighScore);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public double GetAverageScore()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT AVG(max_Score) AS AverageScore FROM scoresTBL", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader["AverageScore"] != DBNull.Value)
                {
                    return Convert.ToDouble(reader["AverageScore"]);
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public int GetTotalGamesPlayed()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(Games_Played) AS TotalGamesPlayed FROM scoresTBL", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader["TotalGamesPlayed"] != DBNull.Value)
                {
                    return Convert.ToInt32(reader["TotalGamesPlayed"]);
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public int CalculateTotalGamesPlayed()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(Games_Played) AS TotalGamesPlayed FROM scoresTBL", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader["TotalGamesPlayed"] != DBNull.Value)
                {
                    return Convert.ToInt32(reader["TotalGamesPlayed"]);
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public string PlayerWithMostGames()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Username FROM scoresTBL ORDER BY Games_Played DESC", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader["Username"] != DBNull.Value)
                {
                    return reader["Username"].ToString();
                }
                return "No players found";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error occurred";
            }
            finally
            {
                connection.Close();
            }
        }

        public bool CheckIfUsernameExists(string username)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM userTBL WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool CheckPasswordForUser(string username, string password)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM userTBL WHERE Username = @Username AND Password = @Password", connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
