<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="searchDocuments.aspx.cs" Inherits="Member_searchDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centerGrid">
        <h2>
            Search Documents:</h2>
        <asp:TextBox ID="searchTB" runat="server" Width="306px"></asp:TextBox>
        <%--    <input type="button" id="searchBtn" value="Search">--%>
        <asp:Button ID="searchBtn" runat="server" Text="Search" 
            OnClick="searchBtn_Click" AccessKey="S" />
        <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None"  DataKeyNames="DocumentId">
            <Columns>
                <asp:BoundField DataField="DateCreated" HeaderText="Date Uploaded" SortExpression="DateCreated"
                    DataFormatString="{0:ddd, MMM d, yyyy}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DateModified" HeaderText="Date Modified" SortExpression="DateModified"
                    Visible="False" DataFormatString="{0:f}" />
                <asp:HyperLinkField DataNavigateUrlFields="DocumentId" DataNavigateUrlFormatString="~/Member/Default.aspx?id={0}&amp;type=doc"
                    DataTextField="name" DataTextFormatString="{0}" HeaderText="File Name" NavigateUrl="~/Member/Default.aspx">
                    <ItemStyle ForeColor="Blue" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="extension" HeaderText="File Type" SortExpression="extension"
                    Visible="False">
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="size" HeaderText="File Size" SortExpression="size" DataFormatString="{0:#,###0 KB}" />
                <asp:BoundField DataField="Role" HeaderText="Level of Access" SortExpression="Role">
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
    </div>
</asp:Content>