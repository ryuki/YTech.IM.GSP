<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MyMaster.master" AutoEventWireup="true"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <table id="list" class="scroll" cellpadding="0" cellspacing="0">
    </table>
    <div id="listPager" class="scroll" style="text-align: center;">
    </div>
    <div id="listPsetcols" class="scroll" style="text-align: center;">
    </div>
    <div id="dialog" title="Status">
        <p>
        </p>
    </div>
    <div id='popup'>
        <iframe width='100%' height='380px' id="popup_frame" frameborder="0"></iframe>
    </div>

   <%-- <input type="button" value="Show MAC Address" onclick="showMacAddress()" />

	<div id="box">
	</div>--%>

    <script type="text/javascript">

//    function showMacAddress(){
// 
//	var obj = new ActiveXObject("WbemScripting.SWbemLocator");
//	var s = obj.ConnectServer(".");
//	var properties = s.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
//	var e = new Enumerator (properties);

// 
//	var output;
//	output='<table border="0" cellPadding="5px" cellSpacing="1px" bgColor="#CCCCCC">';
//	output=output + '<tr bgColor="#EAEAEA"><td>Caption</td><td>MACAddress</td></tr>';
//	while(!e.atEnd())

//	{
//		e.moveNext();
//		var p = e.item ();
//		if(!p) continue;
//		output=output + '<tr bgColor="#FFFFFF">';
//		output=output + '<td>' + p.Caption; + '</td>';
//		output=output + '<td>' + p.MACAddress + '</td>';
//		output=output + '</tr>';
//	}

