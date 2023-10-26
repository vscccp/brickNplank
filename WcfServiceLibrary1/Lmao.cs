using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{

    public class IlmaoService : Ilmao
    {
        private const string ConnectionString = @"Data Source=DESKTOP-2HGT105;Initial Catalog=shopDB;Integrated Security=True";
        private SqlConnection connection = new SqlConnection(ConnectionString);

        public bool AddUser(User user)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", connection);
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Scores (Username, GamesPlayed, HighScore) VALUES (@Username, @GamesPlayed, @HighScore)", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", connection);
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
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @Password WHERE Username = @Username", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Scores WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Scores
                    {
                        Username = reader["Username"].ToString(),
                        GamesPlayed = Convert.ToInt32(reader["GamesPlayed"]),
                        HighScore = Convert.ToInt32(reader["HighScore"])
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Scores", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Scores> scoresList = new List<Scores>();
                while (reader.Read())
                {
                    scoresList.Add(new Scores
                    {
                        Username = reader["Username"].ToString(),
                        GamesPlayed = Convert.ToInt32(reader["GamesPlayed"]),
                        HighScore = Convert.ToInt32(reader["HighScore"])
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
                SqlCommand cmd = new SqlCommand("UPDATE Scores SET GamesPlayed = @GamesPlayed, HighScore = @HighScore WHERE Username = @Username", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT AVG(HighScore) AS AverageScore FROM Scores", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT SUM(GamesPlayed) AS TotalGamesPlayed FROM Scores", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT SUM(GamesPlayed) AS TotalGamesPlayed FROM Scores", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Username FROM Scores ORDER BY GamesPlayed DESC", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password", connection);
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
