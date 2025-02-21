using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;

using MySql.Data.MySqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ReactWebApplication.Data;


namespace ReactWebApplication
{


    public class Utility
    {
     
        public static  MySqlConnection con;
        private static MySqlCommand cmd;
        public static MySqlTransaction Trans;
        public static MySqlDataReader Rdr;
        public static MySqlDataAdapter da;
      
       static Utility()
        {
            try
            {
                Class.Users.ConString=ReactWebApplication.Class.Users.DSOURCE;

                try
                {
                    string[] data = Class.Users.ConString.Split(',');
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con = new MySqlConnection(data[0]);
                            con.Open();
                        }
                    }
                    else
                    {
                        con = new MySqlConnection(data[0]);
                        con.Open();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static MySqlConnection Connect()
        {
            
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
           
            return con;
        }
        public static MySqlConnection DisConnect()
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
               
            }

            return con;
        }
        public async  Task<DataTable> select(string qry, string tbl)
        {
            DataSet ds =await Utility.ExecuteSelectQuery(qry, tbl);
            DataTable dt = ds.Tables[tbl];
            return dt;
        }
        public static  async Task<DataSet> ExecuteSelectQuery(string query, string tblname)
        {
            DataSet ds = new DataSet();
            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Trans = Utility.con.BeginTransaction();
                da = new MySqlDataAdapter(query, con);               
               await Task.Run(() => da.Fill(ds, tblname));
                Trans.Commit();
            }
            catch (Exception ex) { Trans.Rollback(); }
            finally {  }
            return ds;
        }


        public static async Task ExecuteNonQuery(string query)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Trans = Utility.con.BeginTransaction();
                cmd = new MySqlCommand(query, con, Trans);
               
