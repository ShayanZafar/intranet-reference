<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ui/css/galleria.classic.css" rel="stylesheet" type="text/css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <!-- galleria -->
    <script src="//ajax.cdnjs.com/ajax/libs/galleria/1.2.7/galleria.min.js"></script>
    <script src="ui/js/galleria.classic.min.js" type="text/javascript"></script>
    <script>
        Galleria.configure({
            autoplay: true
        });
        Galleria.run('#galleria');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="float: left; text-align: center; width: 200px; margin-top: 50px;">
        <h3>
            Welcome to My Peto</h3>
        <h3>
            Login</h3>
        <asp:Login ID="Login1" runat="server" VisibleWhenLoggedIn="False" DestinationPageUrl="~/Member/profile.aspx"
            BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderPadding="4" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333"
            Orientation="Vertical" onloggingin="Login1_LoggingIn">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LayoutTemplate>
                <div><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label></div>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctl00$Login1" Display="Dynamic">*</asp:RequiredFieldValidator>
                <div><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></div>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$Login1" Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" ValidationGroup="ctl00$Login1" />
                <div>
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></div>
            </LayoutTemplate>
            <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
            <TextBoxStyle Font-Size="0.8em" />
            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        </asp:Login>
        <h4>
            Link to our Main Website:</h4>
        <a href="http://www.petomaccallum.com">Peto Maccallum Ltd.</a>
    </div>
    <div style="float: left; margin-left: 50px; width: 500px;">
        <!-- Slider -->
        <div id="galleria">
            <a href="ui/css/images/Peto_MacCallum-17.jpg">
                <img src="ui/css/images/Peto_MacCallum-17_thumb.jpg" data-big="ui/css/images/Peto_MacCallum-17.jpg"
                    data-title="Another title" data-description="My description" alt="no image" /></a>
            <a href="ui/css/images/Peto_MacCallum-50.jpg">
                <img src="ui/css/images/Peto_MacCallum-50_thumb.jpg" data-big="ui/css/images/Peto_MacCallum-50.jpg"
                    data-title="Another title" data-description="My description" alt="no image" /></a>
            <a href="ui/css/images/Peto_MacCallum-8.jpg">
                <img src="ui/css/images/Peto_MacCallum-8_thumb.jpg" data-big="ui/css/images/Peto_MacCallum-8.jpg"
                    data-title="Another title" data-description="My description" alt="no image" /></a>
        </div>
    </div>
</asp:Content>