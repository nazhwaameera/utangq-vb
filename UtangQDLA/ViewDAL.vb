Imports System.Data
Imports Microsoft.Data.SqlClient
Imports UtangQBO
Imports UtangQInterface
Public Class ViewDAL
    Implements IBillPaymentReport, IBillRecipientStatusReport, IPaymentReceiptReport, ITransactionHistoryReport, IWalletBalanceReport

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        strConn = "Server=.\BSISqlExpress;Database=UtangQ;Trusted_Connection=True;TrustServerCertificate=True;"
        conn = New SqlConnection(strConn)
    End Sub

    Public Function GetBillPaymentReportById(id As Integer) As List(Of BillPaymentReport) Implements IBillPaymentReport.GetBillPaymentReportById
        Dim billPaymentReports As New List(Of BillPaymentReport)
        Try
            Dim strSP = "Transactions.CreateBillPaymentReport"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim paymentReport As New BillPaymentReport
                    paymentReport.BillAmount = dr("BillAmount")
                    paymentReport.BillDate = dr("BillDate")
                    paymentReport.BillDescription = dr("BillDescription")
                    paymentReport.PaymentStatus = dr("PaymentStatus")
                    billPaymentReports.Add(paymentReport)
                End While
            Else
                Return billPaymentReports
            End If
            dr.Close()

            Return billPaymentReports
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetBillRecipientStatusReportById(id As Integer) As List(Of BillRecipientStatusReport) Implements IBillRecipientStatusReport.GetBillRecipientStatusReportById
        Dim billRecipientsReport As New List(Of BillRecipientStatusReport)
        Try
            Dim strSP = "Transactions.CreateBillRecipientStatusReport"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim billReports As New BillRecipientStatusReport
                    billReports.BillRecipientID = dr("BillRecipientID")
                    billReports.BillID = dr("BillID")
                    billReports.SentDate = dr("SentDate")
                    billReports.Status = dr("Status")
                    billRecipientsReport.Add(billReports)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return billRecipientsReport
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetPaymentReceiptReportById(id As Integer) As List(Of PaymentReceiptReport) Implements IPaymentReceiptReport.GetPaymentReceiptReportById
        Dim paymentReceiptReports As New List(Of PaymentReceiptReport)
        Try
            Dim strSP = "Transactions.CreatePaymentReceiptReport"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim paymentReport As New PaymentReceiptReport
                    paymentReport.PaymentReceiptID = dr("PaymentReceiptID")
                    paymentReport.BillRecipientID = dr("BillRecipientID")
                    paymentReport.ReceiptSentDate = dr("ReceiptSentDate")
                    paymentReport.ConfirmationDate = dr("ConfirmationDate")
                    paymentReport.PaymentReceiptURL = dr("PaymentReceiptURL")
                    paymentReceiptReports.Add(paymentReport)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return paymentReceiptReports
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetTransactionHistoryReportById(id As Integer) As List(Of TransactionsHistoryReport) Implements ITransactionHistoryReport.GetTransactionHistoryReportById
        Dim transactionHistoryReports As New List(Of TransactionsHistoryReport)
        Try
            Dim strSP = "Transactions.CreateTransactionHistoryReport"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim transactionReport As New TransactionsHistoryReport
                    transactionReport.TransactionDate = dr("TransactionDate")
                    transactionReport.TransactionAmount = dr("TransactionAmount")
                    transactionReport.TransactionDescription = dr("TransactionDescription")
                    transactionHistoryReports.Add(transactionReport)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return transactionHistoryReports
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)

        Catch ex As Exception
            Throw ex

        Finally
            cmd.Dispose()
            conn.Close()

        End Try
    End Function

    Public Function GetWalletBalanceReportById(id As Integer) As List(Of WalletBalanceReport) Implements IWalletBalanceReport.GetWalletBalanceReportById
        Dim walletBalanceReports As New List(Of WalletBalanceReport)
        Try
            Dim strSP = "Transactions.CreateWalletBalanceReport"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim walletReport As New WalletBalanceReport
                    walletReport.UserID = dr("UserID")
                    walletReport.WalletBalance = dr("WalletBalance")
                    walletReport.TransactionDate = dr("TransactionDate")
                    walletReport.WalletTransactionAmount = dr("WalletTransactionAmount")
                    walletReport.TransactionDescription = dr("TransactionDescription")
                    walletBalanceReports.Add(walletReport)
                End While
            Else
                Return Nothing
            End If
            dr.Close()

            Return walletBalanceReports
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
