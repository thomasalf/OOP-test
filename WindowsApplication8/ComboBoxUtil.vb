Public Class ComboBoxUtil

    Public Sub fyllCombobox1(comboboxnavn As ComboBox, tabellnavn As String, radnavn As String)
        Dim sykkeldao As New SykkelDAO
        comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM " & tabellnavn & ";"
        data = sykkeldao.query(sql)

        If data.Rows.Count >= 1 Then 'Fyller combobox med informasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row(radnavn)
                comboboxnavn.Items.Add(ComboboxTekst)
            Next
        End If
    End Sub

  


End Class
