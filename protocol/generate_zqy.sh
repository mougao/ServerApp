#!/bin/bash

out_dir="/Users/zhangqiyun/work/pao_client/FireToMe/Classes/Network/proto"

for file in ./proto/*
do
    protoc -I="proto/" --cpp_out $out_dir $file
done
