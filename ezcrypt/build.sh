dotnet publish --self-contained -r osx-arm64 -c release -o release/v4.1/MacOS/arm64 -p:PublishSingleFile=true
dotnet publish --self-contained -r osx-amd64 -c release -o release/v4.1/MacOS/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-amd64 -c release -o release/v4.1/Windows/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-x86 -c release -o release/v4.1/Windows/i386 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-amd64 -c release -o release/v4.1/Linux/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-arm64 -c release -o release/v4.1/Linux/arm64 -p:PublishSingleFile=true
md5sum release/v4.1/Linux/arm64/ezcrypt > release/v4.1/Linux/arm64/ezcrypt.md5.txt
md5sum release/v4.1/Linux/amd64/ezcrypt > release/v4.1/Linux/amd64/ezcrypt.md5.txt
md5sum release/v4.1/MacOS/arm64/ezcrypt > release/v4.1/MacOS/arm64/ezcrypt.md5.txt
md5sum release/v4.1/MacOS/amd64/ezcrypt > release/v4.1/MacOS/amd64/ezcrypt.md5.txt
md5sum release/v4.1/Windows/amd64/ezcrypt.exe > release/v4.1/Windows/amd64/ezcrypt.md5.txt
md5sum release/v4.1/Windows/i386/ezcrypt.exe > release/v4.1/Windows/i386/ezcrypt.md5.txt
./clean.sh
./test.sh
