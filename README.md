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
> Press F5 once Codespaces is initialized

### Clone this repo

```bash

git clone https://github.com/bartr/logapp
cd logapp

```

### Build and run locally

```bash

# build the app
cargo build

# run 5 iterations
cargo run -- --iterations 5

# display help
cargo run -- --help

```

### Build the Docker conatainer

```bash

docker build . -t logapp

```

### Run the Docker container

```bash

# 5 iterations
docker run -it --rm logapp --iterations 5

# display help
docker run -it --rm logapp --help

```

## Support

This project uses GitHub Issues to track bugs and feature requests. Please search the existing issues before filing new issues to avoid duplicates.  For new issues, file your bug or feature request as a new issue.

## Contributing

This project welcomes contributions and suggestions and has adopted the [Contributor Covenant Code of Conduct](https://www.contributor-covenant.org/version/2/1/code_of_conduct.html).

For more information see [Contributing.md](./.github/CONTRIBUTING.md)

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Any use of third-party trademarks or logos are subject to those third-party's policies.
