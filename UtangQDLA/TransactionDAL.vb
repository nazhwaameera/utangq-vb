Imports System.Data
Imports Microsoft.Data.SqlClient
Imports UtangQBO
Imports UtangQInterface
Public Class TransactionDAL
    Implements IBillRecipient, IPaymentReceipt, IWalletTransaction

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        strConn = "Server=.\BSISqlExpress;Database=UtangQ;Trusted_Connection=True;TrustServerCertificate=True;"
        conn = New SqlConnection(strConn)
    End Sub

    Public Function CreateBillRecipient(obj As BillRecipient) As Integer Implements ICrud(Of BillRecipient).Create
        Try
            Dim strSP = "Transactions.CreateBillRecipient"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@BillID", obj.BillID)
            cmd.Parameters.AddWithValue("@RecipientUserID", obj.RecipientUserID)
            cmd.Parameters.AddWithValue("@SentDate", obj.SentDate)
            cmd.Parameters.AddWithValue("@BillRecipientStatusID", obj.BillRecipientStatusID)
            cmd.Parameters.AddWithValue("@BillRecipientTaxStatusID", obj.BillRecipientTaxStatusID)
            cmd.Parameters.AddWithValue("@BillRecipientAmount", obj.BillRecipientAmount)

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

    Public Function GetBillRecipientById(id As Integer) As BillRecipient Implements ICrud(Of BillRecipient).GetById
        Try
            Dim strSP = "Transactions.ReadBillRecipientById"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@RecipientUserID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                Dim billRecipient As New BillRecipient
                billRecipient.BillRecipientID = dr("BillRecipientID")
                billRecipient.BillID = dr("BillID")
                billRecipient.RecipientUserID = dr("RecipientUserID")
                billRecipient.SentDate = dr("SentDate")
                billRecipient.BillRecipientStatusID = dr("BillRecipientStatusID")
                billRecipient.BillRecipientTaxStatusID = dr("BillRecipientTaxStatusID")
                billRecipient.BillRecipientAmount = dr("BillRecipientAmount")
                Return billRecipient
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

    Public Function GetAllBillRecipient() As List(Of BillRecipient) Implements ICrud(Of BillRecipient).GetAll
        Dim billRecipients As New List(Of BillRecipient)
        Try
            Dim strSP = "Transactions.ReadAllBillRecipients"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim recipient As New BillRecipient
                    recipient.BillRecipientID = dr("BillRecipientID")
                    recipient.BillID = dr("BillID")
                    recipient.RecipientUserID = dr("RecipientUserID")
                    recipient.SentDate = dr("SentDate")
                    recipient.BillRecipientStatusID = dr("BillRecipientStatusID")
                    recipient.BillRecipientTaxStatusID = dr("BillRecipientTaxStatusID")
                    recipient.BillRecipientAmount = dr("BillRecipientAmount")
                    billRecipients.Add(recipient)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return billRecipients
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function UpdateBillRecipientPaymentStatus(BillRecipientID As Integer, NewStatus As Integer) As Integer Implements IBillRecipient.UpdateBillRecipientPaymentStatus
        Try
            Dim strSP = "Transactions.UpdateBillRecipientPaymentStatus"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@BillRecipientID", BillRecipientID)
            cmd.Parameters.AddWithValue("@NewStatus", NewStatus)
            cmd.CommandType = CommandType.StoredProcedure

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

    Public Function CreatePaymentReceipt(obj As PaymentReceipt) As Integer Implements ICrud(Of PaymentReceipt).Create
        Try
            Dim strSP = "Transactions.CreatePaymentReceiptAndUpdateStatus"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@BillRecipientID", obj.BillRecipientID)
            cmd.Parameters.AddWithValue("@SentDate", obj.SentDate)
            cmd.Parameters.AddWithValue("@ConfirmationDate", obj.ConfirmationDate)
            cmd.Parameters.AddWithValue("@PaymentReceiptURL", obj.PaymentReceiptURL)

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

    Private Function GetPaymentReceiptById(id As Integer) As PaymentReceipt Implements ICrud(Of PaymentReceipt).GetById
        Try
            Dim strSP = "Transactions.ReadPaymentReceiptsByBillCreator"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@CreatorUserID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                Dim paymentReceipt As New PaymentReceipt
                paymentReceipt.PaymentReceiptID = dr("PaymentReceiptID")
                paymentReceipt.BillRecipientID = dr("BillRecipientID")
                paymentReceipt.SentDate = dr("SentDate")
                paymentReceipt.ConfirmationDate = dr("ConfirmationDate")
                paymentReceipt.PaymentReceiptURL = dr("PaymentReceiptURL")
                Return paymentReceipt
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

    Private Function GetAllPaymentReceipt() As List(Of PaymentReceipt) Implements ICrud(Of PaymentReceipt).GetAll
        Dim paymentReceipts As New List(Of PaymentReceipt)
        Try
            Dim strSP = "Transactions.ReadAllPaymentReceipts"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim receipt As New PaymentReceipt
                    receipt.PaymentReceiptID = dr("PaymentReceiptID")
                    receipt.BillRecipientID = dr("BillRecipientID")
                    receipt.SentDate = dr("SentDate")
                    receipt.ConfirmationDate = dr("ConfirmationDate")
                    receipt.PaymentReceiptURL = dr("PaymentReceiptURL")
                    paymentReceipts.Add(receipt)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return paymentReceipts
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Private Function CreateWalletTransaction(obj As WalletTransacations) As Integer Implements IWalletTransaction.CreateWalletTransaction
        Try
            Dim strSP = "Transactions.CreateWalletTransaction"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@WalletID", obj.WalletID)
            cmd.Parameters.AddWithValue("@Amount", obj.WalletTransactionAmount)

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

    Private Function ReadWalletTransactions(id As Integer) As List(Of WalletTransacations) Implements IWalletTransaction.ReadWalletTransactions
        Dim walletTransactions As New List(Of WalletTransacations)
        Try
            Dim strSP = "Transactions.ReadWalletTransaction"
            cmd = New SqlCommand(strSP, conn)
            cmd.Parameters.AddWithValue("@UserID", id)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim transaction As New WalletTransacations
                    transaction.WalletTransactionID = dr("WalletTransactionID")
                    transaction.WalletID = dr("WalletID")
                    transaction.WalletTransactionAmount = dr("WalletTransactionAmount")
                    transaction.TransactionDate = dr("TransactionDate")
                    transaction.TransactionDescription = dr("TransactionDescription")
                    walletTransactions.Add(transaction)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return walletTransactions
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
