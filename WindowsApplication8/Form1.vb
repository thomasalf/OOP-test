Imports MySql.Data.MySqlClient 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Configuration 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private personDAO As New PersonDAO
    Private sykkelDAO As New SykkelDAO

    'tøm alle textboxer i groupbox
    Private Sub clearGroupbox(ByVal Gruppeboksnavn As GroupBox)
        Dim a As Control
        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next
    End Sub

    'vis alle knapper/bokser i GroupBox
    Private Sub visAltIGroupBox(ByVal Gruppeboksnavn As GroupBox)
        Dim a As Control
        For Each a In Gruppeboksnavn.Controls 'vis alle comboboxer
            If TypeOf a Is ComboBox Then
                a.Visible = True
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is TextBox Then 'vis alle textboxer
                a.Visible = True
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is Label Then 'vis alle labels
                a.Visible = True
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is Button Then 'vis alle buttons
                a.Visible = True
            End If
        Next
    End Sub

    'skjul alle knapper/bokser i GroupBox
    Private Sub skjulAltIGroupBox(ByVal Gruppeboksnavn As GroupBox)
        Dim a As Control
        For Each a In Gruppeboksnavn.Controls 'skjul alle comboboxer
            If TypeOf a Is ComboBox Then
                a.Visible = False
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is TextBox Then 'skjul alle textboxer
                a.Visible = False
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is Label Then 'skjul alle labels
                a.Visible = False
            End If
        Next

        For Each a In Gruppeboksnavn.Controls
            If TypeOf a Is Button Then 'skjul alle buttons
                a.Visible = False
            End If
        Next
    End Sub



    'Brukernavn og passord
    Private brukernavn As String = "bruker"
    Private passord As String = "passord"

    'Arrays til bruk i sammenheng med "redigering av kunde"
    Private kundeIDinformasjon() As Double 'Array som lagrer kundeID
    Private kundeIDtilRedigering As Integer 'Lagrer ID til kunde som skal redigeres

    'Arrays til bruk i sammenheng med "registrere sykkel"
    Private sykkelIDinformasjon() As Double 'Lagrer sykkelID
    Private transportorIDinformasjon() As Double 'Lagrer transportørID
    Private statusIDinformasjon() As Double 'Lagrer statusID
    Private sykkelIDtilRedigering As Integer 'lagrer sykkelID


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
        'Dim sql As String = "SELECT * FROM pdk_kunde"
        'data = query(sql)

        data = personDAO.query(personDAO.velgAlleKunder())

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
            TabControl1.SelectTab(7)
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
                ComboboxTekst = row("sykkelID") & " " & row("merke") & " " & row("modell")
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
                                & "WHERE sy.sykkelID = " & "SUBSTR('" & sykkelnavn & "',1,INSTR('" _
                                & sykkelnavn & "',' '));"

            data = query(sql)

        Catch ex As Exception 'Viser feilmelding hvis noe går galt
            MessageBox.Show("Feil: " & ex.Message)
        End Try


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
        'flytte oppkobling til db over til StatistikkDAO
        Dim sql As String = "SELECT b.bookingID, b.uttid, b.inntid, b.kundeID, " _
        & "CONCAT( k.kfornavn,  ' ', k.ketternavn) AS kunde, b.betalt," _
        & "CONCAT( s.fornavn,  ' ', s.etternavn) AS selger FROM pdk_booking b," _
        & "pdk_kunde k, pdk_ansatt s WHERE b.kundeID = k.kundeID and b.selgerID = s.selgerID;"

        bestillinger = query(sql)
        dgvStatistikk.DataSource = bestillinger

        lstAvanse.Visible = False
        dgvStatistikk.Visible = True

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnAvanse.Click
        'Dim avanse As New DataTable
        'Dim rad As DataRow

        With lstAvanse.Items
            .Add("Foreløpig avanse")
            .Add(vbCrLf)
            .Add("Totalt salg utleie:" & vbTab)
            .Add("Totalt utgifter:" & vbTab)
            .Add("Avanse:" & vbTab)
        End With


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



    Private Sub btnOppdatereEksisterendeSykkel_Click(sender As Object, e As EventArgs) Handles btnOppdatereEksisterendeSykkel.Click
        ComboEksisterendeSykler.Items.Clear() 'Fjerner gammel informasjon fra combobox
        ComboEksisterendeSykler.Visible = True

        Dim data As New DataTable
        data = sykkelDAO.query(sykkelDAO.velgAlleSykler())

        If data.Rows.Count >= 1 Then 'Fyller combobox med sykkelinformasjon
            ReDim sykkelIDinformasjon(data.Rows.Count - 1) 'justerer lengde på array 
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("sykkelID") & " " & row("merke") & " " & row("modell")
                ComboEksisterendeSykler.Items.Add(ComboboxTekst)
                sykkelIDinformasjon(teller) = row("sykkelID") 'lagrer sykkelID i array
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If

    End Sub

    Private Sub btnRegistrereNySykkel_Click(sender As Object, e As EventArgs) Handles btnRegistrereNySykkel.Click
        GroupBoxHvaVilDuGjore.Visible = False
        btnSklLagreOppdatering.Visible = False
        visAltIGroupBox(GroupBoxSykkelinformasjon)
        btnSklLagreOppdatering.Visible = False
        LabelSklSykkelIDSomRedigeres.Visible = False
        GroupBoxSykkelinformasjon.Visible = True

        'START: fyll "status"-combobox
        ComboVelgStatus.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_status"
        data = query(sql)


        If data.Rows.Count >= 1 Then 'Fyller combobox med statusinformasjon
            ReDim statusIDinformasjon(data.Rows.Count - 1) 'justerer lengde på array
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("statusnavn")
                ComboVelgStatus.Items.Add(ComboboxTekst)
                statusIDinformasjon(teller) = row("statusID")
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'SLUTT: fyll "status"-combobox


        'Start: fyll "tilhørighet"-combobox
        ComboVelgHjemsted.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data2 As New DataTable
        Dim sql2 As String = "SELECT DISTINCT stedsnavn, postnr FROM pdk_sted"
        data = query(sql2)


        If data.Rows.Count >= 1 Then 'Fyller combobox med tilhørighetsinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("postnr") '& " " & row("stedsnavn")
                ComboVelgHjemsted.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'Slutt: fyll "tilhørighet"-combobox


        'Start: fyll "transportør"-combobox
        ComboVelgTransportor.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data3 As New DataTable
        Dim sql3 As String = "SELECT * FROM pdk_transportor"
        data = query(sql3)


        If data.Rows.Count >= 1 Then 'Fyller combobox med transportørinformasjon
            ReDim transportorIDinformasjon(data.Rows.Count - 1) 'justerer lengde på array
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("transportornavn")
                ComboVelgTransportor.Items.Add(ComboboxTekst)
                transportorIDinformasjon(teller) = row("transportorID")
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'Slutt: fyll "transportør"-combobox



        'START: fyll "merke"-combobox
        ComboSklVelgMerke.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data4 As New DataTable
        Dim sql4 As String = "SELECT * FROM pdk_sykkelmerke"
        data = query(sql4)


        If data.Rows.Count >= 1 Then 'Fyller combobox med merkeinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("merke")
                ComboSklVelgMerke.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'SLUTT: fyll "merke"-combobox

        'START: fyll "modell"-combobox
        ComboSklVelgModell.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data5 As New DataTable
        Dim sql5 As String = "SELECT * FROM pdk_sykkelmodell"
        data = query(sql5)


        If data.Rows.Count >= 1 Then 'Fyller combobox med modellinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("modell")
                ComboSklVelgModell.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'SLUTT: fyll "modell"-combobox

        'START: fyll "type"-combobox
        ComboSklVelgType.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim data6 As New DataTable
        Dim sql6 As String = "SELECT * FROM pdk_sykkeltype"
        data = query(sql6)


        If data.Rows.Count >= 1 Then 'Fyller combobox med typeinformasjon
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("sykkeltype")
                ComboSklVelgType.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If
        'SLUTT: fyll "type"-combobox

    End Sub

    Private Sub ComboEksisterendeMerker_SelectedIndexChanged(sender As Object, e As EventArgs)
        GroupBoxHvaVilDuGjore.Visible = False
        btnSklLagreOppdatering.Visible = False
        btnSklRegistrerEndringer.Visible = False
        GroupBoxSykkelinformasjon.Visible = True
    End Sub

    Private Sub ComboEksisterendeSykler_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboEksisterendeSykler.SelectedIndexChanged
        GroupBoxHvaVilDuGjore.Visible = False
        'btnSklLagreOppdatering.Visible = True
        'btnSklRegistrerEndringer.Visible = False
        visAltIGroupBox(GroupBoxSykkelinformasjon)
        btnSklRegistrerEndringer.Visible = False
        GroupBoxSykkelinformasjon.Visible = True
        LabelSklSykkelIDSomRedigeres.Visible = True


        'Ny kode
        'Fyller kundeinformasjonsfelt med informasjonen som finnes i databasen
        Dim data As New DataTable
        Dim sql As String = "SELECT * FROM pdk_sykkel " _
                                   & "WHERE sykkelID = '" & sykkelIDinformasjon(ComboEksisterendeSykler.SelectedIndex) & "'"
        data = query(sql)
        If data.Rows.Count = 1 Then
            clearGroupbox(GroupBoxSykkelinformasjon)
            Dim row As DataRow = data.Rows(0)
            'kundeIDtilRedigering = row("kundeID")
            'Label3.Text = kundeIDtilRedigering
            TextBoxSkl1.Text = row("merke")
            TextBoxSkl2.Text = row("modell")
            sykkelIDtilRedigering = row("sykkelID")
            LabelSklSykkelIDSomRedigeres.Text = sykkelIDtilRedigering

        End If


        'Viser kundeinformasjonsfelter
        GroupBox3.Visible = True
    End Sub

    Private Sub btnSklRegistrerEndringer_Click(sender As Object, e As EventArgs) Handles btnSklRegistrerEndringer.Click
        GroupBoxSykkelinformasjon.Visible = False
        GroupBoxHvaVilDuGjore.Visible = True



        'START: TESTKODE

        If TextBoxSkl1.Text = "" And TextBoxSkl2.Text = "" Then
            'Lag sykkel vha rullegardinmenyer
            Try
                Dim sykkel As New Sykkel(ComboSklVelgMerke.Text, ComboSklVelgModell.Text, _
                       ComboSklVelgType.Text, transportorIDinformasjon(ComboVelgTransportor.SelectedIndex), _
                               ComboVelgHjemsted.Text, statusIDinformasjon(ComboVelgStatus.SelectedIndex))
                'bruker data fra opprettet sykkel for å lage SQL-spørring
                sykkelDAO.query(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox("Ny sykkel er opprettet")
            Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
                MessageBox.Show("Feil: " & ex.Message)
            End Try
        ElseIf TextBoxSkl1.Text <> "" Then
            'Lag sykkel vha merketekst, modelltekst og rullegardinmeny
            Try
                Dim sykkel As New Sykkel(TextBoxSkl1.Text, TextBoxSkl2.Text, _
                       ComboSklVelgType.Text, transportorIDinformasjon(ComboVelgTransportor.SelectedIndex), _
                               ComboVelgHjemsted.Text, statusIDinformasjon(ComboVelgStatus.SelectedIndex))
                'bruker data fra opprettet sykkel for å lage SQL-spørring
                sykkelDAO.query(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox("Ny sykkel er opprettet")
            Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
                MessageBox.Show("Feil: " & ex.Message)
            End Try

        ElseIf TextBoxSkl2.Text <> "" Then
            'Lag sykkel vha modelltekst og rullegardinmeny
            Try
                Dim sykkel As New Sykkel(ComboSklVelgMerke.Text, TextBoxSkl2.Text, _
                       ComboSklVelgType.Text, transportorIDinformasjon(ComboVelgTransportor.SelectedIndex), _
                               ComboVelgHjemsted.Text, statusIDinformasjon(ComboVelgStatus.SelectedIndex))
                'bruker data fra opprettet sykkel for å lage SQL-spørring
                sykkelDAO.query(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox(sykkelDAO.lagreNySykkeldataSQL(sykkel))
                MsgBox("Ny sykkel er opprettet")
            Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
                MessageBox.Show("Feil: " & ex.Message)
            End Try

        End If

        'SLUTT: TESTKODE

    End Sub

    Private Sub btnSklSlettSykkel_Click(sender As Object, e As EventArgs) Handles btnSklSlettSykkel.Click
        GroupBoxSykkelinformasjon.Visible = False
        GroupBoxHvaVilDuGjore.Visible = True

        Dim sykkel As New Sykkel(sykkelIDtilRedigering)
        Try
            sykkelDAO.query(sykkelDAO.slettSykkeldataSQL(sykkelIDtilRedigering))
            MsgBox("Sykkelen er slettet fra databasen.")
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try

    End Sub

    Private Sub btnSklLagreNyModell_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSklVisSykkelmeny_Click(sender As Object, e As EventArgs) Handles btnSklVisSykkelmeny.Click
        GroupBoxHvaVilDuGjore.Visible = True
        GroupBoxSykkelinformasjon.Visible = False
        ComboEksisterendeSykler.Visible = False
    End Sub



    Private Sub ComboSykkelSomSkalTransporteres_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboSykkelSomSkalTransporteres.SelectedIndexChanged
        'Dim data As New DataTable
        'Dim sql As String = "SELECT sykkelID, merke, modell FROM pdk_sykkel INNER JOIN pdk_sted WHERE pdk_sykkel.postnr = pdk_sted.postnr AND stedsnavn = '" & ComboSykkelSomSkalTransporteres.SelectedValue & "';"
        'data = query(sql)
        '
        'DataGridViewSykkeltransport.DataSource = data

    End Sub

    Private Sub btnLastInnTransportinfo_Click(sender As Object, e As EventArgs) Handles btnLastInnTransportinfo.Click
        'fyller comboboxer
        ComboSykkelSomSkalTransporteres.Items.Clear() 'Fjerner gammel informasjon fra combobox
        ComboStedSykkelSkalTil.Items.Clear()
        Dim data As New DataTable
        Dim sql As String = "SELECT DISTINCT stedsnavn FROM pdk_sted"
        data = query(sql)


        If data.Rows.Count >= 1 Then 'Fyller combobox med steder
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("stedsnavn")
                ComboSykkelSomSkalTransporteres.Items.Add(ComboboxTekst)
                ComboStedSykkelSkalTil.Items.Add(ComboboxTekst)
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If



    End Sub

    Private Sub ComboSklVelgMerke_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboSklVelgMerke.SelectedIndexChanged
        TextBoxSkl1.Clear()
    End Sub

    Private Sub ComboSklVelgModell_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboSklVelgModell.SelectedIndexChanged
        TextBoxSkl2.Clear()
    End Sub



    Private Sub TextBoxSkl1_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSkl1.TextChanged
        ComboSklVelgMerke.SelectedIndex = -1
        ComboSklVelgModell.SelectedIndex = -1
    End Sub

    Private Sub TextBoxSkl2_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSkl2.TextChanged
        ComboSklVelgModell.SelectedIndex = -1
    End Sub

    Private Sub btnVisAltTest_Click(sender As Object, e As EventArgs)
        skjulAltIGroupBox(GroupBoxSykkelinformasjon)
    End Sub

    Private Sub btnSklLagreOppdatering_Click(sender As Object, e As EventArgs) Handles btnSklLagreOppdatering.Click

    End Sub

    Private Sub Button12asdgsdfbsdgbdfb_Click(sender As Object, e As EventArgs) Handles Button12asdgsdfbsdgbdfb.Click
        ComboSklVelgMerke.SelectedIndex = -1
    End Sub

    Private Sub btnLastInnUtstyr_Click(sender As Object, e As EventArgs) Handles btnLastInnUtstyr.Click
        'fyller comboboxer
        cmbUtstyrskategorier.Items.Clear() 'Fjerner gammel informasjon fra combobox

        Dim data As New DataTable
        'Dim sql As String = "SELECT DISTINCT utstyrstype FROM pdk_ekstrautstyr"
        Dim sql As String = "SELECT * FROM pdk_ekstrautstyr"

        data = query(sql)
        DataGridView1.DataSource = data

        If data.Rows.Count >= 1 Then 'Fyller combobox med utstyrstyper
            Dim teller As Integer
            teller = data.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = data.Rows(teller)
                ComboboxTekst = row("utstyrstype")
                cmbUtstyrskategorier.Items.Add(ComboboxTekst)
            Next

        Else
            MsgBox("Ingen informasjon funnet.")
        End If
    End Sub
End Class
