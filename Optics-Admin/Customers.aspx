<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" EnableEventValidation="false" Inherits="Optics_Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <script src="https://cdn.datatables.net/1.10.12/js/dataTables.jqueryui.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Student
                            <small>Manage Students</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Students</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4">
                    <div role="form" class="box" style="width: 100%">
                        <div class="box-body">
                            <div class="form-group">
                                <asp:ValidationSummary ID="vsStudent" runat="server" ForeColor="Red" HeaderText="There was an Error in Processing the request" ShowMessageBox="True" ValidationGroup="vgStudent" />
                            </div>
                            <div class="form-group">
                                <label for="txtFName">First Name</label>
                                <span>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ErrorMessage="First Name is mandatory."
                                        ControlToValidate="txtFName" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                        ToolTip="First Name is mandatory." ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revFirstName" runat="server" ErrorMessage="Name doesnot contain numbers."
                                        ControlToValidate="txtFName" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                        ToolTip="Name doesnot contain numbers." ValidationExpression="^[a-zA-Z]+$" ValidationGroup="vgStudent">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtFName" CssClass="form-control" Width="100%" runat="server" placeholder="Enter First Name" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtLName">Last Name</label>
                                <span>
                                    <asp:RequiredFieldValidator ID="rfvLName" runat="server"
                                        ErrorMessage="Last Name is mandatory" ControlToValidate="txtLName"
                                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                        ToolTip="Last Name is mandatory" ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revLastName" runat="server" ErrorMessage="Last Name doesnot contain numbers."
                                        ControlToValidate="txtLName" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                        ToolTip="Last Name doesnot contain numbers." ValidationExpression="^[a-zA-Z]+$" ValidationGroup="vgStudent">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtLName" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Last Name" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <%--<div class="form-group">
                                <label for="ddlCollege">Select College</label><span>
                                    <asp:RequiredFieldValidator ID="rgvCollege" runat="server"
                                        ErrorMessage="Select College" ControlToValidate="ddlCollege"
                                        Display="Dynamic" ForeColor="Red" InitialValue="-1"
                                        SetFocusOnError="True" ToolTip="Select College" ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlCollege" CssClass="form-control" Width="100%" runat="server">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="form-group">
                                <label for="txtDOB">Date Of Birth</label><span>
                                    <asp:RequiredFieldValidator ID="rfvDOB" runat="server"
                                        ErrorMessage="Date of Birth is mandatory" ControlToValidate="txtDOB"
                                        Display="Dynamic" ForeColor="Red" ToolTip="Date of Birth is mandatory"
                                        ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cVDOB" runat="server" ClientValidationFunction="DateOfBirth"
                                        OnServerValidate="ServerDateOfBirth" Display="Dynamic" ControlToValidate="txtDOB"
                                        ErrorMessage="Date of Birth cannot be Today's Date." ForeColor="Red" SetFocusOnError="True" ToolTip="Date of Birth cannot be Today's Date." ValidationGroup="vgStudent">*</asp:CustomValidator>
                                </span>
                                <asp:TextBox ID="txtDOB" CssClass="form-control" Width="100%" runat="server" TextMode="Date" placeholder="Enter DOB" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtMobNo">Mobile No</label><span>
                                    <asp:RequiredFieldValidator ID="rgvMobNo" runat="server" ErrorMessage="Mobile No is mandatory" ControlToValidate="txtMobNo" Display="Dynamic" ForeColor="Red" ToolTip="Mobile No is mandatory" ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMobNo" runat="server"
                                        ErrorMessage="Mobile No is not Valid" ControlToValidate="txtMobNo"
                                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                        ToolTip="Mobile No is not Valid" ValidationExpression="^[789]\d{9}$" ValidationGroup="vgStudent">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtMobNo" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Mobile No" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAMobNo">Alternate Mob No</label>
                                <span>
                                    <asp:RegularExpressionValidator ID="revAltMob" runat="server" ErrorMessage="Alternate Mobile No is invalid" ControlToValidate="txtAMobNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ToolTip="Alternate Mobile No is invalid" ValidationExpression="^[789]\d{9}$" ValidationGroup="vgStudent">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtAMobNo" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Alt Mob No" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <%--<div class="form-group">
                                <label for="txtEmrContNo">Emergancy Cont No</label>
                                <span>
                                    <asp:RequiredFieldValidator ID="rfvEmgContactNo" runat="server"
                                        ErrorMessage="Emergency Contact No is mandatory"
                                        ControlToValidate="txtEmrContNo" Display="Dynamic" ForeColor="Red"
                                        ToolTip="Emergency Contact No is mandatory"
                                        ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmgContactNo" runat="server"
                                        ErrorMessage="Emergency Contact No is invalid" ControlToValidate="txtEmrContNo"
                                        ForeColor="Red" ToolTip="Emergency Contact No is invalid" ValidationGroup="vgStudent" ValidationExpression="^[789]\d{9}$">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtEmrContNo" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Emergancy Cont No" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtEmrContPer">Emergancy Cont Person</label>
                                <span>
                                    <asp:RequiredFieldValidator ID="rfvEmgContactPerson" runat="server"
                                        ErrorMessage="Emergency Contact Person is mandatory" ControlToValidate="txtEmrContPer"
                                        Display="Dynamic" ForeColor="Red" ToolTip="Emergency Contact Person is mandatory"
                                        ValidationGroup="vgStudent">*</asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtEmrContPer" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Emergancy contact Person" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>--%>
                            <div class="form-group">
                                <label for="ddlBloodGroup">Blood Group</label>
                                <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" Width="100%" runat="server">
                                    <asp:ListItem Value="NA">--Select Blood Group--</asp:ListItem>
                                    <asp:ListItem>O +Ve</asp:ListItem>
                                    <asp:ListItem>O -Ve</asp:ListItem>
                                    <asp:ListItem>A +Ve</asp:ListItem>
                                    <asp:ListItem>A -Ve</asp:ListItem>
                                    <asp:ListItem>B +Ve</asp:ListItem>
                                    <asp:ListItem>B -Ve</asp:ListItem>
                                    <asp:ListItem>AB +Ve</asp:ListItem>
                                    <asp:ListItem>AB -Ve</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Gender</label>
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rbMale" runat="server" GroupName="rbgGender" Checked="true" Text="Male" />
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rbFemale" runat="server" GroupName="rbgGender" Text="Female" />
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email Address</label><span>
                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                        ErrorMessage="EmailID is invalid" ControlToValidate="txtEmail"
                                        Display="Dynamic" ForeColor="Red" ToolTip="EmailID is invalid"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="vgStudent">*</asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" Width="100%" runat="server" placeholder="Enter EmailId" ValidationGroup="vgStudent"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">Address</label>
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="4" CssClass="form-control" Width="100%" runat="server" placeholder="Enter Address"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box-footer" style="width: 100%">
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" class="btn btn-primary" ValidationGroup="vgStudent" OnClick="btnSubmit_Click"></asp:Button>
                            <asp:Button runat="server" Text="Cancel" ID="btnCancel" class="btn btn-primary" OnClick="btnCancel_Click"></asp:Button>

                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <div class="col-lg-8">
                    <div class="box">

                        <div class="box-body">
                            
                            <asp:Repeater ID="rptStudents" runat="server">
                                <HeaderTemplate>
                                    <table width="100%" class="display" id="example" cellspacing="0">
                                        <thead>
                                            <tr>
                                                <th>Id</th>
                                                <th>Name</th>
                                                <%--<th>Colllege</th>--%>
                                                <th>Mobile</th>
                                                <%--<th>Emer. No</th>--%>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th>Id</th>
                                                <th>Name</th>
                                                <%--<th>Colllege</th>--%>
                                                <th>Mobile</th>
                                                <%--<th>Emer. No</th>--%>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </tfoot>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("UserID") %>
                                        </td>
                                        <td>
                                            <span style="margin-right: 7px;"><%# Eval("FirstName") %></span><%# Eval("LastName") %>
                                        </td>
                                        <%--<td>
                                            <%# Eval("CollegeName") %>
                                        </td>--%>
                                        
                                        <td>
                                            <%# Eval("MobileNo") %>
                                        </td>
                                        <%--<td>
                                            <%# Eval("EmergencyContactNo") %>
                                        </td>--%>
                                        <td>
                                            <asp:ImageButton ImageUrl="dist/img/Detail.png" ID="imgInfo" runat="server" CommandName="Details" OnClick="imgInfo_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:ImageButton></td>
                                        <td>

                                            <asp:ImageButton ImageUrl="dist/img/Edit.png" ID="imgEdit" runat="server" CommandName="edit" OnClick="imgEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:ImageButton></td>
                                        <td>
                                            <asp:ImageButton ImageUrl="dist/img/Delete.png" ID="imgDelete" runat="server" CommandName="delete" OnClick="imgDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:ImageButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
            </div>
        </div>
    </section>
      <%--<script src="plugins/datatables/jquery.dataTables.js"></script>--%>
    <!-- /.content -->
   <%-- <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script src="dist/js/adminlte.min.js"></script>--%>
    <script>
        $(document).ready(function () {
            $('#example').DataTable({
                "lengthMenu": [[25, 50, -1], [25, 50, "All"]],
                dom: 'Bfrtip',
                buttons: [
                    'excel', 'pdf', 'print'
                ]
            });
        });
        function DateOfBirth(source, arguments) {
            var today = new Date();
            var yyyy = today.getFullYear();
            var mm = today.getMonth() + 1;
            var dd = ("0" + today.getDate()).slice(-2);
            today = yyyy + '-' + mm + '-' + dd;
            if (arguments.Value == today) {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
        }
    </script>

</asp:Content>
