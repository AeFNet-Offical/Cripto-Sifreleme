Imports System.Text
Imports System.Security.Cryptography
Public Class Cripto
    Public Şifre, Passw As String
    Public Shared sif As String = "password"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Text = Şifre_Olustur(TextBox1.Text)
    End Sub
    Public Function Şifre_Olustur(ByVal Veri As String)
        Dim AES As New RijndaelManaged
        Dim Hash_AES As New MD5CryptoServiceProvider
        Dim encrypted As String
        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(Encoding.GetEncoding(1254).GetBytes(sif))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = CipherMode.ECB
        Dim DESEncrypter As ICryptoTransform = AES.CreateEncryptor
        Dim Buffer As Byte() = Encoding.GetEncoding(1254).GetBytes(Veri)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Dim SifrelenecekVeri As String = encrypted ' Şifrelenecek Veriyi Aldık
        Dim TersCevir As String = StrReverse(SifrelenecekVeri) ' Şifrelenecek Veriyi VbScripteki gibi tersten yazdık . Yani tersten yazdırdık :)
        Dim Base64Cevir As String = Convert.ToBase64String(Encoding.GetEncoding(1254).GetBytes(TersCevir)) ' Base64 ' Terse çevirdiğimiz veriyi yazdık
        Dim VeriUzunluk As String = Len(Base64Cevir) ' Verinin uzunluğu aldık
        Dim VerininYarısı As Integer = VeriUzunluk / 2 ' Verinin Uzunluğunu 2 ye böldük
        Dim VeriyiBöl1 As String = Mid(Base64Cevir, 1, VerininYarısı) ' Verinin Yarısını Aldık
        Dim VeriyiBöl2 As String = Mid(Base64Cevir, VerininYarısı + 1, VeriUzunluk) ' Verinin Diğer Kalanını aldık
        Dim KarmaYap As String = VeriyiBöl2 & VeriyiBöl1 ' İkinci yarı ile İlk Yarı Karıştırıldı 
        Dim TersCevir2 As String = StrReverse(KarmaYap) ' StrReverse ile Tekrar Ters Çevirdik 
        Dim EsittirSil As String = Replace(TersCevir2, "=", "@!") ' Eşittirler @! ile yer değişti

        Dim base64Decoded As String = EsittirSil
        Dim base64Encoded As String
        Dim data As Byte()
        data = Encoding.GetEncoding(1254).GetBytes(base64Decoded)
        base64Encoded = Convert.ToBase64String(data)
        Şifre = base64Encoded

        Return Şifre
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox3.Text = Şifre_Çoz(TextBox2.Text)
    End Sub

    Private Sub Cripto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function Şifre_Çoz(ByVal Veri2 As String)
        Dim base64Encoded As String = Veri2
        Dim base64Decoded As String
        Dim data2() As Byte
        data2 = Convert.FromBase64String(base64Encoded)
        base64Decoded = Encoding.GetEncoding(1254).GetString(data2)
        Passw = base64Decoded

        Dim EsittirSil As String = Replace(Passw, "@!", "=") ' @' ile "=   yer değişti
        Dim TersCevir2 As String = StrReverse(EsittirSil) ' StrReverse ile Tekrar Ters Çevirdik 
        Dim VeriUzunluk As String = Len(TersCevir2) ' Verinin uzunluğu aldık
        Dim VerininYarısı As Integer = VeriUzunluk / 2 ' Verinin Uzunluğunu 2 ye böldük
        Dim VeriyiBöl1 As String = Mid(TersCevir2, 1, VerininYarısı) ' Verinin Yarısını Aldık
        Dim VeriyiBöl2 As String = Mid(TersCevir2, VerininYarısı + 1, VeriUzunluk) ' Verinin Diğer Kalanını aldık
        Dim KarmaYap As String = VeriyiBöl2 & VeriyiBöl1 ' İkinci yarı ile İlk Yarı Karıştırıldı 

        Dim Base64Decode As String
        Dim data() As Byte
        data = Convert.FromBase64String(KarmaYap)
        Base64Decode = Encoding.GetEncoding(1254).GetString(data)
        Dim TersCevir1 As String = StrReverse(Base64Decode)

        Dim AES As New RijndaelManaged
        Dim Hash_AES As New MD5CryptoServiceProvider
        Dim decrypted As String
        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(Encoding.GetEncoding(1254).GetBytes(sif))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = CipherMode.ECB
        Dim DESDecrypter As ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(TersCevir1)
        decrypted = Encoding.GetEncoding(1254).GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted
    End Function

End Class
