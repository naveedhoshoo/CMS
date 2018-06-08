using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using System.Data;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;

namespace ClaimManagement
{
    public class dal
    {
        private OracleConnection con;
        private OracleCommand cmd;
        private OracleDataAdapter adp;


        string constr = System.Configuration.ConfigurationManager.AppSettings["constr"];

        public bool getData(string query, DataTable dt)
        {

            con = new OracleConnection(constr);
            cmd = new OracleCommand(query);
            cmd.Connection = con;
            adp = new OracleDataAdapter(cmd);
            
            con.Open();
            adp.Fill(dt);
            con.Close();
            return true;
        }


        public bool isValidLogin(string username, string password)
        {

            string constr = System.Configuration.ConfigurationManager.AppSettings["constr"];



            con = new OracleConnection(constr);

            bool validlogin = false;

            DataSet dataset = new DataSet();

            using (OracleConnection objConn = new OracleConnection(constr))

            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "HIL.Login_user";

                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("LOGIN_NAME", OracleDbType.NVarchar2).Value = username;

                objCmd.Parameters.Add("LOGIN_PASS", OracleDbType.NVarchar2).Value = password;

                objCmd.Parameters.Add("PRC", OracleDbType.RefCursor).Direction = ParameterDirection.Output;



                try

                {

                    objConn.Open();

                    objCmd.ExecuteNonQuery();

                    OracleDataAdapter da = new OracleDataAdapter(objCmd);

                    da.Fill(dataset);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {

                        validlogin = true;

                    }

                }

                catch (Exception ex)

                {

                    System.Console.WriteLine("Exception: {0}", ex.ToString());

                }

                objConn.Close();

            }

            return validlogin;

        }


        //public bool insertData(string query) //all data from a table
        //{

        //    //con = new SqlConnection(constr);
        //    //cmd = new SqlCommand(query);
        //    //cmd.Connection = con;
        //    //con.Open();
        //    //cmd.ExecuteNonQuery();
        //    //con.Close();
        //    //return true;
        //}


    }
}
