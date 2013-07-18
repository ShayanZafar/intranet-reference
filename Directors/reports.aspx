<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="Directors_reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="centerGrid">
    <h4>Viewing Director Reports</h4>
      
        <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
 
        <asp:Button ID="searchBtn" runat="server" Text="Search" 
            />
     
            <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" GridLines="None" Width="600px" 
            DataKeyNames="ReportId">
          
            <Columns>
           
               
                <asp:HyperLinkField DataNavigateUrlFields="ReportId" 
                    DataNavigateUrlFormatString="~/Member/Default.aspx?id={0}" DataTextField="Name" 
                    DataTextFormatString="{0}" HeaderText="File Name" 
                    NavigateUrl="~/Member/Default.aspx" >
                <ItemStyle ForeColor="Blue" />
                </asp:HyperLinkField>
                 <asp:BoundField DataField="ReportDate" HeaderText="Report Date" 
                    SortExpression="ReportDate" DataFormatString="{0:d}" >
                <ItemStyle HorizontalAlign="Center" />
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
        
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getReportsForRole"
            SortParameterName="sort" TypeName="ReportManager" 
            DeleteMethod="deleteReport">
            <DeleteParameters>
                <asp:Parameter Name="ReportId" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="Director" Name="role" Type="String" />
                <asp:Parameter Name="sort" Type="String" DefaultValue="" />
            </SelectParameters>
        </asp:ObjectDataSource>
 

</div>
</asp:Content>

