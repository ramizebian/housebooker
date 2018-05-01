<%@ Page Language="VB" AutoEventWireup="false" CodeFile="transfer.aspx.vb" Inherits="transfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Please wait...transferring to Bank Audi Payment Gateway</title>
</head>

<%--For local testing, Uncomment below--%>
<body onload="document.checkout.submit()" >
    <form action="paymentRequest.aspx" method="post" id="checkout" name="checkout" >
        <input type="text" name="accessCode" id="accessCode" maxlength="8" size="20" value="176DBFEA" />
        <input type="text" name="merchTxnRef" id="merchTxnRef" maxlength="40" value="" size="20" runat="server" />
        <input type="text" name="merchant" id="merchant" maxlength="40" size="20" value="TEST854301" />
        <input type="text" name="orderInfo" id="orderInfo" maxlength="40" size="20" value="" runat="server"/>
        <input type="text" name="amount" id="amount" maxlength="40" size="20" value="" runat="server"/>
        <input type="text" name="returnURL" id="returnURL" maxlength="250" size="65" value="http://localhost:58692/BankAudiGatewaySample/index.aspx?href=results" />
    </form>
</body>
</html>
