using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Class that holds logic for Main window
    /// </summary>
    public class clsMainLogic
    {
        #region Properties
        DataAccess db;
        clsMainSQL sql;
        #endregion

        #region Methods
        /// <summary>
        /// Deletes an invoice from database
        /// </summary>
        /// <param name="invoiceID">Invoice ID to delete from db</param>
        public void deleteInvoice(int invoiceID)
        {
            try
            {
                db = new DataAccess();
                sql = new clsMainSQL();

                db.ExecuteScalarSQL(sql.deleteInvoiceSQL(invoiceID));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes line items corresponding to an invoice id
        /// </summary>
        /// <param name="invoiceID">The invoice ID whose line items will be deleted</param>
        public void deleteLineItems(int invoiceID)
        {
            try
            {
                db = new DataAccess();
                sql = new clsMainSQL();

                db.ExecuteScalarSQL(sql.deleteLineItemsSQL(invoiceID));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates new invoice
        /// </summary>
        /// <param name="date">Invoice date</param>
        /// <param name="totalCost">Total cost of all invoice items</param>
        /// <param name="invoiceNum">New invoice ID</param>
        public void createInvoice(string date, string totalCost, string invoiceNum)
        {
            try
            {
                db = new DataAccess();
                sql = new clsMainSQL();

                db.ExecuteScalarSQL(sql.createInvoiceSQL(date, totalCost, invoiceNum));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates line items for an invoice
        /// </summary>
        /// <param name="list">List of line items to be created</param>
        /// <param name="invoiceNum">Invoice ID</param>
        public void createLineItem(List<clsLineItem> list, string invoiceNum)
        {
            try
            {
                db = new DataAccess();
                sql = new clsMainSQL();

                for (int k = 0; k < list.Count; k++)
                {
                    list[k].lineItemNum = (list.IndexOf(list[k]) + 1);
                }


                for (int i = 0; i < list.Count; i++)
                {
                    db.ExecuteScalarSQL(sql.createLineItemsSQL(invoiceNum, list[i].lineItemNum, list[i].itemCode));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all line items and puts them in list
        /// </summary>
        /// <returns>List of line items in db</returns>
        public List<clsLineItem> GetItems()
        {
            try
            {
                List<clsLineItem> ItemList = new List<clsLineItem>();
                db = new DataAccess();

                string sSQL;
                int iRet = 0;
                DataSet ds = new DataSet();
                clsLineItem Items;

                sSQL = "select ItemDesc, Cost from ItemDesc";

                ds = db.ExecuteSqlStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    Items = new clsLineItem();
                    Items.lineItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    Items.itemCost = ds.Tables[0].Rows[i]["Cost"].ToString();

                    ItemList.Add(Items);
                }

                return ItemList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets cost of an item
        /// </summary>
        /// <param name="itemDesc">Item to get cost for</param>
        /// <returns>Cost of itemDesc item</returns>
        public string getItemCost(string itemDesc)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                string cost;

                cost = db.ExecuteScalarSQL(sql.getItemCostSQL(itemDesc));

                return cost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Grabs item info for item to be added to item list
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns>Item & item info to be added to list</returns>
        public clsLineItem itemToBeAdded(string name)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                clsLineItem item = new clsLineItem();

                ds = db.ExecuteSqlStatement(sql.getItemInfo(name), ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    item.lineItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    item.itemCode = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                    item.itemCost = ds.Tables[0].Rows[i]["Cost"].ToString();
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the next invoice number in the sequence
        /// </summary>
        /// <returns>Next invoice number</returns>
        public string getNextInvoiceNum()
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                string invoiceNum;

                invoiceNum = db.ExecuteScalarSQL(sql.pullNewestInvoiceSQL());

                return invoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getInvoiceDate(string num)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                string invoiceDate;

                invoiceDate = db.ExecuteScalarSQL(sql.getInvoiceDateSQL(num));

                return invoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string getInvoiceCost(string num)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                string invoiceCost;

                invoiceCost = db.ExecuteScalarSQL(sql.getInvoiceCost(num));

                return invoiceCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<clsLineItem> GetInvoiceItems(string invoiceNum)
        {
            try
            {
                db = new DataAccess();

                List<clsLineItem> lineItemsTable = new List<clsLineItem>();

                clsMainSQL sql = new clsMainSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = sql.getItemsFromInvoice(invoiceNum);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsLineItem items = new clsLineItem();
                    items.itemCode = dr["ItemCode"].ToString();
                    items.lineItemDesc = dr["ItemDesc"].ToString();
                    items.itemCost = dr["Cost"].ToString();
                    lineItemsTable.Add(items);
                }

                return lineItemsTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Checks to see if an invoice exists
        /// </summary>
        /// <param name="invoiceNum">Invoice number to look for</param>
        /// <returns>True/false whether the invoice exists in the db</returns>
        public bool checkForInvoice(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                bool invoiceExist;

                if (Convert.ToInt32(db.ExecuteScalarSQL(sql.checkForInvoiceSQL(invoiceNum))) == 1)
                {
                    invoiceExist = true;
                }
                else
                {
                    invoiceExist = false;
                }

                return invoiceExist;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates invoice in database
        /// </summary>
        /// <param name="invoiceNum">Invoice number to update</param>
        /// <param name="date">New invoice date</param>
        /// <param name="totalCost">New invoice total cost</param>
        public void updateInvoice(string invoiceNum, string date, string totalCost)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                DataAccess db = new DataAccess();

                db.ExecuteScalarSQL(sql.updateInvoiceSQL(invoiceNum, date, totalCost));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
