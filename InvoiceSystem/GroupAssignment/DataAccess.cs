using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace GroupAssignment
{
    /// <summary>
    /// Class with methods for accessing the database
    /// </summary>
    public class DataAccess
    {
        #region Properties
        /// <summary>
        /// Varialbe for holding connection string
        /// </summary>
        private string sConnString;
        #endregion

        #region Constructor
        /// <summary>
        /// Class constructor, sets connection string
        /// </summary>
        public DataAccess()
        {
            sConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";

        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that runs a sql query against database
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="iRetVal"></param>
        /// <returns></returns>
        public DataSet ExecuteSqlStatement(string sSQL, ref int iRetVal)
        {

            DataSet ds = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(sConnString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    conn.Open();

                    adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    adapter.Fill(ds);
                }
            }

            iRetVal = ds.Tables[0].Rows.Count;


            return ds;
        }


        public string ExecuteScalarSQL(string sSQL)
        {
            object obj;

            using (OleDbConnection conn = new OleDbConnection(sConnString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    conn.Open();

                    adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    obj = adapter.SelectCommand.ExecuteScalar();
                }
            }

            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        #endregion
    }
}