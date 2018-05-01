
Partial Class paymentResponse
    Inherits System.Web.UI.Page
    Private Class APCStringComparer
        Implements IComparer
        '
        ' <summary>Customised Compare Class</summary>
        ' <remarks>
        ' <para>
        ' This class provides a Compare method that is used to allow the sorted list
        ' to be ordered using an Ordinal comparison.
        ' </para>
        ' </remarks>
        '


        Public Function Compare(ByVal a As Object, ByVal b As Object) As Integer Implements IComparer.Compare
            '
            ' <summary>Compare method using Ordinal comparison</summary>
            ' <param name="a">The first string in the comparison.</param>
            ' <param name="b">The second string in the comparison.</param>
            ' <returns>An int containing the result of the comparison.</returns>
            '


            ' Return if we are comparing the same object or one of the
            ' objects is null, since we don't need to go any further.
            If a = b Then
                Return 0
            End If
            If a Is Nothing Then
                Return -1
            End If
            If b Is Nothing Then
                Return 1
            End If

            ' Ensure we have string to compare
            Dim sa As String = TryCast(a, String)
            Dim sb As String = TryCast(b, String)

            ' Get the CompareInfo object to use for comparing
            Dim myComparer As System.Globalization.CompareInfo = System.Globalization.CompareInfo.GetCompareInfo("en-US")
            If sa IsNot Nothing AndAlso sb IsNot Nothing Then
                ' Compare using an Ordinal Comparison.
                Return myComparer.Compare(sa, sb, System.Globalization.CompareOptions.Ordinal)
            End If
            Throw New ArgumentException("a and b should be strings.")
        End Function
    End Class


    Const SECURE_SECRET = "4945BEB942F094EEC27B65F94283DCA1"

    'For Audi Payment
    Public Shared Function MD5HashString(ByVal str As String) As String
        Dim StrHash As String = ""
        Dim hash As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arr(), arrHashed() As Byte
        Dim strr As String = str

        arr = System.Text.UTF8Encoding.UTF8.GetBytes(str)
        arrHashed = hash.ComputeHash(arr)
        Dim i As Integer
        Dim b As Byte
        For Each b In arrHashed
            StrHash &= b.ToString("x2")
        Next
        Dim str3 As String = StrHash

        Return StrHash.ToLower()

    End Function
    '  -----------------------------------------------------------------------------

    ' This function uses the Transaction Response code retrieved from the Digital
    ' Receipt and returns an appropriate description for the QSI Response Code
    '
    ' @param vResponseCode containing the QSI Response Code
    '
    ' @return description containing the appropriate description
    '
    Function getResponseDescription(ByVal txnResponseCode)

        Select Case txnResponseCode
            Case "0"
                getResponseDescription = "Transaction Successful"
            Case "1"
                getResponseDescription = "Unknown Error"
            Case "2"
                getResponseDescription = "Bank Declined Transaction"
            Case "3"
                getResponseDescription = "No Reply from Bank"
            Case "4"
                getResponseDescription = "Expired Card"
            Case "5"
                getResponseDescription = "Insufficient Funds"
            Case "6"
                getResponseDescription = "Error Communicating with Bank"
            Case "7"
                getResponseDescription = "Payment Server System Error"
            Case "8"
                getResponseDescription = "Transaction Type Not Supported"
            Case "9"
                getResponseDescription = "Bank declined transaction (Do not contact Bank)"
            Case "A"
                getResponseDescription = "Transaction Aborted"
            Case "C"
                getResponseDescription = "Transaction Cancelled"
            Case "D"
                getResponseDescription = "Deferred transaction received and is awaiting processing"
            Case "F"
                getResponseDescription = "3D Secure Authentication failed"
            Case "I"
                getResponseDescription = "Card Security Code verification failed"
            Case "L"
                getResponseDescription = "Shopping Transaction Locked"
            Case "N"
                getResponseDescription = "Cardholder is not enrolled in Authentication scheme"
            Case "P"
                getResponseDescription = "Transaction is still being processed"
            Case "R"
                getResponseDescription = "Transaction not processed - Reached limit of retry attempts allowed"
            Case "S"
                getResponseDescription = "Duplicate SessionID (OrderInfo)"
            Case "T"
                getResponseDescription = "Address Verification Failed"
            Case "U"
                getResponseDescription = "Card Security Code Failed"
            Case "V"
                getResponseDescription = "Address Verification and Card Security Code Failed"
            Case "?"
                getResponseDescription = "Transaction status is unknown"
            Case "X"
                getResponseDescription = "Card Blocked"
            Case "Y"
                getResponseDescription = "Invalid URL"
            Case "Z"
                getResponseDescription = "Bin Blocked"
            Case "Q"
                getResponseDescription = "IP Blocked"
            Case "M"
                getResponseDescription = "Missing Fields"
            Case "J"
                getResponseDescription = "Transaction in Use"
            Case "G"
                getResponseDescription = "Invalid Merchant"
            Case "E"
                getResponseDescription = "Invalid Credit Card"
            Case "B"
                getResponseDescription = "Pending Transaction"
            Case Else
                getResponseDescription = "Unable to be determined"
        End Select
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Panel_Receipt.Visible = False
        Panel_StackTrace.Visible = False

        ' define message string for errors
        Dim message = ""
        Dim hashValidated = True
        ' transaction response code
        Dim txnResponseCode = ""
        ' error exists flag
        Dim errorExists = False
        Dim rawHashData = SECURE_SECRET
        ' If we have a SECURE_SECRET then validate the incoming data using the MD5 hash
        'included in the incoming data
        If Page.Request.QueryString("vpc_SecureHash").Length > 0 Then

            ' NOTE: We use our own overloaded IComparer interface to ensure we do
            ' an Ordinal sort of the data.
            Dim transactionData As New System.Collections.SortedList(New APCStringComparer())

            ' Collect debug information

            ' Transaction data should not be passed in in this manner for a
            ' production system - see notes above.
            ' loop through all the data in the Page.Request.Form
            For Each item As String In Page.Request.QueryString

                ' Collect debug information

                ' Only include those fields required for a transaction
                ' * Extract the Form POST data and ignore the Virtual Payment Client
                ' * URL, the ReturnURL, the Submit button and any empty form fields,
                ' * as we do not want to send these fields on, or in the case of the
                ' * vpc_ReturnURL modify it before sending.
                '

                ' dont consider any of the additional parameters passed in the return URL
                ' in our case we have the action parameter
                If SECURE_SECRET.Length > 0 AndAlso Not item.Equals("vpc_SecureHash") AndAlso Not item.Equals("action") Then
                    transactionData.Add(item, Page.Request.QueryString(item).Replace(" ", "+"))
                End If
            Next

            ' Loop through all the data in the SortedList transaction data
            For Each item As System.Collections.DictionaryEntry In transactionData
                ' Collect the data required for the MD5 sugnature if required
                rawHashData += item.Value.ToString()
            Next

            ' Create the MD5 signature if required
            Dim signature As String = ""
            If SECURE_SECRET.Length > 0 Then
                ' create the signature and add it to the query string
                signature = MD5HashString(rawHashData)
                Dim secure As String = Page.Request.QueryString("vpc_SecureHash")
                Dim isValid As Boolean = Page.Request.QueryString("vpc_SecureHash").Contains(signature.ToLower())
                ' Validate the Secure Hash (remember MD5 hashes are not case sensitive)
                If isValid Then
                    ' Secure Hash validation succeeded,
                    ' add a data field to be displayed later.
                    lblHashValidation.Text = "<font color='00AA00'><b>CORRECT</b></font>"
                Else
                    ' Secure Hash validation failed, add a data field to be displayed
                    ' later.
                    lblHashValidation.Text = "<font color='FF0066'><b>INVALID HASH</b></font>"
                    hashValidated = False
                End If
            End If


            ' Get the standard receipt data from the parsed response
            txnResponseCode = IIf((Page.Request.QueryString("vpc_TxnResponseCode") IsNot Nothing AndAlso Page.Request.QueryString("vpc_TxnResponseCode").Length > 0), Page.Request.QueryString("vpc_TxnResponseCode"), "Unknown")
            lblTxnResponseCode.Text = txnResponseCode
            lblTxnResponseCodeDesc.Text = getResponseDescription(txnResponseCode)

            lblAmount.Text = IIf((Page.Request.QueryString("amount") IsNot Nothing AndAlso Page.Request.QueryString("amount").Length > 0), Page.Request.QueryString("amount"), "Unknown")
            lblCommand.Text = IIf((Page.Request.QueryString("vpc_Command") IsNot Nothing AndAlso Page.Request.QueryString("vpc_Command").Length > 0), Page.Request.QueryString("vpc_Command"), "Unknown")
            lblVersion.Text = IIf((Page.Request.QueryString("vpc_Version") IsNot Nothing AndAlso Page.Request.QueryString("vpc_Version").Length > 0), Page.Request.QueryString("vpc_Version"), "Unknown")
            lblOrderInfo.Text = IIf((Page.Request.QueryString("orderInfo") IsNot Nothing AndAlso Page.Request.QueryString("orderInfo").Length > 0), Page.Request.QueryString("orderInfo"), "Unknown")
            lblMerchantID.Text = IIf((Page.Request.QueryString("merchant") IsNot Nothing AndAlso Page.Request.QueryString("merchant").Length > 0), Page.Request.QueryString("merchant"), "Unknown")

            ' only display this data if not an error condition
            If Not errorExists AndAlso Not txnResponseCode.Equals("7") Then
                lblBatchNo.Text = IIf((Page.Request.QueryString("vpc_BatchNo") IsNot Nothing AndAlso Page.Request.QueryString("vpc_BatchNo").Length > 0), Page.Request.QueryString("vpc_BatchNo"), "Unknown")
                lblCardType.Text = IIf((Page.Request.QueryString("vpc_Card") IsNot Nothing AndAlso Page.Request.QueryString("vpc_Card").Length > 0), Page.Request.QueryString("vpc_Card"), "Unknown")
                lblReceiptNo.Text = IIf((Page.Request.QueryString("vpc_ReceiptNo") IsNot Nothing AndAlso Page.Request.QueryString("vpc_ReceiptNo").Length > 0), Page.Request.QueryString("vpc_ReceiptNo"), "Unknown")
                lblMerchTxnRef.Text = IIf((Page.Request.QueryString("merchTxnRef") IsNot Nothing AndAlso Page.Request.QueryString("merchTxnRef").Length > 0), Page.Request.QueryString("merchTxnRef"), "Unknown")
                lblAcqResponseCode.Text = IIf((Page.Request.QueryString("vpc_AcqResponseCode") IsNot Nothing AndAlso Page.Request.QueryString("vpc_AcqResponseCode").Length > 0), Page.Request.QueryString("vpc_AcqResponseCode"), "Unknown")
                lblTransactionNo.Text = IIf((Page.Request.QueryString("vpc_TransactionNo") IsNot Nothing AndAlso Page.Request.QueryString("vpc_TransactionNo").Length > 0), Page.Request.QueryString("vpc_TransactionNo"), "Unknown")
                Panel_Receipt.Visible = True
            End If

            ' if message was not provided locally then obtain value from server
            If message.Length = 0 Then
                message = IIf((Page.Request.QueryString("vpc_TransactionNo") IsNot Nothing AndAlso Page.Request.QueryString("vpc_Message").Length > 0), Page.Request.QueryString("vpc_Message"), "Unknown")
            End If
        End If


    End Sub
End Class
