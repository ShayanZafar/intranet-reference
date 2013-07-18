<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="manageUsers.aspx.cs" Inherits="Administrator_manageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="../ui/css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
<%-- <script src="http://code.jquery.com/jquery-1.3.2.min.js" type="text/javascript"></script>
 <script type="text/javascript" src="../ui/js/jquery-ui-1.8.19.custom.min.js"></script>--%>
  <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.js"></script> 
 <script>

     //Code Starts
     $(document).ready(function () {
         $('#<%=recStatLBL.ClientID%>').css('display', 'none');
                  
   // add predictive text
         // get the data from database as a string array
         var data = new Array;
        
         <% foreach(string str in dbData){%>
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

    <div class="centerGrid">
    <h2>Viewing User Accounts on System</h2>
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
        <asp:Button ID="searchBtn"
            runat="server" Text="Search" AccessKey="S" /><asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
            View By Role:
              <asp:DropDownList ID="rolesDDL" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                
         <!-- Columns in DataKeyNames must be unique, they also will not be editable if specified in that attribute -->
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource1" 
            ForeColor="#333333" GridLines="None" 
            DataKeyNames="UserName" ViewStateMode="Enabled" PageSize="50" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
              
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="CreationDate" HeaderText="Created" ReadOnly="True" 
                    SortExpression="CreationDate" DataFormatString="{0:f}" />
                <asp:BoundField DataField="UserName" HeaderText="Username" ReadOnly="True" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" 
                    Visible="False" />
                <asp:BoundField DataField="PasswordQuestion" HeaderText="Password Question" 
                    ReadOnly="True" SortExpression="PasswordQuestion" Visible="False" />
                <asp:BoundField DataField="Comment" HeaderText="Comment" 
                    SortExpression="Comment" Visible="False" />
                <asp:CheckBoxField  DataField="IsApproved" HeaderText="Enabled" 
                    SortExpression="IsApproved" />
                <asp:CheckBoxField  DataField="IsLockedOut" HeaderText="Locked"  
                    SortExpression="IsLockedOut" ReadOnly="true" />
                <asp:BoundField DataField="LastLockoutDate" HeaderText="Last Lockout" 
                    ReadOnly="True" SortExpression="LastLockoutDate" 
                    DataFormatString="{0:f}" Visible="False" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login" 
                    SortExpression="LastLoginDate" DataFormatString="{0:f}" ReadOnly="true" />
                <asp:BoundField DataField="LastActivityDate" HeaderText="Last Active" 
                    SortExpression="LastActivityDate" DataFormatString="{0:f}" ReadOnly="true" />
                <asp:BoundField DataField="LastPasswordChangedDate" 
                    HeaderText="Password Changed" ReadOnly="True" 
                    SortExpression="LastPasswordChangedDate" DataFormatString="{0:f}" 
                    Visible="False" />
                <asp:CheckBoxField DataField="IsOnline" HeaderText="Online" ReadOnly="True" 
                    SortExpression="IsOnline" />
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
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="getUsersForRole" TypeName="AdminManager" 
            DeleteMethod="deleteUser" UpdateMethod="updateUser">
            <DeleteParameters>
                <asp:Parameter Name="UserName" Type="String" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="rolesDDL" Name="role" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="IsApproved" Type="Boolean" />
                <asp:Parameter Name="UserName" Type="String" />
           
            </UpdateParameters>
        </asp:ObjectDataSource>

</div>

</asp:Content>