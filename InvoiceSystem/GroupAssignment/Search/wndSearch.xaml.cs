using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupAssignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Access the SearchLogic class.
        /// </summary>
        clsSearchLogic SearchLogic;



        #region Properties
        /// <summary>
        /// Variable that holds InvoiceNum and makes it accessible to other windows
        /// </summary>
        public string invID;
        /// <summary>
        /// Tells us if the user wants to clear their search.
        /// </summary>
        public bool clearSelection = false;
        #endregion

        #region Constructor
        public wndSearch()
        {
            InitializeComponent();

            SearchLogic = new clsSearchLogic();


            dgTBD.ItemsSource = SearchLogic.GetInvoiceTable();
            cbInvoiceNumber.ItemsSource = SearchLogic.GetInvoiceNumber();
            cbInvoiceDate.ItemsSource = SearchLogic.GetInvoiceDate();
            cbTotalCharge.ItemsSource = SearchLogic.GetTotalCharge();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Allows user to select and Invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //This is where we will set the InvoiceID property of the 
                //clsSearchLogic to the InvoiceNum from the Search Window
                //then we will set the invID equal to InvoiceID, which will 
                //be accessible by the Main Window.
                //clsSearchLogic sl = new clsSearchLogic();
                //clsInvoice invoice = new clsInvoice();

                //sl.InvoiceID = Invoice Number
                //invID = sl.InvoiceID;
                //invID = invoice.InvoiceID;

                if (dgTBD.SelectedItem != null)
                {
                    invID = ((clsInvoice)dgTBD.SelectedItem).InvoiceID;

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
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
        /// <summary>
        /// Allows user to change the Invoice Number they are looking for on the Data Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (clearSelection == false)
                {
                    if ((cbInvoiceDate.SelectedIndex == -1) && (cbTotalCharge.SelectedIndex == -1))
                    {
                        int invoiceNum = int.Parse(((clsInvoice)cbInvoiceNumber.SelectedItem).InvoiceID);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceNumIs(invoiceNum);
                        cbInvoiceDate.ItemsSource = SearchLogic.UpdateInvoiceDateComboboxINVOICENUM(invoiceNum);
                        cbTotalCharge.ItemsSource = SearchLogic.UpdateTotalChargeComboboxINVOICENUM(invoiceNum);

                    }
                    else if (cbTotalCharge.SelectedIndex == -1)
                    {
                        int invoiceNum = int.Parse(((clsInvoice)cbInvoiceNumber.SelectedItem).InvoiceID);
                        string invoiceDate = (((clsInvoice)cbInvoiceDate.SelectedItem).InvoiceDate);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(invoiceNum, invoiceDate);
                        cbTotalCharge.ItemsSource = SearchLogic.UpdateTotalChargeComboboxINVOICENUMandINVOICEDATE(invoiceNum, invoiceDate);
                    }
                    else if (cbInvoiceDate.SelectedIndex == -1)
                    {
                        int invoiceNum = int.Parse(((clsInvoice)cbInvoiceNumber.SelectedItem).InvoiceID);
                        int totalCost = int.Parse(((clsInvoice)cbTotalCharge.SelectedItem).TotalCost);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(invoiceNum, totalCost);
                        cbInvoiceDate.ItemsSource = SearchLogic.UpdateInvoiceDateComboboxINVOICENUMandTOTALCHARGE(invoiceNum, totalCost);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Allows user to change the Invoice Date they are looking for on the Data Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (clearSelection == false)
                {
                    if ((cbInvoiceNumber.SelectedIndex == -1) && (cbTotalCharge.SelectedIndex == -1))
                    {
                        string invoiceDate = (((clsInvoice)cbInvoiceDate.SelectedItem).InvoiceDate);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceDateIs(invoiceDate);
                        cbInvoiceNumber.ItemsSource = SearchLogic.UpdateInvoiceNumberComboboxINVOICEDATE(invoiceDate);
                        cbTotalCharge.ItemsSource = SearchLogic.UpdateTotalChargeComboboxINVOICEDATE(invoiceDate);
                    }
                    else if (cbInvoiceNumber.SelectedIndex == -1)
                    {
                        string invoiceDate = (((clsInvoice)cbInvoiceDate.SelectedItem).InvoiceDate);
                        int totalCost = int.Parse(((clsInvoice)cbTotalCharge.SelectedItem).TotalCost);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(totalCost, invoiceDate);
                        cbInvoiceNumber.ItemsSource = SearchLogic.UpdateInvoiceNumberComboboxINVOICEDATEandTOTALCHARGE(invoiceDate, totalCost);
                    }
                    else if (cbTotalCharge.SelectedIndex == -1)
                    {
                        int invoiceNum = int.Parse(((clsInvoice)cbInvoiceNumber.SelectedItem).InvoiceID);
                        string invoiceDate = (((clsInvoice)cbInvoiceDate.SelectedItem).InvoiceDate);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceNumIsAndInvoiceDateIs(invoiceNum, invoiceDate);
                        cbTotalCharge.ItemsSource = SearchLogic.UpdateTotalChargeComboboxINVOICENUMandINVOICEDATE(invoiceNum, invoiceDate);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Allow user to change the Total Cost they are looking for on the Data Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (clearSelection == false)
                {
                    if ((cbInvoiceNumber.SelectedIndex == -1) && (cbInvoiceDate.SelectedIndex == -1))
                    {
                        int totalCost = int.Parse(((clsInvoice)cbTotalCharge.SelectedItem).TotalCost);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereTotalCostIs(totalCost);
                        cbInvoiceNumber.ItemsSource = SearchLogic.UpdateInvoiceNumberComboboxTOTALCOST(totalCost);
                        cbInvoiceDate.ItemsSource = SearchLogic.UpdateInvoiceDateComboboxTOTALCOST(totalCost);
                    }
                    else if (cbInvoiceNumber.SelectedIndex == -1)
                    {
                        string invoiceDate = (((clsInvoice)cbInvoiceDate.SelectedItem).InvoiceDate);
                        int totalCost = int.Parse(((clsInvoice)cbTotalCharge.SelectedItem).TotalCost);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereTotalCostIsAndInvoiceDateIs(totalCost, invoiceDate);
                        cbInvoiceNumber.ItemsSource = SearchLogic.UpdateInvoiceNumberComboboxINVOICEDATEandTOTALCHARGE(invoiceDate, totalCost);
                    }
                    else if (cbInvoiceDate.SelectedIndex == -1)
                    {
                        int invoiceNum = int.Parse(((clsInvoice)cbInvoiceNumber.SelectedItem).InvoiceID);
                        int totalCost = int.Parse(((clsInvoice)cbTotalCharge.SelectedItem).TotalCost);

                        dgTBD.ItemsSource = SearchLogic.SelectAllInvoicesWhereInvoiceNumIsAndTotalCostIs(invoiceNum, totalCost);
                        cbInvoiceDate.ItemsSource = SearchLogic.UpdateInvoiceDateComboboxINVOICENUMandTOTALCHARGE(invoiceNum, totalCost);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Allows user to clear their search and start over.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bClearSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearSelection = true;
                //Set DataGrid back to original state.
                dgTBD.ItemsSource = SearchLogic.GetInvoiceTable();
                cbInvoiceNumber.ItemsSource = SearchLogic.GetInvoiceNumber();
                cbInvoiceDate.ItemsSource = SearchLogic.GetInvoiceDate();
                cbTotalCharge.ItemsSource = SearchLogic.GetTotalCharge();
                clearSelection = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion




    }
}
