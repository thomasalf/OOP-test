Imports MySql.Data.MySqlClient 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Configuration 'kan fjernes når vi har opprettet egne DAO-klasser for alt
Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private personDAO As New PersonDAO
    Private sykkelDAO As New SykkelDAO
    Private comboBoxUtil As New ComboBoxUtil
    Private kundeInfoUtil As New KundeinfoUtil

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

    'Arrays/variabler til bruk i sammenheng med "redigering av kunde"
    Private kundeIDinformasjon() As Double 'Array som lagrer kundeID
    Private selgerIDinformasjon() As Double
    Private kundeIDtilRedigering As Integer 'Lagrer ID til kunde som skal redigeres

    'Arrays/variabler til bruk i sammenheng med "registrere sykkel"
    Private sykkelIDinformasjon() As Double 'Lagrer sykkelID
    Private transportorIDinformasjon() As Double 'Lagrer transportørID
    Private statusIDinformasjon() As Double 'Lagrer statusID
    Private sykkelIDtilRedigering As Integer 'lagrer sykkelID

    'Arrays/variabler til bruk i sammenheng med "redigere sykkel"
    Private tempModell As String 'lagrer sykkelmodell
    Private tempStatus As String 'lagrer sykkelstatus
    Private tempTilhorighet As String 'lagrer tilhørighet
    Private tempTransportor As String 'lagrer transportør
    Private tempSykkelmerke As String 'lagrer sykkelmerke
    Private tempType As String 'lagrer sykkeltype


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



    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs)
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
                                   TextBox19.Text, TextBox20.Text)
            'bruker data fra opprettet kunde for å lage SQL-spørring
            personDAO.query(personDAO.lagreKundedataSQL(kunde))
            MsgBox("Ny kunde er opprettet")
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try
    End Sub

    Private Sub ComboBoxRegistrerteKunder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRegistrerteKunder.SelectedIndexChanged

        'Fyller kundeinformasjonsfelt med informasjonen som finnes i databasen
        kundeInfoUtil.fyllInnKundeinfo(kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex), Label3, _
                                       TextBox12, TextBox11, TextBox10, TextBox9, TextBox8)
        'START: gammel kode
        'Dim data As New DataTable
        'Dim sql As String = "SELECT * FROM pdk_kunde " _
        '                           & "WHERE kundeID = '" & kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex) & "'"
        'data = query(sql)
        'If data.Rows.Count = 1 Then
        ' clearGroupbox(GroupBox3)
        ' Dim row As DataRow = Data.Rows(0)
        ' kundeIDtilRedigering = row("kundeID")
        ' Label3.Text = kundeIDtilRedigering
        ' TextBox12.Text = row("kfornavn")
        ' TextBox11.Text = row("ketternavn")
        ' TextBox10.Text = row("kadresse")
        ' TextBox9.Text = row("kepost")
        ' TextBox8.Text = row("ktelefon")
        ' End If
        'SLUTT: GAMMEL KODE
        Label3.Text = kundeIDinformasjon(ComboBoxRegistrerteKunder.SelectedIndex)
        kundeIDtilRedigering = Label3.Text

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
            'Har satt postnrverdi til "0000" her siden vi har gått bort fra postnr i kunderegistrering.
            Dim kunde As New Kunde(TextBox12.Text, TextBox11.Text, _
                                   TextBox8.Text, "0000", _
                                   TextBox10.Text, TextBox9.Text)
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

        'Hente sykler som ikke er boooket i valgt tidsperiode og utstyr det finnes mer av på lager
        Dim sql As String = "SELECT distinct e.sykkelID as UtstyrsID, e.merke as Merke, e.sykkeltype as Type, " _
                            & "500 as Dagspris, a.bstatus as Status, s.statusnavn as Statusnavn, a.inntid as Inntid " _
                            & "FROM pdk_sykkel e JOIN pdk_syklerbooket b ON e.sykkelID=b.sykkelID " _
                            & "JOIN pdk_booking a ON b.bookingID=a.bookingID " _
                            & "JOIN pdk_status s ON e.statusID=s.statusID " _
                            & "JOIN pdk_prisnokkel p ON a.prisID=p.prisID " _
                            & "WHERE a.bstatus='tilgjengelig' " _
                            & "OR (a.bstatus='utleid' AND " & fra & " < a.uttid AND " & til & " < a.uttid) " _
                            & "OR (a.bstatus='utleid' AND " & fra & " > a.inntid AND " & til & " < a.inntid) " _
                            & "UNION ALL " _
                            & "select e.sykkelID, merke,sykkeltype, null as prisprosent, null as bstatus, statusnavn, " _
                            & "NULL as inntid FROM pdk_sykkel e " _
                            & "JOIN pdk_status s ON e.statusID=s.statusID " _
                            & "where e.sykkelID NOT IN (SELECT sykkelID FROM pdk_syklerbooket) " _
                            & "UNION ALL " _
                            & "SELECT utstyrID as UtstyrsID, NULL as Merke, utstyrstype as Type, " _
                            & "dagpris as Dagspris, NULL as Status, NULL as Statusnavn, NULL as inntid " _
                            & "FROM pdk_ekstrautstyr where antallutleid < antalltotal;"

        data = query(sql)
        DataGridView3.DataSource = data


        ComboBox8.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim databox As New DataTable
        'Dim sql As String = "SELECT * FROM pdk_kunde"
        'data = query(sql)

        databox = personDAO.query(personDAO.velgAlleKunder())

        If databox.Rows.Count >= 1 Then 'Fyller combobox med kundeinformasjon
            ReDim kundeIDinformasjon(databox.Rows.Count - 1) 'justerer lengde på array 
            Dim teller As Integer
            teller = databox.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = databox.Rows(teller)
                ComboboxTekst = row("kundeID") & " " & row("kfornavn") & " " & row("ketternavn") & " " & row("kadresse")
                ComboBox8.Items.Add(ComboboxTekst)
                kundeIDinformasjon(teller) = row("kundeID") 'lagrer kundeID i array
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If


        ComboBox2.Items.Clear()
        Dim ansattbox As New DataTable
        ansattbox = personDAO.query(personDAO.velgAlleAnsatte())


        If ansattbox.Rows.Count >= 1 Then 'Fyller combobox med kundeinformasjon
            ReDim selgerIDinformasjon(ansattbox.Rows.Count - 1) 'justerer lengde på array 
            Dim teller As Integer
            teller = ansattbox.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ansattboxTekst As String
                Dim row As DataRow = ansattbox.Rows(teller)
                ansattboxTekst = row("selgerID") & " " & row("ansattype") & " " & row("fornavn") & " " & row("etternavn") & " " & row("epost") & " " & row("telefon")
                ComboBox2.Items.Add(ansattboxTekst)
                selgerIDinformasjon(teller) = row("selgerID") 'lagrer kundeID i array
            Next
        Else
            MsgBox("Ingen informasjon funnet.")
        End If




    End Sub


    Private Sub btnRegUtstyr_Click(sender As Object, e As EventArgs) Handles btnRegUtstyr.Click

        txtUtstyrstype.Visible = True
        txtUtstyrLeiepris.Visible = True
        txtAntallInnkjopt.Visible = True

    End Sub


    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        clearGroupbox(GroupBox4)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"


    End Sub


    'Registrer Booking
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            Dim fra As String = DateTimePicker1.Value.Date.ToString("yyyy-MM-dd")
            Dim til As String = DateTimePicker2.Value.Date.ToString("yyyy-MM-dd")

            Dim utpost As String = ComboBox7.SelectedItem.ToString
            Dim innpost As String = ComboBox10.SelectedItem.ToString

            'Konstant dagspris for sykkelleie, kan byttes ut med variabel og hente timepris/ukepris fra database
            Const pris As Integer = 500

            Dim selgerID As String = Label8.Text
            Dim PrisID As String = 2 ' denne vil på et senere tidspunkt brukes for å variere pris avhengig av produkt. 
            Dim kundeID As String = Label5.Text
            Dim SykkelID As String = DataGridView3.SelectedRows(0).Cells(0).Value.ToString
            Dim Antalldager = DateTimePicker2.Value.Subtract(DateTimePicker1.Value).Days + 1

            Dim salgspris As String = Antalldager * pris



            'Registrerer bestilling inn i databasen
            Dim sql As String = "INSERT INTO pdk_booking " _
                        & "(uttid,utpostnr,inntid,innpostnr,betalt,selgerID,prisID,kundeID,pris,bstatus) " _
                        & "VALUES('" & fra & "'," & utpost & ",'" & til & "'," & innpost & ",NULL," & selgerID _
                        & "," & PrisID & "," & kundeID & "," & salgspris & ",'Utleid'); " _
                        & "INSERT INTO pdk_syklerbooket (bookingID,sykkelID) VALUES(LAST_INSERT_ID()," & SykkelID & ");"

            query(sql)

        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btnBestillinger.Click
        Dim bestillinger As New DataTable

        Dim sql As String = "SELECT b.bookingID, b.uttid, b.inntid, b.kundeID, " _
        & "CONCAT( k.kfornavn,  ' ', k.ketternavn) AS kunde, b.betalt," _
        & "CONCAT( s.fornavn,  ' ', s.etternavn) AS selger, b.pris FROM pdk_booking b," _
        & "pdk_kunde k, pdk_ansatt s WHERE b.kundeID = k.kundeID and b.selgerID = s.selgerID;"

        ' Hvordan få tak i denne funksjonen her? visBestillingerSQL(sql)


        bestillinger = query(sql)
        dgvStatistikk.DataSource = bestillinger

        lstAvanse.Visible = False
        dgvStatistikk.Visible = True

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnAvanse.Click
        Dim data As New DataTable
        Dim rad As DataRow
        Dim utgifter As Integer = 5000 ' 'Muligheter senere for å hente inn kostnader så langt fra regnskapssystem
        Dim avanse As Integer
        Dim totalpris As Integer
        Dim inntid As String = Now.Year


        'Henter alle bestillinger fra i år
        Dim sql As String = "SELECT SUM(pris) as totalpris from pdk_booking " _
        & "WHERE SUBSTR(inntid,1,4) = '" & inntid & "';"

        data = query(sql)

        'Legger sammen sum av priser
        For Each rad In data.Rows
            totalpris += rad("totalpris")
        Next rad

        'Hente avanse
        avanse = totalpris - utgifter

        'Tømmer listeboksen
        lstAvanse.Items.Clear()

        With lstAvanse.Items
            .Add("Foreløpig avanse")
            .Add(vbCrLf)
            .Add("Totalt salg utleie så langt i år:" & vbTab & totalpris)
            .Add("Totalt kostnader så langt i år:" & vbTab & utgifter)
            .Add("Avanse:" & vbTab & avanse)
        End With

        dgvStatistikk.Visible = False
        lstAvanse.Visible = True

    End Sub

    Private Sub ButtonSlettKunde_Click(sender As Object, e As EventArgs) Handles ButtonSlettKunde.Click
        Try
            personDAO.query(personDAO.slettKundedataSQL(kundeIDtilRedigering))
            MsgBox("Kunden er slettet fra databasen.")
            ComboBoxRegistrerteKunder.Items.Clear() 'Fjerner gammel informasjon fra combobox
            ComboBoxRegistrerteKunder.SelectedIndex = -1
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
        ComboVelgStatus.Visible = True
        ComboVelgTransportor.Visible = True
        labelSykkel5.Visible = True
        labelSykkel7.Visible = True
        LabelSklOpprettNy.Visible = True
        TextBoxSkl1.Visible = True
        TextBoxSkl2.Visible = True
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
        comboBoxUtil.fyllCombobox1(ComboSklVelgMerke, "pdk_sykkelmerke", "merke")
        'comboBoxUtil.fyllComboBoxMedSykkelmerke(ComboSklVelgMerke)
        'GAMMEL KODE:
        '  ComboSklVelgMerke.Items.Clear() 'Fjerner gammel informasjon fra combobox
        '  Dim data4 As New DataTable
        '  Dim sql4 As String = "SELECT * FROM pdk_sykkelmerke"
        '  data = query(sql4)
        '
        '
        '        If data.Rows.Count >= 1 Then 'Fyller combobox med merkeinformasjon
        ' Dim teller As Integer
        ' teller = data.Rows.Count
        '
        '        For teller = 0 To (teller - 1)
        ' Dim ComboboxTekst As String
        ' Dim row As DataRow = data.Rows(teller)
        ' ComboboxTekst = row("merke")
        ' ComboSklVelgMerke.Items.Add(ComboboxTekst)
        ' Next
        ' Else
        ' MsgBox("Ingen informasjon funnet.")
        ' End If
        'SLUTT: fyll "merke"-combobox

        'START: fyll "modell"-combobox
        'comboBoxUtil.fyllComboboxMedSykkelmodell(ComboSklVelgModell)
        comboBoxUtil.fyllCombobox1(ComboSklVelgModell, "pdk_sykkelmodell", "modell")
        'GAMMEL KODE:
        '  ComboSklVelgModell.Items.Clear() 'Fjerner gammel informasjon fra combobox
        '  Dim data5 As New DataTable
        '  Dim sql5 As String = "SELECT * FROM pdk_sykkelmodell"
        '  data = query(sql5)
        '
        '
        '        If data.Rows.Count >= 1 Then 'Fyller combobox med modellinformasjon
        ' Dim teller As Integer
        ' teller = data.Rows.Count
        '
        '        For teller = 0 To (teller - 1)
        ' Dim ComboboxTekst As String
        ' Dim row As DataRow = data.Rows(teller)
        ' ComboboxTekst = row("modell")
        ' ComboSklVelgModell.Items.Add(ComboboxTekst)
        ' Next
        ' Else
        ' MsgBox("Ingen informasjon funnet.")
        ' End If
        'SLUTT: fyll "modell"-combobox

        'START: fyll "type"-combobox
        comboBoxUtil.fyllCombobox1(ComboSklVelgType, "pdk_sykkeltype", "sykkeltype")
        'GAMMEL KODE:
        '   ComboSklVelgType.Items.Clear() 'Fjerner gammel informasjon fra combobox
        '   Dim data6 As New DataTable
        '   Dim sql6 As String = "SELECT * FROM pdk_sykkeltype"
        '   data = query(sql6)
        '
        '
        '        If data.Rows.Count >= 1 Then 'Fyller combobox med typeinformasjon
        ' Dim teller As Integer
        ' teller = data.Rows.Count
        '
        '        For teller = 0 To (teller - 1)
        ' Dim ComboboxTekst As String
        ' Dim row As DataRow = data.Rows(teller)
        ' ComboboxTekst = row("sykkeltype")
        ' ComboSklVelgType.Items.Add(ComboboxTekst)
        ' Next
        ' Else
        ' MsgBox("Ingen informasjon funnet.")
        ' End If
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
        ComboVelgStatus.Visible = False
        ComboVelgTransportor.Visible = False
        labelSykkel5.Visible = False
        labelSykkel7.Visible = False
        LabelSklOpprettNy.Visible = False
        TextBoxSkl1.Visible = False
        TextBoxSkl2.Visible = False
        GroupBoxSykkelinformasjon.Visible = True
        LabelSklSykkelIDSomRedigeres.Visible = True





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

        'Velger riktig status i combobox
        'ComboSklVelgMerke.SelectedIndex = ComboSklVelgMerke.FindStringExact(temprow("merke"))
        ' Dim rowStatus As DataRow = data.Rows(ComboEksisterendeSykler.SelectedIndex)
        ' ComboVelgStatus.SelectedIndex = ComboVelgStatus.FindStringExact(rowStatus("statusnavn"))

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
        'comboBoxUtil.fyllCombobox1(ComboSklVelgMerke, "pdk_sykkelmerke", "merke")
        Dim sykkeldao As New SykkelDAO
        ComboSklVelgMerke.Items.Clear() 'Fjerner gammel informasjon fra combobox
        Dim dataMerke As New DataTable
        Dim sqlMerke As String = "SELECT * FROM pdk_sykkel ;"
        dataMerke = sykkeldao.query(sqlMerke)

        If data.Rows.Count >= 1 Then 'Fyller combobox med informasjon
            ReDim sykkelIDinformasjon(dataMerke.Rows.Count - 1) 'Justerer lengde på array
            Dim teller As Integer
            teller = dataMerke.Rows.Count

            For teller = 0 To (teller - 1)
                Dim ComboboxTekst As String
                Dim row As DataRow = dataMerke.Rows(teller)
                ComboboxTekst = row("merke")
                ComboSklVelgMerke.Items.Add(ComboboxTekst)
                sykkelIDinformasjon(teller) = row("sykkelID")
            Next
        End If

        'START: fyll "modell"-combobox
        comboBoxUtil.fyllCombobox1(ComboSklVelgModell, "pdk_sykkelmodell", "modell")


        'START: fyll "type"-combobox
        comboBoxUtil.fyllCombobox1(ComboSklVelgType, "pdk_sykkeltype", "sykkeltype")

        'Velger riktig informasjon i comboboxer
        Dim tempdata As New DataTable
        Dim tempsql As String = "SELECT * FROM pdk_sykkel"

        tempdata = query(tempsql)
        Dim temprow As DataRow = tempdata.Rows(ComboEksisterendeSykler.SelectedIndex)

        'velger riktig sykkelmerke i combobox
        ComboSklVelgMerke.SelectedIndex = ComboSklVelgMerke.FindStringExact(temprow("merke"))
        'velger riktig sykkelmodell i combobox
        ComboSklVelgModell.SelectedIndex = ComboSklVelgModell.FindStringExact(temprow("modell"))
        'velger riktig sykkeltype i combobox
        ComboSklVelgType.SelectedIndex = ComboSklVelgType.FindStringExact(temprow("sykkeltype"))
        'velger riktig sted/tilhørighet
        ComboVelgHjemsted.SelectedIndex = ComboVelgHjemsted.FindStringExact(temprow("postnr"))

        LabelSykkelID.Text = sykkelIDinformasjon(ComboEksisterendeSykler.SelectedIndex)
        sykkelIDtilRedigering = LabelSykkelID.Text

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

    Private Sub btnSklVisSykkelmeny_Click(sender As Object, e As EventArgs)
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

        Try
            'Bruker comboboksdata for å opprette ny sykkel (bruker klassen "sykkel")
            Dim sykkel As New Sykkel(ComboSklVelgMerke.Text, ComboSklVelgModell.Text, _
                                   ComboSklVelgType.Text)
            'bruker data fra opprettet sykkel for å lage SQL-spørring
            sykkelDAO.query(sykkelDAO.endreSykkeldataSQL(sykkel, sykkelIDtilRedigering))
            MsgBox("sykkelinformasjon er oppdatert")
        Catch ex As Exception 'Viser feilmelding dersom det er problemer med inndata
            MessageBox.Show("Feil: " & ex.Message)
        End Try

        GroupBoxSykkelinformasjon.Visible = False
        GroupBoxHvaVilDuGjore.Visible = True
    End Sub

    Private Sub Button12asdgsdfbsdgbdfb_Click(sender As Object, e As EventArgs)
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


    Private Sub btnLagreUtstyr_Click(sender As Object, e As EventArgs) Handles btnLagreUtstyr.Click
        Try 'sjekker for feil
            Dim utstyr As New utstyr

            utstyr.utstyrType = txtUtstyrstype.Text
            'InputBox("Skriv inn utstyrstype her", "Registrer nytt utstyr")
            utstyr.utstyrPris = txtUtstyrLeiepris.Text
            utstyr.utstyrAntall = txtAntallInnkjopt.Text


            Dim data As New DataTable
            Dim sql As String = "INSERT INTO pdk_ekstrautstyr SET utstyrstype = '" & utstyr.utstyrType & "', " _
                                & "dagpris = '" & utstyr.utstyrPris & "', " _
                                & "antalltotal = '" & utstyr.utstyrAntall & "'; "

            data = query(sql)
        Catch ex As Exception 'Viser feilmelding hvis noe går galt
            MessageBox.Show("Feil: " & ex.Message)
        End Try
        'MsgBox(utstyr.utstyrType)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TabControl1.SelectTab(7)
    End Sub


    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        GroupBoxSykkelinformasjon.Visible = False
        GroupBoxHvaVilDuGjore.Visible = True
        ComboEksisterendeSykler.Items.Clear()
        TabControl1.SelectTab(9)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        TabControl1.SelectTab(8)
    End Sub

    Private Sub btnTransport_Click(sender As Object, e As EventArgs) Handles btnTransport.Click
        TabControl1.SelectTab(10)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        TabControl1.SelectTab(6)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TabControl1.SelectTab(3)
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub btnTilbaketab9_Click(sender As Object, e As EventArgs) Handles btnTilbaketab9.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub btnTibaketab10_Click(sender As Object, e As EventArgs) Handles btnTibaketab10.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub btnTilbakeTab11_Click(sender As Object, e As EventArgs) Handles btnTilbakeTab11.Click
        TabControl1.SelectTab(7)
    End Sub


    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        Dim ID As String = ComboBox8.SelectedItem.ToString
        Dim IDs As String = ID.Substring(0, 2)
        Label5.Text = IDs
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim ID As String = ComboBox2.SelectedItem.ToString
        Dim IDs As String = ID.Substring(0, 2)
        Label8.Text = IDs
    End Sub

    Private Sub RegistrerendreSykkelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrerendreSykkelToolStripMenuItem.Click
        TabControl1.SelectTab(9)
    End Sub

    Private Sub StatistikkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatistikkToolStripMenuItem.Click
        TabControl1.SelectTab(8)
    End Sub

    Private Sub btnVisKunder_Click(sender As Object, e As EventArgs) Handles btnVisKunder.Click
        Dim kunder As New DataTable

        Dim sql As String = "SELECT * from pdk_kunde;"

        ' Hvordan få tak i denne funksjonen her? visBestillingerSQL(sql)

        kunder = query(sql)
        dgvKunder.DataSource = kunder
    End Sub
End Class
