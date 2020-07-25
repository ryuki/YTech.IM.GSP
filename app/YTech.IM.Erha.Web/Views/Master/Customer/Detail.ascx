<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<RegistrationFormViewModel>" %>
<% if (false)
   { %>
<script src="../../../Scripts/jquery-1.5-vsdoc.js" type="text/javascript"></script>
<% } %>
<%-- <% using (Html.BeginForm())
   {%> --%>
<% using (Ajax.BeginForm(new AjaxOptions
                                       {
                                           //UpdateTargetId = "status",
                                           InsertionMode = InsertionMode.Replace,
                                           OnBegin = "ajaxValidate",
                                           OnSuccess = "onSavedSuccess",
                                           LoadingElementId = "progress"
                                       }

          ))
   {%>
<div id="formArea">
    <%=Html.AntiForgeryToken()%>
    <table>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <label for="Id">
                                No :</label>
                        </td>
                        <td>
                            <%--<%= Html.TextBox("Id",  Model.Customer.Id ?? string.Empty,new {@readonly= Model.CanEditId ? "true" : "false" })%>--%>
                            <%= Model.CanEditId ? Html.TextBox("Id", Model.Customer.Id ?? string.Empty, new { @style = "width:150px" }) :
                                    Html.TextBox("Id", Model.Customer.Id ?? string.Empty, new { @readonly = Model.CanEditId ? "true" : "false", @style = "width:150px" })
                            %>
                            <%= Html.ValidationMessage("Id")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonFirstName">
                                Nama Lengkap
                                <br />
                                (Sesuai KTP/Passport) :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonFirstName", Model.Customer.PersonId.PersonFirstName, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonFirstName")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonPob">
                                Tempat / Tanggal Lahir :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonPob", Model.Customer.PersonId.PersonPob)%>
                            <%= Html.ValidationMessage("PersonPob")%>
                            &nbsp;/&nbsp;
                            <%= Html.TextBox("PersonDob", Model.Customer.PersonId.PersonDob.HasValue ? Model.Customer.PersonId.PersonDob.Value.ToString(CommonHelper.DateFormat):null)%>
                            <%= Html.ValidationMessage("PersonDob")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonGender">
                                Jenis Kelamin :</label>
                        </td>
                        <td>
                            <%=Html.DropDownList("PersonGender",Model.GenderList )%>
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <label for="PersonAnotherName">
                                    Nama Panggilan :</label>
                            </td>
                            <td>
                                <%= Html.TextBox("PersonAnotherName", Model.Customer.PersonId.PersonAnotherName)%>
                                <%= Html.ValidationMessage("PersonAnotherName")%>
                            </td>
                        </tr>--%>
                    <tr>
                        <td>
                            <label for="PersonMobile">
                                Hand Phone :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonMobile", Model.Customer.PersonId.PersonMobile, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonMobile")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonEmail">
                                Email :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonEmail", Model.Customer.PersonId.PersonEmail, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonEmail")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonIdCardType">
                                Kartu Identitas yang digunakan :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonIdCardType", Model.IdCardTypeList)%>
                            <%= Html.ValidationMessage("PersonIdCardType")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonIdCardNo">
                                No. Identitas :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonIdCardNo", Model.Customer.PersonId.PersonIdCardNo, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonIdCardNo")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="AddressIdCardLine1">
                                Alamat Sesuai Kartu Identitas :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressIdCardLine1", Model.Customer.AddressId.AddressIdCardLine1, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressIdCardLine1")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressIdCardLine2", Model.Customer.AddressId.AddressIdCardLine2, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressIdCardLine2")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressIdCardLine3", Model.Customer.AddressId.AddressIdCardLine3, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressIdCardLine3")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="AddressIdCardRt">
                                Rt. :</label>
                            <%= Html.TextBox("AddressIdCardRt", Model.Customer.AddressId.AddressIdCardRt, new { style = "width:20px" })%>
                            <%= Html.ValidationMessage("AddressIdCardRt")%>
                            <label for="AddressIdCardRw">
                                Rw. :</label>
                            <%= Html.TextBox("AddressIdCardRw", Model.Customer.AddressId.AddressIdCardRw, new { style = "width:20px" })%>
                            <%= Html.ValidationMessage("AddressIdCardRw")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="AddressIdCardCity">
                                Kota :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressIdCardCity", Model.Customer.AddressId.AddressIdCardCity, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressIdCardCity")%>
                            <label for="AddressIdCardPostCode">
                                Kode Pos :</label>
                            <%= Html.TextBox("AddressIdCardPostCode", Model.Customer.AddressId.AddressIdCardPostCode, new { style = "width:50px" })%>
                            <%= Html.ValidationMessage("AddressIdCardPostCode")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="AddressLine1">
                                Alamat Domisili :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressLine1", Model.Customer.AddressId.AddressLine1, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressLine1")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressLine2", Model.Customer.AddressId.AddressLine2, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressLine2")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressLine3", Model.Customer.AddressId.AddressLine3, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressLine3")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label for="AddressRt">
                                Rt. :</label>
                            <%= Html.TextBox("AddressRt", Model.Customer.AddressId.AddressRt, new { style = "width:20px" })%>
                            <%= Html.ValidationMessage("AddressRt")%>
                            <label for="AddressRw">
                                Rw. :</label>
                            <%= Html.TextBox("AddressRw", Model.Customer.AddressId.AddressRw, new { style = "width:20px" })%>
                            <%= Html.ValidationMessage("AddressRw")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="AddressCity">
                                Kota :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressCity", Model.Customer.AddressId.AddressCity, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressCity")%>
                            <label for="AddressPostCode">
                                Kode Pos :</label>
                            <%= Html.TextBox("AddressPostCode", Model.Customer.AddressId.AddressPostCode, new { style = "width:50px" })%>
                            <%= Html.ValidationMessage("AddressPostCode")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="AddressPhone">
                                Telepon :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("AddressPhone", Model.Customer.AddressId.AddressPhone, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("AddressPhone")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonNationality">
                                Kewarganegaraan :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonNationality", Model.Customer.PersonId.PersonNationality)%>
                            <%= Html.ValidationMessage("PersonNationality")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonMarriedStatus">
                                Status Perkawinan :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonMarriedStatus", Model.MarriedStatusList )%>
                            <%= Html.ValidationMessage("PersonMarriedStatus")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonReligion">
                                Agama :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonReligion", Model.ReligionList )%>
                            <%= Html.ValidationMessage("PersonReligion")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonOccupation">
                                Pekerjaan :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonOccupation",Model.OccupationList  )%>
                            <%= Html.ValidationMessage("PersonOccupation")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonOfficceName">
                                Nama perusahaan / kantor :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonOfficceName", Model.Customer.PersonId.PersonOfficceName, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonOfficceName")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonOfficceAddress">
                                Alamat kantor :</label>
                        </td>
                        <td>
                            <%= Html.TextArea("PersonOfficceAddress", Model.Customer.PersonId.PersonOfficceAddress,3,200, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonOfficceAddress")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonOfficceCity">
                                Kota :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonOfficceCity", Model.Customer.PersonId.PersonOfficceCity, new { @style = "width:300px" })%>
                            <%= Html.ValidationMessage("PersonOfficceCity")%>
                            <label for="PersonOfficcePostCode">
                                Kode Pos :</label>
                            <%= Html.TextBox("PersonOfficcePostCode", Model.Customer.PersonId.PersonOfficcePostCode)%>
                            <%= Html.ValidationMessage("PersonOfficcePostCode")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonOfficcePhone">
                                Telepon kantor :</label>
                        </td>
                        <td>
                            <%= Html.TextBox("PersonOfficcePhone", Model.Customer.PersonId.PersonOfficcePhone)%>
                            <%= Html.ValidationMessage("PersonOfficcePhone")%>
                            <label for="PersonOfficceFax">
                                Fax :</label>
                            <%= Html.TextBox("PersonOfficceFax", Model.Customer.PersonId.PersonOfficceFax)%>
                            <%= Html.ValidationMessage("PersonOfficceFax")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonLastEdu">
                                Pendidikan Terakhir :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonLastEdu", Model.EducationList )%>
                            <%= Html.ValidationMessage("PersonLastEdu")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonBloodType">
                                Golongan Darah :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonBloodType", Model.BloodTypeList )%>
                            <%= Html.ValidationMessage("PersonBloodType")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="PersonHobby">
                                Hobi :</label>
                        </td>
                        <td>
                            <%= Html.DropDownList("PersonHobby",Model.HobbyList)%>
                            <%= Html.ValidationMessage("PersonHobby")%>
                        </td>
                    </tr>
                    <%-- <tr>
                            <td>
                                <label for="CustomerPhoneJakarta">
                                    Nama dan telepon yang dapat dihubungi di Jakarta :
                                    <br />
                                    (Khusus pasien luar Jakarta)
                                </label>
                            </td>
                            <td>
                                <%= Html.TextBox("CustomerPhoneJakarta", Model.Customer.CustomerPhoneJakarta)%>
                                <%= Html.ValidationMessage("CustomerPhoneJakarta")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="CustomerLetter">
                                    Bila ada surat maka akan dikirim ke :</label>
                            </td>
                            <td>
                                <%= Html.DropDownList("CustomerLetter",Model.LetterList )%>
                                <%= Html.ValidationMessage("CustomerLetter")%>
                            </td>
                        </tr>--%>
                    <%--  <tr>
                            <td colspan="2">
                                <label for="CustomerFaceTreatment">
                                    Perawatan muka yang sekarang dipakai :</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%= Html.TextArea("CustomerFaceTreatment", Model.Customer.CustomerFaceTreatment,5,100,null)%>
                                <%= Html.ValidationMessage("CustomerFaceTreatment")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label for="CustomerAllergy">
                                    Riwayat alergi obat :</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%= Html.TextArea("CustomerAllergy", Model.Customer.CustomerAllergy, 5, 100, null)%>
                                <%= Html.ValidationMessage("CustomerAllergy")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label for="CustomerSkinProblem">
                                    Riwayat masalah kulit sekarang :</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%= Html.TextArea("CustomerSkinProblem", Model.Customer.CustomerSkinProblem, 5, 100, null)%>
                                <%= Html.ValidationMessage("CustomerSkinProblem")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label for="CustomerPlanTreatment">
                                    Rencana Pengobatan / Perawatan :</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%= Html.TextArea("CustomerPlanTreatment", Model.Customer.CustomerPlanTreatment, 5, 100, null)%>
                                <%= Html.ValidationMessage("CustomerPlanTreatment")%>
                            </td>
                        </tr>--%>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div>
                    <span id="toolbar" class="ui-widget-header ui-corner-all">
                        <%--<a id="newCustomer" href="Registration">
                            Registrasi Pasien Baru</a>--%>
                        <button id="Save" type="submit">
                            Simpan</button>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div id="status">
                </div>
                <div class="ui-state-highlight ui-corner-all" style="padding: 5pt; margin-bottom: 5pt;
                    display: none;" id="error">
                    <p>
                        <span class="ui-icon ui-icon-error" style="float: left; margin-right: 0.3em;"></span>
                        <span id="error_msg"></span>.<br clear="all" />
                    </p>
                </div>
            </td>
        </tr>
    </table>
</div>
<%
    }
