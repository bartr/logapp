FROM rust:latest AS build

WORKDIR /usr/src

# Download the target for static linking.
RUN rustup target add x86_64-unknown-linux-musl

RUN USER=root cargo new rustlog

WORKDIR /usr/src/rustlog

COPY Cargo.toml ./
COPY src ./src

RUN cargo build --release

# Copy the source and build the application.
RUN cargo install --target x86_64-unknown-linux-musl --path .

# Copy the statically-linked binary into a scratch container.
FROM scratch

COPY --from=build /usr/local/cargo/bin/rustlog .
USER 10000
ENTRYPOINT ["./rustlog"]
