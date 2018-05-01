
Partial Class paymentRequest
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
    Dim redirectURL
    Dim count
    Dim paramList As New System.Collections.SortedList(New APCStringComparer())

    Function doSecureHash()

        Dim md5HashData
        Dim element As DictionaryEntry
        ' start the MD5 input
        md5HashData = SECURE_SECRET

        ' loop though the hashtable and add each parameter value to the MD5 input
        For Each element In paramList
            If element.Value <> "" And (element.Key = "accessCode" Or element.Key = "merchTxnRef" Or element.Key = "merchant" Or element.Key = "orderInfo" Or element.Key = "amount" Or element.Key = "returnURL") Then
                md5HashData &= element.Value
            End If
        Next

        doSecureHash = MD5HashString(md5HashData)

    End Function



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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim seperator
        Dim item


        ' Create the URL that will send the data to the APC
        redirectURL = "https://gw1.audicards.com/TPGWeb/payment/prepayment.action"

        ' Add each of the appropriate form variables to the data.
        seperator = "?"
        count = 1
        For Each item In Request.Form

            ' Do not include the Virtual Payment Client URL, the Submit button 
            ' from the form post, or any empty form fields, as we do not want to send 
            ' these fields to the Virtual Payment Client. 
            ' Also construct the VPC URL QueryString while looping through the Form data.
            If Request(item) <> "" And item <> "SubButL" And item <> "virtualPaymentClientURL" Then

                ' Add the item to the hashtable
                'MyArray(count, 0) = CStr(item)
                paramList.Add(CStr(item), CStr(Request(item)))

                ' Add the data to the VPC URL QueryString
                redirectURL = redirectURL & seperator & Server.UrlEncode(CStr(item)) & "=" & Server.UrlEncode(CStr(Request(item)))
                seperator = "&"

            End If
        Next

        ' If there is no Secure Secret then there is no need to create the Secure Hash
        If Len(SECURE_SECRET) > 0 Then

            ' Add the againLink to the Array if we need a Secure Hash
            'MyArray(count, 0) = "AgainLink"
            'MyArray(count, 1) = CStr(Request.ServerVariables("HTTP_REFERER"))

            ' Create MD5 Message-Digest Algorithm hash and add it to the data to be sent
            redirectURL = redirectURL & seperator & "vpc_SecureHash=" & doSecureHash()

        End If

        Response.Redirect(redirectURL)
        Response.End()
    End Sub
End Class
