<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPopup.master" AutoEventWireup="true"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
   <%
    if (false)
    {%>
<script src="../../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
<%
    }%>
    <div>  
    <label for="ddlSearchBy">Cari berdasar :</label>  
        <select id="ddlSearchBy">
            <option value="cust.Id">Kode Pasien</option>
            <option value="per.PersonFirstName">Nama</option> 
            <option value="per.PersonMobile">HP</option>
            <option value="adr.AddressLine1">Alamat</option>
            <option value="adr.AddressCity">Kota</option>
        </select>    
    <input id="txtSearch" type="text" />
    <input id="btnSearch" type="button" value="Cari" />
    </div>
    
     <table id="list" class="scroll" cellpadding="0" cellspacing="0">
    </table>
    <div id="listPager" class="scroll" style="text-align: center;">
    </div>
    <div id="listPsetcols" class="scroll" style="text-align: center;">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtSearch").focus();
            $("#dialog").dialog({
                autoOpen: false
            });

            $.jgrid.nav.addtext = "Tambah";
            $.jgrid.nav.edittext = "Edit";
            $.jgrid.nav.deltext = "Hapus";
            $.jgrid.edit.addCaption = "Tambah Pasien Baru";
            $.jgrid.edit.editCaption = "Edit Pasien";
            $.jgrid.del.caption = "Hapus Pasien";
            $.jgrid.del.msg = "Anda yakin menghapus Pasien yang dipilih?";
            $("#list").jqGrid({
                url: '<%= Url.Action("ListSearch", "Customer") %>',
                postData: {
                    searchBy: function () { return $('#ddlSearchBy option:selected').val(); },
                    searchText: function () { return $('#txtSearch').val(); }
                },
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Kode Pasien',
                            'Nama',
                            'Jenis Kelamin',
                            'Alamat',
                            '',
                            'Telp',
                            'Kota',
                            'Keterangan'],
                colModel: [
                    { name: 'Id', index: 'Id', width: 100, align: 'left', key: true, editrules: { required: true, edithidden: false }, hidedlg: true, hidden: false, editable: true },
                   { name: 'PersonName', index: 'PersonName', width: 200, align: 'left', editable: false, edittype: 'text', editrules: { required: false, edithidden: true} },
                    { name: 'PersonGender', index: 'PersonGender', width: 200, sortable: false, align: 'left', editable: true, edittype: 'select', editrules: { required: false} },
                   { name: 'CustomerHealthProblem', index: 'CustomerHealthProblem', width: 200, hidden: false, align: 'left', editable: true, edittype: 'textarea', editoptions: { rows: "3", cols: "20" }, editrules: { required: false, edithidden: true} },
                   { name: 'CustomerProductDisc', index: 'CustomerProductDisc', width: 200, hidden: false, align: 'left', editable: true, edittype: 'text', editrules: { required: false, edithidden: true },
                       editoptions: {
                           dataInit: function (elem) {
                               $(elem).autoNumeric();
                               $(elem).attr("style", "text-align:right;");
                           }
                       }
                   },
                   { name: 'CustomerServiceDisc', index: 'CustomerServiceDisc', width: 200, hidden: false, align: 'left', editable: true, edittype: 'text', editrules: { required: false, edithidden: true },
                       editoptions: {
                           dataInit: function (elem) {
                               $(elem).autoNumeric();
                               $(elem).attr("style", "text-align:right;");
                           }
                       }
                   },
                   { name: 'CustomerMassageStrength', index: 'CustomerMassageStrength', width: 200, hidden: false, align: 'left', editable: true, edittype: 'select', editrules: { required: false, edithidden: true} },
                     { name: 'CustomerDesc', index: 'CustomerDesc', width: 200, hidden: false, sortable: false, align: 'left', editable: true, edittype: 'textarea', editoptions: { rows: "3", cols: "20" }, editrules: { required: false, edithidden: true} }
                   ],

                pager: $('#listPager'),
                rowNum: 20,
                rowList: [20, 30, 50, 100],
                rownumbers: true,
                sortname: 'Id',
                sortorder: "asc",
                viewrecords: true,
                height: 250,
                caption: 'Daftar Pasien',
                autowidth: true,
                loadComplete: function () {

                },
                ondblClickRow: function (rowid, iRow, iCol, e) {
                    var list = $("#list");
                    var rowData = list.getRowData(rowid);
                    window.parent.SetCustomerDetail(rowData["Id"], rowData["PersonName"], 0);
                    return false;
                }
            }).navGrid('#listPager',
                {
                    edit: false, add: false, del: false, search: false, refresh: true
                }
            );

            $('#btnSearch').click(function () {
                $("#list").jqGrid().setGridParam().trigger("reloadGrid");
            });
            $("#txtSearch").keydown(function (event) {
            //if enter pressed
                if (event.keyCode == '13') {
                    $("#list").jqGrid().setGridParam().trigger("reloadGrid");
                }                
            });
        });       
    </script>
    <div id="dialog" title="Status">
        <p>
        </p>
    </div>
</asp:Content>
