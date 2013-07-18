<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbSetup.aspx.cs" Inherits="dbSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="UserId" DataSourceID="SqlDataSource1" 
        EmptyDataText="There are no data records to display.">
        <Columns>
            <asp:BoundField DataField="ApplicationId" HeaderText="ApplicationId" 
                SortExpression="ApplicationId" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" 
                SortExpression="UserId" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:BoundField DataField="LoweredUserName" HeaderText="LoweredUserName" 
                SortExpression="LoweredUserName" />
            <asp:BoundField DataField="MobileAlias" HeaderText="MobileAlias" 
                SortExpression="MobileAlias" />
            <asp:CheckBoxField DataField="IsAnonymous" HeaderText="IsAnonymous" 
                SortExpression="IsAnonymous" />
            <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" 
                SortExpression="LastActivityDate" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:petomaccallum.caConnectionString1 %>" 
        DeleteCommand="DELETE FROM [aspnet_Users] WHERE [UserId] = @UserId" 
        InsertCommand="INSERT INTO [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (@ApplicationId, @UserId, @UserName, @LoweredUserName, @MobileAlias, @IsAnonymous, @LastActivityDate)" 
        ProviderName="<%$ ConnectionStrings:petomaccallum.caConnectionString1.ProviderName %>" 
        SelectCommand="SELECT [ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate] FROM [aspnet_Users]" 
        UpdateCommand="UPDATE [aspnet_Users] SET [ApplicationId] = @ApplicationId, [UserName] = @UserName, [LoweredUserName] = @LoweredUserName, [MobileAlias] = @MobileAlias, [IsAnonymous] = @IsAnonymous, [LastActivityDate] = @LastActivityDate WHERE [UserId] = @UserId">
        <DeleteParameters>
            <asp:Parameter Name="UserId" Type="Object" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ApplicationId" Type="Object" />
            <asp:Parameter Name="UserId" Type="Object" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="LoweredUserName" Type="String" />
            <asp:Parameter Name="MobileAlias" Type="String" />
            <asp:Parameter Name="IsAnonymous" Type="Boolean" />
            <asp:Parameter Name="LastActivityDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="ApplicationId" Type="Object" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="LoweredUserName" Type="String" />
            <asp:Parameter Name="MobileAlias" Type="String" />
            <asp:Parameter Name="IsAnonymous" Type="Boolean" />
            <asp:Parameter Name="LastActivityDate" Type="DateTime" />
            <asp:Parameter Name="UserId" Type="Object" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
