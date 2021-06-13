# Log App

> Simple dotnet core app that writes json logs to stdout and stderr
>
> Used by [Fluent Bit - Log Analytics](https://bartr.co/fbla)

The sample application generates JSON logs. Normal logs are written to stdout. Error logs are written to stderr.

```json

{"date":"2020-12-28T21:19:06.1347849Z","statusCode":200,"path":"/log/app","duration":78,"value":"HWIkixicjA"}
{"date":"2020-12-28T21:19:06.1444807Z","statusCode":500,"path":"/log/app","duration":266,"message":"Server error 9750"}
{"date":"2020-12-28T21:19:06.1613873Z","statusCode":200,"path":"/log/app","duration":34,"value":"olJDPKglhr"}
{"date":"2020-12-28T21:19:06.1660308Z","statusCode":200,"path":"/log/app","duration":86,"value":"lHldzimJSW"}
{"date":"2020-12-28T21:19:06.1669528Z","statusCode":200,"path":"/log/app","duration":65,"value":"BkPCTxoWcp"}
{"date":"2020-12-28T21:19:06.1846021Z","statusCode":400,"path":"/log/app","duration":9,"message":"Invalid paramater: cMwyFA"}
{"date":"2020-12-28T21:19:06.1867848Z","statusCode":200,"path":"/log/app","duration":82,"value":"BAZeQzaLFc"}
{"date":"2020-12-28T21:19:06.1944765Z","statusCode":200,"path":"/log/app","duration":22,"value":"NuUnKjZoNq"}
{"date":"2020-12-28T21:19:06.2080865Z","statusCode":200,"path":"/log/app","duration":74,"value":"wKOBoeYgBc"}
{"date":"2020-12-28T21:19:06.2116748Z","statusCode":200,"path":"/log/app","duration":79,"value":"UQWDWTPbHr"}

```

## Prerequisites

- Bash shell (tested on GitHub Codespaces, Mac, Ubuntu, WSL2)
- Docker CLI ([download](https://docs.docker.com/install/))
- Visual Studio Code (optional) ([download](https://code.visualstudio.com/download))

> The easiest way to experiment is to click `Open with CodeSpaces` from the `Code` button dropdown

## Clone this repo

```bash

git clone https://github.com/bartr/logapp
cd logapp

```

### Run the sample app

```bash

### TODO - change to makefile

# requires Docker CLI
docker run -it --rm retaildevcrew/logapp:latest --iterations 10

# display help
docker run -it --rm retaildevcrew/logapp:latest --help

# requires dotnet sdk
cd src
dotnet run -- --iterations 10

# display help
dotnet run -- --help

cd ..

```

## Contributing

This project welcomes contributions and suggestions. Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit [Microsoft Contributor License Agreement](https://cla.opensource.microsoft.com).

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).

For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments

## Trademarks

This project may contain trademarks or logos for projects, products, or services.

Authorized use of Microsoft trademarks or logos is subject to and must follow [Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).

Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.

Any use of third-party trademarks or logos are subject to those third-party's policies.
