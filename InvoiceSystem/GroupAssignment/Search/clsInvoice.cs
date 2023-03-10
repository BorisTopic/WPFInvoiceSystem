using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GroupAssignment.Search
{
    public class clsInvoice
    {
        #region Properties 
        /// <summary>
        /// Holds InvoiceNum property from database.
        /// </summary>
        private string invoiceID;
        /// <summary>
        /// Holds InvoiceData property from database.
        /// </summary>
        private string invoiceDate;
        /// <summary>
        /// Hold TotalCost property from database.
        /// </summary>
        private string totalCost;

        /// <summary>
        /// Allows access to invoiceID property.
        /// </summary>
        public string InvoiceID { get => invoiceID; set => invoiceID = value; }
        /// <summary>
        /// Allows access to invoiceDate property.
        /// </summary>
        public string InvoiceDate { get => invoiceDate; set => invoiceDate = value; }
        /// <summary>
        /// Allows access to totalCost property.
        /// </summary>
        public string TotalCost { get => totalCost; set => totalCost = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Overrides ToString() method do display data properly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return InvoiceID + InvoiceDate + TotalCost;
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
