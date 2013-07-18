<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="viewTimesheets.aspx.cs" Inherits="Managers_viewTimesheets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../ui/css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
 <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.js"></script>
    <script>

     //Code Starts
     $(document).ready(function () {
         $('#<%=recStatLBL.ClientID%>').css('display', 'none');

   // add predictive text
         // get the data from database as a string array
         var data = new Array;

         <%
          foreach(string str in dbData){%>
            data.push('<%= str %>');
            <% } %>

         // apply predictive text to the search box
         $('#<%=searchTB.ClientID%>').autocomplete({
         autoFocus: true,
             source: data
         });

         $('#<%=searchBtn.ClientID%>').click(function (e) {
             // Hide No records to display label.
             $('#<%=recStatLBL.ClientID%>').css('display', 'none');
             //Hide all the rows.
             $("#<%=GridView1.ClientID%> tr:has(td)").hide();

             var iCounter = 0;
             //Get the search box value
             var sSearchTerm = $('#<%=searchTB.ClientID%>').val();

             //if nothing is entered then show all the rows.
             if (sSearchTerm.length == 0) {
                 $("#<%=GridView1.ClientID%> tr:has(td)").show();
                 return false;
             }
             //Iterate through all the td.
             $("#<%=GridView1.ClientID%> tr:has(td)").children().each(function () {
                 var cellText = $(this).text().toLowerCase();
                 //Check if data matches
                 if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) {
                     $(this).parent().show();
                     iCounter++;
                     return true;
                 }
             });
             if (iCounter == 0) {
                 $('#<%=recStatLBL.ClientID%>').css('display', '');
             }
             e.preventDefault();
         })
     })
     //Code Ends
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float:left;">
        <h2>
            Manage Timesheets:</h2>
        <h4>
            Viewing Timesheets:</h4>
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
        <asp:Button ID="searchBtn" runat="server" Text="Search" AccessKey="S" />
        <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
        &nbsp;TimeSheet Status:&nbsp;
        <asp:DropDownList ID="statusDDL" runat="server" AutoPostBack="True" 
            onselectedindexchanged="statusDDL_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;Branch:&nbsp;
        <asp:DropDownList ID="branchesDDL" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;Week Ending:&nbsp;
        <asp:DropDownList ID="datesDDL" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" PageSize="50">
            <Columns>
                <asp:HyperLinkField NavigateUrl="~/Member/createTimeSheet.aspx" Text="View" 
                    DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Member/createTimeSheet.aspx?time={0}" 
                    Visible="False" />
                <asp:HyperLinkField DataNavigateUrlFields="TimesheetId" DataNavigateUrlFormatString="~/Managers/manageTimeSheet.aspx?time={0}"
                    NavigateUrl="~/Managers/manageTimeSheet.aspx" Text="Manage" />
                <asp:BoundField DataField="WeekEnding" HeaderText="Week Ending" SortExpression="WeekEnding">
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Managers/viewTimesheets.aspx?time={0}&amp;sync=sync" 
                    NavigateUrl="~/Managers/viewTimesheets.aspx" Text="Sync" Visible="False" />
                <asp:BoundField DataField="DateCreated" HeaderText="Created" SortExpression="DateCreated">
                </asp:BoundField>
                <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" SortExpression="ModifiedBy">
                </asp:BoundField>
                <asp:BoundField DataField="DateApproved" HeaderText="Approved" SortExpression="DateApproved" />
                <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" SortExpression="ApprovedBy">
                </asp:BoundField>
                <asp:BoundField DataField="ManagerComments" HeaderText="Manager Comments" SortExpression="ManagerComments"
                    Visible="False" />
                <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="EmployeeName">
                </asp:BoundField>
                <asp:BoundField DataField="TotalHours" HeaderText="Total Hrs" SortExpression="TotalHours" DataFormatString="{0:#0.0}"/>
                <asp:BoundField DataField="TotalDistance" HeaderText="Total Dist" SortExpression="TotalDistance"
                    DataFormatString="{0:##0 KM}" />
                <asp:BoundField DataField="TotalExpenses" HeaderText="Total Exp" SortExpression="TotalExpenses"
                    DataFormatString="$ {0:###0.00}" />
                <asp:BoundField DataField="TotalTruck" HeaderText="Total Truck" 
                    DataFormatString="{0:##0 KM}" />
                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Managers/viewTimesheets.aspx?time={0}&amp;reject=reject" 
                    NavigateUrl="~/Managers/viewTimesheets.aspx" Text="Reject" />
                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Managers/viewTimesheets.aspx?time={0}&amp;accept=accept" 
                    NavigateUrl="~/Managers/viewTimesheets.aspx" Text="Accept" Visible="False" />
                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId" 
                    DataNavigateUrlFormatString="~/Member/createTimeSheet.aspx?time={0}&amp;view=view" 
                    NavigateUrl="~/Member/createTimeSheet.aspx" Text="Peek" Visible="False" />
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getTimesheetsForStatus"
            TypeName="TimesheetManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="statusDDL" Name="status" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="branchesDDL" Name="branch" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="datesDDL" Name="weekending" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>