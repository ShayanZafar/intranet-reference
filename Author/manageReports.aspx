﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="manageReports.aspx.cs" Inherits="Author_manageReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../ui/css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../ui/js/jquery-1.7.2.min.js"></script>
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
            data.push("<%= str %>");
            <% } %>

         // apply predictive text to the search box
         $('#<%=searchTB.ClientID%>').autocomplete({
         autoFocus: true,
             source: data
         });

         $('#<%=searchBtn.ClientID%>').click(function(e) {
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
             $("#<%=GridView1.ClientID%> tr:has(td)").children().each(function() {
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
         });
     })
     //Code Ends
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="centerGrid">
    <h2>
        
        Manage Reports:</h2>
    <h4>
        Viewing Reports</h4>
    
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
    <%--    <input type="button" id="searchBtn" value="Search">--%>
        <asp:Button ID="searchBtn" runat="server" Text="Search" AccessKey="S" 
            />
            <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
            Report Type:
            <asp:DropDownList ID="categoriesDDL" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="3" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" 
            DataKeyNames="ReportId" PageSize="50">
          
            <Columns>
           
               
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="DateCreated" HeaderText="Date Uploaded" 
                    SortExpression="DateCreated" DataFormatString="{0:ddd, MMM d, yyyy}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DateModified" HeaderText="Date Modified" 
                    SortExpression="DateModified" Visible="False" DataFormatString="{0:f}" />
                <asp:HyperLinkField DataNavigateUrlFields="ReportId" 
                    DataNavigateUrlFormatString="~/Member/Default.aspx?id={0}" DataTextField="Name" 
                    DataTextFormatString="{0}" HeaderText="File Name" 
                    NavigateUrl="~/Member/Default.aspx" >
                <ItemStyle ForeColor="Blue" />
                </asp:HyperLinkField>
                         <asp:BoundField DataField="Category" HeaderText="Report Category" 
                    SortExpression="Category" Visible="False" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Extension" HeaderText="File Type" 
                    SortExpression="Extension" Visible="False" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Size" HeaderText="File Size" SortExpression="Size" 
                    DataFormatString="{0:#,###0 KB}" />
                <asp:BoundField DataField="Role" HeaderText="Level of Access" 
                    SortExpression="Role" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="ReportDate" HeaderText="Report Date" 
                    SortExpression="ReportDate" DataFormatString="{0:f}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="Type" HeaderText="Type Of Report" 
                    SortExpression="Type" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getReports"
            SortParameterName="sort" TypeName="ReportManager" 
            DeleteMethod="deleteReport">
            <DeleteParameters>
                <asp:Parameter Name="ReportId" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" Name="sort" 
                    PropertyName="SelectedRow" Type="String" />
                <asp:ControlParameter ControlID="categoriesDDL" Name="category" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
       
        </div>
</asp:Content>

