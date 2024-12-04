#!/bin/bash

FILE=allprojects

while read LINE; 
    do bash ./clean.sh "$(pwd)/../../04-Source/$LINE" || {
        error
        exit 1
    } 
done < $FILE
