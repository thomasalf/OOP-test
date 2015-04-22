Public Class Person
    Private fornavn As String = ""
    Private etternavn As String = ""
    Private adresse As String = ""
    Private postnummer As String = ""
    Private epost As String = ""
    Private telefon As String = ""



    'Konstruktører/Constructors

    'Setter alle verdier
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String, _
                   pnr As String, _
                   adr As String, _
                   ep As String)
        setFornavn(fn)
        setEtternavn(en)
        setAdresse(adr)
        setPostnummer(pnr)
        setEpost(ep)
        setTelefon(tlf)
    End Sub

    'Setter navn og telefonnummer
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String)
        setFornavn(fn)
        setEtternavn(en)
        setTelefon(tlf)
    End Sub

    'Setter navn og e-post
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   ep As String)
        setFornavn(fn)
        setEtternavn(en)
        setEpost(ep)
    End Sub



    'Get- og set-funksjoner
    Public Function getFornavn() As String
        Return fornavn
    End Function

    Public Sub setFornavn(ByVal fn As String)
        If fn.Length > 0 Then 'sjekker at fornavnet er skrevet inn
            fornavn = fn
        Else
            Throw New Exception("Ugyldig fornavn")
        End If

    End Sub






    'Get- og set-funksjoner
    Public Property Fornavnet() As String
        Get
            Return fornavn
        End Get
        Set(ByVal value As String)
            fornavn = value
        End Set
    End Property


    Public Property Etternavnet() As String
        Get
            Return etternavn
        End Get
        Set(ByVal value As String)
            etternavn = value
        End Set
    End Property


    Public Property Adressen() As String
        Get
            Return adresse
        End Get
        Set(ByVal value As String)
            adresse = value
        End Set
    End Property




    Public Property Telefonen() As String
        Get
            Return telefon
        End Get
        Set(ByVal value As String)
            If IsNumeric(value) And value.Length = 8 Then
                telefon = value
            Else
                Throw New Exception("Ugyldig telefonnummer")
            End If
        End Set
    End Property


    Public Property Eposten() As String
        Get
            Return epost
        End Get
        Set(ByVal value As String)
            If value.Contains("@") And value.Contains(".") Then
                epost = value
            Else
                Throw New Exception("Ugyldig e-postadresse")
            End If
        End Set
    End Property







    '    Public Sub New(ByVal etfornavn As String, _
    '                   etEtternavn As String, _
    '                   enAdresse As String)
    '        fornavn = etfornavn
    '        etternavn = etEtternavn
    '        adresse = enAdresse
    '    End Sub

    'Tostring()-funksjon
    'Public Overrides Function ToString() As String
    '   Return fornavn & " " & etternavn & ". " & "Født: " & fødselsdato & " Adresse: " & adresse & " Telefon: " & _
    '      telefon & "E-post: " & epost
    'End Function




End Class
