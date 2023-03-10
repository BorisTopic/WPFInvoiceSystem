using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment.Main
{
    /// <summary>
    /// Class that holds info for a line item
    /// </summary>
    public class clsLineItem
    {

        #region Properties
        /// <summary>
        /// Item code of a line item
        /// </summary>
        public string itemCode;

        /// <summary>
        /// Item number for line item
        /// </summary>
        public int lineItemNum;

        /// <summary>
        /// The invoice number the specific line item belongs to
        /// </summary>
        public string lineItemInvoiceNum;

        /// <summary>
        /// Description/Name of line item
        /// </summary>
        public string lineItemDesc;

        /// <summary>
        /// Cost of line item
        /// </summary>
        public string itemCost;
        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor for a line item
        /// </summary>
        public clsLineItem()
        {

        }

        /// <summary>
        /// Overloaded constructor for line item
        /// </summary>
        /// <param name="code">Item code</param>
        /// <param name="itemDesc">Item description/name</param>
        /// <param name="cost">Item cost</param>
        public clsLineItem(string code, string itemDesc, string cost)
        {
            itemCode = code;
            lineItemDesc = itemDesc;
            itemCost = cost;
        }
        #endregion

        //public override string ToString()
        //{
        //    try
        //    {
        //        return lineItemInvoiceNum + " " + lineItemNum + " " + itemCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Just throw the exception
        //        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
        //                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }
        //}


    }
}
