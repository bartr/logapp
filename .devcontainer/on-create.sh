#!/bin/bash

echo "on-create start" >> $HOME/status

# this runs when container is initially created

# pull docker base images
docker pull rust:latest

echo "on-create complete" >> $HOME/status
