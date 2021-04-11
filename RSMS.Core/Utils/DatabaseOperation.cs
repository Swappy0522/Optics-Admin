using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
/// <summary>
/// DatabaseOperation Class : This Class Contains Function Or Method that does CRUD Operation. 
/// </summary>
public class DatabaseOperation : IDisposable
{
    static SqlConnection ObjConnection = new SqlConnection(@"Data Source=DESKTOP-F1L32QB;Initial Catalog=OPTICS;Integrated Security=True");
    static SqlCommand ObjCommand = new SqlCommand();

    public DatabaseOperation()
    {
        ObjCommand = new SqlCommand();
        ObjCommand.Connection = ObjConnection;
        ObjCommand.CommandType = CommandType.StoredProcedure;
    }
    /// <summary>
    /// ManageConnection Function will open or close connection based on Database operation
    /// </summary>
    /// <param name="TrueOrFalse">This parameter will decide whether the connection needs to be open or closed.</param>
    public static void ManageConnection(Boolean TrueOrFalse)
    {

        switch (TrueOrFalse)
        {
            case true:
                if (ObjConnection.State == ConnectionState.Closed)
                {
                    ObjConnection.Open();
                }
                break;

            case false:
                if (ObjConnection.State == ConnectionState.Open)
                {
                    ObjConnection.Close();
                }
                break;
        }
    }

  
    /// <summary>
    /// This Function will Performed DML Operation
    /// No Overload Method available for this Method.
    /// </summary>
    /// <param name="dbParam"></param>
    /// <param name="storedProcedure"></param>
    /// <returns></returns>
    public static int InsertUpdateDeleteOperation(Hashtable dbParam, String storedProcedure)
    {
        int rowAffected = 0;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            foreach (string key in dbParam.Keys)
            {
                ObjCommand.Parameters.AddWithValue(key, dbParam[key]);
            }
            DatabaseOperation.ManageConnection(true);
            rowAffected = ObjCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "InsertUpdateDeleteOperation", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);
        }
        return rowAffected;

    }

    /// <summary>
    /// Dataset Represent In memory Cache of Data  (+4 Overload(s))
    /// Fetch data from database 
    /// </summary>
    /// <param name="storedProcedure"></param>
    /// <returns></returns>
    public static DataSet SelectDataFromDatabase(String storedProcedure)
    {

        DataSet DsResult = new DataSet();
        SqlDataAdapter Adapt = null;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            Adapt = new SqlDataAdapter(ObjCommand);
            DatabaseOperation.ManageConnection(true);
            Adapt.Fill(DsResult);
            DatabaseOperation.ManageConnection(false);

        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "SelectDataFromDatabase(String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);            
        }
        return DsResult;
    }

    /// <summary>
    /// Dataset Represent In memory Cache of Data  (+4 Overload(s))
    /// Fetch data from database using parameter 
    /// </summary>
    /// <param name="storedProcedure"></param>
    /// <returns></returns>
    public static DataSet SelectDataFromDatabase(Hashtable dbParam, String storedProcedure)
    {
        DataSet dsResult = new DataSet();
        SqlDataAdapter Adapt = null;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            foreach (string key in dbParam.Keys)
            {
                ObjCommand.Parameters.AddWithValue(key, dbParam[key]);
            }
            Adapt = new SqlDataAdapter(ObjCommand);
            DatabaseOperation.ManageConnection(true);
            Adapt.Fill(dsResult);
            DatabaseOperation.ManageConnection(false);
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "SelectDataFromDatabase(Hashtable dbParam, String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);
            //dbOperation.Dispose();
        }
        return dsResult;
    }

    /// <summary>
    /// SqlDataReader Represent In memory Cache of Data  (+4 Overload(s))
    /// Providing way of reading a forwardonly stream of rows from a SQL Server Database.
    /// </summary>
    /// <param name="storedProcedure"></param>
    /// <returns></returns>
    public static SqlDataReader SelectDataFromDatabaseUsingReader(String storedProcedure)
    {
        SqlDataReader Sqlreader = null;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            DatabaseOperation.ManageConnection(true);
            Sqlreader = ObjCommand.ExecuteReader();            
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "SelectDataFromDatabase(String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
           
        }
        return Sqlreader;
    }

    public static String GetSingleValueFromDataabse(String storedProcedure)
    {
        String value = string.Empty;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            DatabaseOperation.ManageConnection(true);
            value = Convert.ToString(ObjCommand.ExecuteScalar());
            DatabaseOperation.ManageConnection(false);
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "GetSingleValueFromDataabse(String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);
        }
        return value;
    }


    public static Boolean ValueAlreadyExist(Hashtable dbParam, String storedProcedure)
    {
        bool isexist = false;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            foreach (string key in dbParam.Keys)
            {
                ObjCommand.Parameters.AddWithValue(key, dbParam[key]);
            }
            DatabaseOperation.ManageConnection(true);
            if (Convert.ToString(ObjCommand.ExecuteScalar()) != "0")
            {
                isexist = true;
            }
            DatabaseOperation.ManageConnection(false);
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "ValueAlreadyExist(Hashtable dbParam, String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);           
        }
        return isexist;
    }

    public static String GetSingleValueFromDataabse(Hashtable dbParam, String storedProcedure)
    {
        String value = string.Empty;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            foreach (string key in dbParam.Keys)
            {
                ObjCommand.Parameters.AddWithValue(key, dbParam[key]);
            }
            DatabaseOperation.ManageConnection(true);
            value = Convert.ToString(ObjCommand.ExecuteScalar());
            DatabaseOperation.ManageConnection(false);
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "GetSingleValueFromDataabse(Hashtable dbParam, String storedProcedure)", "DatabaseOperation");
        }
        finally
        {
            DatabaseOperation.ManageConnection(false);
        }
        return value;
    }

    /// <summary>
    /// SqlDataReader Represent In memory Cache of Data  (+4 Overload(s))
    /// Providing way of reading a forwardonly stream of rows from a SQL Server Database with parameter.
    /// </summary>
    /// <param name="storedProcedure"></param>
    /// <returns></returns>
    public static SqlDataReader SelectDataFromDatabaseUsingReader(Hashtable dbParam, String storedProcedure)
    {
        SqlDataReader Sqlreader = null;
        try
        {
            ObjCommand = new SqlCommand(storedProcedure, ObjConnection);
            ObjCommand.CommandType = CommandType.StoredProcedure;
            foreach (string key in dbParam.Keys)
            {
                ObjCommand.Parameters.AddWithValue(key, dbParam[key]);
            }
            DatabaseOperation.ManageConnection(true);
            Sqlreader = ObjCommand.ExecuteReader();
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "SelectDataFromDatabase(Hashtable dbParam, String storedProcedure)", "DatabaseOperation");
        }
        finally
        {

        }
        return Sqlreader;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public static string base64Encode(string Pass)
    {
        string encodedData = string.Empty;
        try
        {
            byte[] encData_byte = new byte[Pass.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);

            encodedData = Convert.ToBase64String(encData_byte);
        }
        catch (Exception ex)
        {
            Utility.LogError(ex.Message, ex.ToString(), "base64Encode", "DatabaseOperation");
        }
        finally
        {

        }
        return encodedData;

    }


}
