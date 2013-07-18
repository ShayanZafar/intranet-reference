<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true" CodeFile="uploadDocument.aspx.cs" Inherits="Author_uploadDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="center">
        <h3>
            Upload Document:</h3>
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
            Step 4 -Choose a File:
        </h4>
        <asp:FileUpload ID="fu" runat="server"></asp:FileUpload>
        <asp:Button ID="uploadBtn" runat="server" Text="Upload Document" OnClick="uploadBtn_Click"
            ValidationGroup="upload" />
        <asp:Label ID="statusLbl" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

