<%@ Page Language="VB" AutoEventWireup="false" CodeFile="paymentResponse.aspx.vb" Inherits="paymentResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
<form id="Form1" runat="server">
<table align="center" border="0" width="70%">
    
    <tr class="title">
        <td colspan="2"><p><strong>&nbsp;Transaction Receipt Fields</strong></p></td>
    </tr>
    <tr>
        <td align="right"><strong><i>VPC API Version: </i></strong></td>
        <td><asp:Label id="lblVersion" runat="server"/></td>
    </tr>
    <tr class='shade'>
        <td align="right"><strong><i>Command: </i></strong></td>
        <td><asp:Label id="lblCommand" runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>MerchTxnRef: </em></strong></td>
        <td><asp:Label id="lblMerchTxnRef" runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Merchant ID: </em></strong></td>
        <td><asp:Label id="lblMerchantID" runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>OrderInfo: </em></strong></td>
        <td><asp:Label id="lblOrderInfo" runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Transaction Amount: </em></strong></td>
        <td><asp:Label id="lblAmount" runat="server"/></td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <div class='bl'>Fields above are the primary request values.<hr>Fields below are receipt data fields.</div>
        </td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Transaction Response Code: </em></strong></td>
        <td><asp:Label id="lblTxnResponseCode" runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>QSI Response Code Description: </em></strong></td>
        <td><asp:Label id="lblTxnResponseCodeDesc" runat="server"/></td>
    </tr>
    <tr class='shade'>
        <td align="right"><strong><i>Message: </i></strong></td>
        <td><asp:Label id="lblMessage" runat="server"/></td>
    </tr>
<asp:Panel id="Panel_Receipt" runat="server">
<!-- only display these next fields if not an error -->
    <tr>
        <td align="right"><strong><em>Shopping Transaction Number: </em></strong></td>
        <td><asp:Label id="lblTransactionNo" runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Batch Number for this transaction: </em></strong></td>
        <td><asp:Label id="lblBatchNo" runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Acquirer Response Code: </em></strong></td>
        <td><asp:Label id="lblAcqResponseCode" runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Receipt Number: </em></strong></td>
        <td><asp:Label id="lblReceiptNo" runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Authorization ID: </em></strong></td>
        <td><asp:Label id="lblAuthorizeID" runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Card Type: </em></strong></td>
        <td><asp:Label id="lblCardType" runat="server"/></td>
    </tr>
</asp:Panel>
    <tr>
        <td colspan="2"><hr/></td>
    </tr>
    <tr class="title">
        <td colspan="2" height="25"><p><strong>&nbsp;Hash Validation</strong></p></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Hash Validated Correctly: </em></strong></td>
        <td><asp:Label id="lblHashValidation" runat="server"/></td>
    </tr>
<asp:Panel id="Panel_StackTrace" runat="server">
<!-- only display these next fields if an stacktrace output exists-->
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr class="title">
        <td colspan="2"><p><strong>&nbsp;Exception Stack Trace</strong></p></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label id="lblStackTrace" runat="server"/></td>
    </tr>
</asp:Panel>
    <tr>
        <td width="50%">&nbsp;</td>
        <td width="50%">&nbsp;</td>
    </tr>

</table>
</form>
</body>
</html>
