Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class Form1
    'TAH: Funksjon som automatisk fyller ut resten av kundeinformasjonen
    'hvis bruker skriver inn informasjon som entydig identifiserer
    'en spesifikk kunde (f.eks. telefonnummer)
    '
    '    Private Function autofill(ByVal tabellnavn As String, ByVal tekstboksnavn As String)
    '    Dim data As New DataTable
    '   Dim sql As String = "SELECT * FROM " & tabellnavn _
    '                      & " WHERE " & tabellnavn _
    '                     & " = '" & Me.Controls(tekstboksnavn).Text & "'"
    '    data = query(sql)
    '   If data.Rows.Count = 1 Then
    'Dim row As DataRow = Data.Rows(0)
    '       TextBox12.Text = row("kfornavn")
    '      TextBox11.Text = row("ketternavn")
    '     TextBox10.Text = row("kadresse")
    '    TextBox9.Text = row("kepost")
    '   TextBox8.Text = row("ktelefon")
    'Else
    '    Return false
    'End If
    'End Function



    'Funksjon for kobling til database
    Private Function query(sql As String) As DataTable
        Dim data As New DataTable
        Dim conn As MySqlConnection = New MySqlConnection
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("mysql").ConnectionString
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = New MySqlCommand(sql, conn)
            adapter.Fill(data)
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Feil ved oppkobling til database: " & ex.Message)
        Finally
            conn.Dispose()
        End Try
        Return data
    End Function


    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        TabControl1.SelectTab(1)
    End Sub


    Private Sub Side3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Side3ToolStripMenuItem.Click
        TabControl1.SelectTab(2)
    End Sub

    Private Sub SøkMedlemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SøkMedlemToolStripMenuItem.Click
        MsgBox("Fant ingen registrerte Medlemmer med navn: " & ToolStripTextBox1.Text, MsgBoxStyle.Information, "Medlemsregister")
    End Sub



    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        TabControl1.SelectTab(3)
    End Sub



    Private Sub UtstyrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UtstyrToolStripMenuItem.Click
        TabControl1.SelectTab(4)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectTab(4)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectTab(5)
    End Sub

    Private Sub LoginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        TabControl1.SelectTab(0)
    End Sub



    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde " _
                           & "WHERE ktelefon = '" & TextBox8.Text & "'"
        data = query(sql)
        If data.Rows.Count = 1 Then
            Dim row As DataRow = data.Rows(0)
            TextBox12.Text = row("kfornavn")
            TextBox11.Text = row("ketternavn")
            TextBox10.Text = row("kadresse")
            TextBox9.Text = row("kepost")
            TextBox8.Text = row("ktelefon")
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde " _
                           & "WHERE kepost = '" & TextBox9.Text & "'"
        data = query(sql)
        If data.Rows.Count = 1 Then
            Dim row As DataRow = data.Rows(0)
            TextBox12.Text = row("kfornavn")
            TextBox11.Text = row("ketternavn")
            TextBox10.Text = row("kadresse")
            TextBox9.Text = row("kepost")
            TextBox8.Text = row("ktelefon")
        End If
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If TextBox17.Text.Length <= 0 Then 'sjekker at det er skrevet inn fornavn
            MsgBox("Du må skrive inn et fornavn.")
        ElseIf TextBox18.Text.Length <= 0 Then 'sjekker at det er skrevet inn etternavn
            MsgBox("Du må skrive inn et etternavn.")
        ElseIf TextBox20.Text.Length <= 0 And TextBox21.Text.Length <= 0 Then 'sjekker at e-post eller telefonnummer er skrevet inn
            MsgBox("Du må skrive inn e-postadresse eller telefonnummer slik at kunden kan kontaktes.")
        ElseIf TextBox20.Text.Length > 0 And TextBox20.Text.IndexOf(".") = -1 Then 'sjekker at e-postadressen inneholder punktum
            MsgBox("Sjekk at e-postadressen er riktig og prøv igjen.")
        ElseIf TextBox20.Text.Length > 0 And TextBox20.Text.IndexOf("@") = -1 Then 'sjekker at e-postadressen inneholder alfakrøll
            MsgBox("Sjekk at e-postadressen er riktig og prøv igjen.")
        ElseIf TextBox21.Text.Length > 0 And IsNumeric(TextBox21.Text) = False Then 'sjekker at telefonnummeret består av tall
            MsgBox("Sjekk at telefonnummeret er riktig og prøv igjen.")
        ElseIf TextBox16.Text.Length > 0 And IsNumeric(TextBox16.Text) = False Then 'sjekker at postnummeret består av tall
            MsgBox("Sjekk at postnummeret er riktig og prøv igjen.")
        ElseIf TextBox16.Text.Length <> 4 Then 'sjekker at postnummeret består av 4 tall
            MsgBox("Sjekk at postnummeret er riktig og prøv igjen. Det ser ut til å ha feil lengde.")
        Else
            MsgBox("Alt ser ut til å være riktig utfylt. Lagrer til databasen.")
            'Lagrer informasjon fra textboxer til variabler
            Dim fornavnet As String = TextBox17.Text
            Dim etternavnet As String = TextBox18.Text
            Dim adresse As String = TextBox19.Text & ", " & TextBox16.Text
            Dim epost As String = TextBox20.Text
            Dim telefon As String = TextBox21.Text

            'bruker variabler for å lage SQL-kommando
            Dim data As New DataTable
            Dim sql As String = "INSERT INTO pdk_kunde SET kfornavn = '" _
                                & fornavnet & "', ketternavn = '" _
                                & etternavnet & "', kadresse = '" _
                                & adresse & "', kepost = '" _
                                & epost & "', ktelefon = '" _
                                & telefon & "';"

            data = query(sql)


        End If




    End Sub
End Class
