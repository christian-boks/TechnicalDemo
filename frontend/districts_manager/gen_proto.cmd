@echo off
set path=%path%;c:\temp\grpc\

protoc.exe backend_api.proto --proto_path=c:\temp\grpc\include --proto_path=C:\Users\Christian\code\TechnicalDemo\backend\BackendApi\Resources\proto --js_out=import_style=commonjs,binary:./src/app/generated/grpc --grpc-web_out=import_style=typescript,mode=grpcweb:./src/app/generated/grpc

:: in C:\Users\Christian\code\TechnicalDemo\frontend\districts_manager\src\app\generated\grpc\backend_api_pb.js rename the following;
:: var jspb = require('google-protobuf'); 
:: to 
:: import * as jspb from 'google-protobuf'; 
::
:: And
::
:: var google_protobuf_empty_pb = require('google-protobuf/google/protobuf/empty_pb.js');
:: to
:: import * as google_protobuf_empty_pb from 'google-protobuf/google/protobuf/empty_pb';

