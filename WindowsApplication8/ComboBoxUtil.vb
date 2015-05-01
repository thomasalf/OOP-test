Public Class ComboBoxUtil

    Public Sub fyllComboBoxMedSykkelmerke(comboboxnavn As ComboBox)
        Dim sykkeldao As New SykkelDAO
        comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_sykkelmerke"
        data = sykkeldao.query(sql)


        If data.Rows.Count >= 1 Then 'Fyller combobox med merkeinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("merke")
                comboboxnavn.Items.Add(ComboboxTekst)
            Next
        End If
    End Sub


    'fyller combobox, lagrer IDer i array
    '   Public Function fyllComboBox1(combobox As ComboBox, data As DataTable, radnavn As String, IDfeltnavn As String)
    ' Dim array()
    '     ReDim array(data.Rows.Count - 1) 'justerer lengde på array
    ' Dim teller As Integer
    '     teller = data.Rows.Count
    '
    ''        For teller = 0 To (teller - 1)
    '    Dim ComboboxTekst As String
    '    Dim row As DataRow = Data.Rows(teller)
    '            ComboboxTekst = row(radnavn)
    '            combobox.Items.Add(ComboboxTekst)
    '            array(teller) = row(IDfeltnavn)
    '        Next
    '        Return array
    '    End Function
End Class
