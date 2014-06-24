<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AMS.Master" AutoEventWireup="true" CodeBehind="CreateCompany.aspx.cs" Inherits="AMS_SuperAdmin.Forms.CreateCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div class="breadcrumb_holder">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="#">Home</a></li><li><a href="#">About Us</a></li><li><a href="#">Dashboard</a></li>
                <li ><span class="activebread">Create Company</span></li>
                <li><a href="ViewCompanies.aspx">View Company</a></li>
            </ol>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sidemenu_wrapper">
                            <div class="sidemenu_head">Create Company  </div>
                            <div class="sidemenu" style="min-height:400px;">
                                <form id="form1" runat="server">

                                    <table>

                                    <tr>
                                        <td>Company Name</td>
                                        <td><asp:TextBox id="cName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Company Address</td>
                                        <td><asp:TextBox id="cAddress" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td>Company Phone Number</td>
                                        <td><asp:TextBox id="cPhone" runat="server" /></td>
                                    </tr>

                                    </table>

                                    <asp:Button ID="submitbtn"  runat="server" Text="Submit" 
                                        onclick="submit_Click" />


                                </form>
                            </div>
                        </div>
</asp:Content>
