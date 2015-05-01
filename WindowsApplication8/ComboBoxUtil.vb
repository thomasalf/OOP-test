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

    '  Public Function fyllCombobox2(comboboxnavn As ComboBox, tabellnavn As String, tekstradnavn As String, _
    '                           IDradnavn As String, IDarray As Array)
    '  Dim sykkeldao As New SykkelDAO
    '      comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
    '  Dim data As New DataTable
    '  Dim sql As String = "SELECT * FROM " & tabellnavn & ";"
    '      data = sykkeldao.query(sql)
    '
    '
    '        If data.Rows.Count >= 1 Then 'Fyller combobox med statusinformasjon
    '    Dim tempArray(Data.Rows.Count - 1) 'justerer lengde på array
    '    Dim teller As Integer
    '            teller = data.Rows.Count
    '
    '            For teller = 0 To (teller - 1)
    '    Dim ComboboxTekst As String
    '    Dim row As DataRow = Data.Rows(teller)
    '                ComboboxTekst = row(tekstradnavn)
    '                comboboxnavn.Items.Add(ComboboxTekst)
    '                tempArray(teller) = row(IDradnavn)
    '                MsgBox(tempArray(teller))
    '            Next
    '        End If
    '
    '        Return False
    '    End Function

    '   Public Sub fyllComboBoxMedSykkelmerke(comboboxnavn As ComboBox)
    ' Dim sykkeldao As New SykkelDAO
    '     comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
    ' Dim data As New DataTable
    ' Dim sql As String = "SELECT * FROM pdk_sykkelmerke"
    '     data = sykkeldao.query(sql)
    '
    '
    '        If data.Rows.Count >= 1 Then 'Fyller combobox med merkeinformasjon
    '    Dim teller As Integer
    '            teller = data.Rows.Count
    '
    '            For teller = 0 To (teller - 1)
    '    Dim ComboboxTekst As String
    '    Dim row As DataRow = Data.Rows(teller)
    '                ComboboxTekst = row("merke")
    '                comboboxnavn.Items.Add(ComboboxTekst)
    '            Next
    '        End If
    '    End Sub

    ' Public Sub fyllComboboxMedSykkelmodell(comboboxnavn As ComboBox)
    'Dim sykkeldao As New SykkelDAO
    '    comboboxnavn.Items.Clear() 'Fjerner gammel informasjon fra combobox
    'Dim data As New DataTable
    'Dim sql As String = "SELECT * FROM pdk_sykkelmodell"
    '    data = sykkeldao.query(sql)
    '
    '
    '        If data.Rows.Count >= 1 Then 'Fyller combobox med modellinformasjon
    '    Dim teller As Integer
    '            teller = data.Rows.Count
    '
    '            For teller = 0 To (teller - 1)
    '    Dim ComboboxTekst As String
    '    Dim row As DataRow = Data.Rows(teller)
    '                ComboboxTekst = row("modell")
    '                comboboxnavn.Items.Add(ComboboxTekst)
    '            Next
    '        End If
    '    End Sub

    


End Class
