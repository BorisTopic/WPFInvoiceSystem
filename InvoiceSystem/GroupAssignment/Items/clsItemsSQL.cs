using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace GroupAssignment
{
    class clsItemsSQL
    {
        #region class fields
        /// <summary>
        /// The database access object. Used for making queries on the database.
        /// </summary>
        DataAccess db = new DataAccess();

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        public clsItemsSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }
        #endregion

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned
        public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #region Select

        /// <summary>
        /// This SQL gets all data from a given ItemDesc
        /// </summary>
        /// <param name="sDescID">The DescID for the Description to retrieve all data.</param>
        /// <returns>All data for the given ItemDesc.</returns>
        public string SelectDescData()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc ";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// This SQL gets unique InvoiceNum from LineItems where the ItemCome is A.
        /// </summary>
        /// <param name="sInvoiceNumID">The InvoiceNumID for the invoice to retrieve all data.</param>
        /// <returns>Unique data with an ItemCode of A.</returns>
        public string SelectUniqueInvoice(string code)
        {
            try
            {
                string sSQL = "SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string ItemCount(string code)
        {
            try
            {
                string sSQL = "SELECT COUNT(ItemId) FROM InvoiceItems WHERE ItemId =" + code;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// This SQL Updates the ItemDesc of the Item with ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
        /// </summary>
        /// <param name="uItemDescID">The ItemDescID for the Description to update data.</param>
        /// <returns>Updated ItemDesc.</returns>
        public string UpdateItem(string code, string desc, string cost)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET ItemDesc = '" + desc + "', Cost = '" + cost + "' WHERE ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// This SQL inserts data into ItemDesc
        /// </summary>
        /// <param name="iItemDescID">The ItemDescID for the Description to insert data.</param>
        /// <returns>Updated ItemDesc table</returns>
        public string InsertItem(string desc, string cost, string itemCode)
        {
            try
            {
                string sSQL = "INSERT into ItemDesc(ItemCode, ItemDesc, Cost) VALUES('" + itemCode + "','" + desc + "'," + cost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string generateCodeSQL()
        {

            try
            {
                string sSQL = "select count(ItemCode) from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        #endregion

        #region Delete
        /// <summary>
        /// This SQL deletes data from ItemDesc where the ItemCode = 'ABC'
        /// </summary>
        /// <param name="iItemDescID">The ItemDescID for the Description to insert data.</param>
        /// <returns>Updated ItemDesc table</returns>
        public string DeleteItem(string code)
        {
            try
            {
                string sSQL = "DELETE from ItemDesc WHERE ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getItemInfo(string name)
        {
            try
            {
                string sSQL = "select Cost from ItemDesc WHERE ItemDesc = '" + name + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
