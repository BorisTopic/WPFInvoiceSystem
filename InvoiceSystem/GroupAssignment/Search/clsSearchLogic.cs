using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace GroupAssignment.Search
{
    public class clsSearchLogic
    {
        /// <summary>
        /// Access the DataAccess class.
        /// </summary>
        DataAccess db;
        /// <summary>
        /// Access the SearchSQL class for the SQL statements.
        /// </summary>
        clsSearchSQL SearchSQL;
        /// <summary>
        /// Gets the data from the database to be bound to the DataGrid.
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> GetInvoiceTable()
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoiceData();
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets the InvoiceNumbers in the database to be bound into the InvoiceNumber combobox.
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> GetInvoiceNumber()
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoiceData();
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets the InvoiceDates in the database to be bound into the InvoiceDate combobox.
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> GetInvoiceDate()
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoiceData();
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.InvoiceDate == invoice.InvoiceDate)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets the TotalCharges in the database to be bound into the TotalCharge combobox.
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> GetTotalCharge()
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoiceData();
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.TotalCost == invoice.TotalCost)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                lstInvoiceTable = lstInvoiceTable.OrderBy(o => int.Parse(o.TotalCost)).ToList();

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all Invoices for specified Invoice Number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereInvoiceNumIs(int invoiceNum)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIs(invoiceNum);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all Invoices for specified Invoice Date.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereInvoiceDateIs(string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceDateIs(invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all Invoices for specified Total Cost.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereTotalCostIs(int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereTotalCostIs(totalCost);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all Invoices for specified Invoice Number and Invoice Date.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(int invoiceNum, string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(invoiceNum, invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Updates the Invoice Date combobox when an Invoice Number is selected.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceDateComboboxINVOICENUM(int invoiceNum)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIs(invoiceNum);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Updates the Total Charge combobox when an Invoice Number is selected.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateTotalChargeComboboxINVOICENUM(int invoiceNum)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIs(invoiceNum);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.TotalCost == invoice.TotalCost)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                lstInvoiceTable = lstInvoiceTable.OrderBy(o => int.Parse(o.TotalCost)).ToList();

                return lstInvoiceTable;
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
        /// <param name="totalCost"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(int totalCost, string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(totalCost, invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all Invoices for specified InvoiceNum and TotalCost.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(int invoiceNum, int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(invoiceNum, totalCost);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Invoice Number combobox when an Invoice Date is selected.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceNumberComboboxINVOICEDATE(string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceDateIs(invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Total Charge combobox when an Invoice Date is selected.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateTotalChargeComboboxINVOICEDATE(string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceDateIs(invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.TotalCost == invoice.TotalCost)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                lstInvoiceTable = lstInvoiceTable.OrderBy(o => int.Parse(o.TotalCost)).ToList();

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Invoice Number combobox when a Total Cost is selected.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceNumberComboboxTOTALCOST(int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereTotalCostIs(totalCost);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Invoice Date combobox when a Total Cost is selected.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceDateComboboxTOTALCOST(int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereTotalCostIs(totalCost);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.InvoiceDate == invoice.InvoiceDate)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Total Charge combobox when an Invoice Number and Invoice Date is selected.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateTotalChargeComboboxINVOICENUMandINVOICEDATE(int invoiceNum, string invoiceDate)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(invoiceNum, invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.TotalCost = dr["TotalCost"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.TotalCost == invoice.TotalCost)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                lstInvoiceTable = lstInvoiceTable.OrderBy(o => int.Parse(o.TotalCost)).ToList();

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Invoice Date combobox when an Invoice Number and Total Cost is selected.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceDateComboboxINVOICENUMandTOTALCHARGE(int invoiceNum, int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(invoiceNum, totalCost);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                    bool addToTable = true;
                    foreach (clsInvoice inv in lstInvoiceTable)
                    {
                        if (inv.InvoiceDate == invoice.InvoiceDate)
                        {
                            addToTable = false;
                            break;
                        }
                    }
                    if (addToTable == true)
                    {
                        lstInvoiceTable.Add(invoice);
                    }
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Update the Invoice Number combobox when an Invoice Date and Total Cost.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public List<clsInvoice> UpdateInvoiceNumberComboboxINVOICEDATEandTOTALCHARGE(string invoiceDate, int totalCost)
        {
            try
            {
                db = new DataAccess();

                List<clsInvoice> lstInvoiceTable = new List<clsInvoice>();

                SearchSQL = new clsSearchSQL();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = SearchSQL.SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(totalCost, invoiceDate);
                ds = db.ExecuteSqlStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.InvoiceID = dr["InvoiceNum"].ToString();
                    lstInvoiceTable.Add(invoice);
                }

                return lstInvoiceTable;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
