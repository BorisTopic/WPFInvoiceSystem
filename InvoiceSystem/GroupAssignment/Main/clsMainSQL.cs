using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Class for Main window SQL queries
    /// </summary>
    public class clsMainSQL
    {

        #region Methods
        /// <summary>
        /// SQL query that deletes an invoice
        /// </summary>
        /// <param name="invoiceID">InvoiceNum of an invoice</param>
        /// <returns>SQL query in the form of a string</returns>
        public string deleteInvoiceSQL(int invoiceID)
        {
            try
            {
                string sSQL = "DELETE * FROM Invoices WHERE InvoiceNum = " + invoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// SQL query that deletes a line item
        /// </summary>
        /// <param name="invoiceID">InvoiceNum of an invoice</param>
        /// <returns>A SQL query in the form of a string</returns>
        public string deleteLineItemsSQL(int invoiceID)
        {
            try
            {
                string sSQL = "DELETE * FROM LineItems WHERE InvoiceNum = " + invoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// SQL query that inserts a new invoice record into the Invoices table
        /// </summary>
        /// <param name="date">Date of an invoice</param>
        /// <param name="totalCost">Total cost field of the new invoice</param>
        /// <returns>SQL query in the form of a string</returns>
        public string createInvoiceSQL(string date, string totalCost, string invoiceNum)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceNum, InvoiceDate, TotalCost) VALUES (" + invoiceNum + ", '" + date + "'," + totalCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Pulls most recent invoice
        /// </summary>
        /// <returns>SQL query in the form of a string</returns>
        public string pullNewestInvoiceSQL()
        {
            try
            {
                string sSQL = "SELECT max(InvoiceNum) from Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Creates a line item and links it to the most recent invoice
        /// </summary>
        /// <param name="lineItemNum">LineItemNum of the lineItem</param>
        /// <param name="itemCode">ItemCode for the lineItem</param>
        /// <returns>SQL query in the form of a string</returns>
        public string createLineItemsSQL(string newInvoice, int lineItemNum, string itemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ('" + newInvoice + "', " + lineItemNum + ", '" + itemCode + "')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the cost of an item from the database
        /// </summary>
        /// <param name="itemCode">The item code for a line item</param>
        /// <returns>SQL query in the form of a string</returns>
        public string getItemCostSQL(string itemCode)
        {
            try
            {
                string sSQL = "SELECT Cost FROM ItemDesc WHERE ItemDesc = '" + itemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Item description/name for the line item
        /// </summary>
        /// <param name="itemDesc">Item name for the line item</param>
        /// <returns>SQL query in the form of a string</returns>
        public string getItemCodeSQL(string itemDesc)
        {
            try
            {
                string sSQL = "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = " + itemDesc;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets date, cost, and item code for an item
        /// </summary>
        /// <param name="itemDesc">Name of item</param>
        /// <returns>SQL qeury</returns>
        public string getItemInfo(string itemDesc)
        {
            try
            {
                string sSQL = "SELECT ItemCode, Cost, ItemDesc  FROM ItemDesc WHERE ItemDesc = '" + itemDesc + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets date, cost, and item code for an item based off item num
        /// </summary>
        /// <param name="itemDesc">Name of item</param>
        /// <returns>SQL qeury</returns>
        public string getInvoiceDateSQL(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceDate FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getInvoiceCost(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getItemsFromInvoice(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT id.ItemCode, id.ItemDesc, id.Cost FROM ItemDesc id inner join LineItems l on l.ItemCode = id.ItemCode WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an invoice in the db
        /// </summary>
        /// <param name="invoiceNum">Invoice number</param>
        /// <param name="date">Invoice date</param>
        /// <param name="cost">Invoice total cost</param>
        /// <returns>SQL query</returns>
        public string updateInvoiceSQL(string invoiceNum, string date, string cost)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET InvoiceDate = '" + date + "', TotalCost = '" + cost + "' where InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Checks for invoice in database
        /// </summary>
        /// <param name="invoiceNum">Invoice number to look for</param>
        /// <returns>SQL query</returns>
        public string checkForInvoiceSQL(int invoiceNum)
        {
            try
            {
                string sSQL = "select count(InvoiceNum) from Invoices where InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

    }
}
