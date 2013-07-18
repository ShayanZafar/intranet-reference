<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="uploadReport.aspx.cs" Inherits="Author_uploadReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../ui/css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />

  <script type="text/javascript" src="../ui/js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.js"></script>

<script>
    function warn() {

        $('#dialog').html('<%=msg%>');
        $('#dialog').dialog({
            resizable: false,
            draggable: false,
            modal: true,
            width:500

        });
     }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="dialog" title="Warning"></div>
    <div class="center">
        <h3>
            Upload Report:</h3>
        <h4>
            Step 1 - Select Category for File:
        </h4>
        <asp:DropDownList ID="categoryDDL" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
            ControlToValidate="categoryDDL" ValidationGroup="upload"></asp:RequiredFieldValidator>
        <h4>
            Step 2 - Select Permission for File:
        </h4>
        <asp:DropDownList ID="permissionDDL" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
            ControlToValidate="permissionDDL" ValidationGroup="upload"></asp:RequiredFieldValidator>
        <h4>
            Step 4 - Choose a File:
        </h4>
        <asp:FileUpload ID="fu" runat="server"></asp:FileUpload>
        <asp:Button ID="uploadBtn" runat="server" Text="Upload Report" OnClick="uploadBtn_Click"
            ValidationGroup="upload" />
        <asp:Label ID="statusLbl" runat="server" Text=""></asp:Label></div>
</asp:Content>

