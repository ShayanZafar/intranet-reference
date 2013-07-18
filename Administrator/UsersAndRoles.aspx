<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="UsersAndRoles.aspx.cs" Inherits="Administrator_UsersAndRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="center">
<h2>Add/Remove Roles from User</h2>

    <div>
        <h3>
            Select a User:</h3>
        <asp:DropDownList ID="userDDL" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource1">
        </asp:DropDownList>
 
    </div>
    <div>
        <h3>User is in Following Roles:</h3>
        <asp:ListBox ID="userRolesLB" runat="server" DataSourceID="ObjectDataSource2" 
            Height="100px" Width="150px"></asp:ListBox>
         <asp:Button ID="removeBtn" runat="server" Text="Remove Role" 
            onclick="removeBtn_Click" AccessKey="R" />
    </div>
    <div>
    <h3>Add Role to Selected User</h3>
        <asp:DropDownList ID="rolesDDL" runat="server">
        </asp:DropDownList>
        <asp:Button ID="addBtn" runat="server" Text="Add User to Role" 
            OnClick="addBtn_Click" AccessKey="A" />
    </div>
               <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getUsers"
            TypeName="AdminManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="rolesForUser"
            TypeName="AdminManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="userDDL" Name="username" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div>
        <h3>Add Branch Manager to Selected User</h3>
            <asp:DropDownList ID="managerDDL" runat="server">
             
            </asp:DropDownList>
            <asp:Button ID="managerBtn" runat="server" Text="Add Manager" 
                onclick="managerBtn_Click" AccessKey="M" />
        </div>




    <asp:Label ID="statusLBL" runat="server" Text=""></asp:Label>


</div>
</asp:Content>


