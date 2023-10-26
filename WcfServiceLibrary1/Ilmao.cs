using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    [ServiceContract]
    public interface Ilmao
    {
        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool AddScore(Scores score);

        [OperationContract]
        User GetUserByUsername(string username);

        [OperationContract]
        List<User> GetAllUsers();
        
        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        Scores GetScoresByUsername(string username);

        [OperationContract]
        List<Scores> GetAllScores();

        [OperationContract]
        bool UpdateScores(Scores score);

        [OperationContract]
        double GetAverageScore();

        [OperationContract]
        int GetTotalGamesPlayed();

        [OperationContract]
        int CalculateTotalGamesPlayed();

        [OperationContract]
        string PlayerWithMostGames();

        [OperationContract]
        bool CheckIfUsernameExists(string username);

        [OperationContract]
        bool CheckPasswordForUser(string username, string password);
    }
}