//	output=output + '</table>';
//	document.getElementById("box").innerHTML=output;
//}



        $(document).ready(function () {

            $("#dialog").dialog({
                autoOpen: false
            });
            $("#popup").dialog({
                autoOpen: false,
                height: 420,
                width: '80%',
                modal: true,
                close: function(event, ui) {                 
                    $("#list").trigger("reloadGrid");
                 }
            });

            var editDialog = {
                url: '<%= Url.Action("Update", "Action") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , modal: true

                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { Id: rowData.Id };

                    return ajaxData;
                }
                , afterShowForm: function (eparams) {
                    $('#Id').attr('disabled', 'disabled');
                }
                , width: "600"
                , afterComplete: function (response, postdata, formid) {
                    $('#dialog p:first').text(response.responseText);
                    $("#dialog").dialog("open");
                }
            };
            var insertDialog = {
                url: '<%= Url.Action("Insert", "Action") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , modal: true
                , afterShowForm: function (eparams) {
                    $('#Id').attr('disabled', '');

                }
                , afterComplete: function (response, postdata, formid) {
                    $('#dialog p:first').text(response.responseText);
                    $("#dialog").dialog("open");
                }
                , width: "600"
            };
            var deleteDialog = {
                url: '<%= Url.Action("Delete", "Action") %>'
                , modal: true
                , width: "400"
                , afterComplete: function (response, postdata, formid) {
                    $('#dialog p:first').text(response.responseText);
                    $("#dialog").dialog("open");
                }
            };

            $.jgrid.nav.addtext = "Tambah";
            $.jgrid.nav.edittext = "Edit";
            $.jgrid.nav.deltext = "Hapus";
            $.jgrid.edit.addCaption = "Tambah Tindakan Baru";
            $.jgrid.edit.editCaption = "Edit Tindakan";
            $.jgrid.del.caption = "Hapus Produk";
            $.jgrid.del.msg = "Anda yakin menghapus Tindakan yang dipilih?";
            $("#list").jqGrid({
                url: '<%= Url.Action("List", "Action") %>',
                datatype: 'json',
                mtype: 'GET',
                colNames: ['','Kode Tindakan', 'Nama', 'Kategori Tindakan', 'Kategori Tindakan','Status', 'Harga Jual', 'Alat & Bahan', 'Jasa Medis', 'Jasa Dokter', 'Jasa Terapis', 'Keterangan'],
                colModel: [
                    { name: 'act', index: 'act', width: 75, sortable: false },
                    { name: 'Id', index: 'Id', width: 100, align: 'left', key: true, editrules: { required: true, edithidden: false }, hidedlg: true, hidden: false, editable: true },
                    { name: 'ActionName', index: 'ActionName', width: 200, align: 'left', editable: true, edittype: 'text', editrules: { required: true }, formoptions: { elmsuffix: ' *'} },
                    { name: 'ActionCatId', index: 'ActionCatId', width: 200, align: 'left', editable: true, edittype: 'select', editrules: { edithidden: true }, hidden: true },
                    { name: 'ActionCatName', index: 'ActionCatName', width: 200, align: 'left', editable: false, edittype: 'select', editrules: { edithidden: true} },
                   { name: 'ActionStatus', index: 'ActionStatus', width: 200, sortable: true, align: 'left', editable: true, edittype: 'checkbox', editoptions: { value: "Aktif:Tidak Aktif" }, editrules: { required: false} },
                     { name: 'ActionPrice', index: 'ActionPrice', width: 200, sortable: false, align: 'right', editable: true, editrules: { required: false },
                         editoptions: {
                             dataInit: function (elem) {
                                 $(elem).autoNumeric();
                                 $(elem).attr("style", "text-align:right;");
                             }
                         }
                     },
                     { name: 'ActionComponentTool', index: 'ActionComponentTool', width: 200, sortable: false, align: 'right', editable: true, editrules: { required: false },
                         editoptions: {
                             dataInit: function (elem) {
                                 $(elem).autoNumeric();
                                 $(elem).attr("style", "text-align:right;");
                             }
                         }
                     },
                     { name: 'ActionComponentMedician', index: 'ActionComponentMedician', width: 200, sortable: false, align: 'right', editable: true, editrules: { required: false },
                         editoptions: {
                             dataInit: function (elem) {
                                 $(elem).autoNumeric();
                                 $(elem).attr("style", "text-align:right;");
                             }
                         }
                     },
                     { name: 'ActionComponentDoctor', index: 'ActionComponentDoctor', width: 200, sortable: false, align: 'right', editable: true, editrules: { required: false },
                         editoptions: {
                             dataInit: function (elem) {
                                 $(elem).autoNumeric();
                                 $(elem).attr("style", "text-align:right;");
                             }
                         }
                     },
                     { name: 'ActionComponentTherapist', index: 'ActionComponentTherapist', width: 200, sortable: false, align: 'right', editable: true, editrules: { required: false },
                         editoptions: {
                             dataInit: function (elem) {
                                 $(elem).autoNumeric();
                                 $(elem).attr("style", "text-align:right;");
                             }
                         }
                     },
                   { name: 'ActionDesc', index: 'ActionDesc', width: 200, sortable: false, align: 'left', editable: true, edittype: 'textarea', editoptions: { rows: "3", cols: "20" }, editrules: { required: false}}],

                pager: $('#listPager'),
                rowNum: 20,
                rowList: [20, 30, 50, 100],
                rownumbers: true,
                sortname: 'Id',
                sortorder: "asc",
                viewrecords: true,
                height: 300,
                caption: 'Daftar Tindakan',
                autowidth: true,
                loadComplete: function () {
                    $('#list').setColProp('ActionCatId', { editoptions: { value: ActionCats} });
                    var ids = jQuery("#list").getDataIDs();
                    for (var i = 0; i < ids.length; i++) {
                        var cl = ids[i];
                        var be = "<input type='button' value='T' tooltips='Tambah Produk Tindakan' onClick=\"OpenPopup('" + cl + "');\" />";
                        //                        alert(be);
                        $(this).setRowData(ids[i], { act: be });
                    }
                },
                subGrid: true,
                subGridUrl: '<%= Url.Action("ListForSubGrid", "ActionItem") %>',
                subGridModel: [{ name: ['Produk', 'Kuantitas', 'Status', 'Deskripsi'],
                    width: [55, 80, 80, 80],
                    //subrig columns aligns
                    align: ['left', 'right', 'left', 'left'],
                    params: ['Id']
                }],
                ondblClickRow: function (rowid, iRow, iCol, e) {
                    $("#list").editGridRow(rowid, editDialog);                 
                }
            }).navGrid('#listPager',
                {
                    edit: true, add: true, del: true, search: false, refresh: true
                },
                editDialog,
                insertDialog,
                deleteDialog
            );
        });
            var ActionCats = $.ajax({ url: '<%= Url.Action("GetList","ActionCat") %>', async: false, cache: false, success: function (data, result) { if (!result) alert('Failure to retrieve the ActionCats.'); } }).responseText;
        
        function OpenPopup(id)
        {
            $("#popup_frame").attr("src", "<%= Url.Action("AddActionItem", "ActionItem") %>?actionId="+id);
            $("#popup").dialog("open");
            return false;   
        }
    </script>
</asp:Content>
