using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Search
{
    public class clsSearchSQL
    {

        #region Methods
        /// <summary>
        /// Returns everything from the Invoice table.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoiceData()
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }


        /// <summary>
        /// Returns all Invoices for specified InvoiceNum.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereInvoiceNumIs(int InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified InvoiceNum and InvocieDate.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(int InvoiceNum, string InvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + InvoiceNum + " AND InvoiceDate = #" + InvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified InvoiceNum and InvoiceDate and TotalCost.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIsAndTotalCostIs(int InvoiceNum, string InvoiceDate, int TotalCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + InvoiceNum + " AND InvoiceDate = #" + InvoiceDate + "# AND TotalCost = " + TotalCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified TotalCost.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereTotalCostIs(int TotalCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + TotalCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified TotalCost and InvoiceDate.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(int TotalCost, string InvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + TotalCost + "AND InvoiceDate = #" + InvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified InvoiceDate.
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoicesWhereInvoiceDateIs(string InvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + InvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all Invoices for specified InvoiceNum and InvoiceDate
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="TotalCost"></param>
        /// <returns></returns>
        public string SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(int InvoiceNum, int TotalCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + InvoiceNum + " AND TotalCost = " + TotalCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion


    }
}
