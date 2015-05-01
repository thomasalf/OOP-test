Public Class ComboBoxUtil


    'fyller combobox, lagrer IDer i array
    Public Function fyllComboBox1(combobox As ComboBox, data As DataTable, radnavn As String, IDfeltnavn As String)
        Dim array()
        ReDim array(data.Rows.Count - 1) 'justerer lengde på array
        Dim teller As Integer
        teller = data.Rows.Count

        For teller = 0 To (teller - 1)
            Dim ComboboxTekst As String
            Dim row As DataRow = data.Rows(teller)
            ComboboxTekst = row(radnavn)
            combobox.Items.Add(ComboboxTekst)
            array(teller) = row(IDfeltnavn)
        Next
        Return array
    End Function
End Class
