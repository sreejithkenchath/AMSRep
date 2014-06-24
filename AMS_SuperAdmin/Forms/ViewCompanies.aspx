<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AMS.Master" AutoEventWireup="true" CodeBehind="ViewCompanies.aspx.cs" Inherits="AMS_SuperAdmin.Forms.ViewCompanies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div class="breadcrumb_holder">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="#">Home</a></li><li><a href="#">About Us</a></li><li><a href="#">Dashboard</a></li><li><a href="CreateCompany.aspx">Create Company</a></li>
                <li ><span class="activebread">View Company</span></li>
            </ol>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sidemenu_wrapper">
                            <div class="sidemenu_head">View Company  </div>
                            <div class="sidemenu" style="min-height:400px;">
                                <asp:GridView ID="GridViewCompanies" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CompanyID" HeaderText="CompanyID" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                                    <asp:BoundField DataField="CompanyAddress" HeaderText="CompanyAddress" />
                                    <asp:BoundField DataField="CompanyPhone" HeaderText="CompanyPhone" />
                                    <asp:BoundField DataField="UserAdmins" HeaderText="UserAdmins" />
                                    <asp:HyperLinkField DataNavigateUrlFields="AddUserAdmin" DataNavigateUrlFormatString="CreateUserAdmin.aspx?companyID={0}"
                    DataTextField="AddUserAdmin" NavigateUrl="hello" HeaderText="AddUserAdmin" />
                                </Columns>
                                </asp:GridView>
                            </div>
                        </div>
</asp:Content>