                    await cmd.ExecuteNonQueryAsync();
                Trans.Commit();
            }catch(Exception ex) {Trans.Rollback();}
            finally { DisConnect(); }
          
           
            
        }

        //public static void ExecuteNonQueryy(string query)
        //{
        //    cmd = new MySqlCommand(query, con);
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        Connect();
        //    }
        //    cmd.Transaction = Trans;
        //    cmd.CommandTimeout = 360;
        //    cmd.ExecuteNonQuery();
     
        //    // return true;
        //}
        public static async Task<bool> ExecuteNonQuery(string query, Byte[] s)
        {
            using (var tran = Utility.con.BeginTransaction())
                cmd = new MySqlCommand(query, con, tran);
            cmd.Parameters.AddWithValue(":EMPIMAGE", s);
            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch
            {
                Trans.Rollback();
                throw;
            }
            Trans.Commit();

            return true;
            //if (con.State == ConnectionState.Closed)
            //{
            //    Connect();
            //}
            //cmd.CommandTimeout = 360;
            //cmd = new MySqlCommand(query, con);
            //cmd.Parameters.AddWithValue(":EMPIMAGE", s);
            //cmd.ExecuteNonQuery();

            //DisConnect();
            //return true;
        }
        public static MySqlDataReader ExecuteReader(string query)
        {
            //if (con.State == ConnectionState.Closed)
            //{
            //    Connect();
            //}
            cmd.CommandTimeout = 360;
            cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            //DisConnect();
            return dr;
        }
        public static object ExecuteScalar(string MySql)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            cmd.CommandTimeout = 360;
            cmd = new MySqlCommand(MySql, con);
            object scalarValue = cmd.ExecuteScalar();
            DisConnect();
            return scalarValue;

        }
        public static DataTable MySqlQuery(string MySql, Hashtable ParamTable = null)
        {
            MySqlCommand CmdData = new MySqlCommand();
         
            MySqlDataAdapter Sda = new MySqlDataAdapter();
            DataSet Ds = new DataSet();
           // Connect();
            CmdData.CommandText = MySql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            CmdData.CommandTimeout = 180;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                }
            }

            Sda = new MySqlDataAdapter(CmdData);
            Sda.Fill(Ds, "tabresult");
            DataTable MySqlQuery = new DataTable();
            MySqlQuery.Clear();
            MySqlQuery = Ds.Tables["tabresult"];
            return MySqlQuery;
        }

        public static void MySqlNonQuery(string MySql, Hashtable ParamTable = null)
        {
            MySqlCommand CmdData = new MySqlCommand();
          
            Connect();
            CmdData.CommandText = MySql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            CmdData.CommandTimeout = 180;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                }
            }
            CmdData.ExecuteNonQuery();
        }

        public static void MySqlNonQuery1(string MySql, DataTable Dt, Hashtable ParamTable = null)
        {
            try
            {
                MySqlCommand CmdData = new MySqlCommand();
              
               // Connect();
                CmdData.CommandText = MySql;
                CmdData.Connection = con;
                CmdData.CommandType = CommandType.StoredProcedure;
                CmdData.Transaction = Trans;
                CmdData.CommandTimeout = 180;
                if (!(ParamTable == null))
                {
                    foreach (DictionaryEntry DeData in ParamTable)
                    {
                        CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                    }

                }
                CmdData.Parameters.AddWithValue("@Dt", Dt);
                CmdData.ExecuteNonQuery();
            }
            catch (Exception ex) { }

        }

        public static object MySqlScalar(string MySql, Hashtable ParamTable = null)
        {
            MySqlCommand CmdData = new MySqlCommand();
           
          //  Connect();
            CmdData.CommandText = MySql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                }

            }

            return CmdData.ExecuteScalar();
        }

        public static MySqlDataReader MySqlReader(string MySql, Hashtable ParamTable = null)
        {
            MySqlCommand CmdData = new MySqlCommand();
           
            Connect();
            if (Rdr != null)
            {
                Rdr.Close();
            }

            CmdData.CommandText = MySql;
            CmdData.Connection = con;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                }

            }
            Rdr = CmdData.ExecuteReader();
            return Rdr;
        }

        //public static void Load_ListCombo(object Sender, string Sql, string ValMem, string DisMem, Hashtable ParamTable = null, string DefValue = "")
        //{
        //    ''''' Load Combo / Listbox
        //     Warning!!! Optional parameters not supported
        //    try
        //    {
        //        SqlCommand CmdData = new SqlCommand();
        //        Utility.Connect();
        //        CmdData.CommandText = Sql;
        //        CmdData.Connection = con;
        //        if (ParamTable != null)
        //        {
        //            foreach (DictionaryEntry DeData in ParamTable)
        //            {
        //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
        //            }

        //        }

        //        SqlDataAdapter Sda = new SqlDataAdapter(CmdData);
        //        DataTable Dt = new DataTable();
        //        Sda.Fill(Dt);
        //        if (DefValue != "")
        //        {
        //            DataRow Row;
        //            Row = Dt.NewRow();
        //            Row[ValMem] = 0;
        //            Row[DisMem] = DefValue;
        //            Dt.Rows.InsertAt(Row, 0);
        //        }
        //        ((UCComboBox)Sender).SQLQuery = Sql;
        //        ((UCComboBox)Sender).DataSource = null;
        //        ((UCComboBox)Sender).DisplayMember = DisMem;
        //        ((UCComboBox)Sender).ValueMember = ValMem;
        //        ((UCComboBox)Sender).DataSource = Dt;
        //        if (((UCComboBox)Sender).DropDownStyle == ComboBoxStyle.DropDown)
        //        {
        //            if (DefValue != "")
        //            {
        //                ((UCComboBox)Sender).SelectedIndex = 0;
        //            }
        //            else
        //            {
        //                ((UCComboBox)Sender).SelectedIndex = -1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        Utility.DisConnect();
        //    }

        //}

        //public static void Load_DataGrid(object Sender, string Sql, Hashtable ParamTable = null)
        //{
        //    SqlCommand CmdData = new SqlCommand();
        //    Warning!!! Optional parameters not supported
        //    SqlDataAdapter Sda = new SqlDataAdapter();
        //    DataSet Ds = new DataSet();
        //    DataView Dv = new DataView();
        //    try
        //    {
        //        Utility.Connect();
        //        CmdData.CommandText = Sql;
        //        CmdData.Connection = con;
        //        CmdData.CommandTimeout = 180;
        //        if (ParamTable != null)
        //        {
        //            foreach (DictionaryEntry DeData in ParamTable)
        //            {
        //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
        //            }

        //        }

        //        Sda = new SqlDataAdapter(CmdData);
        //        Ds = new DataSet();
        //        Sda.Fill(Ds, "tabresult");
        //        ((DataGridView)(Sender)).DataSource = Ds.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        Utility.DisConnect();
        //    }

        //}

        public static async Task<DataTable> SQLQuery(string Sql, Hashtable ParamTable = null)
        {
            MySqlCommand CmdData = new MySqlCommand();
            MySqlDataAdapter Sda = new MySqlDataAdapter();
            DataSet Ds = new DataSet();
            Utility.Connect();
            CmdData.CommandText = Sql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            CmdData.CommandTimeout = 180;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                   CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
                }

            }

            Sda = new MySqlDataAdapter(CmdData);
            Ds = new DataSet();
           Sda.Fill(Ds, "tabresult");
            DataTable SQLQuery = new DataTable();
            SQLQuery.Clear();
            SQLQuery =Ds.Tables["tabresult"];
            return  SQLQuery;
        }

        //public static void SQLNonQuery(string Sql, Hashtable ParamTable = null)
        //{
        //    SqlCommand CmdData = new SqlCommand();
        //    Warning!!! Optional parameters not supported
        //Utility.Connect(); ();
        //    CmdData.CommandText = Sql;
        //    CmdData.Connection = con;
        //    CmdData.Transaction = Trans;
        //    CmdData.CommandTimeout = 180;
        //    if (ParamTable != null)
        //    {
        //        foreach (DictionaryEntry DeData in ParamTable)
        //        {
        //            CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
        //        }
        //    }
        //    CmdData.ExecuteNonQuery();
        //}

        //public static void SQLNonQuery1(string Sql, DataTable Dt, Hashtable ParamTable = null)
        //{
        //    try
        //    {
        //        SqlCommand CmdData = new SqlCommand();
        //        Warning!!! Optional parameters not supported
        //        Utility.Connect();
        //        CmdData.CommandText = Sql;
        //        CmdData.Connection = Cn;
        //        CmdData.CommandType = CommandType.StoredProcedure;
        //        CmdData.Transaction = Trans;
        //        CmdData.CommandTimeout = 180;
        //        if (!(ParamTable == null))
        //        {
        //            foreach (DictionaryEntry DeData in ParamTable)
        //            {
        //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
        //            }

        //        }
        //        CmdData.Parameters.AddWithValue("@Dt", Dt);
        //        CmdData.ExecuteNonQuery();
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        //}

        //public static object SQLScalar(string Sql, Hashtable ParamTable = null)
        //{
        //    SqlCommand CmdData = new SqlCommand();
        //    Warning!!! Optional parameters not supported
        //    Utility.Connect();
        //    CmdData.CommandText = Sql;
        //    CmdData.Connection = con;
        //    CmdData.Transaction = Trans;
        //    if (ParamTable != null)
        //    {
        //        foreach (DictionaryEntry DeData in ParamTable)
        //        {
        //            CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
        //        }

        //    }

        //    return CmdData.ExecuteScalar();
        //}



        //public static void Sql_Backup(string Path)
        //{
        //    string constring = GlobalVariables.ConnectionString;
        //    string file = Path;
        //    using (SqlConnection conn = new SqlConnection(constring))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            using (SqlBackup mb = new SqlBackup(cmd))
        //            {
        //                cmd.Connection = conn;
        //                conn.Open();
        //                mb.ExportToFile(file);
        //                conn.Close();
        //            }
        //        }
        //    }
        //}

        //public static void Sql_Restore(string Path)
        //{
        //    string constring = GlobalVariables.ConnectionString;
        //    string file = Path;
        //    using (SqlConnection conn = new SqlConnection(constring))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            using (SqlBackup mb = new SqlBackup(cmd))
        //            {
        //                cmd.Connection = conn;
        //                conn.Open();
        //                mb.ImportFromFile(file);
        //                conn.Close();
        //            }
        //        }
        //    }
        //}
    }

}