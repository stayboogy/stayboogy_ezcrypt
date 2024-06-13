// original source for this work comes from here: 
// https://github.com/JohnRush/File-Encryption-Tutorial/tree/master/source/Tutorial03
//
//
// I have extensively modified it to make it more secure
//
// I have made it Cross Platform Publishable as well - Windows, Linux, MacOS Compatible
//
// I have also added error handling to squash unhandled exceptions
//
// I have also added lots of clear notation for anyone who wants to learn from this code
//
//

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace EZCrypt
{
	class cryptwork
	{
		static void Main(string[] args)
		{
			// determine OS Platform
			OperatingSystem os = Environment.OSVersion;
			
			// if running on Windows
			PlatformID pid = os.Platform;
			if (pid is PlatformID.Win32NT)
			{
				// we run this process with "high priority" by default
				// because this can be used for large files and I use it for large disk images
				// very first thing console app does
				System.Diagnostics.Process ezcrypt = System.Diagnostics.Process.GetCurrentProcess();
				ezcrypt.PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
			}
			
			// 1 arg for mode -encrypt or -decrypt
			// 2 arg for input file
			// 3 arg for output file
			// 4 arg for encryption password
			// 5 arg for encryption salt
			
			// if arguments don't equal 5
			if (args.Length != 5)
			{
				// we console print the usage help
				Console.WriteLine("");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("EZcrypt by stayboogy@github.com.");
				Console.WriteLine("");
				Console.ForegroundColor = ConsoleColor.Green;
				
				Console.WriteLine("Encrypt or Decrypt a file.");
				Console.WriteLine("");
				Console.WriteLine("\nezcrypt [-e] [-d] source destination [password] [salt]\n");
				Console.WriteLine("");
				Console.WriteLine("{0,-15}Sets the mode to ENCRYPT.", "-e");
				Console.WriteLine("{0,-15}Sets the mode to DECRYPT.", "-d");
				Console.WriteLine("{0,-15}Specifies the input file path.", "source");
				Console.WriteLine("{0,-15}Specifies the destination file path.", "destination");
				
				// original source had password optional - this is not advised for any encryption
				Console.WriteLine("{0,-15}Specifies the Encryption Password.", "password");
				
				// original source had a static defined salt - this is not advised for any encryption
				Console.WriteLine("{0,-15}Specifies the Encryption Salt.", "salt");
				Console.WriteLine("");
				Console.WriteLine("Examples:");
				Console.WriteLine("");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Password & Salt:  Use Numbers and Letters ONLY for Platform Cross Compatibility");
				Console.WriteLine("");
				Console.ForegroundColor = ConsoleColor.Green;
				// encrypt example
				Console.WriteLine("{0,-15}ezcrypt -e inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn", "encrypt:");
				Console.WriteLine("");
				
				// decrypt example
				Console.WriteLine("{0,-15}ezcrypt -d inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn", "decrypt:");
				Console.WriteLine("");
				
				Console.ForegroundColor = ConsoleColor.Blue;
				// alert user of our auto added file extensions
				Console.WriteLine("{0,-15}encryped files will have '.eze' extension added to them", ".eze:");
				Console.WriteLine("");
				Console.WriteLine("{0,-15}decrypted files will have '.ezd' extension added to them", ".ezd:");
				Console.WriteLine("");
				Console.ResetColor();
				return;
			}
			
			 // args 1 mode
			 var mode = args[0];
			 
			 // args 2 input file path
			 var sourceFilename = args[1];
			 // if the input file doesn't exist
			 if (!File.Exists(sourceFilename))
			 {	
			 	// let the user know something is wrong with their input file path
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n-- source file does not exist --");
				Console.WriteLine("\n-- confirm your entry --");
				Console.WriteLine("\n-- use quotations for paths with spaces --");
				Console.WriteLine("\n");
				Console.ResetColor();
				return;
			 }
			
			// args 3 output file path
			var destinationFilename = args[2];
			// args 3 output file encrypted files extension
			var deFilename = destinationFilename + ".eze";
			// args 3 output file decrypted files extension
			var ddFilename = destinationFilename + ".ezd";
			
			//
			//
			// args 3 encrypted file output file path + .ez-e extension
			//var deFilename = Path.ChangeExtension(args[2], Path.GetExtension(args[2]) + ".ez-e");
			// args 3 decrypted file output file path + .ez-d extension
			//var ddFilename = Path.ChangeExtension(args[2], Path.GetExtension(args[2]) + ".ez-d");
			//
			//
			
			// have to create sha512 instance to be able to use HashAlgorithm method
			HashAlgorithm sha = SHA512.Create();
			
			// args 4 user set password
			// password = sha512 hash of user input for args 4
			var password = sha.ComputeHash(Encoding.UTF8.GetBytes(args[3]));
			// sha512 hash is then conveted to hex string
			// Rfc2898DeriveBytes method below requires string for first argument
			var sauce = Convert.ToHexString(password);
			
			// args 5 user set salt
			// salt = sha512 hash of user input for args 5
			// no static salt here!
			// salt must be byte array for Rfc2898DeriveBytes method below second argument
			var salt = sha.ComputeHash(Encoding.UTF8.GetBytes(args[4]));
			
			byte[] key = null;
			
			// if arguments equal 5
			if (args.Length == 5)
			{
				using (var converter = new Rfc2898DeriveBytes(sauce, salt))
				{
					key = converter.GetBytes(32);
				}
			}
			
			// if we are encrypting a file
			// args 1
			if (mode == "-e")
			{
				// if encrypted output file already exists
				// return
				if (File.Exists(deFilename))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n-- output file already exists --");
					Console.WriteLine("");
					Console.ResetColor();
					return;
				}
				// encrypt the source file and write it to the destination file.
				using (var sourceStream = File.OpenRead(sourceFilename))
				using (var destinationStream = File.Create(deFilename))
				using (var provider = new AesCryptoServiceProvider())
				{
					provider.Key = key;
					using (var cryptoTransform = provider.CreateEncryptor())
					using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
					{
						destinationStream.Write(provider.IV, 0, provider.IV.Length);
						sourceStream.CopyTo(cryptoStream);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\nFile EnCryption Complete! \n");
						Console.WriteLine("");
						Console.ResetColor();
					}
				}
			}
			
			// if we are decrypting a file
			// args 1
			else if (mode == "-d")
			{
				// if decrypted output file already exists
				// return
				if (File.Exists(ddFilename))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n-- output file already exists --");
					Console.WriteLine("");
					Console.ResetColor();
					return;
				}
				
				if (Path.GetExtension(sourceFilename) == (".eze"))
				{
				// decrypt the source file and write it to the destination file.
				using (var sourceStream = File.OpenRead(sourceFilename))
				using (var destinationStream = File.Create(ddFilename))
				using (var provider = new AesCryptoServiceProvider())
				{
					var IV = new byte[provider.IV.Length];
					sourceStream.Read(IV, 0, IV.Length);
					using (var cryptoTransform = provider.CreateDecryptor(key, IV))
					using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
					{
						try
						{
							cryptoStream.CopyTo(destinationStream);
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("\nFile DeCryption Complete! \n");
							Console.WriteLine("");
							Console.ResetColor();
						}
						// if the Password and/or Salt is incorrect
						catch
						{
							// close the file copy stream so file can be deleted if created
							// this is mainly for Windows
							destinationStream.Close();
							Console.ForegroundColor = ConsoleColor.Red;
							// let the user know their Password and/or Salt is incorrect
							Console.WriteLine("\n-- Password + Salt combination incorrect --");
							Console.WriteLine("");
							Console.WriteLine("\n-- confirm your Password --");
							Console.WriteLine("\n-- confirm your Salt --");
							Console.WriteLine("");
							Console.ResetColor();
							// delete the (possibly) created file that is not decrypted due to incorrect password and/or salt
							File.Delete(ddFilename);
							return;
						}
					 }
				  }
				}
				else if (Path.GetExtension(sourceFilename) != (".eze"))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					// notify user inputfile needs to be an ".eze" encrypted file
					Console.WriteLine("\n-- input file not '.eze' encrypted file --");
					Console.WriteLine("");
					Console.ResetColor();
					return;
				}
			// if something other than -e and/or -d is used for args 1
			else
			{
				// let the user know to user either -e and/or -d only
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("select the ecryption/decryption mode using -e or -d.");
				Console.ResetColor();
			}
		}
	}
}
}
							
				
