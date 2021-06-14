# Log App

![License](https://img.shields.io/badge/license-MIT-green.svg)

> Simple app that prints json logs to stdout and stderr
>
> Used to test log processing (like Fluent Bit)

## Overview

The sample application generates JSON logs. Normal logs are written to stdout. Error logs are written to stderr.

```json

{"date":"2020-12-28T21:19:06Z","statusCode":200,"path":"/log/app","duration":78,"value":"HWIkixic"}
{"date":"2020-12-28T21:19:06Z","statusCode":400,"path":"/log/app","duration":9,"message":"Invalid Paramater"}
{"date":"2020-12-28T21:19:06Z","statusCode":500,"path":"/log/app","duration":266,"message":"Server Error"}

```

### Prerequisites

- Bash shell (tested on GitHub Codespaces, Mac, Ubuntu, WSL2)
- Docker CLI ([download](https://docs.docker.com/install/))
- Visual Studio Code (optional) ([download](https://code.visualstudio.com/download))

> The easiest way to experiment is to click `Open with CodeSpaces` from the `Code` button dropdown

### Clone this repo

```bash

git clone https://github.com/bartr/logapp
cd logapp

```

### Build the Docker conatainer

```bash

docker build . -t logapp

```

### Run the Docker container

```bash

# requires Docker CLI
docker run -it --rm logapp --iterations 10

# display help
docker run -it --rm logapp --help

```

## How to file issues and get help  

This project uses GitHub Issues to track bugs and feature requests. Please search the existing issues before filing new issues to avoid duplicates. For new issues, file your bug or feature request as a new issue.

For help and questions about using this project, please open a GitHub issue.

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit <https://cla.opensource.microsoft.com>

When you submit a pull request, a CLA bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services.

Authorized use of Microsoft trademarks or logos is subject to and must follow [Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).

Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.

Any use of third-party trademarks or logos are subject to those third-party's policies.
