using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

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

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary1.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
