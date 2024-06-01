<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="faculty.aspx.cs" Inherits="CampusAtlas.faculty" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faculty and Departments</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-5">
            <h2 class="mb-4">University Faculties and Departments</h2>
            <asp:Literal ID="litDepartments" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
