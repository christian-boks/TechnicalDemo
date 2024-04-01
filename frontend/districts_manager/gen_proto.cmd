@echo off
;set path=%path%;c:\temp\grpc\

c:\temp\grpc\protoc.exe backend_api.proto --proto_path=c:\temp\grpc\include --proto_path=C:\Users\Christian\code\TechnicalDemo\backend\BackendApi\Resources\proto --js_out=import_style=commonjs,binary:./src/app/generated/grpc --grpc-web_out=import_style=typescript,mode=grpcweb:./src/app/generated/grpc