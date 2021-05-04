<%--Dominic Lanza
Professor Pascucci 
Server Side
02/26/2021--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PizzaProject.Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dom&Nicks Pizza Shop</title>
    <link href="PizzaStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <%--    Heading outside of Form--%>
    <h2>Benvenuto</h2>
    <h3 id="WelcomeH">Welcome to Dom&Nicks Gourmet Pizza</h3>

    <%--    Beginning of Form--%>
    <form id="frmPizzaShop" runat="server">

        <%--        Image for Pizza Shop--%>
        <asp:Image ID="Image1" ImageUrl="~/Images/Dom&Nicks.jpg" runat="server" />

        <%--        Div for input of Username, Address, Phone Number--%>
        <div>
            <p>
                Please input your name:
                <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox>
            </p>
            <p>
                Please input your address:
                <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox>
            </p>
            <p>
                Please input your Phone Number:
                <asp:TextBox ID="TextBoxPhoneNumber" runat="server"></asp:TextBox>
            </p>
        </div>
        <%--        Div for delivery preference--%>
        <div>
            <p>
                <b>Please select your delivery option:</b>
                <asp:RadioButtonList ID="RadioButtonListPickup" runat="server" OnSelectedIndexChanged="RadioButtonListPickup_SelectedIndexChanged">
                    <asp:ListItem Value="Delivery">Delivery</asp:ListItem>
                    <asp:ListItem Value="Pickup">Pickup</asp:ListItem>
                </asp:RadioButtonList>
            </p>
        </div>
        <%--        Help labels for user assistance--%>
        <h3>
            <asp:Label ID="lbHint" runat="server" Text="Please select at least one pizza."></asp:Label></h3>

        <p>
            <asp:Label ID="lblCheckboxHelp" runat="server" Text="" Visible="false"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblQuantityHelp" runat="server" Text="" Visible="false"></asp:Label>
        </p>

        <%--        First gridview which is all the data to order a pizza from Pizza.DBO--%>
        <div>
            <asp:GridView ID="gv_Input" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckboxSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PizzaType" HeaderText="Pizza Type" />
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSize" runat="server">
                                <asp:ListItem Value="small" Text="Small"></asp:ListItem>
                                <asp:ListItem Value="medium" Text="Medium"></asp:ListItem>
                                <asp:ListItem Value="large" Text="Large"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="TextboxQuantity" runat="server" type="number"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
        </div>

        <br>
        <%--        Label for Name, Address, and Phone--%>
        <asp:Label ID="lblNameAddressPhone" runat="server" Text="Label"></asp:Label>
        <div>
            <br>

            <%--        Second gridview which is the data that user has selected to order--%>
            <asp:GridView ID="gv_Output" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="PizzaType" HeaderText="Pizza Type" />
                    <asp:BoundField DataField="Size" HeaderText="Size" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                </Columns>
            </asp:GridView>

            <%-- Label for gridview  --%>

            <br>
            <asp:Label ID="gv_totalSalesLabels" runat="server" Text="Total Sales" Visible="false"></asp:Label>
            <br>
            <%-- Total Sales Gridview --%>

            <asp:GridView ID="gv_TotalSales" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="PizzaType" HeaderText="Pizza Type" />
                    <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" />
                </Columns>
            </asp:GridView>
        </div>
        <br>

        <%-- Label for Total Quantity Gridview --%>

        <asp:Label ID="gv_TotalQuantityLabel" runat="server" Text="Total Quantity" Visible="false"></asp:Label>
        <br>
        <%-- Total Quantity Gridview --%>

        <div>
            <asp:GridView ID="gv_TotalQuantity" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="PizzaType" HeaderText="Pizza Type" />
                    <asp:BoundField DataField="TotalQuantityOrdered" HeaderText="Total Quantity" />
                </Columns>
            </asp:GridView>

        </div>
        <%-- Submit button --%>

        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
        <br>
        <%-- Back to order button --%>

        <div>
            <asp:Button ID="btnBackToOrder" runat="server" Text="Back To Order" OnClick="btnBackToOrder_Click" />
        </div>
        <br>
        <%-- Show total Sales and Quantity tbutton --%>

        <div>
            <asp:Button ID="btnShowTotalSalesAndQuantity" runat="server" Text="Show Total Quantity and Sales" OnClick="btnShowTotalSalesAndQuantity_Click" />
        </div>
        <br>
        <%-- Hide total Sales and Quantity button --%>

        <div>
            <asp:Button ID="btnHideTotalSalesAndQauntity" runat="server" Text="Hide Total Quantity and Sales" OnClick="btnHideTotalSalesAndQauntity_Click" />
        </div>
        <br>
    </form>
</body>
</html>
