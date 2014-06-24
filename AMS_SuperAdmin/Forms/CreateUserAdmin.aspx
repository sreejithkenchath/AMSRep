<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AMS.Master" AutoEventWireup="true" CodeBehind="CreateUserAdmin.aspx.cs" Inherits="AMS_SuperAdmin.Forms.CreateUserAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div class="breadcrumb_holder">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="#">Home</a></li><li><a href="#">About Us</a></li><li><a href="#">Dashboard</a></li>
                <li><span class="activebread">Create Company</span></li>
                <li><a href="ViewCompanies.aspx">View Company</a></li>
            </ol>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sidemenu_wrapper">
                            <div class="sidemenu_head">Create User Admin  </div>
                            <div class="sidemenu" style="min-height:400px;">
                                <form id="form1" runat="server">

                                    <table>

                                    <tr>
                                        <td>User Name</td>
                                        <td><asp:TextBox id="uName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>First Name</td>
                                        <td><asp:TextBox id="fName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Last Name</td>
                                        <td><asp:TextBox id="lName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Title</td>
                                        <td><asp:TextBox id="title" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Description</td>
                                        <td><asp:TextBox id="description" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Phone Number</td>
                                        <td><asp:TextBox id="phone" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Email</td>
                                        <td><asp:TextBox id="uEmail" runat="server" /></td>
                                    </tr>

                                    </table>

                                    <asp:Button ID="submitbtn"  runat="server" Text="Submit" 
                                        onclick="submit_Click" />


                                </form>
                            </div>
                        </div>
</asp:Content>
