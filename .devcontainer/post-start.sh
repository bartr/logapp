#!/bin/bash

echo "post-start start" >> ~/status

# this runs in background each time the container starts

# update rustup
rustup self update
rustup update

# pull docker base images
docker pull rust:latest

echo "post-start complete" >> ~/status
