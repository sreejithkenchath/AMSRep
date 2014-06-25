<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AMS.Master" AutoEventWireup="true" CodeBehind="CreateUserAdmin.aspx.cs" Inherits="AMS_SuperAdmin.Forms.CreateUserAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div class="breadcrumb_holder">
        <div class="container">
            <ol class="breadcrumb">
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
                                
                                <asp:Label id="ServerValidation" Text="" runat="server"/>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    HeaderText="There were errors on the page:" />
                                    <table>

                                    <tr>
                                        <td width="1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" display="dynamic" runat="server" 
                                            ControlToValidate="uName"
                                            ErrorMessage="Username cannot contain spaces."
                                            validationexpression="^[^\s]+$">*
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="uName"
                                            ErrorMessage="Username is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>User Name</td>
                                        <td><asp:TextBox id="uName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="fName"
                                            ErrorMessage="First Name is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>First Name</td>
                                        <td><asp:TextBox id="fName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="lName"
                                            ErrorMessage="Last Name is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Last Name</td>
                                        <td><asp:TextBox id="lName" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                    <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ControlToValidate="TitleList"
                                            ErrorMessage="Title is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Title</td>
                                        <td>
                                            <asp:DropDownList id="TitleList" runat="server">
                                            <asp:ListItem Selected="True" Value="Mr."> Mr. </asp:ListItem>
                                                <asp:ListItem Value="Mrs."> Mrs. </asp:ListItem>
                                                <asp:ListItem Value="Ms."> Ms. </asp:ListItem>
                                                <asp:ListItem Value="Miss"> Miss </asp:ListItem>
                                                <asp:ListItem Value="Dr."> Dr. </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                            ControlToValidate="description"
                                            ErrorMessage="Description is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Description</td>
                                        <td><asp:TextBox id="description" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                            ControlToValidate="phone"
                                            ErrorMessage="Phone Number is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Phone Number</td>
                                        <td><asp:TextBox id="phone" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" display="dynamic" runat="server" 
                                            ControlToValidate="uEmail"
                                            ErrorMessage="Email must be in the proper format"
                                            validationexpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                            ControlToValidate="uEmail"
                                            ErrorMessage="Email is required.">*
                                        </asp:RequiredFieldValidator>
                                        </td>
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
