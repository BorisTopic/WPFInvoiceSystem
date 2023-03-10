using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace GroupAssignment
{
    public class clsItemsLogic
    {
        DataAccess db = new DataAccess();
        clsItemsSQL sql = new clsItemsSQL();
        #region Select
        public DataSet SelectDescData()
        {
            try
            {
                var iRetVal = 0;
                var query = sql.SelectDescData();
                var dataset = sql.ExecuteSQLStatement(query, ref iRetVal);
                return dataset;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        internal bool ItemCount(string code)
        {
            try
            {
                string query = sql.SelectUniqueInvoice(code);
                string response = db.ExecuteScalarSQL(query);

                 if (response == "")
                    response = "0";

                int count = Convert.ToInt32(response);
                if (count != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Update
        public void UpdateItem(string code, string desc, string cost)
        {
            try
            {
                var query = sql.UpdateItem(code, desc, cost);
                db.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Insert
        public void InsertItem(string desc, string cost, string code)
        {
            try
            {
                var query = sql.InsertItem(desc, cost, code);
                db.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public string generateCode()
        {
            try
            {
                //var query = sql.generateCode();

                string sSQL = sql.generateCodeSQL();
                string temp = db.ExecuteScalarSQL(sSQL);

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Delete
        public void DeleteItem(string code)
        {
            try
            {
                var query = sql.DeleteItem(code);
                db.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void getItemInfo(string name)
        {
            try
            {
                var query = sql.getItemInfo(name);
                db.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        #endregion

        #region error handling
        /// <summary>
        /// The error handling method. This prints out a user readable stack trace for debugging purposes.
        /// </summary>
        /// <param name="sClass">The class the error originated from</param>
        /// <param name="sMethod">The method the error originated from</param>
        /// <param name="sMessage">The error message</param>
        private void HandleError(String sClass, String sMethod, String sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText("C:\\" + System.AppDomain.CurrentDomain.FriendlyName + "Error.txt", Environment.NewLine + "HandleError Exception: " + e.Message);
            }
        }
        #endregion


    }
}