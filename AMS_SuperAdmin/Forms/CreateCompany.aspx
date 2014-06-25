<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AMS.Master" AutoEventWireup="true" CodeBehind="CreateCompany.aspx.cs" Inherits="AMS_SuperAdmin.Forms.CreateCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div class="breadcrumb_holder">
        <div class="container">
            <ol class="breadcrumb">
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

                                    <asp:Label id="ServerValidation" Text="" runat="server"/>

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    HeaderText="There were errors on the page:" />

                                    <table>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="cName"
                                            ErrorMessage="Company Name is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Company Name</td>
                                        <td><asp:TextBox id="cName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="cAddress"
                                            ErrorMessage="Company Address is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Company Address</td>
                                        <td><asp:TextBox id="cAddress" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" display="dynamic" runat="server" 
                                            ControlToValidate="cPhone"
                                            ErrorMessage="Phone number can only contain numbers."
                                            validationexpression="^[0-9]+$">*
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="cPhone"
                                            ErrorMessage="Company Phone is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
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
