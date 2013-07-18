<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="Member_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Member Profile</h2>
<h3>Welcome to your personal Peto Maccallum Account</h3>
 <div style="float:left;">
           <h4>
           Notifications:</h4>
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
        <asp:Button ID="searchBtn" runat="server" Text="Search" />
        <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
     
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" PageSize="50" DataKeyNames="EmployeeId">
            <Columns>
                <asp:HyperLinkField NavigateUrl="~/Member/createTimeSheet.aspx" Text="View" 
                    DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Member/createTimeSheet.aspx?time={0}" />

                <asp:BoundField DataField="WeekEnding" HeaderText="Week Ending" SortExpression="WeekEnding">
                </asp:BoundField>
                <asp:BoundField DataField="DateCreated" HeaderText="Created" SortExpression="DateCreated">
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="EmployeeName">
                </asp:BoundField>
                <asp:BoundField DataField="TotalHours" HeaderText="Total Hrs" SortExpression="TotalHours" DataFormatString="{0:#0.0}"/>
                <asp:BoundField DataField="TotalDistance" HeaderText="Total Dist" SortExpression="TotalDistance"
                    DataFormatString="{0:##0 KM}" />
                <asp:BoundField DataField="TotalExpenses" HeaderText="Total Exp" SortExpression="TotalExpenses"
                    DataFormatString="$ {0:###0.00}" />
                <asp:BoundField DataField="TotalTruck" HeaderText="Total Truck" />
            
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
            
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
        </asp:GridView>
           <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getTimeSheetForStatusAndEmployee"
            TypeName="TimesheetManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="HiddenField1" Name="empId" PropertyName="Value"
                    Type="Int32" DefaultValue="" />
                <asp:Parameter DefaultValue="Rejected" Name="status" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>

</asp:Content>

