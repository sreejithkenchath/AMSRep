$(function () {

    var CompanyID = 0;
    var UserID = 0;
    BindCompany();

    $("#Date").jqxDateTimeInput({ width: '250px', height: '25px' });
    //    $("#cmbCompany").jqxComboBox('selectIndex', 0)

    //-----------------------------------------------------------------------------------------------------------

    $('#search').click(function () {
        alert('here');

        CompanyID = $("#cmbCompany").jqxComboBox('getSelectedItem');
        if (CompanyID == 0) {
            alert("Select Company First");
        }

        UserID = $("#cmbUser").jqxComboBox('getSelectedItem');
        if (UserID == 0) {
            alert('Select User')
        }
        var ApDate = $('#Date').jqxDateTimeInput('getText');

        BindAppointmentGrid(UserID.value, ApDate);
    });
    //--------------------------------------------------------------------------------------------------------------
});

function BindCompany() {
   
    var url = "/Appointment/GetCompany";
    // prepare the data
    var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'CompanyName' },
                        { name: 'CompanyID' }
                    ],
                    url: url,
                    async: false
                };
    var dataAdapter1 = new $.jqx.dataAdapter(source);
    // Create a jqxComboBox
    $("#cmbCompany").jqxComboBox({ selectedIndex: 0, source: dataAdapter1, displayMember: "CompanyName", valueMember: "CompanyID", width: 200, height: 25 });
    $("#cmbCompany").on('select', function (event) {
       
        if (event.args) {
            var item = event.args.item;
            if (item) {
               
                CompanyID = item.value;

                BindUser();
            }
        }
    });
}
//-------------------------------------------------------------------------------------------------------


function BindUser() {
    var url = "/Appointment/GetUsers?CompanyID=" + CompanyID;
    // prepare the data
    var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'name' },
                        { name: 'MembershipUserID' }
                    ],
                    url: url,
                    async: false
                };
    var dataAdapter1 = new $.jqx.dataAdapter(source);
    // Create a jqxComboBox
    $("#cmbUser").jqxComboBox({ selectedIndex: 0, source: dataAdapter1, displayMember: "name", valueMember: "MembershipUserID", width: 200, height: 25 });
    $("#cmbUser").on('select', function (event) {

        if (event.args) {

            var item = event.args.item;

            if (item) {
                UserID = item.value;
            }
        }
    });
}
//--------------------------------------------------------------------------------------------------------
function BindAppointmentGrid(UID, DT) {
    var source =
            {
                datatype: "json",
                datafields: [
                    { name: 'from', type: 'string' },
                    { name: 'to', type: 'string' },
                    { name: 'Status', type: 'string' }

                ],
                id: 'from',
                url: "/Appointment/GetAppointmentData?UserId=" + UID + "&Date=" + DT
            };
    var dataAdapter = new $.jqx.dataAdapter(source);
    var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {

        if (value == 'Available') {
            return '<span style="margin: 4px; float: ' + columnproperties.cellsalign + '; color: #008000;">' + value + '</span>';
        }
        else {

            return '<span style="margin: 4px; float: ' + columnproperties.cellsalign + '; color: red;">' + value + '</span>';
        }
    }
    $("#jqxgrid").jqxGrid(
            {
                width: 850,
                source: dataAdapter,
                columnsresize: true,
                columns: [
                  { text: 'Start Time', datafield: 'from', width: 300 },
                  { text: 'End Time', datafield: 'to', width: 300 },
                  { text: 'Availability', datafield: 'Status', width: 250, cellsrenderer: cellsrenderer }
              ]
            });
            $("#jqxgrid").on("rowdoubleclick", function () {
               
                UserID = $("#cmbUser").jqxComboBox('getSelectedItem');
                var args = event.args;
                var rowindex = $("#jqxgrid").jqxGrid('getselectedrowindex');
                var rowid = $("#jqxgrid").jqxGrid('getrowid', rowindex);
                var data = $('#jqxgrid').jqxGrid('getrowdatabyid', rowid);
                var ApDate = $('#Date').jqxDateTimeInput('getText');
                $.post("/Appointment/MakeAppointment", { UserID: UserID.value, TimeFrom: data.from, Timeto: data.to, Date: ApDate}).done(function (data) {
                    //PostCargoGridData(data);
                    //PostPackageGridData(data);
                    alert(data);
                });
            });
}


