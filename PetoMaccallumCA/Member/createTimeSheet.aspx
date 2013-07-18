<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/site.master" AutoEventWireup="true"
    CodeFile="createTimeSheet.aspx.cs" Inherits="Member_createTimeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../ui/css/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>--%>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.js"></script>
    <script>
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
      
             source: data
         });

         $('#<%=searchBtn.ClientID%>').click(function(e) {
             // Hide No records to display label.
             $('#<%=recStatLBL.ClientID%>').css('display', 'none');
             //Hide all the rows.
             $("#<%=summaryGV.ClientID%> tr:has(td)").hide();

             var iCounter = 0;
             //Get the search box value
             var sSearchTerm = $('#<%=searchTB.ClientID%>').val();

             //if nothing is entered then show all the rows.
             if (sSearchTerm.length == 0) {
                 $("#<%=summaryGV.ClientID%> tr:has(td)").show();
                 return false;
             }
             //Iterate through all the td.
             $("#<%=summaryGV.ClientID%> tr:has(td)").children().each(function() {
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

        // add calender to weekending textbox
        $('#<%=weekEndingTB.ClientID%>').datepicker({
            beforeShowDay: function(date){ return [date.getDay() == 6,""]},
            showOn: "both",
            buttonImage: "../ui/css/images/calander.jpg",
			buttonImageOnly: true,
            onSelect: function ()
            {
               // this refers to the weekending html input control
                this.focus();
            }
        });
        // add predictive text
         var projectNumbers = new Array;
         <% foreach(string str in projectNoDB){%>
            projectNumbers.push('<%= str %>');
            <% } %>

         $('#<%=projectNumTB.ClientID%>').autocomplete({
            autoFocus: true,
             source: projectNumbers,
             delay: 25
         });

         var icons = {
			header: "ui-icon-circle-arrow-e",
			headerSelected: "ui-icon-circle-arrow-s"
		};

                // ACCORDIANS
         $('#accordion1').accordion({
            collapsible:true,
            icons: icons
         });
           $('#accordion2').accordion({
            collapsible:true,
            icons: icons
         });
            $('#accordion3').accordion({
            collapsible:true,
            icons: icons

         });
         $('#toggle').accordion({
            collapsible: true,
            autoHeight: false,
            icons: icons
         });
    });
     // populate the date labels with the correct day of the month based on weekending date
     function populate(){

        var year = Number($('#<%=weekEndingTB.ClientID%>').val().substring(6));
        var month = Number($('#<%=weekEndingTB.ClientID%>').val().substring(0,2));
        var day = Number($('#<%=weekEndingTB.ClientID%>').val().substring(3,5));
        var date = new Date($('#<%=weekEndingTB.ClientID%>').val());

        if (day != "" || day > 0) {
                 $('#<%=satLBL.ClientID%>').html(day.toString());
                 $('#<%=satLBL2.ClientID%>').html(day.toString());
                 $('#<%=satLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=friLBL.ClientID%>').html(day.toString());
                 $('#<%=friLBL2.ClientID%>').html(day.toString());
                 $('#<%=friLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=thursLBL.ClientID%>').html(day.toString());
                 $('#<%=thursLBL2.ClientID%>').html(day.toString());
                 $('#<%=thursLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=wedsLBL.ClientID%>').html(day.toString());
                 $('#<%=wedsLBL2.ClientID%>').html(day.toString());
                 $('#<%=wedsLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=tuesLBL.ClientID%>').html(day.toString());
                 $('#<%=tuesLBL2.ClientID%>').html(day.toString());
                  $('#<%=tuesLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=monLBL.ClientID%>').html(day.toString());
                 $('#<%=monLBL2.ClientID%>').html(day.toString());
                  $('#<%=monLBL3.ClientID%>').html(day.toString());
                 day = checkDay(day,year,month);
                 $('#<%=sunLBL.ClientID%>').html(day.toString());
                 $('#<%=sunLBL2.ClientID%>').html(day.toString());
                  $('#<%=sunLBL3.ClientID%>').html(day.toString());
        }
    }
    // check to see if day is invalid, if it is reset the day to the last day of the previous month, else decrement the day normally
    function checkDay(day,year,month){
     if (day - 1 == 0){
        return day = getLastDateOfMonth(year,month).getDate();
     }else{
        return day - 1;
     }
    }
    // return the last Date of a given month and year
    function getLastDateOfMonth(year, month){
        return (new Date((new Date(year,month-2,1))-1));
     }
   // confirmation with summary of retrieved project information
   function confirmProject(){
        var summary = "Project Number: " +  $('#<%=projectNumTB.ClientID%>').val()
         + "\n Project Name: " + $('#<%=projectNameTB.ClientID%>').val()
          + "\n Client Name: " + $('#<%=clientNameTB.ClientID%>').val();

        $('#dialog').html(summary);
        $('#dialog').dialog({
            resizable:false,
            draggable:false,
            modal:true,
            focus:true,
            buttons: [
                {
                    text: "Ok",
                    click: function() { $(this).dialog("close"); }}]
        });
   }
   // function throws a dialog box with the string contained in msg variable as its message
   function throwDialog(){
     $('#dialog').html('<%=msg%>');
        $('#dialog').dialog({
            resizable:false,
            draggable:false,
            modal:true,
            buttons: [
                {
                    text: "Ok",
                    click: function() { $(this).dialog("close");
                    }}]
        });
   }

   // Activate a specific accordian element
   function accordionToggle(){
        $('#toggle').accordion({active: <%=aID%>});
   }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:ScriptManager runat="server" ID="manager" />  --%>

    <!-- for JQuery Dialog - Intentionally Empty -->
    <div id="dialog" title="<%=dialogTitle%>">
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            HeaderText="Summary of Errors Encountered:" />
    </div>
    <div style="width: 890px; margin-bottom: 10px; float: left;">
        <h3>
            Employee Timesheet</h3>
        <!-- Inner most divs are for columns -->
        <!-- First Column -->
        <div style="float: left;">
            <h5>
                Emp ID</h5>
            <asp:TextBox ID="empIdTB" runat="server" Enabled="False" Width="50px"></asp:TextBox>
        </div>
        <!-- Second Column -->
        <div style="float: left;">
            <h5>
                Employee Name</h5>
            <asp:TextBox ID="nameTB" runat="server" Enabled="False" Width="300px"></asp:TextBox>
        </div>
        <!-- Third Column -->
        <div style="float: left;">
            <h5>
                Department:</h5>
            <asp:TextBox ID="departmentTB" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <!-- Fourth Column -->
        <div style="float: left;">
            <h5>
                Week Ending</h5>
            <asp:TextBox ID="weekEndingTB" runat="server" AutoCompleteType="Disabled" MaxLength="10"
                Width="95px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                ControlToValidate="weekEndingTB" ValidationGroup="timesheet"></asp:RequiredFieldValidator>
        </div>
        <div style="float: left;">
            <h5>
                Existing Timesheet?</h5>
            <asp:Button ID="verifyBtn" runat="server" Text="Verify" 
                OnClick="verifyBtn_Click" style="height: 26px" AccessKey="V" />
        </div>
    </div>
    <div id="toggle" style="float: left;margin-top: 10px;width:890px;">
        <h3>
            <a href="#" accesskey="J">Chargeable <u>J</u>ob</a></h3>
        <div style="width: 880px;">
            <div>
                <div style="float: left;">
                    <h5>
                        Project #</h5>
                    <asp:TextBox ID="projectNumTB" runat="server" AutoCompleteType="Disabled" AutoPostBack="True"
                        Width="65px" OnTextChanged="projectNumTB_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ControlToValidate="projectNumTB" ValidationGroup="chargeable" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div style="float: left;">
                    <h5>
                        Project Name</h5>
                    <asp:TextBox ID="projectNameTB" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                </div>
                <div style="float: left;">
                    <h5>
                        Client</h5>
                    <asp:TextBox ID="clientNameTB" runat="server" Width="339px" Enabled="False"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 500px;">
                <div style="width: 880px;">
         
                            <div style="float: left; width: 221px;">
                                <h5>
                                    Classification</h5>
                                <asp:DropDownList ID="classificationDDL" runat="server" OnSelectedIndexChanged="classificationDDL_SelectedIndexChanged"
                                    AutoPostBack="True" Width="200px" Height="25px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                    ControlToValidate="classificationDDL" ValidationGroup="chargeable" ></asp:RequiredFieldValidator>
                            </div>
                            <div style="float: left; width: 279px;">
                                <h5>
                                    Activity</h5>
                                <asp:DropDownList Font-Size="0.8em" ID="activitiesDDL" runat="server" Width="250px"
                                    Height="25px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                    ControlToValidate="activitiesDDL" ValidationGroup="chargeable" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>

                </div>
                <table>
                    <tr>
                        <td>
                            Day
                        </td>
                        <th>
                            Sun
                        </th>
                        <th>
                            Mon
                        </th>
                        <th>
                            Tue
                        </th>
                        <th>
                            Wed
                        </th>
                        <th>
                            Thu
                        </th>
                        <th>
                            Fri
                        </th>
                        <th>
                            Sat
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Date:
                        </td>
                        <td>
                            <asp:Label ID="sunLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="monLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tuesLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="wedsLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="thursLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="friLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="satLBL" runat="server" ViewStateMode="Enabled"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Hours
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="sunCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="sunCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="monCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="monCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="monCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="tuesCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="tuesCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="wedsCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="wedsCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ControlToValidate="thursCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="thursCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="friCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ControlToValidate="friCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="friCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="satCHoursTB" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ControlToValidate="satCHoursTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator7" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="satCHoursTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Personal
                            KM
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="*" ControlToValidate="sunDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="*" ControlToValidate="monDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="*" ControlToValidate="tuesDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="*" ControlToValidate="wedsDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="*" ControlToValidate="thursDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="*" ControlToValidate="friDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="*" ControlToValidate="satDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Truck
                            KM
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator71" runat="server" ErrorMessage="*" ControlToValidate="sunTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator72" runat="server" ErrorMessage="*" ControlToValidate="monTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator73" runat="server" ErrorMessage="*" ControlToValidate="tuesTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator74" runat="server" ErrorMessage="*" ControlToValidate="wedsTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator75" runat="server" ErrorMessage="*" ControlToValidate="thursTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator76" runat="server" ErrorMessage="*" ControlToValidate="friTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satTruckDistTB" Text="" runat="server" Rows="1"
                                Columns="2" MaxLength="4"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator77" runat="server" ErrorMessage="*" ControlToValidate="satTruckDistTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MISC
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="*" ControlToValidate="sunMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="monMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="*" ControlToValidate="monMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator24" runat="server" ErrorMessage="*" ControlToValidate="tuesMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator25" runat="server" ErrorMessage="*" ControlToValidate="wedsMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator26" runat="server" ErrorMessage="*" ControlToValidate="thursMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="friMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator27" runat="server" ErrorMessage="*" ControlToValidate="friMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="satMiscTB" runat="server" Rows="1"
                                Columns="5" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator28" runat="server" ErrorMessage="*" ControlToValidate="satMiscTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Meals/Acomm
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="*" ControlToValidate="sunAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="monAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="*" ControlToValidate="monAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="*" ControlToValidate="tuesAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator18" runat="server" ErrorMessage="*" ControlToValidate="wedsAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator19" runat="server" ErrorMessage="*" ControlToValidate="thursAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="friAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="*" ControlToValidate="friAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" Text="" ID="satAccomTB" runat="server" Rows="1"
                                Columns="2" MaxLength="6"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="*" ControlToValidate="satAccomTB"
                                Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunCRemarksTB" runat="server" Rows="1" Columns="10"
                                MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monCRemarksTB" runat="server" Rows="1" Columns="10"
                                MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friCRemarksTB" runat="server" Rows="1" Columns="10"
                                MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satCRemarksTB" runat="server" Rows="1" Columns="10"
                                MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="chargeableBtn" runat="server" Text="Save Chargeable" OnClick="chargeableBtn_Click"
                                ValidationGroup="chargeable" style="height: 26px" AccessKey="C" />
                            <asp:Button ID="updateChargeableBtn" runat="server" Text="Update Chargeable" Visible="false"
                                ValidationGroup="chargeable" OnClick="updateChargeableBtn_Click" AccessKey="C" />
                            <asp:Button ID="clearChargeableBtn" runat="server" Text="Clear Form" CausesValidation="False"
                                OnClick="clearChargeableBtn_Click" AccessKey="B" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

         <h3><a href="#" accesskey="H">&nbsp;Lab <u>H</u>ours &amp; Nuclear Density Tests</a></h3>
        <div style="width:880px;">
            <div  style="width:880px;float:left;">
                <!-- LAB TESTS -->
                <table>
                      <tr>
                      <td>Day</td>
                        <th>
                            Sun
                        </th>
                        <th>
                            Mon
                        </th>
                        <th>
                            Tue
                        </th>
                        <th>
                            Wed
                        </th>
                        <th>
                            Thu
                        </th>
                        <th>
                            Fri
                        </th>
                        <th>
                            Sat
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Date:
                        </td>
                        <td>
                            <asp:Label ID="sunLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="monLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tuesLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="wedsLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="thursLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="friLBL3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="satLBL3" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Lab Hours</td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator64" runat="server" ErrorMessage="*" ControlToValidate="sunLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                  <asp:RangeValidator ID="RangeValidator15" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="sunLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator65" runat="server" ErrorMessage="*" ControlToValidate="monNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator16" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="monLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator66" runat="server" ErrorMessage="*" ControlToValidate="tuesLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator17" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="tuesLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator67" runat="server" ErrorMessage="*" ControlToValidate="wedsLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator18" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="wedsLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator68" runat="server" ErrorMessage="*" ControlToValidate="thursLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator19" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="thursLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator69" runat="server" ErrorMessage="*" ControlToValidate="friLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator20" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="friLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satLabTB" Text="" runat="server" Rows="1"
                                Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator70" runat="server" ErrorMessage="*" ControlToValidate="satLabTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                     <asp:RangeValidator ID="RangeValidator21" runat="server" ErrorMessage="Lab Hours Exceed 24"
                                MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="satLabTB"
                                Display="Dynamic"></asp:RangeValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Nuclear Density Test
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator57" runat="server" ErrorMessage="*" ControlToValidate="sunNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator58" runat="server" ErrorMessage="*" ControlToValidate="monNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator59" runat="server" ErrorMessage="*" ControlToValidate="tuesNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator60" runat="server" ErrorMessage="*" ControlToValidate="wedsNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator61" runat="server" ErrorMessage="*" ControlToValidate="thursNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator62" runat="server" ErrorMessage="*" ControlToValidate="friNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satNuclearDensityTestTB" Text="" runat="server"
                                Rows="1" Columns="5" MaxLength="5"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator63" runat="server" ErrorMessage="*" ControlToValidate="sunNCAccomTB"
                                Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    
                </table>
                <asp:Button ID="labTestsBtn" runat="server" Text="Save Tests"
                    onclick="labTestsBtn_Click" AccessKey="L" />
                    <asp:Button ID="updateLabTestsBtn" Visible="false" runat="server"
                    Text="Update Lab Tests" onclick="updateLabTestsBtn_Click" AccessKey="L"/>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Clear Form" AccessKey="B"/>
            </div>
        </div>

        <!-- Non Chargeable -->
        <h3>
            <a href="#" accesskey="O">N<u>o</u>n-Chargeable</a></h3>
        <div style="float: left; width: 880px;">
            <div>
                <%--<asp:UpdatePanel ID="nonchargePanel" runat="server">
                    <ContentTemplate>--%>
                        <div style="float: left;">
                            <h5>
                                Type of Hours</h5>
                            <asp:DropDownList Font-Size="0.8em" Width="150px" ID="specifyDDL" runat="server"
                                OnSelectedIndexChanged="specifyDDL_SelectedIndexChanged" AutoPostBack="True"
                                Height="25px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                ControlToValidate="specifyDDL" ValidationGroup="timesheet"></asp:RequiredFieldValidator>
                        </div>
                        <div style="float: left; width: 300px;">
                            <h5>
                                Type of Expense</h5>
                            <asp:DropDownList Font-Size="0.8em" Width="300px" ID="detailsDDL" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="detailsDDL_SelectedIndexChanged"
                                Height="25px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                ControlToValidate="detailsDDL" ValidationGroup="timesheet"></asp:RequiredFieldValidator>
                        </div>
                        </div>
            <%--                        </ContentTemplate>
                </asp:UpdatePanel>--%>
            <div style="float: left;">
                <table>
                    <tr>
                       <td>Day</td>
                        <th>
                            Sun
                        </th>
                        <th>
                            Mon
                        </th>
                        <th>
                            Tue
                        </th>
                        <th>
                            Wed
                        </th>
                        <th>
                            Thu
                        </th>
                        <th>
                            Fri
                        </th>
                        <th>
                            Sat
                        </th>
                    </tr>
                    <tr>
                        <td>
                            Date:
                        </td>
                        <td>
                            <asp:Label ID="sunLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="monLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tuesLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="wedsLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="thursLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="friLBL2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="satLBL2" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <asp:Panel ID="NonChargeHoursPanel" runat="server" Visible="false">
                        <tr>
                            <td>
                                Hours
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator29" runat="server" ErrorMessage="*" ControlToValidate="sunNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator8" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="sunNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="monNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator30" runat="server" ErrorMessage="*" ControlToValidate="monNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator9" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="monNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator31" runat="server" ErrorMessage="*" ControlToValidate="tuesNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator10" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="tuesNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator32" runat="server" ErrorMessage="*" ControlToValidate="wedsNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator11" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="wedsNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursNCHoursTB" runat="server"
                                    Rows="1" Columns="5" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator33" runat="server" ErrorMessage="*" ControlToValidate="thursNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator12" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="thursNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="friNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator34" runat="server" ErrorMessage="*" ControlToValidate="friNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator13" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="friNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="satNCHoursTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="5"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator35" runat="server" ErrorMessage="*" ControlToValidate="satNCHoursTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator14" runat="server" ErrorMessage="Daily Hours Exceed 24"
                                    MinimumValue="0" MaximumValue="24" Type="Currency" ControlToValidate="satNCHoursTB"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="NonChargeExpensesPanel" runat="server" Visible="false">
                        <tr>
                            <td>
                                KM
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator36" runat="server" ErrorMessage="*" ControlToValidate="sunNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="monNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator37" runat="server" ErrorMessage="*" ControlToValidate="monNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator38" runat="server" ErrorMessage="*" ControlToValidate="tuesNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator39" runat="server" ErrorMessage="*" ControlToValidate="wedsNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator40" runat="server" ErrorMessage="*" ControlToValidate="thursNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="friNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator41" runat="server" ErrorMessage="*" ControlToValidate="friNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="satNCDistanceTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="4"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator42" runat="server" ErrorMessage="*" ControlToValidate="satNCDistanceTB"
                                    Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MISC
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator50" runat="server" ErrorMessage="*" ControlToValidate="sunNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="monNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator51" runat="server" ErrorMessage="*" ControlToValidate="monNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator52" runat="server" ErrorMessage="*" ControlToValidate="tuesNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator53" runat="server" ErrorMessage="*" ControlToValidate="wedsNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator54" runat="server" ErrorMessage="*" ControlToValidate="thursNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="friNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator55" runat="server" ErrorMessage="*" ControlToValidate="friNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="satNCMiscTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator56" runat="server" ErrorMessage="*" ControlToValidate="satNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Meals/Acomm
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="sunNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator43" runat="server" ErrorMessage="*" ControlToValidate="sunNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="monNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator44" runat="server" ErrorMessage="*" ControlToValidate="monNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="tuesNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator45" runat="server" ErrorMessage="*" ControlToValidate="tuesNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="wedsNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator46" runat="server" ErrorMessage="*" ControlToValidate="wedsNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="thursNCAccomTB" runat="server"
                                    Rows="1" Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator47" runat="server" ErrorMessage="*" ControlToValidate="thursNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="friNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator48" runat="server" ErrorMessage="*" ControlToValidate="friNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox CssClass="hoursTextBox" Text="" ID="satNCAccomTB" runat="server" Rows="1"
                                    Columns="2" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator49" runat="server" ErrorMessage="*" ControlToValidate="satNCAccomTB"
                                    Type="Currency" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <!-- Panel Ends here -->
                    </asp:Panel>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="sunNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="monNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="tuesNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="wedsNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="thursNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="friNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="hoursTextBox" ID="satNCRemarksTB" runat="server" Rows="1"
                                Columns="10" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td>
                            <asp:Button ID="nonChargeBtn" runat="server" Text="Save Non-Chargeable" OnClick="nonChargeBtn_Click"
                                ValidationGroup="timesheet" AccessKey="N"/>
                            <asp:Button ID="updateNonChargeableBtn" Visible="false" runat="server" Text="Update Non-Chargeable"
                                ValidationGroup="timesheet" OnClick="updateNonChargeableBtn_Click" AccessKey="N"/>
                            <asp:Button ID="clearNonChargeBtn" runat="server" Text="Clear Form" CausesValidation="False"
                                OnClick="clearNonChargeBtn_Click" AccessKey="B" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%--   <asp:UpdatePanel runat="server" ID="totalsPanel">
                <ContentTemplate>--%>
    <!-- Gridview for Non-Chargeable Here -->
    <div style="float: left; width: 890px;">
        <div id="accordion1" >
            <h3>
                <a href="#" accesskey="T" ><u>T</u>imesheet Totals</a></h3>
          
                    <div>
                        <!-- TOTALS -->
                        <div style="float: left; margin-left: 20px; font-size: 0.9em;">
                            Total Hours:
                            <asp:Label ID="totalHoursLBL" runat="server" Text="0" ViewStateMode="Enabled"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 50px; font-size: 0.9em;">
                            Total Personal KM:
                            <asp:Label ID="totalDistanceLBL" runat="server" Text="0" 
                                ViewStateMode="Enabled"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 50px; font-size: 0.9em;">
                            Total Truck KM:
                            <asp:Label ID="totalTruckLBL" runat="server" Text="0" ViewStateMode="Enabled"></asp:Label>
                        </div>
                        <div style="float: left; margin-left: 50px; font-size: 0.9em;">
                            Total Expenses: $
                            <asp:Label ID="totalExpensesLBL" runat="server" Text="0" 
                                ViewStateMode="Enabled"></asp:Label>
                        </div>
  
                <div style="width: 700px;margin-top:25px;">
                    <table style="margin:15px;border-spacing:10px;border-collapse:separate;">
                        <tr>
                            <td>
                              Day
                            </td>
                            <th>
                                Sun
                            </th>
                            <th>
                                Mon
                            </th>
                            <th>
                                Tues
                            </th>
                            <th>
                                Weds
                            </th>
                            <th>
                                Thurs
                            </th>
                            <th>
                                Fri
                            </th>
                            <th>
                                Sat
                            </th>
                            <th>
                                Total
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Chargeable Hours
                            </td>
                            <td>
                                <asp:Label ID="sunChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="monChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="tuesChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="wedsChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="thursChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="friChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="satChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="totalChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Non-Chargeable Hours
                            </td>
                            <td>
                                <asp:Label ID="sunNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="monNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="tuesNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="wedsNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="thursNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="friNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="satNonChLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="totalNonChargeLBL" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>
                               Lab Hours
                            </td>
                            <td>
                                <asp:Label ID="sunLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="monLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="tuesLabLBL" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="wedsLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="thursLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="friLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="satLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="totalLabLBL" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Daily Totals
                            </td>
                            <td>
                                <asp:Label ID="sunTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="monTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="tuesTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="wedsTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="thursTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="friTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="satTotalHoursLBL" runat="server" Text="0"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="accordion2">
            <h3>
                <a href="#" accesskey="M">Ti<u>m</u>eSheet Summary</a></h3>
            <div>
                <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
                <asp:Button ID="searchBtn" runat="server" Text="Search" AccessKey="S" />
                <asp:Label ID="recStatLBL" runat="server" Text=""></asp:Label>
                  
           
                        <asp:GridView ID="summaryGV" runat="server" CellPadding="4" Caption="Timesheet Summary" 
                    ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" AllowPaging="True"
                            AllowSorting="True" PageSize="50" Font-Size="0.9em" 
                    onpageindexchanging="summaryGV_PageIndexChanging" 
                    onsorting="summaryGV_Sorting" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId,ProjectId,Classification,Activity,NonChargeableId,ChargeableId,TypeHours,TypeExpense,TestId"
                                    DataNavigateUrlFormatString="~/Member/createTimeSheet.aspx?time={0}&amp;project={1}&amp;class={2}&amp;activity={3}&amp;nonCh={4}&amp;charge={5}&amp;hours={6}&amp;expense={7}&amp;test={8}"
                                    NavigateUrl="~/Member/createTimeSheet.aspx" Text="Edit" />
                                <asp:HyperLinkField DataNavigateUrlFields="TimeSheetId,NonChargeableId,ChargeableId,TestId"
                                    DataNavigateUrlFormatString="~/Member/createTimeSheet.aspx?time={0}&amp;nonCh={1}&amp;charge={2}&amp;test={3}&amp;delete=delete"
                                    NavigateUrl="~/Member/createTimeSheet.aspx" Text="Delete" />
                                <asp:BoundField DataField="Day" HeaderText="Day" SortExpression="Day" />
                                <asp:TemplateField HeaderText="Project #" SortExpression="ProjectNum">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/Member/createTimeSheet.aspx?time={0}&project={1}&class={2}&activity={3}&projectOnly=ProjectOnly", Eval("TimeSheetId"), Eval("ProjectId"), Eval("Classification"), Eval("Activity"))%>'
                                            Text='<%#getProjectNum(Eval("ProjectId")) %>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" 
                                    Visible="False" />
                                <asp:BoundField DataField="TimeSheetId" HeaderText="TimeSheetId" SortExpression="TimeSheetId"
                                    Visible="False" />
                                <asp:BoundField DataField="NonChargeableId" HeaderText="NonChargeableId" SortExpression="NonChargeableId"
                                    Visible="False" />
                                <asp:BoundField DataField="ChargeableId" HeaderText="ChargeableId" SortExpression="ChargeableId"
                                    Visible="False" />
                                <asp:BoundField DataField="Hours" HeaderText="Hrs" SortExpression="Hours" DataFormatString="{0:##.#}" />
                                <asp:BoundField DataField="LabTest" HeaderText="Lab Hr" 
                                    SortExpression="LabTest" DataFormatString="{0:###} "/>
                                <asp:BoundField DataField="Distance" HeaderText="KM" SortExpression="Distance"
                                    DataFormatString="{0:###}" />
                                <asp:BoundField DataField="TruckDistance" HeaderText="Truck KM" SortExpression="TruckDistance"
                                    DataFormatString="{0:###}" />
                                <asp:BoundField DataField="Misc" HeaderText="Misc" SortExpression="Misc" DataFormatString="$ {0:####.##}" />
                                <asp:BoundField DataField="Accom" HeaderText="Accom" SortExpression="Accom" DataFormatString="$ {0:####.##}" />
                                <asp:BoundField DataField="TypeExpense" HeaderText="Expense" SortExpression="TypeExpense" />
                                <asp:BoundField DataField="TypeHours" HeaderText="Hr Type" SortExpression="TypeHours" />
                                <asp:BoundField DataField="Classification" HeaderText="Class" SortExpression="Classification" />
                                <asp:BoundField DataField="Activity" HeaderText="Activity" SortExpression="Activity" />
                                <asp:BoundField DataField="DensityTest" HeaderText="Tests" SortExpression="DensityTest" DataFormatString="{0:###} "/>
                                <asp:BoundField DataField="ProjectId" HeaderText="ProjectId" SortExpression="ProjectId"
                                    Visible="False" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="CRemarks" />
                                <asp:TemplateField ShowHeader="False" Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                             Text="Delete Button" CommandName="selectROW" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
            
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="getSummary"
                    TypeName="TimesheetManager" SortParameterName="sort">
                  
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenField1" Name="id" PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="sort" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
              
        <div style="margin-top: 20px;margin-bottom:25px;">
            <div>
                <h4>
                    Notes & Comments:</h4>
                <asp:TextBox ID="empCommentsTB" runat="server" TextMode="MultiLine" CssClass="resize"
                    Height="75px"></asp:TextBox></div>
            <div style="width: 890px;">
                <asp:Button ID="completeBtn" runat="server" Text="Send for Approval"
                    OnClick="completeBtn_Click" AccessKey="A" />
                   
                <asp:Label ID="timesheetStatusLBL" runat="server" Text=""></asp:Label></div>
        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>

</asp:Content>