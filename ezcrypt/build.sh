dotnet publish --self-contained -r osx-arm64 -c release -o release/v2.2/MacOS/arm64 -p:PublishSingleFile=true
dotnet publish --self-contained -r osx-amd64 -c release -o release/v2.2/MacOS/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-amd64 -c release -o release/v2.2/Windows/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r win-x86 -c release -o release/v2.2/Windows/x86 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-amd64 -c release -o release/v2.2/Linux/amd64 -p:PublishSingleFile=true
dotnet publish --self-contained -r linux-arm64 -c release -o release/v2.2/Linux/arm64 -p:PublishSingleFile=true
./clean.sh
