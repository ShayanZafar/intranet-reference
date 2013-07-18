<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="ManageEmployees.aspx.cs" Inherits="Managers_ManageEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div style="float:left;">
        <h2>
            Users With Incomplete Timesheets:</h2>
        <h4>
            Viewing Users:</h4>
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
        <asp:Button ID="searchBtn" runat="server" Text="Search" />
        <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
        &nbsp;
        &nbsp;&nbsp;Week Ending:&nbsp;
        <asp:DropDownList ID="datesDDL" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" PageSize="50">
            <Columns>
                <asp:BoundField DataField="empNo" HeaderText="Emp No" SortExpression="empNo">
                </asp:BoundField>
                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" 
                    Visible="False">
                </asp:BoundField>
                <asp:BoundField DataField="username" HeaderText="Username" 
                    SortExpression="username">
                </asp:BoundField>
                <asp:BoundField DataField="password" HeaderText="password" 
                    SortExpression="password" Visible="False" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                    SortExpression="FirstName">
                </asp:BoundField>
                <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" 
                    SortExpression="MiddleName">
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                    SortExpression="LastName" />
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role"/>
                <asp:BoundField DataField="ManagedBy" HeaderText="Managed By" 
                    SortExpression="ManagedBy" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" 
                    Visible="False" />
                <asp:BoundField DataField="branch" HeaderText="Branch" 
                    SortExpression="branch" />
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getUsersWithPendingTimeSheet"
            TypeName="TimesheetManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="datesDDL" Name="weekending" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:Parameter Name="sort" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </div>
</asp:Content>

