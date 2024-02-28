Imports UtangQBO

Public Interface IPaymentReceiptReport
    Function GetPaymentReceiptReportById(ByVal id As Integer) As List(Of PaymentReceiptReport)
End Interface
