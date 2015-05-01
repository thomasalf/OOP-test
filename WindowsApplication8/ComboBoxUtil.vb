﻿Public Class ComboBoxUtil

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

    Public Sub fyllComboboxMedSykkelmodell(comboboxnavn As ComboBox)
        Dim sykkeldao As New SykkelDAO
        comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_sykkelmodell"
        data = sykkeldao.query(sql)


        If data.Rows.Count >= 1 Then 'Fyller combobox med modellinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("modell")
                comboboxnavn.Items.Add(ComboboxTekst)
            Next
        End If
    End Sub


   
End Class
