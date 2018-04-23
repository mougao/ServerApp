#!/bin/bash

out_dir="/Users/mini-jane/Documents/shoyoo/pao/pao_client/FireToMe/Classes/Network/proto"

for file in ./proto/*
do
    protoc -I="proto/" --cpp_out $out_dir $file
done
