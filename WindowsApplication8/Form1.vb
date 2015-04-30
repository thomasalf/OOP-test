Imports MySql.Data.MySqlClient 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Configuration 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private personDAO As New PersonDAO

    'funksjon som tømmer groupbox
    Private Sub clearGroupbox(ByVal Gruppeboksnavn As GroupBox)
        Dim a As Control
        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next
    End Sub

    ' DERSOM DETTE FUNKER BLIR VI GLAD

    'Brukernavn og passord
    Private brukernavn As String = "bruker"
    Private passord As String = "passord"

    'Array som lagrer kundeID til bruk under "redigering av kunde"
    Private kundeIDinformasjon() As Double

    'Lagrer ID til kunde som skal redigeres
    Private kundeIDtilRedigering As Integer


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


    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Try
            'Bruker tekstboksdata for å opprette ny kunde (bruker klassen "Kunde")
            Dim kunde As New Kunde(TextBox17.Text, TextBox18.Text, _
                                   TextBox21.Text, TextBox16.Text, _
                                   TextBox19.Text, TextBox20.Text, _
                                   ComboBox11.SelectedValue)
            'bruker data fra opprettet kunde for å lage SQL-spørring
            personDAO.query(personDAO.lagreKundedataSQL(kunde))
            MsgBox("Ny kunde er opprettet")
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try
    End Sub

    Private Sub ComboBoxRegistrerteKunder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRegistrerteKunder.SelectedIndexChanged
        'MsgBox("Kunde-ID = " & kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex))

        'Fyller kundeinformasjonsfelt med informasjonen som finnes i databasen
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_kunde " _
                                   & "WHERE kundeID = '" & kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex) & "'"
        data = query(sql)
        If data.Rows.Count = 1 Then
            clearGroupbox(GroupBox3)
            Dim row As DataRow = data.Rows(0)
            kundeIDtilRedigering = row("kundeID")
            Label3.Text = kundeIDtilRedigering
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
        Try
            Dim sykkelnavn As String = ComboBox1.SelectedItem
            Dim status As String = ComboBox4.SelectedItem
            Dim statusbeskrivelse As String = rtbSykkelstatus.Text
            Dim data As New DataTable


            Dim sqlHjelp As String = status.Substring(0, status.IndexOf(" "))
            'Lager variabel med bare sykkelmerke, og ikke modell utifra valg i combobox
            'Dim sykkelnavnMerke As String() = sykkelnavn.Split(" ") 'sykkelnavnMerke(0) angir kun første ordet i setiningen
            'MsgBox(sykkelnavn.Substring(0, sykkelnavn.IndexOf(" ")))

            'sykkelnavn.Substring(0, sykkelnavn.IndexOf(" "))

            'Dim sql As String = "Update pdk_sykkel SET statusID='" & sqlHjelp & "'" & "WHERE merke='" & sykkelnavn.Substring(0, sykkelnavn.IndexOf(" ")) & "';"
            Dim sql As String = "UPDATE pdk_sykkel sy SET sy.statusID = " _
                                & "(SELECT st.statusID from pdk_status st " _
                                & "WHERE st.statusnavn = " & "'" & status & "'), " _
                                & "sy.statusbeskrivelse = " & "'" & statusbeskrivelse & "'" _
                                & "WHERE CONCAT(sy.merke, ' ',sy.modell) = " & "'" & sykkelnavn & "';"

            data = query(sql)

        Catch ex As Exception 'Viser feilmelding hvis noe går galt
            MessageBox.Show("Feil: " & ex.Message)
        End Try
        'Hjelp

        'Vi må hente hver enkelt sykkel her utifra sykkelID, siden vi kan ha flere DBS Intruder f.eks.


    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs)
        MenuStrip1.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Try
            'Bruker tekstboksdata for å opprette ny kunde (bruker klassen "Kunde")
            Dim kunde As New Kunde(TextBox12.Text, TextBox11.Text, _
                                   TextBox8.Text, TextBox7.Text, _
                                   TextBox10.Text, TextBox9.Text, _
                                   ComboBox6.SelectedValue)
            'bruker data fra opprettet kunde for å lage SQL-spørring
            personDAO.query(personDAO.endreKundedataSQL(kunde, kundeIDtilRedigering))
            MsgBox("Kundeinformasjon er oppdatert")
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try






        'START: GAMMEL KODE
        'Sjekker inndata
        ' Try
        'Bruker tekstboksdata for å opprette ny kunde (bruker klassen "Kunde")
        '  Dim kunde As New Kunde(TextBox12.Text, TextBox11.Text, _
        '                         TextBox8.Text, TextBox7.Text, _
        '                         TextBox10.Text, TextBox9.Text, _
        '                         ComboBox6.SelectedValue)
        'bruker data fra opprettet kunde for å lage SQL-kommando
        '  Dim data As New DataTable
        '  Dim sql As String = "UPDATE pdk_kunde " _
        '                      & "SET kfornavn = '" & kunde.getFornavn() _
        '                      & "', ketternavn = '" & kunde.getEtternavn() _
        '                      & "', kadresse = '" & kunde.getGateadresse() & ", " & kunde.getPostnummer() _
        '                      & "', kepost = '" & kunde.getEpost() _
        '                      & "', ktelefon = '" & kunde.getTelefon() _
        '                      & "' WHERE kundeID = '" & Label3.Text & "'"
        '
        '       data = query(sql)
        '      Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
        'MessageBox.Show("Feil: " & ex.Message)
        'End Try
        'SLUTT: GAMMEL KODE




    End Sub


    'knapp til utleie/bestillingsskjerm
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles btnRegUtleie.Click
        TabControl1.SelectTab(6)
    End Sub

    'knapp for tilgjengelighet basert på dato
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim fra As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim til As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")


        Dim data As New DataTable
        Dim sql As String = "SELECT merke, prisprosent, bstatus, statusnavn, inntid FROM pdk_sykkel e JOIN pdk_syklerbooket b ON e.sykkelID=b.bookingID JOIN pdk_booking a ON b.bookingID=a.bookingID JOIN pdk_status s ON e.statusID=s.statusID JOIN pdk_prisnokkel p ON a.prisID=p.prisID WHERE bstatus='tilgjengelig' OR (bstatus='utleid' AND " & fra & " < uttid AND " & til & " < uttid) OR (bstatus='utleid' AND " & fra & " > inntid AND " & til & " < inntid)"

        data = query(sql)
        DataGridView3.DataSource = data




    End Sub


    Private Sub btnRegUtstyr_Click(sender As Object, e As EventArgs) Handles btnRegUtstyr.Click
        Try 'sjekker for feil
            Dim utstyr As New utstyr
            utstyr.utstyrType = _
            InputBox("Skriv inn utstyrstype her", "Registrer utstyr")

            Dim data As New DataTable
            Dim sql As String = "INSERT INTO pdk_ekstrautstyr SET utstyrstype = '" & utstyr.utstyrType & "'"



            data = query(sql)
        Catch ex As Exception 'Viser feilmelding hvis noe går galt
            MessageBox.Show("Feil: " & ex.Message)
        End Try
        'MsgBox(utstyr.utstyrType)
    End Sub


    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        clearGroupbox(GroupBox4)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    'Registrer Booking
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim fra As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim til As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")
        Dim utpost As String = ComboBox7.SelectedText ' usikker på om det skal være text eller value eller noe annet. 
        Dim innpost As String = ComboBox10.SelectedText
        Dim selgerID As Integer ' må hente selgerID fra en plass?
        Dim PrisID As Integer ' PrisID må også hentes, kanskje i forbindelse med henting av tilgjengelige sykler.
        Dim kundeID As String = ComboBox8.SelectedText
        Dim SykkelID As Integer ' Må være String for spørringen sin del? 

        Dim sql As String = "INSERT INTO pdk_booking (uttid,utpostnr,inntid,innpostnr,betalt,selgerID,prisID,kundeID,bstatus) VALUES(" & fra & "," & utpost & "," & til & "," & innpost & ",NULL," & selgerID & "," & PrisID & "," & kundeID & ",'Utleid'); INSERT INTO pdk_syklerbooket (bookingID,sykkelID) VALUES(LAST_INSERT_ID()," & SykkelID & ")"
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btnBestillinger.Click
        Dim bestillinger As New DataTable

        'Ønsker her å hente inn data fra funksjon visBestillingerSQL i StatistikkDAO
        'I tillegg skal bookingprisen inn bakerst
        Dim sql As String = "SELECT b.bookingID, b.uttid, b.inntid, b.kundeID, " _
        & "CONCAT( k.kfornavn,  ' ', k.ketternavn) AS kunde, b.betalt," _
        & "CONCAT( s.fornavn,  ' ', s.etternavn) AS selger FROM pdk_booking b," _
        & "pdk_kunde k, pdk_ansatt s WHERE b.kundeID = k.kundeID and b.selgerID = s.selgerID;"

        bestillinger = query(Sql)
        dgvStatistikk.DataSource = bestillinger

        lstAvanse.Visible = False
        dgvStatistikk.Visible = True

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnAvanse.Click
        Dim avanse As New DataTable
        'Dim rad As DataRow

        Dim sql As String = "SELECT * from pdk_ansatt where ansattype = 'Selger'"



        dgvStatistikk.Visible = False
        lstAvanse.Visible = True

    End Sub

    Private Sub ButtonSlettKunde_Click(sender As Object, e As EventArgs) Handles ButtonSlettKunde.Click
        Try
            personDAO.query(personDAO.slettKundedataSQL(kundeIDtilRedigering))
            MsgBox("Kunden er slettet fra databasen.")
            ComboBoxRegistrerteKunder.Items.Clear() 'Fjerner gammel informasjon fra combobox
            ComboBoxRegistrerteKunder.Text = "Registrerte kunder"
            clearGroupbox(GroupBox3)
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try
    End Sub

    Private Sub btnUtvideMedNyModell_Click(sender As Object, e As EventArgs) Handles btnUtvideMedNyModell.Click
        ComboEksisterendeMerker.Visible = True
    End Sub

    Private Sub btnOppdatereEksisterendeSykkel_Click(sender As Object, e As EventArgs) Handles btnOppdatereEksisterendeSykkel.Click
        ComboEksisterendeSykler.Visible = True
    End Sub
End Class
