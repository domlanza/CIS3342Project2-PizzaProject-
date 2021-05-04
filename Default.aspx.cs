using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using PizzaLibrary;
using System.Data;
using System.Collections;

namespace PizzaProject
{
    public partial class Default : System.Web.UI.Page
    {
        //ArrayList for Pizza
        List<Pizza> pizzaList = new List<Pizza>();
        //Connect to DB
        DBConnect dBConnect = new DBConnect();
        //Call Order class to create new order
        Order order = new Order();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInput(true);
                displayInputData();
                lblNameAddressPhone.Visible = false;
                btnBackToOrder.Visible = false;
                btnShowTotalSalesAndQuantity.Visible = false;
                btnHideTotalSalesAndQauntity.Visible = false;
                lblCheckboxHelp.Visible = true;
                lblQuantityHelp.Text = "";
                lblCheckboxHelp.Text = "";
            }
        }
      
        public void ShowInput(Boolean tf)
        {
            lbHint.Visible = tf;
            gv_Input.Visible = tf;
        }
        
        public void displayInputData()
        {
            String str = "SELECT * FROM Pizza";
            DataSet mydata = dBConnect.GetDataSet(str);

            gv_Input.DataSource = mydata;
            gv_Input.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Name Validation
            String username = TextBoxUsername.Text;
            if (username == "")
            {
                Response.Write("<script>alert('Type in your Name')</script>");
            }

            //Address Validation
            String address = TextBoxAddress.Text;
            if (address == "")
            {
                Response.Write("<script>alert('Type in your Address')</script>");
            }

            //Phone Number Validation
            String phonenumber = TextBoxPhoneNumber.Text;
            if (phonenumber == "")
            {
                Response.Write("<script>alert('Type in your Phone Number')</script>");
            }

            //Delivery or Pickup Validation
            if (RadioButtonListPickup.SelectedValue != "Delivery" && RadioButtonListPickup.SelectedValue != "Pickup")
            {
                Response.Write("<script>alert('Please check Delivery option')</script>");
            }
            
            //total price variable
            double totPrice = 0;
            //count checkboxes
            int count = 0;
        
            //beginning checking rows in input gridView
            for (int row = 0; row < gv_Input.Rows.Count; row++)
            {
                CheckBox Cbox = (CheckBox)gv_Input.Rows[row].FindControl("CheckboxSelect");
                TextBox Tbox = (TextBox)gv_Input.Rows[row].FindControl("TextboxQuantity");

                //If Checkbox is checked
                if (Cbox.Checked)
                {
                    //Check if Tbox is null, if it is, Display error msg.
                    if (Tbox.Text == null || string.IsNullOrEmpty(Tbox.Text) == true)
                    {
                        lblQuantityHelp.Visible = true;
                        lblQuantityHelp.Text = "Please input a Quantity to proceed";
                    }
                    //if Textbox with quantity is filled proceed
                    else
                    {
                        //create the new pizza object
                        Pizza objPizza = new Pizza();

                        //Hide griw view input
                        gv_Input.Visible = false;

                        //add to the pizza object
                        string pizzaTypeGv = gv_Input.Rows[row].Cells[1].Text;
                        objPizza.PizzaType = pizzaTypeGv;

                        DropDownList PizzaSize = (DropDownList)gv_Input.Rows[row].FindControl("ddlSize");
                        objPizza.Size = PizzaSize.SelectedValue.ToString();

                        int pizzaQuantity = Convert.ToInt32(Tbox.Text);
                     
                        //int pizzaQuantity = Convert.ToInt32(Tbox.Text);
                        objPizza.Quantity = pizzaQuantity;

                        double PizzaPrice = Order.getPrice(objPizza.PizzaType, objPizza.Size);
                        objPizza.Price = PizzaPrice;

                        double TotalCostGV = (PizzaPrice * pizzaQuantity);
                        objPizza.TotalCost = TotalCostGV;

                        totPrice += TotalCostGV;

                        //add pizza to the array list
                        pizzaList.Add(objPizza);

                        //Hide Name box, Address, PhoneNumber
                        TextBoxUsername.Visible = false;
                        TextBoxAddress.Visible = false;
                        TextBoxPhoneNumber.Visible = false;
                        //Display Name Address Phone input
                        lblNameAddressPhone.Visible = true;
                        lblNameAddressPhone.Text = "Name: " + username + " Phone Number: " + phonenumber + " Address: " + address;

                        //Move data to gv_Output
                        gv_Output.DataSource = pizzaList;
                        gv_Output.Columns[0].FooterText = "Totals:";
                        gv_Output.Columns[4].FooterText = totPrice.ToString();

                        gv_Output.DataBind();
                        //make gv_ouput visible
                        gv_Output.Visible = true;

                        //update database
                        Order.updateDB(pizzaTypeGv, pizzaQuantity, PizzaPrice);

                        ///Populate the gv_Total Sales with records
                        String str = "SELECT PizzaType, TotalSales FROM Pizza ORDER BY TotalSales DESC";
                        DataSet TotalSalesGV = dBConnect.GetDataSet(str);

                        gv_TotalSales.DataSource = TotalSalesGV;
                        gv_TotalSales.DataBind();


                        ////populate the Total Quantity Gridview with records

                        String str2 = "SELECT PizzaType, TotalQuantityOrdered FROM Pizza ORDER BY TotalQuantityOrdered DESC";
                        DataSet TotalQuantityGV = dBConnect.GetDataSet(str2);

                        gv_TotalQuantity.DataSource = TotalQuantityGV;
                        gv_TotalQuantity.DataBind();

                        //Hide the buttons
                        btnBackToOrder.Visible = true;
                        btnSubmit.Visible = false;
                        btnShowTotalSalesAndQuantity.Visible = true;

                        //Hide the gridviews
                        gv_TotalQuantity.Visible = false;
                        gv_TotalSales.Visible = false;

                        //Controls hide
                        TextBoxUsername.Visible = false;
                        TextBoxPhoneNumber.Visible = false;
                        TextBoxAddress.Visible = false;
                        RadioButtonListPickup.Visible = false;
                        lbHint.Visible = false;
                        lblCheckboxHelp.Text = "";

                        //increment count
                        count = count + 1;
                    }
                }                
                }//end of for loop
            //If Checkbox is not checked display error message
            if (count == 0) {
                lblCheckboxHelp.Visible = true;
                lblCheckboxHelp.Text = "You must select one pizza";
                lblCheckboxHelp.Focus();
                return;
            }
        }


        protected void RadioButtonListPickup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBackToOrder_Click(object sender, EventArgs e)
        {
            //Visibility controls to show first screen
            lblCheckboxHelp.Visible = false;
            lblQuantityHelp.Visible = false;
            lblQuantityHelp.Text = "";
            btnBackToOrder.Visible = false;
            btnSubmit.Visible = true;
            gv_Input.Visible = true;
            gv_Output.Visible = false;
            gv_TotalQuantity.Visible = false;
            gv_TotalSales.Visible = false;

            TextBoxUsername.Visible = true;
            TextBoxPhoneNumber.Visible = true;
            TextBoxAddress.Visible = true;
            RadioButtonListPickup.Visible = true;
            lbHint.Visible = true;
        }

        protected void btnShowTotalSalesAndQuantity_Click(object sender, EventArgs e)
        {
            //Display gridviews
            gv_TotalQuantity.Visible = true;
            gv_TotalSales.Visible = true;
            btnShowTotalSalesAndQuantity.Visible = false;
            btnHideTotalSalesAndQauntity.Visible = true;
            gv_TotalQuantityLabel.Visible = true;
            gv_totalSalesLabels.Visible = true;

        }

        protected void btnHideTotalSalesAndQauntity_Click(object sender, EventArgs e)
        {
            //Hide grid views
            gv_TotalQuantity.Visible = false;
            gv_TotalSales.Visible = false;
            btnShowTotalSalesAndQuantity.Visible = true;
            btnHideTotalSalesAndQauntity.Visible = false;
            gv_TotalQuantityLabel.Visible = false;
            gv_totalSalesLabels.Visible = false;

        }
    }
}