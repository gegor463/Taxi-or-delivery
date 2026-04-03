using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class DBConnection : MonoBehaviour
{
    private string _fileName = "PlayersSQLite.db";
    private string _dBPath = "C:\\Users\\Наталья\\Desktop\\Taxi or delivery\\Assets\\StreamingAssets\\PlayersSQLite.db";
    private SqliteConnection _connection;
    private SqliteCommand _command;
    public int lastNumber;

    private void OpenConnection()
    {
        _connection = new SqliteConnection("Data Source=" + _dBPath);
        _command = new SqliteCommand(_connection);
        _connection.Open();
    }

    private void CloseConnection()
    {
        _connection.Close();
        _command.Dispose();
    }

    public void ExecuteQueryWithoutAnswer(string query)
    {
        if (query.Length > 0)
        {
            OpenConnection();
            _command.CommandText = query;
            _command.ExecuteNonQuery();
            CloseConnection();
        }
    }

    public int GetDataFromCars(string columnName,string carName)
    {
        string query = $"SELECT {columnName} FROM Cars WHERE NameOfCar = '{carName}'";
        OpenConnection();
        _command.CommandText = query;
        var result = _command.ExecuteScalar();
        CloseConnection();

        return result != null ? Convert.ToInt32(result) : 0;
    }


}
