# EZCRYPT


## C# Cross Platform Console Application 



## Supports Windows [x86, amd64], Linux [amd64, arm64], MacOS [amd64, arm64]



## Easily Encrypt / Decrypt Files in Windows / Linux / MacOS Terminal

- The way the program works is it takes a mode argument, a source file and a destination file, and then a user supplied Password and Salt to derive a key, and encrypt said source file to the destination file.

- The program makes an SHA512 hash of the user supplied Password and Salt values >> then converts the password hash bytes to a hex string but leaves the salt in a byte array format >> then using the converted password hex string coupled with the salt hash byte array uses the Rfc2898DeriveBytes.Pbkdf2 method to create an Encryption Key in a byte array of 32 bytes

- Due to Special Functions active in Linux / MacOS Terminals, Special Key Characters should not be used in Password or Salt. Special Key Characters work fine in Windows for encryption and decryption, but if used in Windows you will not be able to decrypt the file in Linux / MacOS.

- DeCryption happens in reverse using a correct Password and Salt.

- Very Secure Method if I do say so myself.

### Usage:

- MacOS / Linux - make executable first
```sh
chmod +x ./ezcrypt
```


- Running without arguments - ./ezcrypt [MacOS / Linux] | ezcrypt [Windows] - gives the following usage help
```sh
Encrypt or Decrypt a file.


ezcrypt [-e] [-d] source destination [password] [salt]


-e             Sets the mode to ENCRYPT.
-d             Sets the mode to DECRYPT.
source         Specifies the input file path.
destination    Specifies the destination file path.
password       Specifies the Encryption Password.
salt           Specifies the Encryption Salt.

Examples:

Password & Salt:  Use Numbers and Letters ONLY for Platform Cross Compatibility

encrypt:       ezcrypt -e inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn
decrypt:       ezcrypt -d inputFilePath outputFilePath Trs89Ely3Ui9031 89073ey38Y6uwq90bn
```


### Special Note:

```sh
Because of the way Terminal works in Linux/MacOS,
certain special characters cause issues in Password & Salt.

In Windows, you can use special characters
in any location in Password & Salt with No Issues.

For True Cross Platform Compatibility,
Do Not Use Special Characters in Password & Salt.

If only using EZCrypt in Windows,
with no plan of decrypting the file in Linux / MacOS,
use special characters within Password & Salt
```


### Usage Examples:

#### Encrypt File:

```sh
MacOS:
./ezcrypt -e Path/to/input/file Path/to/output/file password salt

Linux:
./ezcrypt -e Path/to/input/file Path/to/output/file password salt

Windows:
ezcrypt -e Path:\To\Input\File Path:\To\Output\File password salt
```

#### Decrypt file:

```sh
MacOS:
./ezcrypt -d Path/to/input/file Path/to/output/file password salt

Linux:
./ezcrypt -d Path/to/input/file Path/to/output/file password salt

Windows:
ezcrypt -d Path:\To\Input\File Path:\To\Output\File password salt
```


## Build it Yourself on All Platforms:

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

dotnet publish --self-contained -r osx-arm64 -c release -o release/v3.0/MacOS/arm64 -p:PublishSingleFile=true
dotnet publish --self-contained -r osx-amd64 -c release -o release/v3.0/MacOS/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-amd64 -c release -o release/v3.0/Windows/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-x86 -c release -o release/v3.0/Windows/i386 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-amd64 -c release -o release/v3.0/Linux/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-arm64 -c release -o release/v3.0/Linux/arm64 -p:PublishSingleFile=true
```
