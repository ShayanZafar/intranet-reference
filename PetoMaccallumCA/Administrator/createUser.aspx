<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="createUser.aspx.cs" Inherits="Administrator_createUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centerGrid">
        <h2>
            Create a User</h2>
        <table>
            <tr>
                <td class="box">
                    <h4>
                        Employee Number:</h4>
                    <asp:TextBox ID="empNoTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ControlToValidate="empNoTB" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="box">
                    <h4>
                        First Name</h4>
                    <asp:TextBox ID="fNameTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="fNameTB" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
                <td class="box">
                    <h4>
                        Middle Name</h4>
                    <asp:TextBox ID="mNameTB" runat="server"></asp:TextBox>
                </td>
                <td class="box">
                    <h4>
                        Last Name</h4>
                    <asp:TextBox ID="lNameTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        ControlToValidate="lNameTB" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="box">
                    <h4>
                        User Role</h4>
                    <asp:DropDownList ID="roleDDL" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="roleDDL" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
                <td class="box">
                    <h4>
                        Branch</h4>
                    <asp:DropDownList ID="branchDDL" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        ControlToValidate="branchDDL" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="box">
                    <h4>
                        Username</h4>
                    <asp:TextBox ID="userTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                        ControlToValidate="userTB" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
                <td class="box">
                    <h4>
                        Password</h4>
                    <asp:TextBox ID="passwordTB" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                        ControlToValidate="passwordTB" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="passwordTB"
                        ErrorMessage="6-10 characters and 1 special character" ValidationExpression="(?=^.{6,15}$)(?=(?:.*?\d){0})(?=.*[a-z])(?=(?:.*?[A-Z]){0})(?=(?:.*?[!@#$%*()_+^&amp;}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#$%*()_+^&amp;]*$"></asp:RegularExpressionValidator>
                    <asp:Button ID="createBtn" runat="server" Text="Create User" OnClick="createBtn_Click"
                        ValidationGroup="user" />
                    <asp:Label ID="statusLBL" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
