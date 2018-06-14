using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using System.Data;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;
using CMS.Common.Db;
using CMS;

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


        public LoggedUser isValidLogin(string username, string password)
        {

            string constr = System.Configuration.ConfigurationManager.AppSettings["constr"];



            con = new OracleConnection(constr);

            LoggedUser user = null;

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

                        user = new LoggedUser();
                        user.UserID = dataset.Tables[0].Rows[0]["USERID"].ToString();
                        user.Username    = dataset.Tables[0].Rows[0]["USERNAME"].ToString();
                        user.UserType = dataset.Tables[0].Rows[0]["USERTYPE"].ToString();
                        user.PageAllow = dataset.Tables[0].Rows[0]["PAGEALLOW"].ToString();
                        user.UserLocations = new List<Locations>();

                        for (int i = 0; i < dataset.Tables[0].Rows.Count; i++) {
                            Locations loc = new Locations();
                            loc.POR_ORGACODE = dataset.Tables[0].Rows[i]["POR_ORGACODE"].ToString();
                            loc.POR_LOCACODE = dataset.Tables[0].Rows[i]["PLC_LOCACODE"].ToString();
                            Branches branch = new Branches();
                            branch.LOCADesc  = dataset.Tables[0].Rows[i]["PLC_LOCADESC"].ToString();
                            branch.LOCADesc = dataset.Tables[0].Rows[i]["PLC_LOCASHORT"].ToString();
                            loc.User_Branches = new List<Branches>();
                            loc.User_Branches.Add(branch);
                            user.UserLocations.Add(loc);
                        }


                    }

                }

                catch (Exception ex)

                {

                    System.Console.WriteLine("Exception: {0}", ex.ToString());

                }

                objConn.Close();

            }

            return user;

        }

        //HIL.GetDashBoardData

        public DataTable GetDashBoardData()
        {

            string constr = System.Configuration.ConfigurationManager.AppSettings["constr"];



            con = new OracleConnection(constr);

            LoggedUser user = null;

            DataTable dataset = new DataTable();

            using (OracleConnection objConn = new OracleConnection(constr))

            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "HIL.GetDashBoardData";

                objCmd.CommandType = CommandType.StoredProcedure;

                
                objCmd.Parameters.Add("PRC", OracleDbType.RefCursor).Direction = ParameterDirection.Output;



                try

                {

                    objConn.Open();

                    objCmd.ExecuteNonQuery();

                    OracleDataAdapter da = new OracleDataAdapter(objCmd);




                    da.Fill(dataset);

                }

                catch (Exception ex)

                {

                    System.Console.WriteLine("Exception: {0}", ex.ToString());

                }

                objConn.Close();

            }

            return dataset;

        }

        public DataSet1 GetPolicyData()
        {

            string constr = System.Configuration.ConfigurationManager.AppSettings["constr"];



            con = new OracleConnection(constr);

            LoggedUser user = null;
            DataSet1   dataset = new DataSet1();

            using (OracleConnection objConn = new OracleConnection(constr))

            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "HIL.PolicyDetails";

                objCmd.CommandType = CommandType.StoredProcedure;


                objCmd.Parameters.Add("PRC", OracleDbType.RefCursor).Direction = ParameterDirection.Output;



                try

                {

                    objConn.Open();

                    objCmd.ExecuteNonQuery();

                    OracleDataAdapter da = new OracleDataAdapter(objCmd);




                    da.Fill(dataset);

                }

                catch (Exception ex)

                {

                    System.Console.WriteLine("Exception: {0}", ex.ToString());

                }

                objConn.Close();

            }

            return dataset;

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
