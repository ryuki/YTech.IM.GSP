<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPopup.master" AutoEventWireup="true"
    Inherits="System.Web.Mvc.ViewPage" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        //SETTING UP OUR POPUP
        //0 means disabled; 1 means enabled;
        var popupStatus = 0;

        //loading popup with jQuery magic!
        function loadPopup() {
            //loads popup only if it is disabled
            if (popupStatus == 0) {
                $("#backgroundPopup").css({
                    "opacity": "0.7"
                });
                $("#backgroundPopup").fadeIn("slow");
                $("#popupContact").fadeIn("slow");
                popupStatus = 1;
            }
        }

        //disabling popup with jQuery magic!
        function disablePopup() {
            //disables popup only if it is enabled
            if (popupStatus == 1) {
                $("#backgroundPopup").fadeOut("slow");
                $("#popupContact").fadeOut("slow");
                popupStatus = 0;
            }
        }

        //centering popup
        function centerPopup() {
            //request data for centering
            var windowWidth = document.documentElement.clientWidth;
            var windowHeight = document.documentElement.clientHeight;
            var popupHeight = $("#popupContact").height();
            var popupWidth = $("#popupContact").width();
            //centering
            $("#popupContact").css({
                "position": "absolute",
                "top": windowHeight / 2 - popupHeight / 2,
                "left": windowWidth / 2 - popupWidth / 2
            });
            //only need force for IE6

            $("#backgroundPopup").css({
                "height": windowHeight
            });

        }

        $(document).ready(function () {
            //following code will be here
            //LOADING POPUP
            //Click the button event!
            $("#button").click(function () {
                //centering with css
                centerPopup();
                //load popup
                loadPopup();
            });
            //CLOSING POPUP
            //Click the x event!
            $("#popupContactClose").click(function () {
                disablePopup();
            });
            //Click out event!
            $("#backgroundPopup").click(function () {
                disablePopup();
            });
            //Press Escape event!
            $(document).keypress(function (e) {
                if (e.keyCode == 27 && popupStatus == 1) {
                    disablePopup();
                }
            });

        });


        $(function () {
            $("button, input:submit, a", ".demo").button();

            $("a", ".demo").click(function () { return false; });
        });

        var actionId = '<%= Request.QueryString["actionId"] %>';

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
                url: '<%= Url.Action("Update", "ActionItem") %>'
                , beforeSubmit: function (postdata, formid) {
                    postdata.ActionId = actionId;
                    return [true, ''];
                }
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
                , width: "400"
                , afterComplete: function (response, postdata, formid) {
                    $('#dialog p:first').text(response.responseText);
                    $("#dialog").dialog("open");
                }
            };
            var insertDialog = {
                url: '<%= Url.Action("Insert", "ActionItem") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , modal: true
                , beforeSubmit: function (postdata, formid) {
                    postdata.ActionId = actionId;
                    return [true, ''];
                }
                , afterShowForm: function (eparams) {
                    $('#Id').attr('disabled', '');
                    $('#imgItemId').click(function () {
                        OpenPopupItemSearch();
                    });
                }
                , afterComplete: function (response, postdata, formid) {
                    $('#dialog p:first').text(response.responseText);
                    $("#dialog").dialog("open");
                }
                , width: "400"
            };
            var deleteDialog = {
                url: '<%= Url.Action("Delete", "ActionItem") %>'
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
            $.jgrid.edit.addCaption = "Tambah Produk Tindakan Baru";
            $.jgrid.edit.editCaption = "Edit Produk Tindakan";
            $.jgrid.del.caption = "Hapus Produk Tindakan";
            $.jgrid.del.msg = "Anda yakin menghapus Produk Tindakan yang dipilih?";

            //            alert(packetId);
            var imgLov = '<%= Url.Content("~/Content/Images/window16.gif") %>';
            $("#list").jqGrid({
                url: '<%= Url.Action("List", "ActionItem") %>',
                postData: {
                    ActionId: function () { return actionId; }
                },
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Kode Produk', 'Kode Produk', 'Nama', 'Kuantitas', 'Status', 'Deskripsi'],
                colModel: [
                    { name: 'Id', index: 'Id', width: 100, align: 'left', key: true, editrules: { required: true, edithidden: false }, hidedlg: true, hidden: true, editable: false },
                   { name: 'ItemId', index: 'ItemId', width: 200, align: 'left', editable: true, edittype: 'text', editrules: { edithidden: true }, hidden: true,
                       formoptions: {
                           "elmsuffix": "&nbsp;<img src='" + imgLov + "' style='cursor:hand;' id='imgItemId' />"
                       }
                   }, 
                    { name: 'ItemName', index: 'ItemName', width: 200, align: 'left', editable: true, edittype: 'text', editrules: { edithidden: true} },
                    { name: 'ActionItemQty', index: 'ActionItemQty', width: 200, align: 'right', editable: true, edittype: 'text', editrules: { edithidden: true }, hidden: false,
                        editoptions: {
                            dataInit: function (elem) {
                                $(elem).autoNumeric();
                                $(elem).attr("style", "text-align:right;");
                            }
                        }
                    },
                    { name: 'ActionItemStatus', index: 'ActionItemStatus', width: 200, align: 'left', editable: true, edittype: 'text', editrules: { edithidden: true} },
                    { name: 'ActionItemDesc', index: 'ActionItemDesc', width: 200, align: 'left', editable: true, edittype: 'text', editrules: { edithidden: true }, hidden: false}],

                pager: $('#listPager'),
                rowNum: 20,
                rowList: [20, 30, 50, 100],
                rownumbers: true,
                sortname: 'Id',
                sortorder: "asc",
                viewrecords: true,
                height: 250,
                caption: 'Daftar Produk Tindakan',
                autowidth: true,
                loadComplete: function () {
//                    $('#list').setColProp('ItemId', { editoptions: { value: items} });
                },
                ondblClickRow: function (rowid, iRow, iCol, e) {
                    $('#list').editGridRow(rowid, editDialog);
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

          //  var items = $.ajax({ url: '<%= Url.Action("GetList","Item") %>', async: false, cache: false, success: function (data, result) { if (!result) alert('Failure to retrieve the items.'); } }).responseText;

            //alert(itemCats.toString());
          function OpenPopupItemSearch()
        {
          $("#popup_frame").attr("src", "<%= ResolveUrl("~/Master/Item/Search?Price=True") %>");
            $("#popup").dialog("open");
            return false;   
        }

         function SetItemDetail(itemId, itemName, price)
        {
//        alert(itemId);
//        alert(itemName);
//        alert(price);
  $("#popup").dialog("close");
          $('#ItemId').attr('value', itemId);
          $('#ItemName').attr('value', itemName);       
        }
    </script>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <table id="list" class="scroll" cellpadding="0" cellspacing="0">
    </table>
    <div id="listPager" class="scroll" style="text-align: center;">
    </div>
    <div id="listPsetcols" class="scroll" style="text-align: center;">
    </div>
    <div id="dialog" title="Status">
        <p></p>
    </div>
<div id='popup'>
    <iframe width='100%' height='380px' id="popup_frame"></iframe>
</div>
</asp:Content>
