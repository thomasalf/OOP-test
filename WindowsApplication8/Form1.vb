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



    'Brukernavn og passord
    Private brukernavn As String = "bruker"
    Private passord As String = "passord"

    'Array som lagrer kundeID til bruk under "redigering av kunde"
    Private kundeIDinformasjon() As Double


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
        GroupBox3.Visible = False
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
        TabControl1.SelectTab(3)
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
        '        Dim data As New DataTable
        '        Dim sql As String = "SELECT * FROM pdk_kunde " _
        '                           & "WHERE ktelefon = '" & TextBox8.Text & "'"
        '        data = query(sql)
        '        If data.Rows.Count = 1 Then
        ' Dim row As DataRow = Data.Rows(0)
        ' TextBox12.Text = row("kfornavn")
        ' TextBox11.Text = row("ketternavn")
        ' TextBox10.Text = row("kadresse")
        ' TextBox9.Text = row("kepost")
        ' TextBox8.Text = row("ktelefon")
        ' End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        '        Dim data As New DataTable
        '        Dim sql As String = "SELECT * FROM pdk_kunde " _
        '                           & "WHERE kepost = '" & TextBox9.Text & "'"
        '        data = query(sql)
        '        If data.Rows.Count = 1 Then
        ' Dim row As DataRow = Data.Rows(0)
        ' TextBox12.Text = row("kfornavn")
        ' TextBox11.Text = row("ketternavn")
        ' TextBox10.Text = row("kadresse")
        ' TextBox9.Text = row("kepost")
        ' TextBox8.Text = row("ktelefon")
        ' End If
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

    Private Sub ComboBoxRegistrerteKunder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRegistrerteKunder.SelectedIndexChanged
        'MsgBox("Kunde-ID = " & kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex))

        'Fyller kundeinformasjonsfelt med informasjonen som finnes i databasen
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde " _
                                   & "WHERE kundeID = '" & kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex) & "'"
        data = query(sql)
        If data.Rows.Count = 1 Then
            Dim row As DataRow = data.Rows(0)
            Label3.Text = row("kundeID")
            TextBox12.Text = row("kfornavn")
            TextBox11.Text = row("ketternavn")
            TextBox10.Text = row("kadresse")
            TextBox9.Text = row("kepost")
            TextBox8.Text = row("ktelefon")
        End If

        'Viser kundeinformasjonsfelter
        GroupBox3.Visible = True
    End Sub

    Private Sub ButtonLastInnRegistrerteKunder_Click(sender As Object, e As EventArgs) Handles ButtonLastInnRegistrerteKunder.Click
        ComboBoxRegistrerteKunder.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde"
        data = query(sql)


        If data.Rows.Count >= 1 Then 'Fyller combobox med kundeinformasjon
            ReDim kundeIDinformasjon(data.Rows.Count - 1) 'justerer lengde på array 
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = "Kunde-ID: " & row("kundeID") & " " & row("kfornavn") & " " & row("ketternavn") & " " & row("kadresse")
                ComboBoxRegistrerteKunder.Items.Add(ComboboxTekst)
                kundeIDinformasjon(teller) = row("kundeID") 'lagrer kundeID i array
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If



    End Sub

    Private Sub TabPage1Innlogging_Click(sender As Object, e As EventArgs) Handles TabPage1Innlogging.Click

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If TextBox14.Text = brukernavn And TextBox13.Text = passord Then
            MenuStrip1.Show()
        Else
            MsgBox("Feil brukernavn/passord")
            MenuStrip1.Hide()
        End If
    End Sub

    Private Sub ComboBox1_dropdown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_sykkel"
        data = query(sql)

        'SLetter unna slik at det ikke vises samme info mange ganger
        ComboBox1.Items.Clear()

        If data.Rows.Count >= 1 Then
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("merke") & " " & row("modell")
                ComboBox1.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen sykkelmodeller funnet i databasen.")
        End If
    End Sub

    Private Sub ComboBox4_dropdown(sender As Object, e As EventArgs) Handles ComboBox4.DropDown
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_status"
        data = query(sql)

        'SLetter unna slik at det ikke vises samme info mange ganger
        ComboBox4.Items.Clear()

        If data.Rows.Count >= 1 Then
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("statusnavn")
                ComboBox4.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen status funnet i databasen.")
        End If
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Dim sykkelnavn As String = ComboBox1.SelectedItem
        Dim status As String = ComboBox4.SelectedItem
        Dim data As New DataTable

        Dim sqlHjelp As String = status.Substring(0, status.IndexOf(" "))
        'LAger variabel med bare sykkelmerke, og ikke modell utifra valg i combobox
        'Dim sykkelnavnMerke As String() = sykkelnavn.Split(" ") 'sykkelnavnMerke(0) angir kun første ordet i setiningen
        'MsgBox(sykkelnavn.Substring(0, sykkelnavn.IndexOf(" ")))

        'sykkelnavn.Substring(0, sykkelnavn.IndexOf(" "))

        Dim sql As String = "Update pdk_sykkel SET statusID='" & sqlHjelp & "'" & "WHERE merke='" & sykkelnavn.Substring(0, sykkelnavn.IndexOf(" ")) & "';"
        data = query(sql)
        'Hjelp




    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs)
        MenuStrip1.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim data As New DataTable
        Dim sql As String = "UPDATE pdk_kunde " _
                                & "SET kfornavn = '" & TextBox12.Text _
                                & "', ketternavn = '" & TextBox11.Text _
                                & "', kadresse = '" & TextBox10.Text _
                                & "', kepost = '" & TextBox9.Text _
                                & "', ktelefon = '" & TextBox8.Text _
                                & "' WHERE kundeID = '" & Label3.Text & "'"
        data = query(sql)

    End Sub


    'knapp til utleie/bestillingsskjerm
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        TabControl1.SelectTab(6)
    End Sub

    'knapp for tilgjengelighet basert på dato
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim fra As Date = DateTimePicker1.Value
        Dim til As Date = DateTimePicker2.Value

        ' If setningen under tror jeg her kan gjøres til "If not" setning, hvor resultatet vil være at vi henter ut alle syklene/utstyret som ER tilgjengelig
        ' fremfor å komme med en feilmelding på hvilke sykler som ikke er tilgjengelig. tilgjengelighet har et eget felt når syklene er "lastet inn".
        ' Hvordan vi kan få dette over til en fornuftig sql spørring er en annen problemstilling. 
        ' if(DateTimePicker1.Value >= fra Or DateTimePicker2.Value <= til Or DateTimePicker1.Value < fra And DateTimePicker2.Value > til) then
        '   MsgBox("Sykkel er allerede utleid i perioden: " & DateTimePicker1.Value " til " & DateTimePicker2.Value
        ' SELECT * from "Utstyr?" WHERE fradato < fra Or tildato > ?? Alternativt henter vi inn alt utstyr og sorterer i Visual Basic ved hjelp av
        ' foreslått IF NOT setning. 



    End Sub
End Class
