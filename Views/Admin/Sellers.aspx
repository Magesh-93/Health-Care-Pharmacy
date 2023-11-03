<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Sellers.aspx.cs" Inherits="Health_Care_Pharmacy.Views.Admin.Sellers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class ="container-fluid">
        <div class="row mt-5">
            <div class="col-md-4">
                        <div class="row mb-3">
                            <asp:TextBox ID="Sel_Id_Txt" type="text" class="form-control" placeholder="Seller Id" aria-label="First name" Visible="False" runat="server"></asp:TextBox>
                            <div class="col">
                                <asp:TextBox ID="Sel_Name_Txt" type="text" class="form-control" placeholder="Seller Name" aria-label="First name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col">
                                <asp:TextBox ID="Sel_Email_Txt" type="email" class="form-control" placeholder="Seller Email" aria-label="Last name" runat="server"></asp:TextBox>
                            </div>
                         </div>
                         <div class="row  mb-3">
                            <div class="col">
                                <asp:TextBox ID="Sel_Pwd_Txt" type="password" class="form-control" placeholder="Seller Password" aria-label="First name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col">
                                <asp:TextBox ID="sel_Dob_Txt" type="date" class="form-control"  aria-label="Last name" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="row mb-3">
                            <div class="col">
                                <asp:DropDownList ID="Sel_Gender_Txt" class="form-control" runat="server">
                                    <asp:ListItem>Seller Gender</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <asp:TextBox ID="Sel_Address_Txt" type="text" class="form-control" placeholder="Seller Address" aria-label="First name" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                             <asp:Label type="text" class="text-danger" ID="Error_Message" runat="server"></asp:Label>
                            <div class="col d-grid">
                                <asp:Button ID="Edit_Btn" runat="server" Text="Edit" class="btn btn-success btn-block" OnClick="Edit_Btn_Click" />
                            </div>
                            <div class="col d-grid">
                                <asp:Button ID="Save_Btn" runat="server" Text="Save" class="btn btn-primary btn-block" OnClick="Save_Btn_Click" />
                            </div>
                            <div class="col d-grid">
                                <asp:Button ID="Delete_Btn" runat="server" Text="Delete" class="btn btn-danger btn-block" OnClick="Delete_Btn_Click" />
                            </div>
                         </div>
                </div>
                    <div class="col-7">
                        <h2>Sellers List</h2>
                        <asp:GridView ID="Sellers_List" class="table" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Medicine_List_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                    </div>
            </div>
        </div>
</asp:Content>
