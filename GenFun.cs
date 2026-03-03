
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace ReactWebApplication
{

    public static class GenFun
    {
        public static string text2 { get; set; }
        public static decimal DecVal(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value.ToString() == string.Empty)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(value);
            }
        }
        public static int IntVal(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value.ToString() == string.Empty)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }
        public static string IsNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToString().Trim();
            }
        }
        public static decimal ToDecimal(object value)
        {
            try
            {
                if (value == null)
                {
                    return 0;
                }
                else if (value.ToString() == string.Empty)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(value);
                }
            }
            catch
            {
                return 0;
            }
        }
        public static string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"\r\n", " ", RegexOptions.Singleline).TrimEnd();
        }
        public static string GetMotherBoardID()
        {
            String serial = "";
            try
            {
                //ManagementObjectSearcher  mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                // ManagementObjectCollection moc = mos.Get();

                // foreach (ManagementObject mo in moc)
                // {

                //     serial = mo["SerialNumber"].ToString();
                // }
                return serial;
            }
            catch (Exception)
            {
                return serial;
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string GetMacAddress()
        {
            string addr = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return "";
        }
   
        public static int ToInt(object value)
        {
            try
            {
                if (value == null)
                {
                    return 0;
                }
                else if (value.ToString() == string.Empty)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }
            catch
            {
                return 0;
            }
        }
        public static string ToString(object value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }
                else
                {
                    return value.ToString().Trim();
                }
            }
            catch
            {
                return "";
            }
        }
        internal static string Get_TxtFile(string FileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(assembly.GetName().Name + "." + FileName));
            return reader.ReadToEnd();
        }

        public static string Encrypt(string input)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(input);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    input = Convert.ToBase64String(ms.ToArray());
                }
            }
            return input;
        }
        public static string Decrypt(string input)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            input = input.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(input);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    input = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return input;
        }
        public static string Set_StrLength(string sender, int NoofDigit, string Prefix = "0", bool StrBefore = true)
        {
            string Rtn = "";
            if (sender.Trim().Length < NoofDigit)
            {
                string Cpy = "";
                for (int i = sender.Trim().Length; i <= NoofDigit - 1; i++)
                {
                    Cpy += Prefix;
                }
                if (StrBefore == true)
                {
                    Rtn = Cpy + sender.Trim();
                }
                else
                {
                    Rtn = sender.Trim() + Cpy;
                }
            }
            return Rtn;
        }
    }
}
