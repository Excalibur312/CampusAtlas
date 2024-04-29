﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uni.aspx.cs" Inherits="CampusAtlas.uni" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="litUniversityData" runat="server" />
        </div>
    </form>
    <script>
        function goToDepartmentPage(pageNo) {
            var url = "Department.aspx?pageNo=" + encodeURIComponent(pageNo);
            window.open(url, "_blank");
        }
    </script>
</body>
</html>