Public Class KundeinfoUtil


    Public Sub fyllInnKundeinfo(ByVal kundeid As String, kundeIDlabel As Label, fornavntextbox As TextBox, _
                                etternavntextbox As TextBox, adressetextbox As TextBox, _
                                eposttextbox As TextBox, telefontextbox As TextBox)
        Dim persondao As New PersonDAO
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde " _
                                   & "WHERE kundeID = '" & kundeid & "'"
        data = persondao.query(sql)
        If data.Rows.Count = 1 Then
            'clearGroupbox(groupboxnavn)
            Dim row As DataRow = data.Rows(0)
            kundeIDlabel.Text = row("kundeID")
            fornavntextbox.Text = row("kfornavn")
            etternavntextbox.Text = row("ketternavn")
            adressetextbox.Text = row("kadresse")
            eposttextbox.Text = row("kepost")
            telefontextbox.Text = row("ktelefon")
        End If
    End Sub

End Class

