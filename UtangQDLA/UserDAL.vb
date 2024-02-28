Imports System.Data
Imports System.Runtime.InteropServices
Imports Microsoft.Data.SqlClient
Imports UtangQBO
Imports UtangQInterface

Public Class UserDAL
    Implements IUser, IBill, IWallet

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        strConn = "Server=.\BSISqlExpress;Database=UtangQ;Trusted_Connection=True;TrustServerCertificate=True;"
        conn = New SqlConnection(strConn)
    End Sub

    Public Function CreateUser(obj As User) As Integer Implements ICrud(Of User).Create
        Try
            Dim strSP = "Users.CreateUser"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@Username", obj.Username)
            cmd.Parameters.AddWithValue("@UserPassword", obj.UserPassword)
            cmd.Parameters.AddWithValue("@UserEmail", obj.UserEmail)
            cmd.Parameters.AddWithValue("@UserFullName", obj.UserFullName)
            cmd.Parameters.AddWithValue("@UserPhoneNumber", obj.UserPhoneNumber)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            Return result

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetUserById(id As Integer) As User Implements ICrud(Of User).GetById
        Try
            Dim strSP = "Users.ReadUser"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@UserID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                Dim user As New User
                user.UserID = CInt(dr("UserID"))
                user.Username = dr("Username").ToString()
                user.UserPassword = dr("UserPassword").ToString()
                user.UserEmail = dr("UserEmail").ToString()
                user.UserFullName = dr("UserFullName").ToString()
                user.UserPhoneNumber = dr("UserPhoneNumber").ToString()
                Return user
            Else
                Return Nothing
            End If

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function CreateBill(obj As Bill) As Integer Implements ICrud(Of Bill).Create
        Try
            Dim strSP = "Users.CreateBill"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", obj.UserID)
            cmd.Parameters.AddWithValue("@BillAmount", obj.BillAmount)
            cmd.Parameters.AddWithValue("@BillDate", obj.BillDate)
            cmd.Parameters.AddWithValue("@BillDescription", obj.BillDescription)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            Return result

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Private Function GetBillById(id As Integer) As Bill Implements ICrud(Of Bill).GetById
        Try
            Dim strSP = "Users.ReadBill"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@BillID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                Dim bill As New Bill
                bill.BillID = CInt(dr("BillID"))
                bill.UserID = CInt(dr("UserID"))
                bill.BillAmount = CDec(dr("BillAmount"))
                bill.BillDate = CDate(dr("BillDate"))
                bill.BillDescription = dr("BillDescription").ToString()
                Return bill
            Else
                Return Nothing
            End If

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function CreateWallet(obj As Wallet) As Integer Implements ICrud(Of Wallet).Create
        Try
            Dim strSP = "Users.CreateWallet"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", obj.UserID)
            cmd.Parameters.AddWithValue("@WalletBalance", obj.WalletBalance)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            Return result

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Private Function GetWalletById(id As Integer) As Wallet Implements ICrud(Of Wallet).GetById
        Try
            Dim strSP = "Users.ReadWallet"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@WalletID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                Dim wallet As New Wallet
                wallet.WalletID = CInt(dr("WalletID"))
                wallet.UserID = CInt(dr("UserID"))
                wallet.WalletBalance = CDec(dr("WalletBalance"))
                Return wallet
            Else
                Return Nothing
            End If

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()
        End Try

    End Function

    Public Function UpdateWalletBalance(UserID As Integer, Amount As Decimal, OperationFlag As String) As Integer Implements IWallet.UpdateWalletBalance
        Try
            Dim strSP = "Users.UpdateWalletBalance"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", UserID)
            cmd.Parameters.AddWithValue("@Amount", Amount)
            cmd.Parameters.AddWithValue("@OperationFlag", OperationFlag)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            Return result

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function GetAllUser() As List(Of User) Implements ICrud(Of User).GetAll
        Dim users As New List(Of User)
        Try
            Dim strSP = "Users.ReadAllUsers"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim user As New User
                    user.UserID = dr("UserID")
                    user.Username = dr("Username")
                    user.UserPassword = dr("UserPassword")
                    user.UserEmail = dr("UserEmail")
                    user.UserFullName = dr("UserFullName")
                    user.UserPhoneNumber = dr("UserPhoneNumber")
                    users.Add(user)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return users
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Private Function GetAllBill() As List(Of Bill) Implements ICrud(Of Bill).GetAll
        Dim bills As New List(Of Bill)
        Try
            Dim strSP = "Transactions.ReadAllBills"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim bill As New Bill
                    bill.BillID = dr("BillID")
                    bill.UserID = dr("UserID")
                    bill.BillAmount = dr("BillAmount")
                    bill.BillDate = dr("BillDate")
                    bill.BillDescription = dr("BillDescription")
                    bills.Add(bill)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return bills
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Private Function GetAllWallet() As List(Of Wallet) Implements ICrud(Of Wallet).GetAll
        Dim wallets As New List(Of Wallet)
        Try
            Dim strSP = "Transactions.ReadAllWallets"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim wallet As New Wallet
                    wallet.WalletID = dr("WalletID")
                    wallet.UserID = dr("UserID")
                    wallet.WalletBalance = dr("WalletBalance")
                    wallets.Add(wallet)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return wallets
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetUserBill(id As Integer) As List(Of Bill) Implements IBill.GetUserBill
        Dim bills As New List(Of Bill)
        Try
            Dim strSP = "Transactions.ReadUserBills"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@UserID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim bill As New Bill
                    bill.BillID = dr("BillID")
                    bill.UserID = dr("UserID")
                    bill.BillAmount = dr("BillAmount")
                    bill.BillDate = dr("BillDate")
                    bill.BillDescription = dr("BillDescription")
                    bills.Add(bill)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return bills
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function
End Class
