using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace GroupAssignment.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        private clsItemsLogic ItemsLogic;

        private string SelectedItemId;

        private bool IsDataRowSelected = false;


        #region Constructor
        public wndItems()
        {
            try
            {
                InitializeComponent();
                ItemsLogic = new clsItemsLogic();
                GetItemDataset();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Calls the Logic class to get the Items DataSet. 
        /// After it receives this, it populates the DataGrid.
        /// </summary>
        private void GetItemDataset()
        {
            try
            {
                var dataset = ItemsLogic.SelectDescData();
                ItemsDataGrid.ItemsSource = dataset.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #region Methods
        /// <summary>
        /// The on click listener for the Add button
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                clsItemsSQL sql = new clsItemsSQL();

                if (TextBoxesAreFilled())
                {
                    Random rnd = new Random();

                    int rand = rnd.Next(0, 10000);

                    ItemsLogic.InsertItem(tbDescription.Text, tbPrice.Text, rand.ToString()) ;
                    GetItemDataset();
                    tbDescription.Text = "";
                    tbPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// The on click listener for the Edit button
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataRowIsSelected() && TextBoxesAreFilled())
                {
                    ItemsLogic.UpdateItem(SelectedItemId, tbDescription.Text, tbPrice.Text);
                    GetItemDataset();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// The on click listener for the Delete button
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataRowIsSelected() && ClearedToDeleteItem(SelectedItemId))
                {
                    ItemsLogic.DeleteItem(SelectedItemId);
                    GetItemDataset();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// The on click listener for the Cancel button
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ///this will close out of the window
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Helper method that checks if the Description and Price textboxes are filled, and
        /// that the Price contains a decimal.
        /// </summary>
        /// <returns>A true/false value on if the textboxes are filled</returns>
        private bool TextBoxesAreFilled()
        {
            try
            {
                var isDiscValid = false;
                var isPriceValid = false;

                if (string.IsNullOrWhiteSpace(tbDescription.Text))
                    DescriptionErrorLabel.Visibility = Visibility.Visible;
                else
                {
                    DescriptionErrorLabel.Visibility = Visibility.Hidden;
                    isDiscValid = true;
                }

                if (!decimal.TryParse(tbPrice.Text, out decimal price))
                    PriceErrorLabel.Visibility = Visibility.Visible;
                else
                {
                    PriceErrorLabel.Visibility = Visibility.Hidden;
                    isPriceValid = true;
                }

                return (isDiscValid && isPriceValid);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event is called every time a new row is selected in the DataGrid. This method verifies
        /// if a row is selected, and passes the Id, description, and price for the new row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IsDataRowSelected = true;

                var selectedItem = ItemsDataGrid.SelectedItem;

                if (selectedItem == null) // Happens after Update or Delete
                {
                    IsDataRowSelected = false;
                    tbDescription.Text = "";
                    tbPrice.Text = "";
                    return;
                }

                var dataset = ItemsLogic.SelectDescData();

                var id = (string)dataset.Tables[0].Rows[ItemsDataGrid.SelectedIndex].ItemArray[0];
                var description = (string)dataset.Tables[0].Rows[ItemsDataGrid.SelectedIndex].ItemArray[1];
                var price = (decimal)dataset.Tables[0].Rows[ItemsDataGrid.SelectedIndex].ItemArray[2]; ;

                SelectedItemId = id.ToString();
                tbDescription.Text = description;
                tbPrice.Text = price.ToString();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Verifies that a row is selected, using the global variable.
        /// </summary>
        /// <returns>If a row in the DataGrid is selected</returns>
        private bool DataRowIsSelected()
        {
            try
            {
                if (IsDataRowSelected == false)
                    RowSelectionErrorLabel.Visibility = Visibility.Visible;
                else
                    RowSelectionErrorLabel.Visibility = Visibility.Hidden;
                return IsDataRowSelected;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Calls the Logic class to check if an Item can be deleted from the database. It
        /// also notifies the user if the Item can't be deleted.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>If an Item can be deleted</returns>
        private bool ClearedToDeleteItem(string itemId)
        {
            try
            {
                var cantDelete = ItemsLogic.ItemCount(itemId);

                if (cantDelete)
                    DeleteItemErrorTextBlock.Visibility = Visibility.Visible;
                else
                    DeleteItemErrorTextBlock.Visibility = Visibility.Hidden;
                return !cantDelete;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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

        private void btnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