%>
    <div id="dialog" title="Status">
        <p>
        </p>
    </div>
<script language="javascript" type="text/javascript">
    function onSavedSuccess(e) {
        //        alert(e.get_response().get_object());
        if (e) {
            var json = e.get_response().get_object();
            var success = json.Success;
            var msg = json.Message;
//            alert(success);
//            alert(msg);
            if (success) {
                $("#Save").attr('disabled', 'disabled');
                $('#status').html(msg);
            }
            else {
                $("#Save").attr('disabled', '');
                if (msg) {
                    $('#dialog p:first').text(msg);
                    $("#dialog").dialog("open");
//                    return false;
                }
            }
        }
    }

    function ajaxValidate() {
        var errorimg = '<%= Url.Content("~/Content/Images/cross.gif") %>';
        var checkIdUrl = '<%= Url.Action("CheckCustomer","Customer") %>';
        return $('form').validate({
            rules: {
                "Id": { required: true
                    //                    <% if (string.IsNullOrEmpty(Request.QueryString["customerId"])) {	%>
                    // ,remote: {
                    //                            url: checkIdUrl,
                    //                            type: "post",
                    //                            data: {
                    //                                customerId: function () {
                    //                                    return $("#Id").val();
                    //                                }
                    //                            }
                    //                        }
                    //  <% } %>                       
                },
                "PersonFirstName": { required: true }
            },
            messages: {
                "Id": { required: "<img id='Iderror' src='" + errorimg + "' hovertext='No Pasien harus diisi' />"
                    //                     <% if (string.IsNullOrEmpty(Request.QueryString["customerId"])) {	%>
                    //                    , remote: "<img id='remoteIderror' src='" + errorimg + "' hovertext='No Pasien sudah pernah diinput.' />" <% } %>
                },
                "PersonFirstName": "<img id='PersonFirstNameerror' src='" + errorimg + "' hovertext='Nama harus diisi' />"
            },
            invalidHandler: function (form, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    var message = "Validasi data kurang";
                    $("div#error span#error_msg").html(message);
                    $("div#error").dialog("open");
                    return false;
                } else {
                    $("div#error").dialog("close");
                }
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element);
                //	generateTooltips();
            }
        }).form();
    }


    $(function () {
        $("#newCustomer").button();
        $("#Save").button();
        $("#PersonDob").datepicker({ dateFormat: "dd-M-yy" });
    });

    $(document).ready(function () {
        $("form").mouseover(function () {
            generateTooltips();
        });

        $("#dialog").dialog({
            autoOpen: false
        });

        $("div#error").dialog({
            autoOpen: false
        });
    });

    //function to generate tooltips
    function generateTooltips() {
        //make sure tool tip is enabled for any new error label
        //          alert('s');
        $("img[id*='error']").tooltip({
            showURL: false,
            opacity: 0.99,
            fade: 150,
            positionRight: true,
            bodyHandler: function () {
                return $("#" + this.id).attr("hovertext");
            }
        });
        //make sure tool tip is enabled for any new valid label
        $("img[src*='tick.gif']").tooltip({
            showURL: false,
            bodyHandler: function () {
                return "OK";
            }
        });
    }
</script>
