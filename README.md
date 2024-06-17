# EZCRYPT


- C# Cross Platform Console Application 

  
  - Windows [x86, amd64]
  
  - Linux [amd64, arm64]
  
  - MacOS [amd64, arm64]

  - SEE HERE FOR LATEST BINARIES FOR YOUR PLATFORM:  https://github.com/stayboogy/stayboogy_ezcrypt/releases/tag/v4.0

- Base Code Comes From Here: https://github.com/JohnRush/File-Encryption-Tutorial/tree/master/source/Tutorial03
  
## Usage Demo:
![App Screenshot](https://github.com/stayboogy/stayboogy_ezcrypt/blob/b3f04ea523573140b7e340bfc4d3e9a4548ad02f/ezcrypt/media/one.gif)


## Encryption Demo:
![App Screenshot](https://github.com/stayboogy/stayboogy_ezcrypt/blob/573053d054d4ff8ec82e4d5889af80d173c2874f/ezcrypt/media/two.gif)


## Decryption Demo:
![App Screenshot](https://github.com/stayboogy/stayboogy_ezcrypt/blob/66bfd8e32baa17c247668f597fb645cb6d6dcd1b/ezcrypt/media/three.gif)


## Video Demonstration:  https://rumble.com/v51asel-ezcrypt-v3.2-easily-encrypt-decrypt-any-size-any-type-of-file-windows-linux.html


# Easily Encrypt / Decrypt Files in Terminal
 
- Windows / Linux / MacOS - Cross Platform Operations - OS Cross Compatible Encryption & Decryption

- The way the program works is it takes a mode argument, a source file and a destination file, and then a user supplied Password and Salt to derive a key, and encrypt said source file to the destination file.

- The program makes an SHA512 hash of the user supplied Password and Salt values >> then converts the password hash bytes to a hex string but leaves the salt in a byte array format >> then using the converted password hex string coupled with the salt hash byte array uses the Rfc2898DeriveBytes.Pbkdf2 method to create an Encryption Key in a byte array of 32 bytes

- Use only Letters and Numbers for Password and Salt.  Only Letters and Numbers are allowed for Password and Salt.  Should you use symbols or special characters in Password or Salt, unless in very rare circumstances in Linux and MacOS, you will get a warning to only use Letters and Numbers.

- Files Encrypted with EZcrypt will have ".eze" extension added to supplied output file name

- DeCryption happens in reverse using a correct Password and Salt.

- Files Decrypted with EZcrypt will have ".ezd" extension added to supplied output file name


- Very Secure Method if I do say so myself.
  

## Usage:

- MacOS / Linux - make executable first
```sh
chmod +x ./ezcrypt
```


- Running without arguments - ./ezcrypt [MacOS / Linux] | ezcrypt [Windows] - gives the following usage help
```sh
EZcrypt by stayboogy@github.com

Encrypt or Decrypt a file in Windows/Linux/MacOS Terminals


ezcrypt [-e] [-d] source destination [password] [salt]


-e             Sets the mode to ENCRYPT.
-d             Sets the mode to DECRYPT.
source         Specifies the input file path.
destination    Specifies the destination file path.
password       Specifies the Encryption Password.
salt           Specifies the Encryption Salt.

Examples:

Password & Salt:  Numbers and Letters Only Allowed

encrypt:       ezcrypt -e inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn

decrypt:       ezcrypt -d inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn

.eze:          encryped files will have '.eze' extension added to them

.ezd:          decrypted files will have '.ezd' extension added to them
```


## Special Note:

```sh
Letters and Numbers ONLY Allowed for Password and Salt
```


## Usage Examples:

### Encrypt File:

```sh
MacOS:
./ezcrypt -e Path/to/input/file Path/to/output/file password salt

Linux:
./ezcrypt -e Path/to/input/file Path/to/output/file password salt

Windows:
ezcrypt -e Path:\To\Input\File Path:\To\Output\File password salt



encrypted file will have ".eze" extension
```

### Decrypt file:

```sh
MacOS:
./ezcrypt -d Path/to/input/file Path/to/output/file password salt

Linux:
./ezcrypt -d Path/to/input/file Path/to/output/file password salt

Windows:
ezcrypt -d Path:\To\Input\File Path:\To\Output\File password salt



decrypted file will have ".ezd" extension
```


# Build it Yourself on All Platforms:

MacOS:

```sh
brew install dotnet
cd ezcrypt
./build.sh
```

Linux:

install the dotnet sdk and make sure it is in your $PATH
```sh
cd ezcrypt
./build.sh
```

Windows:

make sure the dot net sdk is in your path
```sh
open ezcrypt dir in cmd prompt

dotnet publish --self-contained -r osx-arm64 -c release -o release/v4.0/MacOS/arm64 -p:PublishSingleFile=true
dotnet publish --self-contained -r osx-amd64 -c release -o release/v4.0/MacOS/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-amd64 -c release -o release/v4.0/Windows/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-x86 -c release -o release/v4.0/Windows/i386 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-amd64 -c release -o release/v4.0/Linux/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-arm64 -c release -o release/v4.0/Linux/arm64 -p:PublishSingleFile=true
```



# LICENSE

Copyright (c) 2024, stayboogy@github.com, stayboogy@mail.com, M.C.

All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

* Neither the name of "licensecc" nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
