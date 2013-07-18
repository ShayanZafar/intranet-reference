<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="manageDocuments.aspx.cs" Inherits="Administrator_manageDocuments" %>

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
     function hideRows(){
     
     }
 </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="centerGrid">
    <h2>
        
        Manage Documents:</h2>
    <h4>
        Viewing Documents</h4>
   
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
    <%--    <input type="button" id="searchBtn" value="Search">--%>
        <asp:Button ID="searchBtn" runat="server" Text="Search" AccessKey="S" 
            />
           
            <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" Width="800px" 
            DataKeyNames="DocumentId">
          
            <Columns>
           
               
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="DateCreated" HeaderText="Date Uploaded" 
                    SortExpression="DateCreated" DataFormatString="{0:ddd, MMM d, yyyy}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DateModified" HeaderText="Date Modified" 
                    SortExpression="DateModified" Visible="False" DataFormatString="{0:f}" />
                <asp:HyperLinkField DataNavigateUrlFields="DocumentId" 
                    DataNavigateUrlFormatString="~/Member/Default.aspx?id={0}&amp;type=doc" DataTextField="name" 
                    DataTextFormatString="{0}" HeaderText="File Name" 
                    NavigateUrl="~/Member/Default.aspx" >
                <ItemStyle ForeColor="Blue" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="extension" HeaderText="File Type" 
                    SortExpression="extension" Visible="False" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="size" HeaderText="File Size" SortExpression="size" 
                    DataFormatString="{0:#,###0 KB}" />
                <asp:BoundField DataField="Role" HeaderText="Level of Access" 
                    SortExpression="Role" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getDocuments"
            SortParameterName="sort" TypeName="FileManager" 
            DeleteMethod="deleteDocument">
            <DeleteParameters>
                <asp:Parameter Name="DocumentId" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" Name="sort" 
                    PropertyName="SelectedRow" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
       
        </div>

</asp:Content>
