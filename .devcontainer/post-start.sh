#!/bin/bash

echo "post-start start" >> ~/status

# this runs in background each time the container starts

# update rustup
rustup self update
rustup update

echo "post-start complete" >> ~/status
