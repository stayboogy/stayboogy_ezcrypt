// Copyright (c) 2024, stayboogy@github.com, stayboogy@mail.com, M.C.
//
// original source for this work comes from here: 
// https://github.com/JohnRush/File-Encryption-Tutorial/tree/master/source/Tutorial03
//
// this software is open source
// this source code is provided free of charge
// this source code is made available for the learners out there
//
//

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
				Console.WriteLine("EZcrypt by stayboogy@github.com");
				Console.WriteLine("");
				Console.ForegroundColor = ConsoleColor.Green;
				
				Console.WriteLine("Encrypt or Decrypt a file in Windows/Linux/MacOS Terminals");
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
				Console.WriteLine("Password & Salt:  Numbers and Letters Only Allowed");
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
				Console.WriteLine("\n! source file does not exist !");
				Console.WriteLine("\n! confirm your entry !");
				Console.WriteLine("\n! use quotations for paths with spaces !");
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
			
			// this constructs a way of verifying Password and Salt don't use symbols or special characters
			bool pvalid;
			bool svalid;
			if (Regex.IsMatch(args[3], @"^[\p{L}\p{N}]+$"))
			{
				pvalid = true;
			}
			else
			{
				pvalid = false;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n! use only Letters and Numbers for Password !");
				Console.WriteLine("");
				Console.ResetColor();
			}
			if (Regex.IsMatch(args[4], @"^[\p{L}\p{N}]+$"))
			{
				svalid = true;
			}
			else
			{
				svalid = false;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n! use only Letters and Numbers for Salt !");
				Console.WriteLine("");
				Console.ResetColor();
			}
			
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
			if ((mode == "-e" && pvalid == true && svalid == true))
			{
				// if encrypted output file already exists
				// return
				if (File.Exists(deFilename))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n! output file already exists !");
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
			else if ((mode == "-d" && pvalid == true && svalid == true))
			{
				// if decrypted output file already exists
				// return
				if (File.Exists(ddFilename))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n! output file already exists !");
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
							Console.WriteLine("\n! Password + Salt combination Incorrect !");
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
					Console.WriteLine("\n! input file not '.eze' encrypted file !");
					Console.WriteLine("");
					Console.ResetColor();
					return;
				}
			// if something other than -e and/or -d is used for args 1
			else
			{
				// let the user know to user either -e and/or -d only
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("! select the ecryption/decryption mode using -e or -d !");
				Console.ResetColor();
			}
		}
	}
}
}

//
//		
// unused code
// snippets kept for possible further use
// I like to remember useful methods
//					
//				
