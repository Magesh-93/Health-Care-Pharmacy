<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Health_Care_Pharmacy.Views.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../Assets/Lib/css/bootstrap.min.css" />
    <style>
        *{
            font-family:Poppins;
        }
    </style>
</head>
<body >
    <div class="container-fluid">
        <div class="row mt-5 mb-3">
            <div class="col-md-4"></div>
            <div class="col-md-4 bg-white">
                <h5 class="text-center">Health Care Pharmacy</h5>

                <form runat="server">
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">Email address</label>
                        <asp:TextBox ID="Email_Txt" type="email" autocomplete="off" class="form-control" aria-describedby="emailHelp" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">Password</label>
                        <asp:TextBox ID="Pwd_Txt" type="password" class="form-control" autocomplete="off" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3 form-check">
                        <input type="checkbox" required="required" class="form-check-input" id="exampleCheck1"/>
                        <label class="form-check-label" for="exampleCheck1">Check me out</label>
                    </div>
                    <div class="d-grid mb-3">
                    <asp:Label ID="ErrMsg" class="text-danger" runat="server"></asp:Label>
                        <a href="../Seller/Login.aspx" class="text-success">Seller</a>
                        <asp:Button id="Login_Btn" type="submit" class="btn btn-primary btn-block" runat="server" Text="Login" OnClick="Login_Btn_Click"  />
                    </div>
                </form>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</body>
</html>
