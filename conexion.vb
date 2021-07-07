Imports System.Threading
Imports System.Configuration
Imports SR = System.Reflection
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Security.Principal

Namespace ROP_Informe
    Public Class conexion
        Private Const LOGON32_PROVIDER_DEFAULT As Integer = 0
        Private Const LOGON32_LOGON_INTERACTIVE As Integer = 2
        Private Const LOGON32_LOGON_NETWORK As Integer = 3
        Private Const LOGON32_LOGON_BATCH As Integer = 4
        Private Const LOGON32_LOGON_SERVICE As Integer = 5
        Private Const LOGON32_LOGON_UNLOCK As Integer = 7
        Private Const LOGON32_LOGON_NETWORK_CLEARTEXT As Integer = 8
        Private Const LOGON32_LOGON_NEW_CREDENTIALS As Integer = 9

        Private Shared ImpersonationContext As WindowsImpersonationContext

        Declare Function LogonUserA Lib "advapi32.dll" (ByVal lpszUsername As String, ByVal lpszDomain As String, ByVal lpszPassword As String, ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, ByRef phToken As IntPtr) As Integer

        Declare Auto Function DuplicateToken Lib "advapi32.dll" (ByVal ExistingTokenHandle As IntPtr, ByVal ImpersonationLevel As Integer, ByRef DuplicateTokenHandle As IntPtr) As Integer
        Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
        Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long


        Public Shared Function ImpersonateValidUser(ByVal strUserName As String,
            ByVal strDomain As String, ByVal strPassword As String) As Boolean
            Dim token As IntPtr = IntPtr.Zero
            Dim tokenDuplicate As IntPtr = IntPtr.Zero
            Dim tempWindowsIdentity As WindowsIdentity

            ImpersonateValidUser = False

            If RevertToSelf() <> 0 Then
                If LogonUserA(strUserName, strDomain,
                strPassword,
                LOGON32_LOGON_INTERACTIVE,
                LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                    If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                        tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                        ImpersonationContext = tempWindowsIdentity.Impersonate()

                        If Not (ImpersonationContext Is Nothing) Then
                            ImpersonateValidUser = True
                        End If
                    End If
                End If
            End If

            If Not tokenDuplicate.Equals(IntPtr.Zero) Then
                CloseHandle(tokenDuplicate)
            End If

            If Not token.Equals(IntPtr.Zero) Then
                CloseHandle(token)
            End If

        End Function

        Public Shared Sub UndoImpersonation()
            ImpersonationContext.Undo()
        End Sub
    End Class
End Namespace