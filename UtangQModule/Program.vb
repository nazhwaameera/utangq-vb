Imports System
Imports UtangQBO
Imports UtangQDAL

Module Program
    Sub Main(args As String())
        ' Display ASCII art
        DisplayAsciiArt()

        Dim optionSelected As Integer

        ' Show menu and handle user input
        While True
            ShowMenu()

            ' Read user input
            Integer.TryParse(Console.ReadLine(), optionSelected)

            ' Process the selected option
            Select Case optionSelected
                Case 1
                    UserProfile()
                Case 2
                    Transaction()
                Case 3
                    Report()
                Case Else
                    Console.WriteLine("Please input a valid option (1, 2, or 3).")
            End Select

            ' Exit loop if the user chose to exit
            If optionSelected = 3 Then
                Exit While
            End If
        End While

        Console.ReadLine() ' Keeps the console window open until a key is pressed
    End Sub

    Sub DisplayAsciiArt()
        ' ASCII art lines
        Dim lines As String() = {
            "            ██╗   ██╗████████╗ █████╗ ███╗   ██╗ ██████╗  ██████╗             ",
            "            ██║   ██║╚══██╔══╝██╔══██╗████╗  ██║██╔════╝ ██╔═══██╗            ",
            "█████╗█████╗██║   ██║   ██║   ███████║██╔██╗ ██║██║  ███╗██║   ██║█████╗█████╗",
            "╚════╝╚════╝██║   ██║   ██║   ██╔══██║██║╚██╗██║██║   ██║██║▄▄ ██║╚════╝╚════╝",
            "            ╚██████╔╝   ██║   ██║  ██║██║ ╚████║╚██████╔╝╚██████╔╝            ",
            "             ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚══▀▀═╝             "
        }

        ' Calculate the maximum width of the console window
        Dim maxWidth As Integer = Console.WindowWidth

        ' Calculate the length of the longest line in the ASCII art
        Dim maxLength As Integer = lines.Max(Function(line) line.Length)

        ' Calculate the number of spaces to add before each line to center it
        Dim spaces As Integer = (maxWidth - maxLength) \ 2

        ' Display each line of the ASCII art with the appropriate number of spaces added
        For Each line As String In lines
            Console.WriteLine($"{New String(" "c, spaces)}{line}")
        Next
    End Sub

    Private Sub ShowMenu()
        Console.WriteLine()
        Console.WriteLine("Please select an option:")
        Console.WriteLine("1. User Profile")
        Console.WriteLine("2. Transaction")
        Console.WriteLine("3. Report")
        Console.Write("Option: ")
    End Sub

    Private Sub ShowUserProfileMenu()
        Dim optionSelected As Integer

        Console.WriteLine()
        Console.WriteLine("Please select an option:")
        Console.WriteLine("1. User List")
        Console.WriteLine("2. Bills")
        Console.WriteLine("3. Wallet")
        Console.Write("Option: ")

        ' Read user input
        Integer.TryParse(Console.ReadLine(), optionSelected)

        ' Process the selected option
        Select Case optionSelected
            Case 1
                UserList()
            Case 2
                Bills()
            Case 3
                Wallet()
            Case Else
                Console.WriteLine("Please input a valid option (1, 2, or 3).")
        End Select

    End Sub



    Sub UserProfile()
        Dim userDAL As New UtangQDAL.UserDAL

        Console.WriteLine()
        Console.WriteLine("Welcome to User Profile")
        ShowUserProfileMenu()

    End Sub

    Private Sub UserList()
        Dim userDAL As New UtangQDAL.UserDAL

        Console.WriteLine("Listing all users:")
        Console.WriteLine()
        Console.WriteLine("User List")
        Console.WriteLine(New String("="c, 110)) ' Header separator

        Dim allUsers As List(Of User) = userDAL.GetAllUser()
        If allUsers IsNot Nothing Then
            Console.WriteLine($"| {"UserID",-10} | {"Username",-20} | {"Email",-30} | {"Full Name",-20} | {"Phone Number",-15} |")
            Console.WriteLine(New String("=", 110)) ' Table header separator

            For Each user As User In allUsers
                Console.WriteLine($"| {user.UserID,-10} | {user.Username,-20} | {user.UserEmail,-30} | {user.UserFullName,-20} | {user.UserPhoneNumber,-15} |")
            Next
        Else
            Console.WriteLine("No users found.")
        End If
    End Sub
    Private Sub Bills()
        Dim userDAL As New UtangQDAL.UserDAL
        Console.WriteLine("Please input your user ID: ")
        Dim userID As Integer
        Integer.TryParse(Console.ReadLine(), userID)

        Dim user = UserDAL.GetUserById(userID)

        If user IsNot Nothing Then
            Console.WriteLine($"User Profile: UserID: {user.UserID}, Username: {user.Username}, Email: {user.UserEmail}")
        Else
            Console.WriteLine("User not found.")
        End If
    End Sub

    Private Sub Wallet()
        Dim userDAL As New UtangQDAL.UserDAL
        Console.WriteLine("Please input your user ID: ")
        Dim userID As Integer
        Integer.TryParse(Console.ReadLine(), userID)

        Dim user = userDAL.GetUserById(userID)

        If user IsNot Nothing Then
            Console.WriteLine($"User Profile: UserID: {user.UserID}, Username: {user.Username}, Email: {user.UserEmail}")
        Else
            Console.WriteLine("User not found.")
        End If
    End Sub

    Sub Transaction()
        Console.WriteLine()
        Console.WriteLine("You choose Transaction")
        Console.ReadLine()
    End Sub

    Sub Report()
        Console.WriteLine()
        Console.WriteLine("You choose Report")
        Console.ReadLine()
    End Sub
End Module
