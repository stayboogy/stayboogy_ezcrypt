# EZCRYPT


## C# Cross Platform Console Application 


## Easily Encrypt/Decrypt Files in Windows / Linux / MacOS Terminal

### Usage:

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

### Note:

```sh
Because of the way Terminal works in Linux/MacOS, certain special characters cause issues in Password & Salt.

In Windows, you can use special characters in any location in Password & Salt with No Issues.

For True Cross Platform Compatibility Do Not Use Special Characters in Password & Salt.

If only using in Windows, please use special characters for Password & Salt
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


### Build it Yourself in MacOS for All Platforms:

```sh
brew install dotnet
cd ezcrypt
./build.sh
```
