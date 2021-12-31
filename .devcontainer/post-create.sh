#!/bin/bash

echo "post-create start" >> $HOME/status

# this runs in after on-create

# (optional) upgrade packages
#sudo apt update
#sudo apt upgrade -y
#sudo apt autoremove -y
#sudo apt clean -y

echo "post-create complete" >> $HOME/status
