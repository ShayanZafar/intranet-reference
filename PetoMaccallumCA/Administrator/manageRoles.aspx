<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="manageRoles.aspx.cs" Inherits="Administrator_manageRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="center">
        <h2>
            Manage User Roles</h2>
        <asp:ListBox ID="rolesLB" runat="server" Height="300px" Width="300px"></asp:ListBox>
        <h2>
            Add a new Role</h2>
     
        <asp:TextBox ID="newRoleTB" runat="server"></asp:TextBox>
        <asp:Button ID="addbtn" runat="server" Text="Add Role" OnClick="addbtn_Click" />
        <asp:Label ID="statusLBL" runat="server" Text=""></asp:Label>
   </div>

</asp:Content>



