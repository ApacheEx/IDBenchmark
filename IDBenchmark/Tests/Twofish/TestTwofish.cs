using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using ManyMonkeys.Cryptography;

namespace TwofishTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class TestTwofish
	{
		/// <summary>
		/// This encrypts our data using twofish and then converts to base64 and then reverses the process 
		/// </summary>
		/// <param name="Key">The Key to use for the encryption stage</param>
		/// <param name="plainText">The plain text to encrypt and encode and then to compare when it has been decoded and decrypted</param>
		public static void Cascade(ref byte[] Key, ref byte[] plainText)
		{
			Twofish fish = new Twofish();

			fish.Mode = CipherMode.ECB;

			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			// create an encoder
			ICryptoTransform encode = new ToBase64Transform();

			//create Twofish Encryptor from this instance
			ICryptoTransform encrypt = fish.CreateEncryptor(Key,plainText); // we use the plainText as the IV as in ECB mode the IV is not used

			// we have to work backwords defining the last link in the chain first
			CryptoStream cryptostreamEncode = new CryptoStream(ms,encode,CryptoStreamMode.Write);
			CryptoStream cryptostream = new CryptoStream(cryptostreamEncode,encrypt,CryptoStreamMode.Write);

			// or we could do this as we don't need to use cryptostreamEncode
			//CryptoStream cryptostream = new CryptoStream(new CryptoStream(ms,encode,CryptoStreamMode.Write), 
			//										encrypt,CryptoStreamMode.Write);


			cryptostream.Write(plainText,0,plainText.Length);


			cryptostream.Close();

			//long pos = ms.Position; // our stream is closed so we cannot find out what the size of the buffer is - daft
			byte[] bytOut = ms.ToArray();

			// and now we undo what we did

			// create a decoder
			ICryptoTransform decode = new FromBase64Transform();

			//create DES Decryptor from our des instance
			ICryptoTransform decrypt = fish.CreateDecryptor(Key,plainText);

			System.IO.MemoryStream msD = new System.IO.MemoryStream();

			//create crypto stream set to read and do a Twofish decryption transform on incoming bytes
			CryptoStream cryptostreamD = new CryptoStream(msD,decrypt,CryptoStreamMode.Write);
			CryptoStream cryptostreamDecode = new CryptoStream(cryptostreamD,decode,CryptoStreamMode.Write);

			// again we could do the following
			//CryptoStream cryptostreamDecode = new CryptoStream(new CryptoStream(msD,decrypt,CryptoStreamMode.Write),
			//											decode,CryptoStreamMode.Write);


			//write out the decrypted stream
			cryptostreamDecode.Write(bytOut,0,bytOut.Length); 

			cryptostreamDecode.Close();

			byte[] bytOutD = msD.ToArray(); // we should now have our plain text back			

			for (int i=0;i<plainText.Length;i++)
			{
				if (bytOutD[i] != plainText[i])
				{
					Trace.Write("Plaintext match failure");
					break;
				}
			}
		}

		/// <summary>
		/// Test the twofish cipher in ECB mode
		/// </summary>
		/// <param name="Key">The used to encrypt the data.</param>
		/// <param name="plainText">The plain text.</param>
		/// <param name="cryptText">The encrypted text to be used for comparison.</param>
		public static void TestTwofishECB(ref byte[] Key, ref byte[] plainText, ref byte[] cryptText)
		{
			Twofish fish = new Twofish();

			fish.Mode = CipherMode.ECB;

			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			//create Twofish Encryptor from this instance
			ICryptoTransform encrypt = fish.CreateEncryptor(Key,plainText); // we use the plainText as the IV as in ECB mode the IV is not used

			//Create Crypto Stream that transforms file stream using twofish encryption
			CryptoStream cryptostream = new CryptoStream(ms,encrypt,CryptoStreamMode.Write);

			//write out Twofish encrypted stream
			cryptostream.Write(plainText,0,plainText.Length);

			cryptostream.Close();

			byte[] bytOut = ms.ToArray();

			for (int i=0;i<cryptText.Length;i++)
			{
				if (bytOut[i] != cryptText[i])
				{
					Trace.Write("Cryptext match failure");
					break;
				}
			}

			//create Twofish Decryptor from our twofish instance
			ICryptoTransform decrypt = fish.CreateDecryptor(Key,plainText);

			System.IO.MemoryStream msD = new System.IO.MemoryStream();

			//create crypto stream set to read and do a Twofish decryption transform on incoming bytes
			CryptoStream cryptostreamDecr = new CryptoStream(msD ,decrypt,CryptoStreamMode.Write);

			//write out Twofish encrypted stream
			cryptostreamDecr.Write(bytOut,0,bytOut.Length);

			cryptostreamDecr.Close();

			byte[] bytOutD = msD.GetBuffer();

			for (int i=0;i<plainText.Length;i++)
			{
				if (bytOutD[i] != plainText[i])
				{
					Trace.Write("Plaintext match failure");
					break;
				}
			}
		}

		/// <summary>
		/// Test the twofish cipher in ECB mode
		/// </summary>
		/// <param name="Key">The used to encrypt the data.</param>
		/// <param name="plainText">The plain text.</param>
		/// <param name="cryptText">The encrypted text to be used for comparison.</param>
		public static void TestTwofishCBC(ref byte[] Key, ref byte[] plainText, ref byte[] iv, ref byte[] cryptText)
		{
			Twofish fish = new Twofish();

			fish.Mode = CipherMode.CBC;

			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			//create Twofish Encryptor from this instance
			ICryptoTransform encrypt = fish.CreateEncryptor(Key,iv); // we use the plainText as the IV as in ECB mode the IV is not used

			//Create Crypto Stream that transforms file stream using twofish encryption
			CryptoStream cryptostream = new CryptoStream(ms,encrypt,CryptoStreamMode.Write);

			//write out Twofish encrypted stream
			cryptostream.Write(plainText,0,plainText.Length);

			cryptostream.Close();

			byte[] bytOut = ms.ToArray();

/*
 			// check the first block only

			for (int i=0;i<cryptText.Length;i++)
			{
				if (bytOut[i] != cryptText[i])
				{
					Trace.Write("Cryptext match failure\n");
					break;
				}
			}
*/
			//create Twofish Decryptor from our twofish instance
			ICryptoTransform decrypt = fish.CreateDecryptor(Key,iv);

			System.IO.MemoryStream msD = new System.IO.MemoryStream();

			//create crypto stream set to read and do a Twofish decryption transform on incoming bytes
			CryptoStream cryptostreamDecr = new CryptoStream(msD ,decrypt,CryptoStreamMode.Write);

			//write out Twofish encrypted stream
			cryptostreamDecr.Write(bytOut,0,bytOut.Length);

			cryptostreamDecr.Close();

			byte[] bytOutD = msD.GetBuffer();

			// check
			for (int i=0;i<plainText.Length;i++)
			{
				if (bytOutD[i] != plainText[i])
				{
					Trace.Write("Plaintext match failure\n");
					break;
				}
			}
		}
	}
}
