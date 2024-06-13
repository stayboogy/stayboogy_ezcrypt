dotnet publish --self-contained -r osx-arm64 -c release -o release/v2.3/MacOS/arm64 -p:PublishSingleFile=true
dotnet publish --self-contained -r osx-amd64 -c release -o release/v2.3/MacOS/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-amd64 -c release -o release/v2.3/Windows/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-x86 -c release -o release/v2.3/Windows/i386 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-amd64 -c release -o release/v2.3/Linux/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-arm64 -c release -o release/v2.3/Linux/arm64 -p:PublishSingleFile=true
./clean.sh
