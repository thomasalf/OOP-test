Public Class Person
        Private fornavn, etternavn, adresse, epost, telefon As String
        Private fødselsdato As Date

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


        Public Property Fødselsdatoen() As Date
            Get
                Return fødselsdato
            End Get
            Set(ByVal value As Date)
                fødselsdato = value
            End Set
        End Property


        Public Property Telefonen() As String
            Get
                Return telefon
            End Get
            Set(ByVal value As String)
                If IsNumeric(value) And value.length = 8 Then
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


        'Konstruktører/Constructors
        Public Sub New(ByVal etFornavn As String, _
                       etEtternavn As String, _
                       enFødselsdato As Date, _
                       enTelefon As String, _
                       enAdresse As String, _
                       enEpost As String)
            fornavn = etFornavn
            etternavn = etEtternavn
            fødselsdato = enFødselsdato
            telefon = enTelefon
            adresse = enAdresse
            epost = enEpost
        End Sub

        Public Sub New(ByVal etFornavn As String, _
                       etEtternavn As String)
            fornavn = etFornavn
            etternavn = etEtternavn
        End Sub

        Public Sub New(ByVal etFornavn As String, _
                       etEtternavn As String, _
                       enTelefon As String, _
                       enEpost As String)
            fornavn = etFornavn
            etternavn = etEtternavn
            telefon = enTelefon
            epost = enEpost
        End Sub




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
