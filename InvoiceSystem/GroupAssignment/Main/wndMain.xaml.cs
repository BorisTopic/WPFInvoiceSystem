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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region Properties
        Search.wndSearch SearchWindow;
        Items.wndItems ItemsWindow;
        List<Main.clsLineItem> itemList;
        int tempCost;
        #endregion

        #region Constructor
        public wndMain()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Button that allows user to create a new invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceDateBox.Clear();
                invoiceNumWarninglbl.Visibility = Visibility.Hidden;
                dateWarninglbl.Visibility = Visibility.Hidden;

                invoiceDateBox.IsEnabled = true;
                itemsComboBox.IsEnabled = true;
                addItemBtn.IsEnabled = true;
                deleteItemBtn.IsEnabled = true;
                saveInvoiceBtn.IsEnabled = true;

                Main.clsMainLogic ML = new Main.clsMainLogic();

                itemsComboBox.ItemsSource = ML.GetItems().Select(z => z.lineItemDesc).ToList();
                invoiceNumBox.Text = "TBD";
                itemList = new List<Main.clsLineItem>();

                lineItemsDG.ItemsSource = itemList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Lets the user browse through available items. Also populates the item's cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Main.clsMainLogic ML = new Main.clsMainLogic();

                itemCostBox.Text = ("$" + ML.getItemCost(itemsComboBox.SelectedItem.ToString()) + ".00");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Button that opens a new search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSearchWinbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.clsMainLogic ML = new Main.clsMainLogic();

                SearchWindow = new Search.wndSearch();
                SearchWindow.ShowDialog();

                if(SearchWindow.invID != null)
                { 
                    string invoice = SearchWindow.invID;
                    invoiceNumBox.Text = invoice;
                    invoiceDateBox.Text = ML.getInvoiceDate(invoiceNumBox.Text);
                    totalCostBox.Text = ML.getInvoiceCost(invoiceNumBox.Text);

                    DataSet ds = new DataSet("LineItems");
                    List<Main.clsLineItem> invoiceItems = new List<Main.clsLineItem>();
                    invoiceItems = ML.GetInvoiceItems(invoiceNumBox.Text);

                    DataTable itemsTable = ds.Tables.Add("LineItem");
                    tempCost = 0;

                    itemsTable.Columns.Add("ItemCode", typeof(string));
                    itemsTable.Columns.Add("ItemDesc", typeof(string));
                    itemsTable.Columns.Add("Cost", typeof(int));

                    for (int i = 0; i < invoiceItems.Count; i++)
                    {

                        ds.Tables[0].Rows.Add(invoiceItems[i].itemCode, invoiceItems[i].lineItemDesc, invoiceItems[i].itemCost);

                        //tempCost += Convert.ToInt32(invoiceItems[i].itemCost);
                        //totalCostBox.Text = "$" + tempCost.ToString() + ".00";
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lineItemsDG.ItemsSource = ds.Tables[0].DefaultView;
                    }

                    itemList = invoiceItems;
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Button that opens a new Item window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openItemWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.clsMainLogic ML = new Main.clsMainLogic();

                ItemsWindow = new Items.wndItems();
                ItemsWindow.ShowDialog();

                //At this point we will add code to refresh the itemsComboBox
                //with the new Items that may have been added. We'll do this
                //by calling the function that populates the combo box again
                itemsComboBox.ItemsSource = ML.GetItems().Select(z => z.lineItemDesc).ToList();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Adds items to the lineItem DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemsComboBox.SelectedItem != null)
                {
                    Main.clsMainLogic ML = new Main.clsMainLogic();
                    DataSet ds = new DataSet("LineItems");
                    DataTable itemsTable = ds.Tables.Add("LineItem");
                    tempCost = 0;


                    itemList.Add(ML.itemToBeAdded(itemsComboBox.SelectedItem.ToString()));


                    itemsTable.Columns.Add("ItemCode", typeof(string));
                    itemsTable.Columns.Add("ItemDesc", typeof(string));
                    itemsTable.Columns.Add("Cost", typeof(int));

                    for (int i = 0; i < itemList.Count; i++)
                    {

                        ds.Tables[0].Rows.Add(itemList[i].itemCode, itemList[i].lineItemDesc, itemList[i].itemCost);

                        tempCost += Convert.ToInt32(itemList[i].itemCost);
                        totalCostBox.Text = "$" + tempCost.ToString() + ".00";
                    }


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lineItemsDG.ItemsSource = ds.Tables[0].DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes items from the lineItem DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemsComboBox.SelectedItem != null)
                {
                    if (itemList.Count == 1)
                    {
                        itemList.Remove(itemList[0]);
                        lineItemsDG.ItemsSource = itemList;
                        int tempCost = 0;

                        totalCostBox.Text = "$" + tempCost + ".00";
                    }
                    else
                    {
                        int tempIndex = lineItemsDG.SelectedIndex;
                        Main.clsMainLogic ML = new Main.clsMainLogic();
                        DataSet ds = new DataSet("LineItems");
                        DataTable itemsTable = ds.Tables.Add("LineItem");
                        tempCost = 0;


                        itemList.Remove(itemList[tempIndex]);

                        itemsTable.Columns.Add("ItemCode", typeof(string));
                        itemsTable.Columns.Add("ItemDesc", typeof(string));
                        itemsTable.Columns.Add("Cost", typeof(int));

                        for (int i = 0; i < itemList.Count; i++)
                        {

                            ds.Tables[0].Rows.Add(itemList[i].itemCode, itemList[i].lineItemDesc, itemList[i].itemCost);

                            tempCost += Convert.ToInt32(itemList[i].itemCost);
                            totalCostBox.Text = "$" + tempCost.ToString() + ".00";
                        }


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lineItemsDG.ItemsSource = ds.Tables[0].DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Saves currently selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(invoiceDateBox.Text))
                {
                    dateWarninglbl.Visibility = Visibility.Visible;
                }
                else
                {
                    dateWarninglbl.Visibility = Visibility.Hidden;
                    invoiceNumWarninglbl.Visibility = Visibility.Hidden;


                    Main.clsMainLogic ML = new Main.clsMainLogic();

                    if (invoiceNumBox.Text == "TBD")
                    {
                        invoiceNumBox.Text = (Convert.ToInt32(ML.getNextInvoiceNum()) + 1).ToString();

                        ML.createInvoice(invoiceDateBox.Text, tempCost.ToString(), invoiceNumBox.Text);
                        ML.createLineItem(itemList, invoiceNumBox.Text);
                    }
                    else
                    {
                        ML.deleteLineItems(Convert.ToInt32(invoiceNumBox.Text));
                        ML.createLineItem(itemList, invoiceNumBox.Text);

                        ML.updateInvoice(invoiceNumBox.Text, invoiceDateBox.Text, totalCostBox.Text);
                    }

                    invoiceDateBox.IsEnabled = false;
                    itemsComboBox.IsEnabled = false;
                    addItemBtn.IsEnabled = false;
                    deleteItemBtn.IsEnabled = false;
                    saveInvoiceBtn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes currently selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoiceNumBox.Text == "TBD")
                {
                    invoiceNumWarninglbl.Visibility = Visibility.Visible;
                    invoiceNumWarninglbl.Content = "Please select an invoice!";
                }
                else
                {
                    if (string.IsNullOrEmpty(invoiceDateBox.Text))
                    {
                        invoiceNumWarninglbl.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        invoiceNumWarninglbl.Visibility = Visibility.Hidden;

                        Main.clsMainLogic ML = new Main.clsMainLogic();
                        int invoiceToDelete = Convert.ToInt32(invoiceNumBox.Text);


                        ML.deleteLineItems(invoiceToDelete);
                        ML.deleteInvoice(invoiceToDelete);

                        invoiceDateBox.Clear();
                        invoiceNumBox.Clear();
                        itemList.Clear();
                        itemCostBox.Clear();
                        totalCostBox.Clear();
                        lineItemsDG.ItemsSource = itemList;

                        invoiceDateBox.IsEnabled = false;
                        itemsComboBox.IsEnabled = false;
                        addItemBtn.IsEnabled = false;
                        deleteItemBtn.IsEnabled = false;
                        saveInvoiceBtn.IsEnabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Allows user to edit currently selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceNumWarninglbl.Visibility = Visibility.Hidden;
                dateWarninglbl.Visibility = Visibility.Hidden;

                invoiceDateBox.IsEnabled = true;
                itemsComboBox.IsEnabled = true;
                addItemBtn.IsEnabled = true;
                deleteItemBtn.IsEnabled = true;
                saveInvoiceBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method for error handling
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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
